using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class DeckOverview
{
    [Parameter]
    public IList<DeckCardReadDetailDTO> Deck { get; init; }

    [Inject]
    public IMapper Mapper { get; set; }

    private void RemoveCardFromDeck(DeckCardReadDetailDTO deckCard)
    {
        Deck.Remove(deckCard);
    }
}
