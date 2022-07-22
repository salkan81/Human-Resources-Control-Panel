using FluentValidation;
using UIHRMP.Areas.EmployeeArea.Models;

namespace UIHRMP.Areas.CompanyManagerArea.VMValidations
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordVM>
    {
        public ChangePasswordValidation()
        {
            RuleFor(request => request.NewPassword)
            .NotEmpty()
            .MinimumLength(8).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[$@!%*.,:;?+#&'()[=\"€])[A-Za-z\\d$.,;:@!%*?+#&'()[=\"€']{8,}").WithMessage("Şifre en az 1 büyük harf, 1 küçük harf, 1 rakam ve 1 sembol içermelidir.");
            RuleFor(x => x).Must(x => x.NewPassword == x.NewPasswordControl)
                    .WithMessage("Yeni şifre ve tekrarı eşleşmiyor.");

        }
    }
}
