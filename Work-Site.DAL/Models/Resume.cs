using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Work_Site.DAL.Interfaces;

namespace Work_Site.DAL.Models
{
    public class Resume: IModel
    {
        [Required]
        [Key]
        public string Guid { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public User User { get; set; }

        [ForeignKey("Vacation")]
        public string VacationId { get; set; }

        [Required]
        public Vacation Vacation { get; set; }

        [Required]
        public string YearsOfExperience { get; set; }

        public string SpecialMessage { get; set; }

        public string PortfolioLink { get; set; }
    }
}