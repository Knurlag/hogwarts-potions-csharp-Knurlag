using System;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Models;

public class LoginForm
{
    public string Username;
    public string Password;
    public LoginForm(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }
}