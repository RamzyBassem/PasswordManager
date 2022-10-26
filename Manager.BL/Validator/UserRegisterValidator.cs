using FluentValidation;
using Manager.BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.BL.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterDto> 
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty().Length(3, 15);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 15);
            RuleFor(x => x.Role).NotEmpty();
            var conditions = new List<string>() { "Admin", "User"};
            RuleFor(x => x.Role)
              .Must(x => conditions.Contains(x))
              .WithMessage("Please only use: " + String.Join(",", conditions));
            RuleFor(x => x.PhoneNumber).NotEmpty();
          
            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });

        }
    }
}
