using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class SiteManagerService : GenericService<SiteManager>, ISiteManagerService
    {
        private readonly ISiteManagerRepository siteMaangerRepository;

        public SiteManagerService(ISiteManagerRepository siteMaangerRepository) : base(siteMaangerRepository)
        {
            this.siteMaangerRepository = siteMaangerRepository;
        }

        public SiteManager GetByEmailAndPassword(string email, string password)
        {
            return siteMaangerRepository.GetByEmailAndPassword(email, password);
        }
    }
}
