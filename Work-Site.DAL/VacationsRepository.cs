using System.Data.Entity;
using System.Linq;
using System.Web.Management;
using Work_Site.DAL.Interfaces;
using Work_Site.DAL.Models;

namespace Work_Site.DAL
{
    public class VacationsRepository :IRepository<Vacation>
    {
        private readonly WorkSiteDbContext _db;

        public VacationsRepository(WorkSiteDbContext context)
        {
            _db = context;
        }

        public void Create(Vacation entity)
        {
            _db.Vacations.Add(entity);
        }

        public Vacation Read(string guid)
        {
            return _db.Vacations.Find(guid);
        }

        public void Update(Vacation entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string guid)
        {
            var entity = _db.Vacations.Find(guid);
            if(entity!=null)
            {
                _db.Vacations.Remove(entity);
            }
        }

        public IQueryable<Vacation> GetAll()
        {
            return _db.Vacations;
        }
    }
}