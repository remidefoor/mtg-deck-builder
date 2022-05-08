using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class CardsOverview
{
    [Parameter]
    public IEnumerable<CardReadDTO> Cards { get; init; }
}
