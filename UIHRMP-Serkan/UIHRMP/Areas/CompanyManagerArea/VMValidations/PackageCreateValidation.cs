using FluentValidation;
using HRMP.CORE.Entities;

namespace UIHRMP.Areas.CompanyManagerArea.VMValidations
{
    public class PackageCreateValidation : AbstractValidator<Package>
    {
        public PackageCreateValidation()
        {
            RuleFor(a => a.Name).NotNull().WithMessage("Paket Adı kısmı boş olamaz.").MinimumLength(2).WithMessage("Şirket Adı en az 2 karakter olmalıdır.").Matches("^[A-Za-z]+$").WithMessage("Sadece harf giriniz.");
            RuleFor(x => x.NumberOfUser).GreaterThan(-1).WithMessage("Kulanıcı sayısı 0 yada 0'dan büyük olmalıdır.");
            RuleFor(x => x.PackageTimeInDays).GreaterThan(0).WithMessage("Kullanım gün sayısı 0'dan büyük olmalıdır.");
        }
    }
}
