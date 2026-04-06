using FluentValidation;
using MarothiMohale.Portfolio.Api.Application.DTOs.Requests;

namespace MarothiMohale.Portfolio.Api.Application.Validators;

public class ContactRequestValidator : AbstractValidator<ContactRequest>
{
    public ContactRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(120).WithMessage("Name must not exceed 120 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please provide a valid email address")
            .MaximumLength(150).WithMessage("Email must not exceed 150 characters");

        RuleFor(x => x.Phone)
            .MaximumLength(40).WithMessage("Phone must not exceed 40 characters")
            .When(x => !string.IsNullOrWhiteSpace(x.Phone));

        RuleFor(x => x.Subject)
            .NotEmpty().WithMessage("Subject is required")
            .MaximumLength(160).WithMessage("Subject must not exceed 160 characters");

        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Message is required")
            .MinimumLength(10).WithMessage("Message must be at least 10 characters long");
    }
}