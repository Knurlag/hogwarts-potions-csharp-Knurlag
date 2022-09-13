using System.ComponentModel.DataAnnotations.Schema;


namespace HogwartsPotions.Models.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Student Student { get; set; }
    }
}
