using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventCatalogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Below code we are storing the host details into host variable
            var host = CreateHostBuilder(args).Build();
            //The below using keyword is used to dispose the object out of memory as soon as the scope is done helps with memory mgmt
            using  (var scope = host.Services.CreateScope())
            {
                //Below code gets all service provides from the startup 
                var serviceProviders = scope.ServiceProvider;
                //Below line is to get the DB service provider details from the startup
                var context = serviceProviders.GetRequiredService<EventContext>();
                //At this point I know the host machine is created
                EventSeed.Seed(context);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
