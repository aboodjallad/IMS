using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using IMS.Applecation;
using System.Diagnostics;
using System.Xml.Linq;
namespace IMS.Services
{

    public class ItemService : iItem
    {
        private readonly string _connectionString;

        public ItemService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddItem(string name, int quantity, decimal price , string category)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "INSERT INTO goods (Name,Quantity,Price,Category) VALUES (@Name,  @Quantity,  @Price, @Category)";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Category", category);

                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Item have been added successfully");

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public List<Good> GetItems()
        {
            var items = new List<Good>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "SELECT id, Name, Quantity,Price FROM goods";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Good
                                {
                                    GoodId = Convert.ToInt32(reader["id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToInt32(reader["Price"]),
                                    category = reader["category"].ToString(),

                                };


                                items.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return items; 
        }

        public void SearchByName(string searchName)
        {
            var items = new List<Good>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM goods WHERE Name ILIKE @Name ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", $"%{searchName}%");
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Good
                                {
                                    GoodId = Convert.ToInt32(reader["id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToInt32(reader["Price"]),
                                    category = reader["category"].ToString(),

                                };
                                Console.WriteLine(item.ToString());
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

        }
        public void SearchByCategory(string category)
        {
            var items = new List<Good>();
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM goods WHERE Category ILIKE @Category ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Category", $"%{category}%");
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Good
                                {
                                    GoodId = Convert.ToInt32(reader["id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToInt32(reader["Price"]),
                                    category = reader["category"].ToString(),

                                };
                                Console.WriteLine(item.ToString());
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SearchByPriceASC()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM goods ORDER BY price ASC ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Good
                                {
                                    GoodId = Convert.ToInt32(reader["id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToInt32(reader["Price"]),
                                    category = reader["category"].ToString(),

                                };
                                Console.WriteLine(item.ToString());
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SearchByPriceDESC()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "SELECT * FROM goods ORDER BY price DESC ";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new Good
                                {
                                    GoodId = Convert.ToInt32(reader["id"]),
                                    Name = reader["Name"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    Price = Convert.ToInt32(reader["Price"]),
                                    category = reader["category"].ToString(),

                                };
                                Console.WriteLine(item.ToString());
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public string GetStatusBasedOnQuantity(int quantity)
        {
            if (quantity == 0)
            {
                return "Out of Stock";
            }
            else if (quantity > 0 && quantity <= 10) 
            {
                return "Low Stock";
            }
            else
            {
                return "In Stock";
            }
        }

        public bool UpdateItem(int id, string name, int quantity, decimal price, int role)
        {
            try
            {
                if (role == 1)
                {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        const string query = "UPDATE goods SET Name = @Name, Quantity = @Quantity, Price = @Price WHERE id = @id";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.Parameters.AddWithValue("@Price", price);

                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Item updated successfully");
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Access Denied Only Admins Can Update And Delete.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool UpdateItem(int id, string name, int quantity, decimal price)
        {
            try
            {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        const string query = "UPDATE goods SET Name = @Name, Quantity = @Quantity, Price = @Price WHERE id = @id";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", id);
                            command.Parameters.AddWithValue("@Name", name);
                            command.Parameters.AddWithValue("@Quantity", quantity);
                            command.Parameters.AddWithValue("@Price", price);

                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Item updated successfully");
                        return true;
                        }
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool DeleteItem(int itemId, int role)
        {
            try { 
                if (role == 1) 
                {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        const string query = "DELETE FROM goods WHERE id = @id";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", itemId);

                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Item deleted successfully");
                            return true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Access Denied Only Admins Can Update And Delete.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
        public bool DeleteItem(int itemId)
        {
            try
            {
                    using (var connection = new NpgsqlConnection(_connectionString))
                    {
                        const string query = "DELETE FROM goods WHERE id = @id";
                        using (var command = new NpgsqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", itemId);

                            connection.Open();
                            command.ExecuteNonQuery();
                            Console.WriteLine("Item deleted successfully");
                        return true;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


    }
}