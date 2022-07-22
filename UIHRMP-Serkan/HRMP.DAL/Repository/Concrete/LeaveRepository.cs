using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace HRMP.DAL.Repository.Concrete
{
    public class LeaveRepository : GenericRepoistory<Leave>, ILeaveRepository
    {
        private readonly ApplicationDbContext context;

        public LeaveRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Leave> GetAllByEmployeeId(int id)
        {
            return context.Leaves.Where(x => x.EmployeeId == id);
        }

        IEnumerable<Leave> ILeaveRepository.ListLeaveRequestWithEmployee()
        {
            return context.Leaves.Where(x => x.IsRead == false || x.ConfirmStatus == CORE.Enum.ConfirmStatus.Pending).Include(x => x.Employee).ToList();
        }

        public IEnumerable<Leave> GetAllByManagerId(int id)
        {
            return context.Leaves.Where(x => x.CompanyManagerId == id).Include(x => x.Employee).ToList();
        }
    }
}
