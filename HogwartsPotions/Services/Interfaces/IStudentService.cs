using HogwartsPotions.Models.Entities;

namespace HogwartsPotions.Services.Interfaces;

public interface IStudentService
{
    Student GetStudent(string username);
}