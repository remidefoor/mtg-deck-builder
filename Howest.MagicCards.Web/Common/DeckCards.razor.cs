using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class DeckCards
{
    #region Parameters
    [Parameter]
    public IEnumerable<DeckCardReadDetailDTO> AllDeckCards { get; init; }

    [Parameter]
    public DeckReadDetailDTO Deck { get; init; }
    #endregion
}
