using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HogwartsPotions.Models.Entities
{
    public class Student : IdentityUser
    {
        public HouseType HouseType { get; set; }
        public PetType PetType { get; set; }
        
    }
}
