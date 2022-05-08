using System.Reflection;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtension
{
    private const string DefaultFilter = "All"; // TODO move to appsettings

    public static IQueryable<Card> Filter(this IQueryable<Card> cards, CardFilter cardFilter)
    {
        if (!cardFilter.HasFilters()) return cards.Take(150);

        if (!cardFilter.Name.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Name.Contains(cardFilter.Name));
        if (!cardFilter.Text.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Text.Contains(cardFilter.Text));
        if (!cardFilter.Set.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Set.Name.Contains(cardFilter.Set));
        if (!cardFilter.Rarity.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Rarity.Name.Contains(cardFilter.Rarity));
        if (!cardFilter.Artist.Equals(DefaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Artist.FullName.Contains(cardFilter.Artist));

        return cards;
    }

    public static IQueryable<Card> Sort(this IQueryable<Card> cards, SortingFilter sortingFilter)
    {
        if (sortingFilter.Sort == "Asc")
        {
            return cards.OrderBy(card => card.Name);
        } else if (sortingFilter.Sort == "Desc")
        {
            return cards.OrderByDescending(card => card.Name);
        } else
        {
            return cards;
        }
    }
}
