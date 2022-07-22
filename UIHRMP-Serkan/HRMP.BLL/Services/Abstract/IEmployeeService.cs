using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface IEmployeeService : IGenericService<Employee>
    {
        Employee GetByEmailAndPassword(string email, string password);
        string CreateEmployeeMail(int id, string name, string surrname);
        IEnumerable<Employee> ListEmployeesByManagerID(int Id);
        string CreateRandomPassword();
    }
}
