using HRMP.CORE.Entities;
using HRMP.CORE.Enum;
using HRMP.DAL.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UIHRMP.Areas.EmployeeArea.Models;
using UIHRMP.FileManger;

namespace UIHRMP.Areas.EmployeeArea.Controllers
{
    [Area("EmployeeArea")]
    public class ExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExpensesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Expenses
        public async Task<IActionResult> Index()
        {
            ViewData["ExpenseTypeID"] = new SelectList(_context.Expenses.OrderBy(x => x.ExpenseType), "ID", "ExpenseTypeName");
            var databaseContext = _context.Expenses.Include(e => e.Employee);
            var vm = new IndexViewModel()
            {
                Expenses = await databaseContext.ToListAsync()
            };
            return View(vm);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        [HttpGet("[controller]/[action]/{Id}")]
        public IActionResult Create(string Id)
        {
            ViewData["ExpenseTypeID"] = new SelectList(_context.Expenses.OrderBy(x => x.ExpenseType), "ID", "ExpenseTypeName");
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndexViewModel expense)
        {
            if (ModelState.IsValid)
            {
                var enums = ConfirmStatus.Pending;
                var newexpense = new Expense
                {
                    ExpenseDocumentPath = expense.CheckDocument.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment),
                    ConfirmStatus = enums,
                    Id = Convert.ToInt32(HttpContext.Session.GetString("userId")),
                    ExpenseType = (ExpenseType)expense.ExpenseTypeID,
                    Amount = expense.Amount,
                    DateOfExpense = expense.DateOfExpense,
                    Description = expense.Explanation,
                };
                _context.Add(newexpense);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }



            ViewData["ExpenseTypeID"] = new SelectList(_context.Expenses.OrderBy(x => x.ExpenseType), "ID", "ExpenseTypeName", expense.ExpenseTypeID);
            return View(expense);
        }

        //// GET: Expenses/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {

        //        return NotFound();
        //    }

        //    var expense = await _context.Expenses.FindAsync(id);
        //    ExpenseEditVM editVM = new()
        //    {
        //        ID = expense.Id,
        //        Amount = expense.Amount,
        //        CheckDocumentName = expense.CheckDocument,
        //        DateOfExpense = expense.DateOfExpense,
        //        ExpenseTypeID = expense.ExpenseTypeID,
        //        Explanation = expense.Explanation,

        //    };
        //    if (expense == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ExpenseTypeID"] = new SelectList(_context.ExpenseTypes, "ID", "ExpenseTypeName", expense.ExpenseTypeID);

        //    return View(editVM);
        //}

        //// POST: Expenses/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, ExpenseEditVM editVM)
        //{
        //    var newExpense = await _context.Expenses.FindAsync(id);
        //    if (id != editVM.ID)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        newExpense.Amount = editVM.Amount;
        //        newExpense.DateOfExpense = editVM.DateOfExpense;
        //        newExpense.ExpenseTypeID = editVM.ExpenseTypeID;
        //        newExpense.Explanation = editVM.Explanation;

        //        if (editVM.CheckDocument is not null)
        //        {
        //            newExpense.CheckDocument = editVM.CheckDocument.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);
        //            FileManager.RemoveImageFromDisk(editVM.CheckDocumentName, _webHostEnvironment);
        //        }
        //        _context.SaveChanges();
        //        TempData["message"] = "Harcama Güncellendi";
        //        return RedirectToAction("Index", "Expenses", new { Id = HttpContext.Session.GetString("userId") });


        //    }

        //    ViewData["ExpenseTypeID"] = new SelectList(_context.ExpenseTypes, "ID", "ExpenseTypeName");


        //    return View(editVM);
        //}

        //// GET: Expenses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var deleted = _context.Expenses.FirstOrDefault(x => x.ID == id);

        //    if (deleted == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Expenses.Remove(deleted);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index", "Expenses", new { Id = HttpContext.Session.GetString("userId") });
        //}


        private bool ExpenseExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }

    }
}
