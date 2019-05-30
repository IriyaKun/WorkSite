using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Work_Site.DAL.Interfaces;

namespace Work_Site.DAL.Models
{
    public class Vacation : IModel
    {
        [Required]
        [Key]
        public string Guid { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        //actually a string array representing filters
        public string Keywords { get; set; }

        public virtual  ICollection<User> Users { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DatePosted { get; set; }
    }
}