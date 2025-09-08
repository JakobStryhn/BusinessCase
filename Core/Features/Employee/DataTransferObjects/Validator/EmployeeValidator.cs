using FluentValidation;

namespace Core.Features.Employee.DataTransferObjects.Validator
{
    public class CreateEmployeeRequestValidator : AbstractValidator<CreateEmployeeRequest>
    {
        public CreateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be empty");

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First name length must be less than 100");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("Last name cannot be empty");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last name length must be less than 100");

            RuleFor(x => x.LastName)
                .Must(lastname => !lastname.Any(char.IsWhiteSpace))
                .WithMessage("Last name must not contain white spaces");

            RuleFor(x => x.Birthdate)
                .NotNull()
                .WithMessage("Birthdate cannot be empty or null");

            RuleFor(x => x.Birthdate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(-16)))
                .WithMessage("Employee cannot be below the age of 16 years old");

            // INFO: This rule only works as intended as long as we hard delete employees from our database.
            RuleFor(x => x.Birthdate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(-120)))
                .WithMessage("Employee should not be above the age of 120 years old.");
        }
    }

    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be empty");

            RuleFor(x => x.FirstName)
                .MaximumLength(100)
                .WithMessage("First name length must be less than 100");

            RuleFor(x => x.LastName)
               .NotEmpty()
               .WithMessage("Last name cannot be empty");

            RuleFor(x => x.LastName)
                .MaximumLength(100)
                .WithMessage("Last name length must be less than 100");

            RuleFor(x => x.LastName)
                .Must(lastname => !lastname.Any(char.IsWhiteSpace))
                .WithMessage("Last name must not contain white spaces");

            RuleFor(x => x.Birthdate)
                .NotNull()
                .WithMessage("Birthdate cannot be empty or null");

            RuleFor(x => x.Birthdate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(-16)))
                .WithMessage("Employee cannot be below the age of 16 years old");

            // INFO: This rule only works as intended as long as we hard delete employees from our database.
            RuleFor(x => x.Birthdate)
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today.AddYears(-120)))
                .WithMessage("Employee should not be above the age of 120 years old.");
        }
    }
}
