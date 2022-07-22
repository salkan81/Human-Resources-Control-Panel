using HRMP.CORE.Enum;
using HRMP.DAL.Database;
using Microsoft.AspNetCore.Mvc;
using UIHRMP.Areas.CompanyManagerArea.Models;

namespace UIHRMP.Areas.CompanyManagerArea.Controllers
{
    [Area("CompanyManagerArea")]
    public class ManagerCheckController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ManagerCheckController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var enums = ConfirmStatus.Pending;
            var leaves = (from lt in _context.Leaves
                          join p in _context.Employees on lt.Id equals p.Id
                          where p.CompanyManagerID == Convert.ToInt32(HttpContext.Session.GetString("usercompanyId")) && lt.ConfirmStatus == enums
                          select new ManagerLeaveList
                          {
                              Id = lt.Id,
                              FirstName = p.Name,
                              LastName = p.Surname,
                              Description = lt.Description,
                              LeaveTypeName = lt.LeaveType.ToString(),
                              TotalDaysOff = (int)lt.TotalDaysOff,
                              StartLeaveDate = lt.StartLeaveDate,
                              EndLeaveDate = lt.EndLeaveDate,
                              ConfirmStatus = lt.ConfirmStatus,
                              LeaveDocument = lt.LeaveContent

                          }).ToList();
            var expenses = (from e in _context.Expenses
                            join pd in _context.Employees on e.Id equals pd.Id
                            where pd.CompanyManagerID == Convert.ToInt32(HttpContext.Session.GetString("usercompanyId")) && e.ConfirmStatus == enums
                            select new ManagerExpenseList
                            {
                                Id = e.Id,
                                Amount = (double)e.Amount,
                                FirstName = pd.Name,
                                LastName = pd.Surname,
                                CheckDocument = e.ExpenseDocumentPath,
                                ConfirmStatus = e.ConfirmStatus,
                                DateOfExpense = e.DateOfExpense,
                                ExpenseTypeName = e.ExpenseType.ToString(),
                                Explanation = e.Description
                            }).ToList();
            return View();
        }
        public IActionResult AcceptLeave(int id)
        {
            var enums = ConfirmStatus.Accepted;
            var leave = _context.Leaves.Find(id);
            leave.ConfirmStatus = enums;
            _context.SaveChanges();
            return RedirectToAction("Index", "ManagerCheck", new { Id = HttpContext.Session.GetString("userId") });
        }
        public IActionResult RejectLeave(int id)
        {
            var enums = ConfirmStatus.Rejected;
            var leave = _context.Leaves.Find(id);
            leave.ConfirmStatus = enums;
            _context.SaveChanges();
            return RedirectToAction("Index", "ManagerCheck", new { Id = HttpContext.Session.GetString("userId") });
        }
        public IActionResult AcceptExpense(int id)
        {
            var enums = ConfirmStatus.Accepted;
            var expense = _context.Expenses.Find(id);
            expense.ConfirmStatus = enums;
            _context.SaveChanges();
            return RedirectToAction("Index", "ManagerCheck", new { Id = HttpContext.Session.GetString("userId") });
        }
        public IActionResult RejectExpense(int id)
        {
            var enums = ConfirmStatus.Rejected;
            var expense = _context.Expenses.Find(id);
            expense.ConfirmStatus = enums;
            _context.SaveChanges();
            return RedirectToAction("Index", "ManagerCheck", new { Id = HttpContext.Session.GetString("userId") });
        }
    }
}
