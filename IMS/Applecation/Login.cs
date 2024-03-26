using IMS.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Applecation
{
    internal class Login
    {
        private static bool ValidateLogin(string username, string password)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null) return false;
                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }
        }
    }
}
