using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services.Interfaces;

public interface IStudentService
{
    bool ValidateLogin(LoginForm loginForm);
    bool Register(Student user);
    Student GetStudent(string username);
}