using FluentValidation;
using PasswordManagerConsumer.Dtos;
using PasswordManagerConsumer.Models;
using System;

namespace PasswordManagerConsumer.Validations
{
    public class EmployeeValidator : AbstractValidator<UserRegisterDto>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Firstname).NotEmpty().Length(3, 15);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 15);
            RuleFor(x => x.Role).NotEmpty();
            //var conditions = new list<string>() { "admin", "user" };
            //rulefor(x => x.role)
            //      .must(x => conditions.contains(x))
            //      .withmessage("please only use: " + string.join(",", conditions));
            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.ConfirmPassword).NotEmpty();
            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });
            RuleFor(request => request.Password)
        .NotEmpty()
        .MinimumLength(8)
        .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
        .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
        .Matches(@"\d").WithMessage("Password must contain one or more digits.")
        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password must contain one or more special characters.");
        }
       
    }
}
