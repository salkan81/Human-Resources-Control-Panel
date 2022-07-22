using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        {
            CompanyManagers = new HashSet<CompanyManager>();
        }
        public string? CompanyName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FoundationYear { get; set; }
        public IFormFile CompanyLogo { get; set; }
        public string? CompanyLogoPath { get; set; } = "~/img/default.jpg";
        public int? NumberOfEmployees { get; set; }
        public string? EmailExtension { get; set; }
        public string? Address { get; set; }
        public int? PackageId { get; set; }
        public Package? Package { get; set; }
        public ICollection<CompanyManager> CompanyManagers { get; set; }

    }
}
