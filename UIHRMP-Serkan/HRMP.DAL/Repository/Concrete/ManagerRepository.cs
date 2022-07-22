using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.DAL.Repository.Concrete
{
    public class ManagerRepository : GenericRepoistory<CompanyManager>, IManagerRepository
    {
        private readonly ApplicationDbContext context;

        public ManagerRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public CompanyManager GetByEmailAndPassword(string email, string password)
        {
            return context.CompanyManagers.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public IEnumerable<Mesaj> ListAllMesajlar(int ManegerId)
        {
            return context.Mesajlar.Where(x => x.Id == ManegerId).OrderByDescending(a => a.Date).ToList();
        }

        public IEnumerable<Mesaj> ListUnreadedMesajlar(int ManegerId)
        {
            return context.Mesajlar.Where(x => x.Id == ManegerId && x.Okundu == false).OrderByDescending(a => a.Date).ToList();
        }


    }
}
