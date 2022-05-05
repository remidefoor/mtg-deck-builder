using Microsoft.AspNetCore.Components;

namespace Howest.MagicCards.Web.Pages;

public partial class Index
{
    private IEnumerable<CardReadDTO> _cards = new List<CardReadDTO>()
    {
        new CardReadDTO() { Id = 1, Name = "Ancestor's Chosen", ManaCost = "{5}{W}{W}", Power = "4", Toughness = "4", ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=130550&type=card" },
        new CardReadDTO() { Id = 1, Name = "Ancestor's Chosen", ManaCost = "{5}{W}{W}", Power = "4", Toughness = "4", ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=130550&type=card" },
        new CardReadDTO() { Id = 1, Name = "Ancestor's Chosen", ManaCost = "{5}{W}{W}", Power = "4", Toughness = "4", ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=130550&type=card" },
        new CardReadDTO() { Id = 1, Name = "Ancestor's Chosen", ManaCost = "{5}{W}{W}", Power = "4", Toughness = "4", ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=130550&type=card" },
        new CardReadDTO() { Id = 1, Name = "Ancestor's Chosen", ManaCost = "{5}{W}{W}", Power = "4", Toughness = "4", ImageUrl = "http://gatherer.wizards.com/Handlers/Image.ashx?multiverseid=130550&type=card" }
    };

    [Inject]
    public IHttpClientFactory httpClientFactory { get; init; }
}
