using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardWriteDTO
{
    [Required(ErrorMessage = "Please provide the deck ID of the deck card")]
    public long CardId { get; init; }
    [Required(ErrorMessage = "Pleas provide an amount for the deck card")]
    [Range(1, 60)]
    public int Amount { get; init; } = 1;
}
