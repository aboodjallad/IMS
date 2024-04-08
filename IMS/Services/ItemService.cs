using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using IMS.Applecation;
namespace IMS.Services
{

    public class ItemService : iItem
    {
        private readonly string _connectionString;

        public ItemService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddItem(string name, int quantity, decimal price, string status)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "INSERT INTO InventoryItems (Name, Quantity, Price, Status) VALUES (@Name, @Description, @Quantity, @CategoryId, @Price, @Status)";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Status", status);

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

        public void GetItems()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                const string query = "SELECT ItemId, Name, Quantity FROM InventoryItems";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int quantity = int.Parse(reader["Quantity"].ToString());
                            string status = GetStatusBasedOnQuantity(quantity);

                            Console.WriteLine($"ID: {reader["ItemId"]}, Name: {reader["Name"]}, Quantity: {quantity}, Status: {status}");
                        }
                    }
                }
            }
        }

        private string GetStatusBasedOnQuantity(int quantity)
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

        public bool UpdateItem(int itemId, string name, int quantity, decimal price, string status)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    const string query = "UPDATE InventoryItems SET Name = @Name, Quantity = @Quantity, Price = @Price, Status = @Status WHERE ItemId = @ItemId";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Status", status);

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
                    const string query = "DELETE FROM InventoryItems WHERE ItemId = @ItemId";
                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ItemId", itemId);

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