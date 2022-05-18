using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class DeckManager
{
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _webApi;
    private HttpClient _minimalApi;

    private IList<DeckReadDetailDTO> _decks = null;

    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    public DeckManager()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _webApi = HttpClientFactory.CreateClient("WebApi");
        _minimalApi = HttpClientFactory.CreateClient("MinimalApi");
        await GetDecks();
    }

    private async Task GetDecks()
    {
        HttpResponseMessage response = await _webApi.GetAsync("Decks");
        if (response.IsSuccessStatusCode)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            IList<DeckReadDetailDTO>? decks = JsonSerializer.Deserialize<IList<DeckReadDetailDTO>>(apiResponse, _jsonOptions);
            _decks = decks;
        } else
        {
            _decks = new List<DeckReadDetailDTO>();
        }
    }

    private async Task DeleteDeck(DeckReadDetailDTO deck)
    {
        Console.WriteLine("before delete deck");
        HttpResponseMessage response = await _minimalApi.DeleteAsync($"Decks/{deck.Id}");
        Console.WriteLine("after delete deck");
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("success");
            _decks.Remove(deck);
        } else
        {
            Console.WriteLine("failure");
        }
    }
}
