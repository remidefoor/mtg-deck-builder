namespace Howest.MagicCards.Shared.Filters;

public class CardFilter
{
    private const string DefaultFilter = "All"; // TODO move to appsettings

    public string Name { get; init; } = DefaultFilter;
    public string Text { get; init; } = DefaultFilter;
    public string Set { get; init; } = DefaultFilter;
    public string Rarity { get; init; } = DefaultFilter;
    public string Artist { get; init; } = DefaultFilter;

    public bool HasFilters()
    {
        return !Name.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Text.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Set.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Rarity.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)
            || !Artist.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase);
    }
}
