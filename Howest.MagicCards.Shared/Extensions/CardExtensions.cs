using System.Reflection;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtensions
{
    const int defaultCardAmount = 150;
    const string defaultFilter = "All";
    const string ascendingOrder = "Asc";
    const string descendingOrder = "Desc";

    public static IQueryable<Card> Filter(this IQueryable<Card> cards, CardFilter cardFilter)
    {
        if (!cardFilter.HasFilters()) return cards.Take(defaultCardAmount);

        if (!cardFilter.Name.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Name.Contains(cardFilter.Name));
        if (!cardFilter.Text.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Text.Contains(cardFilter.Text));
        if (!cardFilter.Set.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Set.Name.Contains(cardFilter.Set));
        if (!cardFilter.RarityCode.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.RarityCode == cardFilter.RarityCode);
        if (!cardFilter.Artist.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Artist.FullName.Contains(cardFilter.Artist));
        if (!cardFilter.Power.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Power == cardFilter.Power);
        if (!cardFilter.Toughness.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Toughness == cardFilter.Toughness);

        return cards;
    }

    public static IQueryable<Card> Sort(this IQueryable<Card> cards, SortFilter sortingFilter)
    {
        if (sortingFilter.Order.Equals(ascendingOrder, StringComparison.OrdinalIgnoreCase))
        {
            return cards.OrderBy(card => card.Name);
        } else if (sortingFilter.Order.Equals(descendingOrder, StringComparison.OrdinalIgnoreCase))
        {
            return cards.OrderByDescending(card => card.Name);
        } else
        {
            return cards;
        }
    }
}
