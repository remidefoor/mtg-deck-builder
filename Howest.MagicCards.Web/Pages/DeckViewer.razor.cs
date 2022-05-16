using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class DeckViewer
{
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    private IEnumerable<DeckReadDetailDTO> _decks = null;

    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; init; }

    public DeckViewer()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = HttpClientFactory.CreateClient("WebApi");
        await GetDecks();
    }

    private async Task GetDecks()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("Decks");
        if (response.IsSuccessStatusCode)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            IEnumerable<DeckReadDetailDTO>? decks = JsonSerializer.Deserialize<IEnumerable<DeckReadDetailDTO>>(apiResponse, _jsonOptions);
            _decks = decks;
        } else
        {
            _decks = new List<DeckReadDetailDTO>();
        }
    }
}
