using FluentValidation;
using JobApplicationTrackerAPI.DTOs.Command.Company;

namespace JobApplicationTrackerAPI.Validation.Company
{
    public class UpdateCompanyCommandDtoValidator : AbstractValidator<UpdateCompanyCommandDto>
    {
        public UpdateCompanyCommandDtoValidator()
        {
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