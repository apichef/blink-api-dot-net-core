using System;
using System.Linq;
using System.Net.Http;
using Blink.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blink.IntegrationTests.Controllers
{
    public class BaseIntegrationTest
    {
        protected readonly HttpClient TestClient;
        protected readonly BlinkContext DbContext;

        protected BaseIntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(ConfigureWebHost);
            
            var scopeFactory = appFactory.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                DbContext = scope.ServiceProvider.GetService<BlinkContext>();
            }

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
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            });
        }
    }
}