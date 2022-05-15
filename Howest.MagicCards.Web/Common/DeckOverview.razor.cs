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
    public IList<DeckCardReadDetailDTO> DeckCards { get; init; }

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    public IMapper Mapper { get; set; }
    #endregion

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
            DeckCards.Remove(deckCard);
        }
    }

    public async Task SaveDeck()
    {
        int deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
        if (GetDeckCount() == deckSize)
        {
            if (await PostDeck(new DeckWriteDTO()) is DeckReadDetailDTO createdDeck)
            {
                await PostDeckCards(createdDeck.Id);
            }
        }
    }

    private int GetDeckCount()
    {
        return DeckCards.Sum(deckCard => deckCard.Amount);
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

    private async Task PostDeckCards(long deckId)
    {
        foreach (DeckCardReadDetailDTO deckCard in DeckCards)
        {
            DeckCardReadDTO? createdDeckCard = await PostDeckCard(deckId, Mapper.Map<DeckCardWriteDTO>(deckCard));
            if (!(createdDeckCard is DeckCardReadDTO))
            {
                return;
            }
        }
        ClearDeckCards();
    }

    private async Task<DeckCardReadDTO?> PostDeckCard(long deckId, DeckCardWriteDTO deckCard)
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

    private void ClearDeckCards()
    {
        DeckCards.Clear();
    }
}
