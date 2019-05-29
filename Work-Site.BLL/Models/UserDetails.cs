using Work_Site.BLL.Interfaces;

namespace Work_Site.BLL.Models
{
    public class UserDetails : IDetailsModel
    {
        public string Guid { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string Role { get; set; }
    }
}