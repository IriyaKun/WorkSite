using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Work_Site.DAL.Interfaces;
using Work_Site.DAL.Models;

namespace Work_Site.DAL
{
    public class UsersRepository : IRepository<User>
    {
        private readonly WorkSiteDbContext _db;

        public UsersRepository(WorkSiteDbContext context)
        {
            _db = context;
        }

        public void Create(User entity)
        {
            _db.Users.Add(entity);
        }

        public User Read(string guid)
        {
            return _db.Users.Find(guid);
        }

        public void Update(User entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string guid)
        {
            var user = _db.Users.Find(guid);
            if (user != null)
            {
                _db.Users.Remove(user);
            }
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;
        }
    }
}