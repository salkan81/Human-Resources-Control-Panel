     using HRMP.BLL.Services.Abstract;
using HRMP.BLL.Services.Concrete;
using HRMP.CORE.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UIHRMP.Models;

namespace UIHRMP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICompanyManagerService _managerService;
        private readonly IEmployeeService _employeeService;
        private readonly ISiteManagerService siteManagerService;

        public HomeController(ILogger<HomeController> logger, ICompanyManagerService managerService,IEmployeeService employeeService, ISiteManagerService siteManagerService)
        {
            _logger = logger;
            _managerService = managerService;
            _employeeService = employeeService;
            this.siteManagerService = siteManagerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult UserStories()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
  
                var _companyManager = _managerService.GetByEmailAndPassword(email, password);
                if (_companyManager != null)
                {
                    HttpContext.Session.SetInt32("managerID", _companyManager.Id);
                    HttpContext.Session.SetString("username", _companyManager.Name);
                    HttpContext.Session.SetString("usersurname", _companyManager.Surname);
                    if (_companyManager.PhotoPath != null)
                    {
                        HttpContext.Session.SetString("picPath", _companyManager.PhotoPath);
                    }

                    return RedirectToAction("Index", "CompanyManager", new { area = "CompanyManagerArea" });
                }
                var _employee = _employeeService.GetByEmailAndPassword(email, password);
                if (_employee != null)
                {
                    HttpContext.Session.SetInt32("employeeId", _employee.Id);
                    HttpContext.Session.SetString("username", _employee.Name);
                    HttpContext.Session.SetString("usersurname", _employee.Surname);
                    if (_employee.PhotoPath != null)
                    {
                        HttpContext.Session.SetString("picPath", _employee.PhotoPath);
                    }
                    return RedirectToAction("Details", "Employees", new { area = "EmployeeArea" });
                }

                var _siteManager = siteManagerService.GetByEmailAndPassword(email, password);
                if (_siteManager != null)
                {
                    HttpContext.Session.SetInt32("siteManagerId", _siteManager.Id);
                    HttpContext.Session.SetString("username", _siteManager.Name);
                    HttpContext.Session.SetString("usersurname", _siteManager.Surname);
                    if (_siteManager.PhotoPath != null)
                    {
                        HttpContext.Session.SetString("picPath", _siteManager.PhotoPath);
                    }
                    return RedirectToAction("Index", "SiteManager", new { area = "SiteManagerArea" });
                }
            else
            {
                TempData["LoginError"] = "Giriş Bilgileri Hatalı!";
                return View();
            }
                

        }   

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}