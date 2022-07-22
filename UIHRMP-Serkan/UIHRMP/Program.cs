using FluentValidation.AspNetCore;
using HRMP.BLL.Abstract;
using HRMP.BLL.Concrete;
using HRMP.BLL.Services.Abstract;
using HRMP.BLL.Services.Concrete;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using HRMP.DAL.Repository.Concrete;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using UIHRMP.Mailing;
using UIHRMP.Mailing.FakeMailSender;

var builder = WebApplication.CreateBuilder(args);

var mvcBuilder = builder.Services.AddRazorPages();
builder.Services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
{
    var libraryPath = Path.GetFullPath(
        Path.Combine(builder.Environment.ContentRootPath, "..", "UIHRMP", "HRMP.BLL", "HRMP.CORE", "HRMP.DAL"));

    options.FileProviders.Add(new PhysicalFileProvider(libraryPath));
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(5000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();//Runtimeda derleme iþlemi yapmaya yarar.Nuget paketide eklenmeli.
builder.Services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
builder.Services.AddDbContext<ApplicationDbContext>();





/*---------------Mail--------------------*/
//builder.Services.AddScoped<IMailService, SmtpMailService>();
builder.Services.AddScoped<IMailService, FakeMailSender>();

/*---------------Service--------------------*/
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<ICompanyManagerService, CompanyManagerService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ISiteManagerService, SiteManagerService>();
builder.Services.AddScoped<IAdvancePaymentsService, AdvancePaymentsService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IMesajService, MesajService>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
/*-------------------------------------------*/



/*----------------DAL------------------*/
builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepoistory<>));
builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
builder.Services.AddScoped<IPersonelRepository, PersonelRepository>();
builder.Services.AddScoped<ISiteManagerRepository, SiteManagerRepository>();
builder.Services.AddScoped<IAdvancePaymentsRepository, AdvancePaymentsRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<IMesajRepository, MesajRepository>();
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
/*----------------------------------*/


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();

app.MapControllerRoute(
    name: "CompanyManager",
    pattern: "{area:exists}/{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "SiteManager",
    pattern: "{area:exists}/{controller}/{action}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");



app.Run();
