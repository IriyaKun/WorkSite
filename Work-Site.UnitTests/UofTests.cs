using Work_Site.DAL;
using Work_Site.DAL.Models;
using Xunit;

namespace Work_Site.UnitTests
{
    public class UofTests
    {
        private WorkSiteUOF _uof;


        [Fact]
        public void UserShouldBeAdded()
        {
            var user = new User()
            {
                Guid = System.Guid.NewGuid().ToString(),
                Email = "mock-email@email.com",
                Name = "Ilya",
                Surname = "Gomel",
                HashedPassword = "myawesomehashedpassword",
                Role = "admin"
            };
            _uof = new WorkSiteUOF();
            _uof.Users.Create(user);
            _uof.Save();
            _uof.Dispose();
            
        }
    }
}