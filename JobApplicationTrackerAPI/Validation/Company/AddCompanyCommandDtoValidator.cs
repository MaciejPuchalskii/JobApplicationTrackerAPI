using FluentValidation;
using JobApplicationTrackerAPI.DTOs.Command.Company;

namespace JobApplicationTrackerAPI.Validation.Company
{
    public class AddCompanyCommandDtoValidator : AbstractValidator<AddCompanyCommandDto>
    {
        public AddCompanyCommandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100).WithMessage("Company name must be at most 100 characters long.");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            RuleFor(x => x.ContactPerson)
                .MaximumLength(50).WithMessage("Contact person's name must be at most 50 characters long.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{9,15}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.CompanyURL)
                .Must(url => string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute, out _))
                .WithMessage("Invalid URL format.");
        }
    }
}