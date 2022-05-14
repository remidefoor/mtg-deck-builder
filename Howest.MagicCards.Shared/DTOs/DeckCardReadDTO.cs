namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardReadDTO
{
    public long CardId { get; init; }
    public int Amount { get; init; }
}
