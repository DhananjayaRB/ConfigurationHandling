using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Config.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            // Adding Custom Config Json and overriding the existing values with Arguments
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var replacement = new Dictionary<string, string>
                    {
                        {"-v","Version" }
                    };

                    webBuilder.UseStartup<Startup>()
                    .ConfigureAppConfiguration(c =>
                    c.AddJsonFile("Config/CustomConfig.json")
                    .AddCommandLine(args,replacement));
                });
    }
}
