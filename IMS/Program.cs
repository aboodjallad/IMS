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
{/*
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123";
            var authService = new AuthService(connectionString);
            var itemService = new ItemService(connectionString);
            static int ExtractIdFromUrl(string url)
            {
                var segments = url.Split('/');
                if (segments.Length >= 3)
                {
                    var idSegment = segments[segments.Length - 1];
                    if (int.TryParse(idSegment, out int itemId))
                    {
                        return itemId;
                    }
                }
                throw new ArgumentException("Invalid URL format or missing item ID.");
            }

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


            if(itemService.AddItem("abood", 122121212, 15000000))
            {
                Console.WriteLine("Item added successfuly");
            }
            else
            {
                Console.WriteLine("Add failed"); 
            }

            if (itemService.DeleteItem(1))
            {
                Console.WriteLine("Item deleted successfuly");
            }
            else
            {
                Console.WriteLine("Delete failed");
            }

            if (itemService.UpdateItem(500, "koko", 123,4))
            {
                Console.WriteLine("Item updated successfuly");
            }
            else
            {
                Console.WriteLine("Update failed");
            }

            itemService.GetItems();

            Console.WriteLine(ExtractIdFromUrl("http://localhost:5000/items/1049"));
            



            // Logout
            authService.Logout();
        }
    }*/
}

