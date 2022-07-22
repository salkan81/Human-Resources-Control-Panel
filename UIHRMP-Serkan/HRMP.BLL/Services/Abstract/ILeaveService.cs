using HRMP.BLL.Abstract;
using HRMP.CORE.Entities;

namespace HRMP.BLL.Services.Abstract
{
    public interface ILeaveService : IGenericService<Leave>
    {
        IEnumerable<Leave> ListLeaveRequestWithEmployee();
        IEnumerable<Leave> GetAllByEmployeeId(int id);
        IEnumerable<Leave> GetAllByManagerId(int id);
    }
}
