using Work_Site.DAL.Interfaces;
using Work_Site.DAL.Models;

namespace Work_Site.BLL.Models
{
    public class VacationDetails:IModel
    {
        public string Guid { get; set; }

        public UserDetails User { get; set; }
    }
}