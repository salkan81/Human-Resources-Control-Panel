using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface IPersonelRepository : IRepository<Employee>
    {
        IEnumerable<Employee> ListEmployeesByManagerID(int Id);
        Employee GetByEmailAndPassword(string email, string password);
        string CreateEmployeeMail(int id, string name, string surrname);
        string CreateRandomPassword();
    }
}
