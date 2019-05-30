using System;

namespace Work_Site.DAL
{
    public class WorkSiteUow : IDisposable
    {
        private readonly WorkSiteDbContext _db;

        public WorkSiteUow()
        {
            _db = new WorkSiteDbContext();
        }

        public WorkSiteUow(WorkSiteDbContext context)
        {
            _db = context;
        }

        private UsersRepository _usersRepository;
        private VacationsRepository _vacationsRepository;
        private ResumeRepository _resumeRepository;

        public UsersRepository Users
        {
            get
            {
                if (_usersRepository == null)
                    _usersRepository = new UsersRepository(_db);
                return _usersRepository;
            }
        }

        public VacationsRepository Vacations
        {
            get
            {
                if (_vacationsRepository == null)
                    _vacationsRepository = new VacationsRepository(_db);
                return _vacationsRepository;
            }
        }

        public ResumeRepository Resumes
        {
            get
            {
                if (_resumeRepository == null)
                    _resumeRepository = new ResumeRepository(_db);
                return _resumeRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}