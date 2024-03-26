using IMS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Applecation
{
    internal class Register
    {
        private static void RegisterUser(string username, string password)
        {
            using (var db = new ApplicationDbContext())
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User { Username = username, PasswordHash = passwordHash };
                db.Users.Add(user);
                db.SaveChanges();
                Console.WriteLine("User registered successfully.");
            }
        }
    }
}
