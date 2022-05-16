using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class Decks
{
    [Parameter]
    public IEnumerable<DeckReadDetailDTO> SavedDecks { get; init; }
}
