using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartData.Data.ViewModels.Validations
{
    public class LoginModelValidation : AbstractValidator<LoginModel>
    {
        public LoginModelValidation()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.").MinimumLength(3).WithMessage("Username cannot be less that 3 characters.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Password is required.").Length(0, 10).WithMessage("Username is required.");
        }
    }
}