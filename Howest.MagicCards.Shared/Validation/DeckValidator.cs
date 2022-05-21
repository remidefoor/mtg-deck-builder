using FluentValidation;

namespace Howest.MagicCards.Shared.Validation;

public class DeckValidator : AbstractValidator<DeckWriteDTO>
{
    public DeckValidator()
    {
        RuleFor(deck => deck.Name)
            .NotNull()
            .WithMessage("Please provide a name for the deck")
            .NotEmpty()
            .WithMessage("Please provide a name for the deck")
            .Length(1, 255)
            .WithMessage("The name of a deck may at most be 255 characters long");
    }
}
