using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IMS.Applecation;
using Npgsql;
namespace IMS.Services
{



    public class AuthService : iAuth
    {
        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Register(string username, string password)
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

                    using (var command = new NpgsqlCommand("INSERT INTO Users (username, password ) VALUES (@username, @password)", connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", passwordhashed);

                        var result = command.ExecuteNonQuery();
                        Console.WriteLine("Registration successful.");
                        return result == 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registration failed.");
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public int GetRole(string username , string password)
        { 
            int role = Login(username,password);
            return role;
        }


        public int Login(string username, string password)
        {
            int role=0;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand("SELECT password, role FROM users WHERE username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString(0); 
                            role = Convert.ToInt32(reader["role"]); 
                            return role;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }

        public void Logout()
        {
            
            Console.WriteLine("User logged out successfully.");
        }


    }
}

