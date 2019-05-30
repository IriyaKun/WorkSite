using System;
using Work_Site.BLL.Interfaces;

namespace Work_Site.BLL.Models
{
    public class VacationDto:IDtoModel
    {
        public string Guid { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public UserDto User { get; set; }

        public DateTime DatePosted { get; set; }
    }
}