using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Common;

public partial class CardOverview
{
    [Parameter]
    public IEnumerable<CardReadDTO> Cards { get; init; }
}
