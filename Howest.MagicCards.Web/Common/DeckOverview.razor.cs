using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class DeckOverview
{
    [Parameter]
    public IList<CardReadDTO> Deck { get; init; }

    private void RemoveCardFromDeck(CardReadDTO card)
    {
        Deck.Remove(card);
    }
}
