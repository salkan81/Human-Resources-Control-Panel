using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class EmployeeService : GenericService<Employee>, IEmployeeService

    {
        private readonly IPersonelRepository employeeRepository;

        public EmployeeService(IPersonelRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public string CreateEmployeeMail(int id, string name, string surrname)
        {
            if (id == 0)
            {
                throw new Exception("Yönetici null geldi!");
            }
            else
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surrname))
                {
                    throw new Exception("Çalışan ad veya soyad boş!");
                }
            }

            return employeeRepository.CreateEmployeeMail(id, name, surrname);

        }

        public string CreateRandomPassword()
        {
            return employeeRepository.CreateRandomPassword();
        }

        public Employee GetByEmailAndPassword(string email, string password)
        {
            return employeeRepository.GetByEmailAndPassword(email, password);
        }

        public IEnumerable<Employee> ListEmployeesByManagerID(int Id)
        {
            return employeeRepository.ListEmployeesByManagerID(Id);
        }
    }
}
