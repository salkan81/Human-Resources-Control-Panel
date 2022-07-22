using FluentValidation;
using HRMP.CORE.Entities;
using System.Text.RegularExpressions;

namespace HRMP.BLL.ValidationRules
{
    public class CompanyManagerValidation : AbstractValidator<CompanyManager>
    {
        public CompanyManagerValidation()
        {
            RuleFor(a => a.Name).NotNull().WithMessage("Ad kısmı boş olamaz.").MinimumLength(2).WithMessage("Ad en az 2 karakter olmalıdır.").Matches("^[A-Za-z]+$").WithMessage("Sadece harf giriniz.");
            RuleFor(a => a.Surname).NotNull().WithMessage("Soyad kısmı boş olamaz.").MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalıdır.").Matches("^[A-Za-z]+$").WithMessage("Sadece harf giriniz.");
            RuleFor(p => p.Gender).NotNull().WithMessage("Cinsiyet kısmı boş olamaz.");
            RuleFor(a => a.Title).NotNull().WithMessage("Ünvan kısmı boş olamaz.");
            RuleFor(p => p.PhoneNumber).NotNull().WithMessage("Lütfen telefon numarası girin.").Matches(new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}")).WithMessage("Telefon numarası 5XX-XXX-XXXX şeklinde olmalı ve rakamlardan oluşmalıdır.");
            RuleFor(a => a.Address).NotEmpty().WithMessage("Adres kısmı boş bırakılamaz.").MinimumLength(25).WithMessage("Adres 25 karakterden az olamaz").MaximumLength(300).WithMessage("Adres 300 karakterden fazla olamaz.");
            RuleFor(t => t.TCNo).NotNull().WithMessage("TCNo boş olamaz.").Length(11).WithMessage("TCNo 11 karakter olmalıdır.").Matches("^[0-9]+$").WithMessage("Sadece rakam giriniz.");
            RuleFor(p => p.BirthDate).LessThan(DateTime.Now.AddYears(-18).Date).WithMessage("18 yaşından küçüklerin kaydı yapılamaz.").GreaterThan(DateTime.Now.AddYears(-65).Date).WithMessage("65 yaşından büyüklerin kaydı yapılamaz.");
        }
    }
}
