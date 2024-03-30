using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
namespace IMS.Applecation
{



    public class AuthService : iAuth
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Register(string username, string password, int role)
        {

            try
            {
                using (var connection = new NpgsqlConnection(_connectionString))
                {
                    connection.Open();

                    string passwordhashed = BCrypt.Net.BCrypt.HashPassword(password);

                    using (var checkCommand = new NpgsqlCommand("SELECT COUNT(*) FROM Users WHERE username = @username", connection))
                    {
                        checkCommand.Parameters.AddWithValue("@username", username);

                        int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (userCount > 0)
                        {
                            Console.WriteLine("Username already exists. Please choose a different username.");
                            return false;
                        }
                    }

                    using (var command = new NpgsqlCommand("INSERT INTO Users (username, password , role) VALUES (@username, @password, @role)", connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", passwordhashed);
                        command.Parameters.AddWithValue("@role", role);

                        var result = command.ExecuteNonQuery();
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public bool Login(string username, string password)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT password FROM users WHERE username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString(0);
                            return BCrypt.Net.BCrypt.Verify(password, storedHash);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
        }

        public void Logout()
        {
            // Implementation depends on how you manage user sessions.
            // This could simply clear a session variable, for example.
            Console.WriteLine("User logged out successfully.");
        }

        
    }
}

