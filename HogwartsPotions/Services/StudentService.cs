using System.Linq;
using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Services.Interfaces;

namespace HogwartsPotions.Services;

public class StudentService : IStudentService
{
    private readonly HogwartsContext _context;

    public StudentService(HogwartsContext context)
    {
        _context = context;
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