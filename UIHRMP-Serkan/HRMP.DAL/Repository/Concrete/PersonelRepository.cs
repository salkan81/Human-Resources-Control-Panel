using HRMP.CORE.Entities;
using HRMP.DAL.Database;
using HRMP.DAL.Repository.Abstract;
using System.Collections;

namespace HRMP.DAL.Repository.Concrete
{
    public class PersonelRepository : GenericRepoistory<Employee>, IPersonelRepository
    {
        private readonly ApplicationDbContext context;

        public PersonelRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public string CreateEmployeeMail(int id, string name, string surrname)
        {
            string manager_mail = context.CompanyManagers.Where(x => x.Id == id).Select(x => x.Email).FirstOrDefault();
            string mailDomain = manager_mail.Split('@').Last();
            string personel_mail = name.ToLower() + "." + surrname.ToLower() + "@" + mailDomain;
            return personel_mail;
        }

        public Employee GetByEmailAndPassword(string email, string password)
        {
            return context.Employees.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
        }

        public IEnumerable<Employee> ListEmployeesByManagerID(int Id)
        {
            return context.Employees.Where(x => x.CompanyManagerID == Id).OrderBy(x => x.Name).ToList();
        }

        public string CreateRandomPassword()
        {
            Random rnd = new Random();
            ArrayList arraychar = new ArrayList();
            for (int i = 0; i < 2; i++)
            {
                int number = rnd.Next(10);
                char LowerChar = Convert.ToChar(rnd.Next(97, 123));
                char UpperChar = Convert.ToChar(rnd.Next(65, 91));
                char AlphaNumeric = Convert.ToChar(rnd.Next(33, 48));
                arraychar.Add(number);
                arraychar.Add(LowerChar);
                arraychar.Add(UpperChar);
                arraychar.Add(AlphaNumeric);
            }
            string password = "";
            foreach (var item in arraychar)
            {
                password += item;
            }
            return password;
        }
    }
}
