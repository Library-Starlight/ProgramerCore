using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Utility.Log.Serilog
{
    public class Logger
    {
        private static void WithAppConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            while (true)
            {
                logger.Information("Hello, world!Hello, world!Hello, world!Hello, world!Hello, world!Hello, world!");
            }
        }
    }
}
