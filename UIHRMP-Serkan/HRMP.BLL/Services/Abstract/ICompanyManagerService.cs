using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface ICompanyManagerService : IGenericService<CompanyManager>
    {
        CompanyManager GetByEmailAndPassword(string email, string password);
        IEnumerable<Mesaj> ListAllMesajlar(int ManegerId);
        IEnumerable<Mesaj> ListUnreadedMesajlar(int ManegerId);

    }
}
