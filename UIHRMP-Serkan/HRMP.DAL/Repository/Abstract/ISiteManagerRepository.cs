using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface ISiteManagerRepository : IRepository<SiteManager>
    {
        SiteManager GetByEmailAndPassword(string email, string password);
    }
}
