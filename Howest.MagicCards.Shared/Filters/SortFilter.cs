namespace Howest.MagicCards.Shared.Filters;

public class SortFilter
{
    const string unordered = "None";
    const string ascending = "Asc";
    const string descending = "Desc";

    public string Order { get; init; } = unordered;

    public bool HasOrder()
    {
        return Order.Equals(ascending, StringComparison.OrdinalIgnoreCase)
            || Order.Equals(descending, StringComparison.OrdinalIgnoreCase);
    }
}
