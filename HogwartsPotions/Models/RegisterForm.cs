using System;
using HogwartsPotions.Models.Enums;

namespace HogwartsPotions.Models;

public class RegisterForm
{
    public string Username { get; }
    public string Password { get; }
    public HouseType HouseType { get; }
    public PetType PetType { get; }

    public RegisterForm(string username, string password, HouseType houseType, PetType petType)
    {
        Username = username;
        Password = password;
        HouseType = houseType;
        PetType = petType;
    }
}