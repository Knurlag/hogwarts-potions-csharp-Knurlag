using System.Linq;
using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models;
using System.Security.Cryptography;
using System.Text;
using HogwartsPotions.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Services;

public class StudentService : IStudentService
{
    private readonly HogwartsContext _context;

    public StudentService(HogwartsContext context)
    {
        _context = context;
    }
    public bool ValidateLogin(LoginForm loginForm)
    {
        string hashedPassword = PasswordHash.HashPassword(loginForm.Password);

        return _context.Students.AsEnumerable().Any(u => u.UserName == loginForm.Username && FixedTimeEquals(u.PasswordHash, hashedPassword));
    }

    private bool FixedTimeEquals(string str1, string str2)
    {
        if (str1 == null || str2 == null)
        {
            return false;
        }
        return CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(str1), Encoding.UTF8.GetBytes(str2));
    }
    private bool CheckRegistrationStatus(string name)
    {
        var u = _context.Students.FirstOrDefault(u => u.UserName == name);
        return u == null;
    }

    public bool Register(Student user)
    {
        if (CheckRegistrationStatus(user.UserName))
        {

            _context.Students.Add(user);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public Student GetStudent(string username)
    {
        var student = _context.Students.FirstOrDefault(p => p.UserName == username);
        if (student == null)
        {
            //TODO add Error logging
        }
        return student;
    }
}