using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface IManagerRepository : IRepository<CompanyManager>
    {
        CompanyManager GetByEmailAndPassword(string email, string password);
        IEnumerable<Mesaj> ListAllMesajlar(int ManegerId);
        IEnumerable<Mesaj> ListUnreadedMesajlar(int ManegerId);



    }
}
