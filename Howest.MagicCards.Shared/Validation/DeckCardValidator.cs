using FluentValidation;

namespace Howest.MagicCards.Shared.Validation;

public class DeckCardValidator : AbstractValidator<DeckCardWriteDTO>
{
    public DeckCardValidator()
    {
        RuleFor(deckCard => deckCard.Amount)
            .NotNull()
            .WithMessage("Pleas provide an amount for the deck card")
            .InclusiveBetween(1, 60)
            .WithMessage("Minimum 1 deck card must be provided, but maximum 60");
    }
}
