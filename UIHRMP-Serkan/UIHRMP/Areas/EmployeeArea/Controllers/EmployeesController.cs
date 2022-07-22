using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using Microsoft.AspNetCore.Mvc;
using UIHRMP.Areas.EmployeeArea.Models;

namespace UIHRMP.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IMesajService _mesajService;
        private readonly IAdvancePaymentsService _advancePaymentsService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;

        public EmployeesController(IEmployeeService employeeService, IAdvancePaymentsService advancePaymentsService, IExpenseService expenseService, ILeaveService leaveService, IMesajService mesajService)
        {
            this.employeeService = employeeService;
            _mesajService = mesajService;
            _advancePaymentsService = advancePaymentsService;
            _expenseService = expenseService;
            _leaveService = leaveService;
        }

        // GET: EmployeeArea/Employees

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Employee employee)
        {
            var _employeeService = employeeService.GetByEmailAndPassword(employee.Email, employee.Password);
            HttpContext.Session.SetInt32("employeeId", _employeeService.Id);
            HttpContext.Session.SetString("username", _employeeService.Name);
            HttpContext.Session.SetString("usersurname", _employeeService.Surname);

            if (_employeeService == null)
            {
                TempData["Message"] = "Giriş Bilgileri Hatalı!";
                return View();
            }
            return RedirectToAction("Details", "Employees");
        }

        // GET: EmployeeArea/Employees/Details/5
        public IActionResult Details()
        {
            int id = (int)HttpContext.Session.GetInt32("employeeId");
            Employee employee = employeeService.GetById(id);

            return View(employee);
        }


        // GET: EmployeeArea/Employees/Edit/5
        public IActionResult Edit()
        {
            int id = (int)HttpContext.Session.GetInt32("employeeId");
            Employee employee = employeeService.GetById(id);
            return View(employee);
        }

        // POST: EmployeeArea/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employee)
        {
            Employee employee1 = employeeService.GetById(id);
            employee1.Password = employee.Password;
            employee1.PhoneNumber = employee.PhoneNumber;
            employee1.Address = employee.Address;

            employeeService.Update(employee1);
            return RedirectToAction(nameof(Details));

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("employeeId");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("usersurname");
            return RedirectToAction("Login", "Home", new { area = "" });
        }


        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM change)
        {
            var personelPassword = employeeService.GetById((int)HttpContext.Session.GetInt32("employeeId")).Password;
            if (ModelState.IsValid)
            {
                if (change.Password == personelPassword && change.NewPassword == change.NewPasswordControl)
                {
                    bool isDone = employeeService.ChangePassword(personelPassword, change.NewPassword);
                    if (isDone)
                    {
                        TempData["ValidationMessage"] = "Şifre değiştirildi.";
                        return View();
                    }
                }
                else
                {
                    TempData["ValidationMessage"] = "Şifre yanlış girilmiştir.";
                    return View(change);
                }
            }
            return View();
        }

        public IActionResult Leave()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Leave(Leave leave)
        {

            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("employeeId"));
            leave.EmployeeId = employee.Id;
            leave.CompanyManagerId = employee.CompanyManagerID;
            if (employee.Gender==HRMP.CORE.Enum.Gender.Erkek || leave.LeaveType==HRMP.CORE.Enum.LeaveType.Doğum)
            {
                ModelState.AddModelError("", "Erkek çalışanlar için doğum izni talebi oluşturulamaz.");
                return View();
            }
            if (employee.Gender == HRMP.CORE.Enum.Gender.Kadın || leave.LeaveType == HRMP.CORE.Enum.LeaveType.Babalık)
            {
                ModelState.AddModelError("", "Kadın çalışanlar için babalık izni talebi oluşturulamaz.");
                return View();
            }
            if (leave.LeaveType==HRMP.CORE.Enum.LeaveType.Babalık)
            {
                leave.TotalDaysOff = 3;
            }
            if (leave.LeaveType == HRMP.CORE.Enum.LeaveType.Doğum)
            {
                leave.TotalDaysOff = 80;
            }
            if (DateTime.Now.Year - employee.HiredDate.Year <= 5 && leave.LeaveType==HRMP.CORE.Enum.LeaveType.Yıllık)
            {
                leave.TotalDaysOff = 14;
            }
            else if (DateTime.Now.Year - employee.HiredDate.Year > 5 && leave.LeaveType == HRMP.CORE.Enum.LeaveType.Yıllık)
            {
                leave.TotalDaysOff = 21;
            }

            leave.EndLeaveDate = leave.StartLeaveDate.AddDays((int)leave.TotalDaysOff);

            _leaveService.Add(leave);
            return RedirectToAction("Details");
        }

        public IActionResult Expense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Expense(Expense expense)
        {
            if (expense.ExpenseDocument != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() + @"/wwwroot/files/" + ticks + Path.GetExtension(expense.ExpenseDocument.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    expense.ExpenseDocument.CopyTo(stream);
                }

                expense.ExpenseDocumentPath = @"/files/" + ticks + Path.GetExtension(expense.ExpenseDocument.FileName);
            }
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("employeeId"));
            expense.EmployeeId = employee.Id;
            expense.CompanyManagerId = employee.CompanyManagerID;

            _expenseService.Add(expense);
            return RedirectToAction("Details");
        }

        public IActionResult AdvancePayments()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdvancePayments(AdvancedPayment advanced)
        {
            Employee employee = employeeService.GetById((int)HttpContext.Session.GetInt32("employeeId"));
            advanced.EmployeeId = employee.Id;
            advanced.CompanyManagerId = employee.CompanyManagerID;

            _advancePaymentsService.Add(advanced);
            return RedirectToAction("Details");
        }

        public IActionResult ShowAdvancePayments()
        {
            IEnumerable<AdvancedPayment> advancedPayments = _advancePaymentsService.GetAllByEmployeeId((int)HttpContext.Session.GetInt32("employeeId"));
            return View(advancedPayments);
        }

        public IActionResult ShowExpense()
        {
            IEnumerable<Expense> expenses = _expenseService.GetAllByEmployeeId((int)HttpContext.Session.GetInt32("employeeId"));
            return View(expenses);
        }

        public IActionResult ShowLeave()
        {
            IEnumerable<Leave> leaves = _leaveService.GetAllByEmployeeId((int)HttpContext.Session.GetInt32("employeeId"));
            return View(leaves);
        }



    }
}
