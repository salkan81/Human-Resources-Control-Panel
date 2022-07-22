using HRMP.BLL.Services.Abstract;
using HRMP.CORE.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UIHRMP.Areas.EmployeeArea.Models;

namespace UIHRMP.Areas.SiteManagerArea.Controllers
{
    [Area("SiteManagerArea")]
    public class SiteManagerController : Controller
    {
        private readonly ISiteManagerService siteManagerService;
        private readonly ICompanyManagerService companyManagerService;
        private readonly IPackageService packageService;
        private readonly ICompanyService companyService;

        public SiteManagerController(ISiteManagerService siteManagerService, ICompanyManagerService companyManagerService,IPackageService packageService, ICompanyService companyService)
        {
            this.siteManagerService = siteManagerService;
            this.companyManagerService = companyManagerService;
            this.packageService = packageService;
            this.companyService = companyService;
        }
        // GET: SiteManagerController
        public IActionResult Index()
        {
            IEnumerable<Package> packages = packageService.GetAll();
            return View(packages);
        }

        // GET: SiteManagerController/Details/5
        public IActionResult Details()
        {
            int id = (int)HttpContext.Session.GetInt32("siteManagerId");
            SiteManager siteManager = siteManagerService.GetById(id);
            return View(siteManager);
        }

        // GET: SiteManagerController/Create
        public IActionResult CreatePackage()
        {
            ViewBag.companyList = companyService.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult CreatePackage(Package package)
        {
            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Paket Eklenemedi!";
                return View();
            }

            if (package.PackagePhoto != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() + @"/wwwroot/img/" + ticks + Path.GetExtension(package.PackagePhoto.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    package.PackagePhoto.CopyTo(stream);
                }

                package.PackagePhotoPath = @"~/img/" + ticks + Path.GetExtension(package.PackagePhoto.FileName);

                packageService.Add(package);
                return RedirectToAction(nameof(Index));
            }

            else
            {
                packageService.Add(package);
                return RedirectToAction(nameof(Index));
            }

        }

        public IActionResult CreateCompanyManager()
        {
            IEnumerable<Company> companies = companyService.GetAll();
            ViewBag.companies = companies;
            return View();
        }
        [HttpPost]
        public IActionResult CreateCompanyManager(CompanyManager companyManager)
        {
            companyManagerService.Add(companyManager);
            return RedirectToAction(nameof(ListCompanyManagers));
        }

        public IActionResult ListCompanyManagers()
        {
            IEnumerable<CompanyManager> list = companyManagerService.GetAll();
            return View(list);
        }
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCompany(Company company)
        {
            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Şirket Eklenemedi!";
                return View();
            }
            else
            {

            
            if (company.CompanyLogo != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path = Directory.GetCurrentDirectory() + @"/wwwroot/img/" + ticks + Path.GetExtension(company.CompanyLogo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    company.CompanyLogo.CopyTo(stream);
                }

                company.CompanyLogoPath = @"~/img/" + ticks + Path.GetExtension(company.CompanyLogo.FileName);
                companyService.Add(company);
                return RedirectToAction(nameof(ListCompanies));
            }
                else
                {
                    companyService.Add(company);
                    return RedirectToAction(nameof(ListCompanies));
                }
            }

        }
        public IActionResult ListCompanies()
        {
            IEnumerable<Company> companies = companyService.GetAll();
            return View(companies);
        }  

        // GET: SiteManagerController/Edit/5
        public IActionResult Edit()
        {
            int id = (int)HttpContext.Session.GetInt32("siteManagerId");
            SiteManager siteManager = siteManagerService.GetById(id);
            return View(siteManager);
        }

        // POST: SiteManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SiteManager siteManager)
        {
            SiteManager siteManager1 = siteManagerService.GetById(id);
            siteManager1.Address = siteManager.Address;
            siteManager1.PhoneNumber = siteManager.PhoneNumber;
            siteManager = siteManager1;

            if (!ModelState.IsValid)
            {
                TempData["Validation"] = "Bilgiler Güncellenemedi!";
                return View();
            }
            siteManagerService.Update(siteManager1);
            return RedirectToAction(nameof(Details));           
        }
        
        public IActionResult ChangePassword()
        {
            ChangePasswordVM change = new ChangePasswordVM();
            return View(change);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordVM change)
        {
            var siteManagerPasssword = siteManagerService.GetById((int)HttpContext.Session.GetInt32("siteManagerId")).Password;
            if (ModelState.IsValid)
            {
                if (change.Password == siteManagerPasssword && change.NewPassword == change.NewPasswordControl)
                {
                    bool isDone = siteManagerService.ChangePassword(siteManagerPasssword, change.NewPassword);
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

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("siteManagerId");
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("usersurname");
            return RedirectToAction("Login", "Home", new { area = "" });
        }
    }
}
