using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Repository.Abstract;

namespace HRMP.BLL.Services.Concrete
{
    public class LeaveService : GenericService<Leave>, ILeaveService
    {
        private readonly ILeaveRepository leaveRepository;

        public LeaveService(ILeaveRepository leaveRepository) : base(leaveRepository)
        {
            this.leaveRepository = leaveRepository;
        }

        public IEnumerable<Leave> GetAllByEmployeeId(int id)
        {
            return leaveRepository.GetAllByEmployeeId(id);
        }

        public IEnumerable<Leave> ListLeaveRequestWithEmployee()
        {
            return leaveRepository.ListLeaveRequestWithEmployee();
        }

        public IEnumerable<Leave> GetAllByManagerId(int id)
        {
            return leaveRepository.GetAllByManagerId(id);
        }
    }
}
