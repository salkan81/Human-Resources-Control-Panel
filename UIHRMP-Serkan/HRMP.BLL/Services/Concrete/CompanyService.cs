using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMP.BLL.Services.Concrete
{
    public class CompanyService:GenericService<Company>,ICompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository):base(companyRepository)
        {
            this.companyRepository = companyRepository;
        }
    }
}
