using System;
using System.Linq;
using System.Net.Http;
using Blink.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blink.AcceptanceTests.Controllers
{
    public class BaseIntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        protected readonly BlinkContext DbContext;

        protected BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(ConfigureWebHost);
            
            DbContext = appFactory.Services.CreateScope().ServiceProvider.GetService<BlinkContext>();
            TestClient = appFactory.CreateClient();
        }

        private void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<BlinkContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<BlinkContext>(options =>
                    options.UseInMemoryDatabase("blink-test-db"));
            });
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
        }
    }
}