using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Package : BaseEntity
    {
        public Package()
        {
            Companies = new HashSet<Company>();
        }
        public string? Name { get; set; }
        public double? Cost { get; set; }
        public IFormFile? PackagePhoto { get; set; }
        public string? PackagePhotoPath { get; set; }
        public int? NumberOfUser { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateToUse { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastDateToUse { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime GenerationDate { get; set; }
        public bool IsActive { get; set; } = false;
        public int? PackageTimeInDays { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
