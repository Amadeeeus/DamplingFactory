using FluentValidation;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Validators;

public class RecipeValidator:AbstractValidator<Recipe>
{
    public RecipeValidator()
    {
        RuleFor(x=>x.Name).Length(2,50).NotEmpty().NotNull().WithMessage("Name cannot be empty or greater than 50 characters.");
        RuleFor(x=>x.Ingredients).NotNull().NotEmpty().WithMessage("Ingredients cannot be empty.");
        RuleFor(x=>x.Rating).NotNull().NotEmpty().LessThan(5).WithMessage("Rating cannot be greater than 5 characters.");
    }
}