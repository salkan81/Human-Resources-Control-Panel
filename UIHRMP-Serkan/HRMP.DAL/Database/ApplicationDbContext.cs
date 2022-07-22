using HRMP.CORE.Entities;
using HRMP.CORE.Enum;
using Microsoft.EntityFrameworkCore;


namespace HRMP.DAL.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {


        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-MLA1I95\\SQLEXPRESS;Database=HRMPProject;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Expense>().Ignore(x => x.ExpenseDocument);
            modelBuilder.Entity<Employee>().Ignore(x => x.EmployeePhoto);
            modelBuilder.Entity<CompanyManager>().Ignore(x => x.ManagerPhoto);
            modelBuilder.Entity<SiteManager>().Ignore(x => x.ManagerPhoto);
            modelBuilder.Entity<Company>().Ignore(x => x.CompanyLogo);
            modelBuilder.Entity<Package>().Ignore(x => x.PackagePhoto);
            modelBuilder.Entity<CompanyManager>().HasData(new CompanyManager { Id = 1, Address = "Üsküdar", BirthDate = DateTime.Now.AddYears(-20), Email = "manager@etik.com", Gender = Gender.Erkek, Name = "İsmail", Surname = "Hakkı", PhoneNumber = "05422097845", TCNo = "22345675506", IsActive = true, Title = "Manager", Password = "Admin12!" });
            modelBuilder.Entity<Employee>().HasData(new Employee { Id = 1, Address = "Maltepe", BirthDate = DateTime.Now.AddYears(-30), Email = "employee@etik.com", Gender = Gender.Erkek, Name = "Enes", Surname = "Toprak", PhoneNumber = "05345550987", TCNo = "22345115506", IsActive = true, Title = "Employee", Password = ".Employee48", HiredDate = DateTime.Now.AddMonths(-7), ActivationCode = "erty", CompanyManagerID = 1 });
            modelBuilder.Entity<SiteManager>().HasData(new SiteManager { Address = "Kartepe", BirthDate = DateTime.Now.AddYears(-33), Email = "emre@sitemanager.com", Gender = Gender.Erkek, Id = 1, Name = "Emre", Surname = "Özpınar", Password = "Emre_89", PhoneNumber = "05369455521", Title = "Site Manager", TCNo = "22557788833" });
        }

        public DbSet<CompanyManager> CompanyManagers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SiteManager> SiteManagers { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<AdvancedPayment> AdvancedPayments { get; set; }
        public DbSet<Mesaj> Mesajlar { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Package> Packages { get; set; }
    }
}
