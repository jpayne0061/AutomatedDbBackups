using AutomatedDbBackUp.Data;
using AutomatedDbBackUp.Models;
using AutomatedDbBackUp.Services;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace AutomatedDbBackUp
{
    class Program
    {
        static Settings _settings; 

        static void Main(string[] args)
        {
            try
            {
                StartUpService startupService = new StartUpService();
                _settings = startupService.GetSettings();


                ShellCommands shellCommands = new ShellCommands();
                Backup backup = new Backup(_settings);

                backup.CreateBackUp();

                shellCommands.ExecuteShellCommand("mv", "/var/opt/mssql/data/AppDb.bak /var/data");

                shellCommands.ExecuteShellCommand("chown", "jpayne0061 /var/data/AppDb.bak");

                BlobServiceClient blobServiceClient = new BlobServiceClient(_settings.BlobConnString);

                var blobClient = blobServiceClient.GetBlobContainerClient("six-week-sql-db-backups");

                FileStream uploadFileStream = File.OpenRead("/var/data/AppDb.bak");
                blobClient.UploadBlob("AppDb" + DateTime.UtcNow.ToFileTimeUtc().ToString() + ".bak", uploadFileStream);
            }
            catch (Exception ex)
            {
                File.AppendAllText("dbBackUpLog", ex.StackTrace);

                throw;
            }

        }

    }
}
