using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface ISiteManagerService : IGenericService<SiteManager>
    {
        SiteManager GetByEmailAndPassword(string email, string password);
    }
}
