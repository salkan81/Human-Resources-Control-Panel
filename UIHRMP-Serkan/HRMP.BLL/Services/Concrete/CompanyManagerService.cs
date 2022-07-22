using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class CompanyManagerService : GenericService<CompanyManager>, ICompanyManagerService
    {
        private readonly IManagerRepository managerRepository;

        public CompanyManagerService(IManagerRepository managerRepository) : base(managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public CompanyManager GetByEmailAndPassword(string email, string password)
        {
            return managerRepository.GetByEmailAndPassword(email, password);
        }

        public IEnumerable<Mesaj> ListAllMesajlar(int ManegerId)
        {
            return managerRepository.ListAllMesajlar(ManegerId);
        }

        public IEnumerable<Mesaj> ListUnreadedMesajlar(int ManegerId)
        {
            return managerRepository.ListUnreadedMesajlar(ManegerId);
        }
    }
}
