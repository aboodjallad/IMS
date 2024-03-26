using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Applecation;
using IMS.Database;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace IMS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            using (var context = new ApplicationDbContext(connectionString))
            {
                // Example usage of the DbContext
                // Ensure you handle exceptions and logging as needed
                Console.WriteLine("ApplicationDbContext initialized successfully.");
            }
        }
    }
}

