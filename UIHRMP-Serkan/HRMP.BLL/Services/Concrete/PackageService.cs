using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class PackageService : GenericService<Package>, IPackageService
    {
        private readonly IPackageRepository packageRepository;

        public PackageService(IPackageRepository packageRepository) : base(packageRepository)
        {
            this.packageRepository = packageRepository;
        }
    }
}
