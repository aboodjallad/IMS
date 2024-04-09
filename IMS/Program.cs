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
{///*
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123";
            var authService = new AuthService(connectionString);
            var itemService = new ItemService(connectionString);


            Console.WriteLine("Welcome to the Inventory Management System");
            Console.WriteLine("\n(1) Login \n (2) Register");
            Console.Write("Enter your choice: ");
            string command = Console.ReadLine();
            bool isParsed = int.TryParse(command, out int choice);

            if (!isParsed)
            {
                Console.WriteLine("Please enter a valid integer.");
            }
            string username="";
            string password="";
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter your username :");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password");
                    password = Console.ReadLine();
                    authService.Login(username, password);
                    break;
                case 2:
                    Console.WriteLine("Enter your username :");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter your password");
                    password = Console.ReadLine();
                    authService.Register(username, password);
                    break;

                default:
                    Console.WriteLine("Unknown command. Type 'help' for available commands.");
                    break;
            }
            while (true)
            {
                
                Console.WriteLine("\nWhat do u want to do :)  :");
                Console.WriteLine("\n(1) Add an item \n (2) Delete an item \n (3) Update an item \n(4) Search \n (5) Logout ");
                Console.Write("Enter your choice: ");
                command = Console.ReadLine();
                isParsed = int.TryParse(command, out  choice);

                if (!isParsed)
                {
                    Console.WriteLine("Please enter a valid integer.");
                }

                switch (choice)
                {
                    case 1:

                        Console.WriteLine("Enter item name :");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter item quantity");
                        var quantity = Console.ReadLine();
                        isParsed = int.TryParse(quantity, out int newQuantity);
                        Console.WriteLine("Enter item price");
                        var price = Console.ReadLine();
                        isParsed = int.TryParse(price, out int newPrice);
                        Console.WriteLine("Enter item category");
                        var category = Console.ReadLine();
                        itemService.AddItem(name, newQuantity,newPrice,category);
                        break;

                    case 2:
                        Console.WriteLine("Enter item id");
                        var id = Console.ReadLine();
                        isParsed = int.TryParse(id, out int newId);
                        itemService.DeleteItem(newId, authService.Login(username,password));
                        break;
                    
                    case 3:

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for available commands.");
                        break;
                }

            }
        }

        //authService.Register("Jallad123", "aboodjallad12345");


        //authService.Login("Jallad1", "aboodjallad12345");

        /*

                    if(itemService.AddItem("mooooooz", 122121212, 1, "gooooooooooooooooooooooooool"))
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
        */
        /*if (itemService.UpdateItem(500, "koko", 123,4, authService.Login("Jallad1", "aboodjallad12345")))
        {
            Console.WriteLine("Item updated successfuly");
        }
        else
        {
            Console.WriteLine("Update failed");
        }

        //itemService.GetItems();


        //itemService.SearchByPriceDESC();

        authService.Logout();
    }
}//*/
    }
}

