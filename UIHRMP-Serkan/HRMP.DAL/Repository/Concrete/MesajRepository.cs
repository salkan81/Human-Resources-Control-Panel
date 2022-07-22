using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.DAL.Repository.Concrete
{
    public class MesajRepository : GenericRepoistory<Mesaj>, IMesajRepository
    {
        private readonly ApplicationDbContext context;
        public MesajRepository(ApplicationDbContext _context) : base(_context)
        {
            context = _context;
        }


    }
}
