using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFlower
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>().UseKestrel(o => o.Limits.MaxRequestBodySize = 157286400);

            });

        //.ConfigureWebHostDefaults(webBuilder =>
        //{
        //    webBuilder
        //    .ConfigureKestrel(serverOptions =>
        //    {
        //        serverOptions.Limits.MaxRequestBodySize = 157286400;
        //    })
        //    .UseStartup<Startup>();
        //});

    }
}
