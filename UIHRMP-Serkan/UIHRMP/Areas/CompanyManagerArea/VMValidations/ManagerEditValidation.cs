using FluentValidation;
using HRMP.CORE.Entities;

namespace UIHRMP.Areas.CompanyManagerArea.VMValidations
{
    public class ManagerEditValidation : AbstractValidator<CompanyManager>
    {
        public ManagerEditValidation()
        {
            RuleFor(p => p.PhoneNumber).NotNull().WithMessage("Lütfen telefon numarası girin.");
            RuleFor(a => a.Address).NotEmpty().WithMessage("Adres kısmı boş bırakılamaz.").MinimumLength(10).WithMessage("Adres 10 karakterden az olamaz").MaximumLength(300).WithMessage("Adres 300 karakterden fazla olamaz.");
        }
    }
}
