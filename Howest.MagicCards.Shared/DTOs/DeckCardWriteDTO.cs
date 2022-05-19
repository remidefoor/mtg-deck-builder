using System.ComponentModel.DataAnnotations;

namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardWriteDTO
{
    public long CardId { get; init; }
    [Range(1, 60)]
    public int Amount { get; init; } = 1;
}
