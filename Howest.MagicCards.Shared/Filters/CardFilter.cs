namespace Howest.MagicCards.Shared.Filters;

public class CardFilter
{
    const string defaultFilter = "All";

    public string Name { get; init; } = defaultFilter;
    public string Text { get; init; } = defaultFilter;
    public string Set { get; init; } = defaultFilter;
    public string RarityCode { get; init; } = defaultFilter;
    public string Artist { get; init; } = defaultFilter;
    public string Power { get; init; } = defaultFilter;
    public string Toughness { get; init; } = defaultFilter;

    public bool HasFilters()
    {
        return !Name.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Text.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Set.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !RarityCode.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Artist.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Power.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Toughness.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase);
    }
}
