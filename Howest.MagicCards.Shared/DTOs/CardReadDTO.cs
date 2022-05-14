namespace Howest.MagicCards.Shared.DTOs;

public record CardReadDTO
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string ImageUrl { get; init; }
}
