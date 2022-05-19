using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Howest.MagicCards.Web.Common;

public partial class Deck
{
    private string _message = string.Empty;
    private DeckWriteDTO _deck;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    [Parameter]
    public IList<DeckCardReadDetailDTO> DeckCards { get; init; }

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    public IMapper Mapper { get; set; }

    [Inject]

    public ProtectedLocalStorage Storage { get; init; }
    #endregion

    public Deck()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _deck = new DeckWriteDTO();
        _httpClient = HttpClientFactory.CreateClient("MinimalApi");
    }

    private async Task RemoveCardFromDeck(DeckCardReadDetailDTO deckCard)
    {
        if (deckCard.Amount > 1)
        {
            deckCard.Amount--;
        } else
        {
            DeckCards.Remove(deckCard);
        }
        await SetDeckInLocalStorage();
    }

    private async Task SetDeckInLocalStorage()
    {
        await Storage.SetAsync("deck", DeckCards);
    }

    private async Task SaveDeck()
    {
        int deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
        if (GetDeckCount() == deckSize)
        {
            if (await PostDeck(_deck) is DeckReadDetailDTO createdDeck)
            {
                await PostDeckCards(createdDeck.Id);
                if (DeckCards.Count > 0)
                {
                    SetMessage("Error: Failed to save the deck, please try again");
                }
            }
        } else
        {
            Console.WriteLine("error set");
            SetMessage($"Error: A deck must contain {deckSize} cards");
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
        SetMessage("The deck was successfully saved");
        await ClearDeck();
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

    private async Task ClearDeck()
    {
        _deck = new DeckWriteDTO();
        DeckCards.Clear();
        await SetDeckInLocalStorage();
    }

    private void SetMessage(string message)
    {
        _message = message;
    }
}
