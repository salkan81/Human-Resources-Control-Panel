using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.DAL.Repository.Concrete
{
    public class PackageRepository : GenericRepoistory<Package>, IPackageRepository
    {
        private readonly ApplicationDbContext context;

        public PackageRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
