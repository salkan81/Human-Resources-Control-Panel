using HRMP.BLL.Abstract;
using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UIHRMP.Areas.CompanyManagerArea.Models;
using UIHRMP.Areas.EmployeeArea.Models;

namespace UIHRMP.Areas.CompanyManagerArea.Controllers
{

    [Area("CompanyManagerArea")]
    public class CompanyManagerController : Controller
    {
        private readonly ICompanyManagerService _companyManagerService;
        private readonly IEmployeeService _employeeService;
        private readonly ApplicationDbContext _context;

        private readonly IAdvancePaymentsService _advancePaymentsService;
        private readonly IExpenseService _expenseService;
        private readonly ILeaveService _leaveService;
        private readonly IMesajService _mesajService;
        private readonly IPackageService packageService;
        private readonly ICompanyService companyService;

        public CompanyManagerController(ICompanyManagerService companyManagerService, ApplicationDbContext context, IEmployeeService employeeService, IAdvancePaymentsService advancePaymentsService, IExpenseService expenseService, ILeaveService leaveService, IMesajService mesajService, IPackageService packageService, ICompanyService companyService)
        {
            _companyManagerService = companyManagerService;
            _context =context;
            _employeeService = employeeService;

            _advancePaymentsService = advancePaymentsService;
            _expenseService = expenseService;
            _leaveService = leaveService;
            _mesajService = mesajService;
            this.packageService = packageService;
            this.companyService = companyService;
        }
        // GET: CompanyManagerController

        public ActionResult Index()
        {
            RequestsVM requests = new RequestsVM();
            requests.Leaves = _leaveService.ListLeaveRequestWithEmployee();
            requests.Advanceds = _advancePaymentsService.ListAdvancePaymentsRequestWithEmployee();
            requests.Expenses = _expenseService.ListAdvanceExpenseWithEmployee();

            int NotificationCount = requests.Leaves.Count() + requests.Advanceds.Count() + requests.Expenses.Count();
            ViewBag.Count = NotificationCount;
            return View(requests);
        }

        public IActionResult ChangePassword()
        {
            ChangePasswordVM change = new ChangePasswordVM();
            return View(change);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM change)
        {
            var companyManagerPassword = _companyManagerService.GetById((int)HttpContext.Session.GetInt32("managerID")).Password;
            if (ModelState.IsValid) 
            {
                if (change.Password == companyManagerPassword && change.NewPassword == change.NewPasswordControl)
                {
                    bool isDone = _companyManagerService.ChangePassword(companyManagerPassword, change.NewPassword);
                    if (isDone)
                    {
                        TempData["ValidationMessage"] = "Şifre değiştirildi.";
                        return View();
                    }

                }
                else
                {
                    TempData["ValidationMessage"] = "Şifre yanlış girilmiştir.";
                    return View();
                }

            }
            return View();
        }

        public ActionResult EditManager()
        {
            int id = (int)HttpContext.Session.GetInt32("managerID");
            CompanyManager companyManager = _companyManagerService.GetById(id);
            return View(companyManager);
        }

        // POST: CompanyManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManager(int id, CompanyManager companyManager)
        {
            CompanyManager companyManager2 = _companyManagerService.GetById(id);
            companyManager2.Address = companyManager.Address;
            companyManager2.PhoneNumber = companyManager.PhoneNumber;

            companyManager = companyManager2;

            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Bilgeler Güncellenemedi!";
                return View();
            }

            _companyManagerService.Update(companyManager2);

            return RedirectToAction(nameof(Listele));
        }
        public ActionResult ManagerDetails()
        {
            int id = (int)HttpContext.Session.GetInt32("managerID");
            CompanyManager companyManager = _companyManagerService.GetById(id);
            return View(companyManager);
        }

