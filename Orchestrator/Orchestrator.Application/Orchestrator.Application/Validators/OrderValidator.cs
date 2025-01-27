using FluentValidation;
using Orchestrator.Domain.Entities;

namespace Orchestrator.Application.Validators;

public class OrderValidator:AbstractValidator<UserChoice>
{
    public OrderValidator()
    {
        RuleFor(x => x.PortionsCount).NotEmpty().NotNull().WithMessage("Portions count cannot be empty");
        RuleFor(x=>x.RecipeId).NotEmpty().NotNull().WithMessage("Recipe id cannot be empty");
    }

}