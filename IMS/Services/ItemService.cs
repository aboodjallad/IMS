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

        public bool AddItem(string name, int quantity, decimal price)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "INSERT INTO goods (Name, Quantity, Price) VALUES (@Name,  @Quantity,  @Price)";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);

                        connection.Open();
                        command.ExecuteNonQuery();
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
                            // Create a new Item object and populate it with the data from the reader
                            var item = new Good
                            {
                                GoodId = Convert.ToInt32(reader["id"]),
                                Name = reader["Name"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToInt32(reader["Price"]),
                                Status = GetStatusBasedOnQuantity(Convert.ToInt32(reader["Quantity"]))
                            };

                            // Add the new item to the list
                            items.Add(item);
                        }
                    }
                }
            }
            return items; // Return the list of items
        }
        
        public string GetStatusBasedOnQuantity(int quantity)
        {
            if (quantity == 0)
            {
                return "Out of Stock";
            }
            else if (quantity > 0 && quantity <= 10) // Assuming 10 as the low stock threshold
            {
                return "Low Stock";
            }
            else
            {
                return "In Stock";
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