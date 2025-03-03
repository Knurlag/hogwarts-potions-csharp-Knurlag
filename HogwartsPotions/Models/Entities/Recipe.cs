﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HogwartsPotions.Models.Entities
{
    public class Recipe
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Student Student { get; set; }
    }
}
