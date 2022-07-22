using FluentValidation;
using HRMP.CORE.Entities;

namespace UIHRMP.Areas.CompanyManagerArea.VMValidations
{
    public class CompanyCreateValidation : AbstractValidator<Company>
    {
        public CompanyCreateValidation()
        {
            RuleFor(a => a.CompanyName).NotNull().WithMessage("Şirket Adı kısmı boş olamaz.").MinimumLength(2).WithMessage("Şirket Adı en az 2 karakter olmalıdır.").Matches("^[A-Za-z]+$").WithMessage("Sadece harf giriniz.");
            RuleFor(a => a.Address).NotEmpty().WithMessage("Adres kısmı boş bırakılamaz.").MinimumLength(25).WithMessage("Adres 25 karakterden kısa olamaz.").MaximumLength(300).WithMessage("Adres 300 karakterden uzun olamaz.");
            RuleFor(x => x.FoundationYear).GreaterThan(DateTime.Now.Date.AddYears(-100)).WithMessage("Kuruluş tarihi günümüzden en fazla 100 yıl geride olabilir.").LessThan(DateTime.Now.Date).WithMessage("Kuruluş tarihi günümüzden ileri bir tarih olamaz.");
            RuleFor(x => x.EmailExtension).Matches("^\\@\\S+\\.\\S+$").WithMessage("Uzantı @example.com şeklinde olmalıdır.");
            RuleFor(x => x.NumberOfEmployees).GreaterThan(0).WithMessage("Çalışan sayısı 0'dan büyük olmalıdır.");
        }
    }
}
