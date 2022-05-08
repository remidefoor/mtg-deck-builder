﻿using System.Reflection;

namespace Howest.MagicCards.Shared.Extensions;

public static class CardExtension
{
    private const string defaultFilter = "All"; // TODO move to appsettings
    private const string ascendingOrder = "Asc"; // TODO move to appsettings
    private const string descendingOrder = "Desc"; // TODO move to appsettings

    public static IQueryable<Card> Filter(this IQueryable<Card> cards, CardFilter cardFilter)
    {
        if (!cardFilter.HasFilters()) return cards.Take(150);

        if (!cardFilter.Name.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Name.Contains(cardFilter.Name));
        if (!cardFilter.Text.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Text.Contains(cardFilter.Text));
        if (!cardFilter.Set.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Set.Name.Contains(cardFilter.Set));
        if (!cardFilter.Rarity.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Rarity.Name.Contains(cardFilter.Rarity));
        if (!cardFilter.Artist.Equals(defaultFilter, StringComparison.OrdinalIgnoreCase)) cards = cards.Where(card => card.Artist.FullName.Contains(cardFilter.Artist));

        return cards;
    }

    public static IQueryable<Card> Sort(this IQueryable<Card> cards, SortFilter sortingFilter)
    {
        if (sortingFilter.Sort.Equals(ascendingOrder, StringComparison.OrdinalIgnoreCase))
        {
            return cards.OrderBy(card => card.Name);
        } else if (sortingFilter.Sort.Equals(descendingOrder, StringComparison.OrdinalIgnoreCase))
        {
            return cards.OrderByDescending(card => card.Name);
        } else
        {
            return cards;
        }
    }
}
