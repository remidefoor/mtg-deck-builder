namespace Howest.MagicCards.Shared.DTOs;

public record DeckCardReadDetailDTO
{
    public long CardId { get; init; }
    public string Name { get; init; }
    public int Amount { get; set; } = 1;
}
