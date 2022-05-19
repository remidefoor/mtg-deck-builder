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
    private DeckReadDetailDTO _selectedDeck;
    private IEnumerable<DeckCardReadDetailDTO> _deckCards;

    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    public DeckManager()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    private async Task SetSelectedDeck(DeckReadDetailDTO deck)
    {
        _selectedDeck = deck;
        await GetDeckCards();
    }

    protected override async Task OnInitializedAsync()
    {
        _webApi = HttpClientFactory.CreateClient("WebApi");
        _minimalApi = HttpClientFactory.CreateClient("MinimalApi");
        await GetDecks();
        _selectedDeck = new DeckReadDetailDTO();
        _deckCards = new List<DeckCardReadDetailDTO>();
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

    private async Task GetDeckCards()
    {
        string path = $"Decks/{_selectedDeck.Id}/DeckCards";
        HttpResponseMessage response = await _webApi.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<DeckCardReadDetailDTO>? deckCards = JsonSerializer.Deserialize<IEnumerable<DeckCardReadDetailDTO>>(apiResponse, _jsonOptions);
            _deckCards = deckCards;
        } else
        {
            _deckCards = new List<DeckCardReadDetailDTO>();
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
            if (_selectedDeck.Equals(deck)) ClearDeckCards();
        } else
        {
            Console.WriteLine("failure");
        }
    }

    private void ClearDeckCards()
    {
        _selectedDeck = new DeckReadDetailDTO();
        _deckCards = new List<DeckCardReadDetailDTO>();
    }
}
