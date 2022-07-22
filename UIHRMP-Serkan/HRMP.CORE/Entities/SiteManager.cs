using HRMP.CORE.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class SiteManager : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Title { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? TCNo { get; set; }
        public IFormFile? ManagerPhoto { get; set; }
        public string PhotoPath { get; set; } = "~/img/default.jpg";
    }
}
