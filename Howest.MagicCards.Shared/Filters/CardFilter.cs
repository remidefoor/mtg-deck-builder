namespace Howest.MagicCards.Shared.Filters;

public class CardFilter
{
    private const string defaultFilter = "All"; // TODO move to appsettings

    public string Name { get; init; } = defaultFilter;
    public string Text { get; init; } = defaultFilter;
    public string Set { get; init; } = defaultFilter;
    public string Rarity { get; init; } = defaultFilter;
    public string Artist { get; init; } = defaultFilter;

    public bool HasFilters()
    {
        return !Name.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Text.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Set.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Rarity.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Artist.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase);
    }
}
