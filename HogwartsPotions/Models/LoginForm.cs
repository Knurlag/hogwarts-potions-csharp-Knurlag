using System;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models;

public class LoginForm
{
    public string Username { get; }
    public string Password { get; }
    public LoginForm(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }
}