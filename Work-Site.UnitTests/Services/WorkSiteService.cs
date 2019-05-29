using Work_Site.DAL;

namespace Work_Site.UnitTests.Services
{
    public class WorkSiteService
    {
        private WorkSiteDbContext _context;

        public WorkSiteService(WorkSiteDbContext context)
        {
            _context = context;
        }

        
}
}