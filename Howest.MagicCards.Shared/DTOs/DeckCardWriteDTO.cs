namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardWriteDTO
{
    public long CardId { get; init; }
    public int Amount { get; init; } = 1;
}
