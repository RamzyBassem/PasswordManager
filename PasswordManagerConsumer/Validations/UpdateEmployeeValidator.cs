using FluentValidation;
using PasswordManagerConsumer.Models;

namespace PasswordManagerConsumer.Validations
{
    public class UpdateEmployeeValidator : AbstractValidator<Employee>
    {
        public UpdateEmployeeValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(3, 15);
            RuleFor(x => x.LastName).NotEmpty().Length(3, 15);
            RuleFor(x => x.PhoneNumber).NotEmpty();

            RuleFor(x => x.role).NotEmpty();
         
            
        }
    }
}
