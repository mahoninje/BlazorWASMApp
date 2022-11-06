using BlazorWASMApp.Auth.Abstract.Models;
using BlazorWASMApp.Auth.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWASMApp.Auth.Implementations.Models
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IUser
    {
        public string Login { get; private set; }

        public string Password { get; private set; }

        public UserRole Role { get; private set; }

        public User(string login, string password, UserRole role)
        {
            Login = login;
            Password = password;
            Role = role;
        }

        public void ChangePassword(string newPassword) 
        {
            if (string.IsNullOrEmpty(newPassword)) return;

            Password = newPassword;
        }
    }
}
