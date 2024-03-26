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
            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            RegisterUser(username, password);


            Console.WriteLine("Enter username:");
            var username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            var password = Console.ReadLine();

            bool isValidUser = ValidateLogin(username, password);

            if (isValidUser)
            {
                Console.WriteLine("Login successful.");
                // Proceed with authenticated user operations here...
            }
            else
            {
                Console.WriteLine("Login failed. Invalid username or password.");
            }
        }
    }
}

