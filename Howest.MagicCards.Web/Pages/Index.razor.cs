using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class Index
{
    private string _defaultFilter = Configuration.GetAppSetting("QueryParamDefaults:Filter");
    private string _defaultSort = Configuration.GetAppSetting("QueryParamDefaults:Sort");
    private FilterViewModel _filter;

    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    private IEnumerable<RarityReadDTO> _rarities;
    private IEnumerable<CardReadDTO>? _cards = null;
    private IList<DeckCardReadDetailDTO>? _deckCards = null;

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    [Inject]
    public IMapper Mapper { get; set; }

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
        await GetRarities();
        await GetCards();
    }

    private async Task GetRarities()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("Rarities");
        if (response.IsSuccessStatusCode)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<RarityReadDTO>? rarities = JsonSerializer.Deserialize<IEnumerable<RarityReadDTO>>(apiResponse, _jsonOptions);
            _rarities = rarities;
        } else
        {
            _rarities = new List<RarityReadDTO>();
        }
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _deckCards = await GetDeckFromLocalStorage();
            StateHasChanged();
        }
    }

    private async Task<IList<DeckCardReadDetailDTO>> GetDeckFromLocalStorage()
    {
        ProtectedBrowserStorageResult<IList<DeckCardReadDetailDTO>> deckCards = await Storage.GetAsync<IList<DeckCardReadDetailDTO>>("deck");
        if (deckCards.Success)
        {
            return deckCards.Value;
        }
        else
        {
            return new List<DeckCardReadDetailDTO>();
        }
    }

    private async Task AddCardToDeck(CardReadDTO card)
    {
        if (!DeckIsFull())
        {
            DeckCardReadDetailDTO? deckCard = GetDeckCard(card.Id);
            if (deckCard is DeckCardReadDetailDTO)
            {
                deckCard.Amount++;
            } else
            {
                _deckCards.Add(Mapper.Map<DeckCardReadDetailDTO>(card));
            }

            await SetDeckInLocalStorage();
        }
    }

    private bool DeckIsFull()
    {
        int deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
        return GetDeckCount() == deckSize;
    }

    private int GetDeckCount()
    {
        return _deckCards.Sum(deckCard => deckCard.Amount);
    }

    private DeckCardReadDetailDTO? GetDeckCard(long cardId)
    {
        return _deckCards.SingleOrDefault(deckCard => deckCard.CardId == cardId);
    }

    private async Task SetDeckInLocalStorage()
    {
        await Storage.SetAsync("deck", _deckCards);
    }
}
