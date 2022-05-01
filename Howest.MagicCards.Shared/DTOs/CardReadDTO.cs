namespace Howest.MagicCards.Shared.DTOs;

public record CardReadDTO
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string ManaCost { get; init; }
    public string Power { get; init; }
    public string Toughness { get; init; }
    public string ImageUrl { get; init; }
}
