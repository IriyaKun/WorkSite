using System.Data.Entity;
using Work_Site.DAL.Models;

namespace Work_Site.DAL
{
    public class WorkSiteDbContext : DbContext
    {
        public WorkSiteDbContext() : base("Data Source=DESKTOP-GI49IJ2;Initial Catalog=model;Integrated Security=True")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
    }
}