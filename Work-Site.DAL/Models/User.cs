using System;
using System.Collections;
using System.Collections.Generic;
using Work_Site.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Work_Site.DAL.Models
{
    public class User : IModel
    {
        public User()
        {
            Vacations = new List<Vacation>();
        }

        [Required]
        [Key]
        public string Guid { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string HashedPassword { get; set; }

        [Required]
        public string Role { get; set; }

        [ForeignKey("Guid")]
        public Resume Resume { get; set; }

        public ICollection<Vacation> Vacations { get; set; }
    }
}