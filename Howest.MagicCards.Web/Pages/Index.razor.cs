using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class Index
{
    private FilterViewModel _filter;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;
    private IEnumerable<CardReadDTO>? _cards = null;
    private IList<DeckCardReadDetailDTO>? _deck = null;

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    [Inject]
    public ProtectedLocalStorage Storage { get; init; }
    #endregion

    public Index()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _filter = new FilterViewModel();
        _httpClient = HttpClientFactory.CreateClient("WebApi");
        await GetCards();
        _deck = new List<DeckCardReadDetailDTO>();
    }

    private async Task GetCards()
    {
        string queryString = _filter.GetQueryString();
        HttpResponseMessage response = await _httpClient.GetAsync($"Cards{queryString}");
        if (response.IsSuccessStatusCode)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            _cards = JsonSerializer.Deserialize<IEnumerable<CardReadDTO>>(apiResponse, _jsonOptions);
        } else
        {
            _cards = new List<CardReadDTO>();
        }
    }

    /* protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _deck = await GetDeckFromLocalStorage();
        }
    } */

    private async Task<IList<CardReadDTO>> GetDeckFromLocalStorage()
    {
        ProtectedBrowserStorageResult<IList<CardReadDTO>> deck = await Storage.GetAsync<IList<CardReadDTO>>("deck");
        if (deck.Success)
        {
            return deck.Value;
        }
        else
        {
            return new List<CardReadDTO>();
        }
    }

    private void AddCardToDeck(CardReadDTO card)
    {
        if (!DeckIsFull())
        {
            DeckCardReadDetailDTO deckCard = GetDeckCard(card.Id);
            if (deckCard is DeckCardReadDetailDTO)
            {
                deckCard.Amount++;
            } else
            {
                _deck.Add(new DeckCardReadDetailDTO()
                {
                    CardId = card.Id,
                    Name = card.Name,
                    Amount = 1
                });
            }

            SetDeckInLocalStorage();
        }
    }

    private bool DeckIsFull()
    {
        int deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
        return GetDeckCount() == deckSize;
    }

    private int GetDeckCount()
    {
        return _deck.Sum(deckCard => deckCard.Amount);
    }

    private DeckCardReadDetailDTO? GetDeckCard(long cardId)
    {
        return _deck.SingleOrDefault(deckCard => deckCard.CardId == cardId);
    }

    private void SetDeckInLocalStorage()
    {
        _ = Storage.SetAsync("deck", _deck);
    }
}
