using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.DAL.Repository.Concrete
{
    public class GenericRepoistory<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;

        public GenericRepoistory(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool Add(T entity)
        {
            try
            {
                context.Set<T>().Add(entity);
                return context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public bool ChangePassword(string password, string newPassword)
        {

            Employee employee = context.Employees.Where(x => x.Password == password).SingleOrDefault();
            if (employee != null)
            {
                employee.Password = newPassword;
            }

            CompanyManager companyManager = context.CompanyManagers.Where(x => x.Password == password).SingleOrDefault();
            if (companyManager != null)
            {
                companyManager.Password = newPassword;
            }

            SiteManager siteManager = context.SiteManagers.Where(x => x.Password == password).SingleOrDefault();
            if (siteManager != null)
            {
                siteManager.Password = newPassword;
            }
            return context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            try
            {
                context.Set<T>().Remove(entity);
                return context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().FirstOrDefault(a => a.Id == id);
        }

        public bool Update(T entity)
        {
            try
            {
                context.Set<T>().Update(entity);
                return context.SaveChanges() > 0;
            }
            catch
            {

                return false;
            }



        }
    }
}
