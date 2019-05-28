using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Work_Site.DAL.Interfaces;

namespace Work_Site.DAL.Models
{
    public class Vacation : IModel
    {
        [Required]
        public Guid Guid { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public virtual List<Resume> Resumes { get; set; }
    }
}