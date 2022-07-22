using HRMP.CORE.Entities;

namespace HRMP.DAL.Repository.Abstract
{
    public interface ILeaveRepository : IRepository<Leave>
    {
        IEnumerable<Leave> ListLeaveRequestWithEmployee();
        IEnumerable<Leave> GetAllByEmployeeId(int id);
        IEnumerable<Leave> GetAllByManagerId(int id);
    }
}
