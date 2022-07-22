using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMP.DAL.Repository.Concrete
{
    public class CompanyRepository:GenericRepoistory<Company>,ICompanyRepository
    {
        private readonly ApplicationDbContext context;

        public CompanyRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

    }
}
