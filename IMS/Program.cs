using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Applecation;
using IMS.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace IMS
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123";
            var authService = new AuthService(connectionString);
            var itemService = new ItemService(connectionString);


            // Example usage:
            // Register a new user
            if (authService.Register("Jallad1", "aboodjallad12345", 1))
            {
                Console.WriteLine("Registration successful.");
            }
            else
            {
                Console.WriteLine("Registration failed.");
            }

            // Login
            if (authService.Login("Jallad1", "aboodjallad12345"))
            {
                Console.WriteLine("Login successful.");
            }
            else
            {
                Console.WriteLine("Login failed.");
            }




            // Logout
            authService.Logout();
        }
    }
}

