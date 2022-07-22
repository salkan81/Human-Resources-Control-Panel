using HRMP.CORE.Enum;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HRMP.CORE.Entities
{
    public class Expense : BaseEntity
    {
        public double? Amount { get; set; }
        public string? ExpenseContent { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfExpense { get; set; }
        public ConfirmStatus ConfirmStatus { get; set; } = ConfirmStatus.Pending;
        public ExpenseType ExpenseType { get; set; }
        public string? ExpenseDocumentPath { get; set; }
        public IFormFile ExpenseDocument { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int? CompanyManagerId { get; set; }
        public CompanyManager CompanyManager { get; set; }
        public bool IsRead { get; set; } = false;
    }
}