        // GET: CompanyManagerController/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = _employeeService.GetById(id);
            return View(employee);
        }

        // GET: CompanyManagerController/Create
        public ActionResult Create()
        {
            EmployeeCreationVM employeeCreationVM = new EmployeeCreationVM();
            return View(employeeCreationVM);
        }

        // POST: CompanyManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeCreationVM employeeCreationVM)
        {
            Employee employee = new Employee();
            employee.CompanyManagerID = (int)HttpContext.Session.GetInt32("managerID");
            employee.Email = _employeeService.CreateEmployeeMail(employee.CompanyManagerID, employeeCreationVM.Name, employeeCreationVM.Surname);
            employee.Password = _employeeService.CreateRandomPassword();           
            employee.ActivationCode = "123";
            employee.Address = employeeCreationVM.Address;
            employee.BirthDate = employeeCreationVM.BirthDate;
            employee.PhoneNumber = employeeCreationVM.PhoneNumber;
            employee.Gender = employeeCreationVM.Gender;
            employee.Name = employeeCreationVM.Name;
            employee.Surname = employeeCreationVM.Surname;
            employee.TCNo = employeeCreationVM.TCNo;
            employee.Title = employeeCreationVM.Title;
            employee.HiredDate = employeeCreationVM.HiredDate;
            employee.EmployeePhoto = employeeCreationVM.EmployeePhoto;
            if (employeeCreationVM.EmployeePhoto != null)
            { 
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() + @"/wwwroot/img/" + ticks + Path.GetExtension(employeeCreationVM.EmployeePhoto.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    employeeCreationVM.EmployeePhoto.CopyTo(stream);
                }
                
                employeeCreationVM.PhotoPath = @"~/img/" + ticks + Path.GetExtension(employeeCreationVM.EmployeePhoto.FileName);
            }
            
            employee.PhotoPath = employeeCreationVM.PhotoPath;
            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Çalışan Eklenemedi!";
                return View();
            }

            try
            {
                _employeeService.Add(employee);
                return RedirectToAction(nameof(Listele));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("managerID");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("usersurname");
            return RedirectToAction("Login", "Home", new { area = "" });
        }


        public ActionResult Listele()
        {
            //CheckActivate();
            int x = (int)HttpContext.Session.GetInt32("managerID");
            IEnumerable<Employee> employees = _employeeService.ListEmployeesByManagerID(x);
            return View(employees);
        }

        public void CheckActivate()
        {
            IEnumerable<Employee> employees = _employeeService.GetAll();
            foreach (var item in employees)
            {
                if (item.HiredDate < DateTime.Now && item.QuitDate > DateTime.Now)
                {
                    item.IsActive = true;
                    _employeeService.Update(item);
                }
                else
                {
                    item.IsActive = false;
                    _employeeService.Update(item);
                }
            }
        }

        // GET: CompanyManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            Employee employee = _employeeService.GetById(id);
            return View(employee);
        } 

        // POST: CompanyManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee employee)
        {
            Employee employee2 = _employeeService.GetById(id);
            employee2.Address = employee.Address;
            employee2.BirthDate = employee.BirthDate;
            employee2.PhoneNumber = employee.PhoneNumber;
            employee2.Gender = employee.Gender;
            employee2.Name = employee.Name;
            employee2.Surname = employee.Surname;
            employee2.TCNo = employee.TCNo;
            employee2.Title = employee.Title;
            employee2.HiredDate = employee.HiredDate;
            employee2.EmployeePhoto = employee.EmployeePhoto;

            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Çalışan Güncellenemedi!";
                return View();
            }

            _employeeService.Update(employee2);

            return RedirectToAction(nameof(Listele));
        }

        public IActionResult Advance()
        {
            RequestsVM requests = new RequestsVM();
            int x = (int)HttpContext.Session.GetInt32("managerID");
            requests.Advanceds = _advancePaymentsService.GetAllByManagerId(x);

            return View(requests);
        }
        public IActionResult AdvanceDetails(int id)
        {
            AdvancedPayment advanced = _advancePaymentsService.GetById(id);
            Employee employee = _employeeService.GetById((int)advanced.EmployeeId);
            ViewBag.Emp = employee;
            return View(advanced);
        }
        [HttpPost]
        public IActionResult AdvanceDetails(AdvancedPayment advancedPayment)
        {
            advancedPayment.IsRead = true;
            _advancePaymentsService.Update(advancedPayment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AcceptAdvanceRequest(int id)
        {
            AdvancedPayment advancedPayment = _advancePaymentsService.GetById(id);
            advancedPayment.IsCorfirmed = HRMP.CORE.Enum.ConfirmStatus.Accepted;
            advancedPayment.IsRead = true;
            _advancePaymentsService.Update(advancedPayment);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RejectAdvanceRequest(int id)
        {
            AdvancedPayment advancedPayment = _advancePaymentsService.GetById(id);
            advancedPayment.IsCorfirmed = HRMP.CORE.Enum.ConfirmStatus.Rejected;
            advancedPayment.IsRead = true;
            _advancePaymentsService.Update(advancedPayment);
            return RedirectToAction("Index");
        }

        public IActionResult Expense()
        {
            RequestsVM requests = new RequestsVM();
            int x = (int)HttpContext.Session.GetInt32("managerID");
            requests.Expenses = _expenseService.GetAllByManagerId(x);

            return View(requests);
        }

        public IActionResult ExpenseDetails(int id)
        {
            Expense expense = _expenseService.GetById(id);
            Employee employee = _employeeService.GetById((int)expense.EmployeeId);
            ViewBag.Emp = employee;
            return View(expense);
        }

        [HttpPost]
        public IActionResult ExpenseDetails(Expense expense)
        {
            expense.IsRead = true;
            _expenseService.Update(expense);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AcceptExpenseRequest(int id)
        {
            Expense expense = _expenseService.GetById(id);
            expense.ConfirmStatus = HRMP.CORE.Enum.ConfirmStatus.Accepted;
            expense.IsRead = true;
            _expenseService.Update(expense);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RejectExpenseRequest(int id)
        {
            Expense expense = _expenseService.GetById(id);
            expense.ConfirmStatus = HRMP.CORE.Enum.ConfirmStatus.Rejected;
            expense.IsRead = true;
            _expenseService.Update(expense);
            return RedirectToAction("Index");
        }

        public IActionResult Leave()
        {
            RequestsVM requests = new RequestsVM();
            int x = (int)HttpContext.Session.GetInt32("managerID");
            requests.Leaves = _leaveService.GetAllByManagerId(x);

            return View(requests);
        }

        public IActionResult LeaveDetails(int id)
        {
            
            Leave leave = _leaveService.GetById(id);

            Employee employee = _employeeService.GetById((int)leave.EmployeeId);
            ViewBag.Emp = employee;
            return View(leave);
        }

        [HttpPost]
        public IActionResult LeaveDetails(Leave leave)
        {
            leave.IsRead = true;
            _leaveService.Update(leave);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AcceptLeaveRequest(int id)
        {
            Leave leave = _leaveService.GetById(id);
            leave.ConfirmStatus = HRMP.CORE.Enum.ConfirmStatus.Accepted;
            leave.IsRead = true;
            _leaveService.Update(leave);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RejectLeaveRequest(int id)
        {
            Leave leave = _leaveService.GetById(id);
            leave.ConfirmStatus = HRMP.CORE.Enum.ConfirmStatus.Rejected;
            leave.IsRead = true;
            _leaveService.Update(leave);
            return RedirectToAction("Index");
        }

        public IActionResult SeeCompanyPackages()
        {
            CompanyManager companyManager = _companyManagerService.GetById((int)HttpContext.Session.GetInt32("managerID"));
            Company company = companyService.GetById((int)companyManager.CompanyId);
            Package package = packageService.GetById((int)company.PackageId);
            return View(package);
        }       
    }
}
