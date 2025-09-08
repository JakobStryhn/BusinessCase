using Core.Features.Office.DataTransferObjects;
using FluentValidation;

namespace Api.Features.Office.Models.Validator
{
    public class OfficeCreateRequestValidator : AbstractValidator<CreateOfficeRequest>
    {
        public OfficeCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("First name cannot be empty");

        }
    }
}
