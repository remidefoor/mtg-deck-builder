namespace Howest.MagicCards.Shared.DTOs;

public record DeckWriteDTO
{
    public IEnumerable<long> Cards { get; init; }
}
