using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IMS.Applecation;
using IMS.Services;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace IMS.Program
{
    class CLI
    {
        static void Main(string[] args)
        {
            string connectionString = "Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123";
            var authService = new AuthService(connectionString);
            var itemService = new ItemService(connectionString);
            var api = new Controller();
            HttpListener listener;
            int x = 9999;




            Console.WriteLine("Welcome to the Inventory Management System");
            Console.WriteLine("(1) API test\n(2) CLI");
            string command = Console.ReadLine();
            bool isParsed = int.TryParse(command, out int choice);
            if (choice == 1)
            {
                listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:5000/");
                listener.Start();
                Console.WriteLine("Listening...");

                while (true)
                {

                        HttpListenerContext context = listener.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;

                        string path = request.Url.AbsolutePath.ToString();
                        if (path.Contains("/items/") && request.HttpMethod == "PUT")
                        {
                            api.Update(context);
                        }
                        else if (path.Contains("/items/") && request.HttpMethod == "DELETE")
                        {
                            api.Delete(context);
                        }
                        else if (path.Contains("/item") && request.HttpMethod == "POST")
                        {
                            api.Add(context);
                        }
                        else if (path.Contains("/item") && request.HttpMethod == "GET")
                        {
                            api.Get(context);
                        }
                        else if (path.Contains("/item/") && request.HttpMethod == "GET")
                        {
                            api.Get(context);
                        }
                        else
                        {
                            response.StatusCode = 404;

                        }

                        response.OutputStream.Close();
                }
            }
            else
            {
                Console.WriteLine("\n(1) Login \n(2) Register");
                Console.Write("Enter your choice: ");
                command = Console.ReadLine();
                isParsed = int.TryParse(command, out choice);

                if (!isParsed)
                {
                    Console.WriteLine("Please enter a valid integer.");
                }
                string username = "";
                string password = "";
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter your username :");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter your password");
                        password = Console.ReadLine();
                        
                        x = authService.GetRole(username, password);
                        if(x == 0)
                        {
                            Console.WriteLine("Login failed. Try to login with valid username and password");
                            return;
                        }
                        else
                            Console.WriteLine("Login successful.");
                        break;

                    case 2:
                        Console.WriteLine("Enter your username :");
                        username = Console.ReadLine();
                        Console.WriteLine("Enter your password");
                        password = Console.ReadLine();
                        if (! authService.Register(username, password))
                        {
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Go back and log in");
                            return;
                        }

                    default:
                        Console.WriteLine("Unknown command. Type 'help' for available commands.");
                        return;
                }
                while (true)
                {

                    Console.WriteLine("\nWhat do u want to do :)  :");
                    Console.WriteLine("\n(1) Add an item \n(2) Delete an item \n(3) Update an item \n(4) Search \n(5) Logout/Exit\n");
                    Console.Write("Enter your choice: ");
                    command = Console.ReadLine();
                    isParsed = int.TryParse(command, out choice);

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
                            itemService.AddItem(name, newQuantity, newPrice, category);
                            break;

                        case 2:
                            Console.WriteLine("Enter item id");
                            var id = Console.ReadLine();
                            isParsed = int.TryParse(id, out int newId);
                            itemService.DeleteItem(newId, authService.Login(username, password));
                            break;

                        case 3:
                            Console.WriteLine("Enter item id");
                            id = Console.ReadLine();
                            isParsed = int.TryParse(id, out newId);
                            Console.WriteLine("Enter item name :");
                            name = Console.ReadLine();
                            Console.WriteLine("Enter item quantity");
                            quantity = Console.ReadLine();
                            isParsed = int.TryParse(quantity, out newQuantity);
                            Console.WriteLine("Enter item price");
                            price = Console.ReadLine();
                            isParsed = int.TryParse(price, out newPrice);
                            Console.WriteLine("Enter item category");
                            category = Console.ReadLine();
                            itemService.UpdateItem(newId, name, newQuantity, newPrice, authService.Login(username, password));
                            break;

                        case 4:
                            Console.WriteLine("Search by:");
                            Console.WriteLine("(1) Name\n(2) Category\n(3) Price in ACS\n(4) Price in DESC");
                            command = Console.ReadLine();
                            isParsed = int.TryParse(command, out choice);

                            switch (choice)
                            {
                                case 1:
                                    Console.WriteLine("Enter a name");
                                    name = Console.ReadLine();
                                    itemService.SearchByName(name);
                                    break;
                                case 2:
                                    Console.WriteLine("Enter a category");
                                    category = Console.ReadLine();
                                    itemService.SearchByCategory(category);
                                    break;
                                case 3:
                                    Console.WriteLine("Price in ACS order");
                                    itemService.SearchByPriceASC();
                                    break;
                                case 4:
                                    Console.WriteLine("Price in DESC order");
                                    itemService.SearchByPriceDESC();
                                    break;
                            }

                            break;
                        case 5:
                            authService.Logout();
                            return;

                        default:
                            Console.WriteLine("Unknown command. Type 'help' for available commands.");
                            break;
                    }

                }
            }

        }
    }
}

