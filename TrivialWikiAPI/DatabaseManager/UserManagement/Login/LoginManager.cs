﻿using DatabaseManager.DatabaseModels;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace DatabaseManager.UserManagement.Login
{
    public sealed class LoginManager
    {
        public User Login(string username, string password)
        {
            using (var databaseContext = new DatabaseContext())
            {
                var user = databaseContext.Users.Include("Role")
                    .Single(u => u.UserName == username && u.Password == password);
                if (user == null)
                {
                    return null;
                }

                CreateUserSecurityToken(user);
                databaseContext.SaveChanges();

                return user;
            }
        }

        private static void CreateUserSecurityToken(User user)
        {
            var salt = new byte[32];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            user.SecurityToken = Convert.ToBase64String(salt).ToUpperInvariant();
        }
    }
}
