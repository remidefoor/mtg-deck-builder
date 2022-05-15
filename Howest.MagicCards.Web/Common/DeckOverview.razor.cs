using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Howest.MagicCards.Web.Common;

public partial class DeckOverview
{
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    [Parameter]
    public IList<DeckCardReadDetailDTO> Deck { get; init; }

    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; set; }

    public DeckOverview()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = HttpClientFactory.CreateClient("MinimalApi");
    }

    private void RemoveCardFromDeck(DeckCardReadDetailDTO deckCard)
    {
        if (deckCard.Amount > 1)
        {
            deckCard.Amount--;
        } else
        {
            Deck.Remove(deckCard);
        }
    }

    private void SaveDeck()
    {
        int deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
        if (GetDeckCount() == deckSize)
        {

        }
    }

    private int GetDeckCount()
    {
        return Deck.Sum(deckCard => deckCard.Amount);
    }

    private async Task<DeckReadDetailDTO?> PostDeck(DeckWriteDTO deck)
    {
        HttpContent body = new StringContent(JsonSerializer.Serialize(deck), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync("Decks", body);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            string apiResponse  = await response.Content.ReadAsStringAsync();
            DeckReadDetailDTO createdDeck = JsonSerializer.Deserialize<DeckReadDetailDTO>(apiResponse, _jsonOptions);
            return createdDeck;
        } else
        {
            return default;
        }
    }

    public async Task<DeckCardReadDTO?> PostDeckCard(long deckId, DeckCardWriteDTO deckCard)
    {
        HttpContent body = new StringContent(JsonSerializer.Serialize(deckCard), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync($"Decks/{deckId}/DeckCards", body);
        if (response.StatusCode == HttpStatusCode.Created)
        {
            string apiResponse = await response.Content?.ReadAsStringAsync();
            DeckCardReadDTO createdDeckCard = JsonSerializer.Deserialize<DeckCardReadDTO>(apiResponse, _jsonOptions);
            return createdDeckCard;
        } else
        {
            return default;
        }
    }
}
