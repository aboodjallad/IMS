using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Applecation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace IMS.Services
{

    public class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private readonly NpgsqlConnection _connection;
        private static readonly object _lock = new object();

        private DatabaseConnection()
        {
            _connection = new NpgsqlConnection("Host=localhost; Port=5432; Database=ims; Username=postgres; Password=123");
            _connection.Open();
        }

        public static DatabaseConnection GetInstance()
        {
            
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseConnection();
                        }
                    }
                }
                return _instance;
            }
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public NpgsqlConnection Connection => _connection;

    }



}
