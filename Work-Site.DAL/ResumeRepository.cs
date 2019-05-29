using System.Data.Entity;
using System.Linq;
using Work_Site.DAL.Interfaces;
using Work_Site.DAL.Models;

namespace Work_Site.DAL
{
    public class ResumeRepository : IRepository<Resume>
    {
        private readonly WorkSiteDbContext _db;

        public ResumeRepository(WorkSiteDbContext context)
        {
            _db = context;
        }

        public void Create(Resume entity)
        {
            _db.Resumes.Add(entity);
        }

        public Resume Read(string guid)
        {
            return _db.Resumes.Find(guid);
        }

        public void Update(Resume entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string guid)
        {
            var resume = _db.Resumes.Find(guid);
            if (resume != null)
            {
                _db.Resumes.Remove(resume);
            }
        }

        public IQueryable<Resume> GetAll()
        {
            return _db.Resumes;
        }
    }
}