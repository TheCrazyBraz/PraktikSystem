using System;
using System.Collections.Generic;

namespace PraktikSystem.Services
{
    public class UserService
    {
        public static readonly List<User> Users = new List<User>
        {
            new User {Username = "admin", Password = "admin", Role = "Admin"},
            new User {Username = "praktik", Password = "praktik", Role = "Praktikant"},
        };

        public User getUser(string username)
        {
            return Users.Find(u => u.Username == username);
        }
        public void AddUser(User user)
        {
            Users.Add(user);
        }
        public class User
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }

    }
}
