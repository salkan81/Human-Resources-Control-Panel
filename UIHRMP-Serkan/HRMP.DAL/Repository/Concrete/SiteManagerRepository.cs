using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.DAL.Repository.Concrete
{
    public class SiteManagerRepository : GenericRepoistory<SiteManager>, ISiteManagerRepository
    {
        private readonly ApplicationDbContext context;

        public SiteManagerRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public SiteManager GetByEmailAndPassword(string email, string password)
        {
            return context.SiteManagers.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }
    }
}
