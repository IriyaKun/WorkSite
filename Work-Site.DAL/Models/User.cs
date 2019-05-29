using System;
using Work_Site.DAL.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Work_Site.DAL.Models
{
    public class User : IModel
    {
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
    }
}