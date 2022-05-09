using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class Index
{
    private FilterViewModel _filter;
    private IEnumerable<CardReadDTO>? _cards = null;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    public Index()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _filter = new FilterViewModel();
        _httpClient = HttpClientFactory.CreateClient("CardAPI");
        await GetCards();
    }

    private async Task GetCards()
    {
        string queryString = _filter.GetQueryString();
        HttpResponseMessage response = await _httpClient.GetAsync($"Cards{queryString}");
        string apiResponse = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            _cards = JsonSerializer.Deserialize<IEnumerable<CardReadDTO>>(apiResponse, _jsonOptions);
        } else
        {
            _cards = new List<CardReadDTO>();
        }
    }
}
