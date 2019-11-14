using AutomatedDbBackUp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutomatedDbBackUp.Services
{
    public class StartUpService
    {
        public Settings GetSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return new Settings() {
                MasterConnString = configuration.GetConnectionString("master"),
                BlobConnString = configuration.GetConnectionString("blob")
            };
        }
    }
}
