using System.Linq;
using Work_Site.DAL.Models;

namespace Work_Site.DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        void Create(T entity);

        T Read(string guid);

        void Update(T entity);

        void Delete(string guid);

        IQueryable<T> GetAll();
    }
}