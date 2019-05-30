using Work_Site.BLL.Interfaces;

namespace Work_Site.BLL.Models
{
    public class VacationDto:IDtoModel
    {
        public string Guid { get; set; }

        public UserDto User { get; set; }
    }
}