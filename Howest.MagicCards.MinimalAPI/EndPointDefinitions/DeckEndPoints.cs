using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions;

public class DeckEndPoints : IEndpointDefinition
{
    private readonly string _urlPrefix;

    public DeckEndPoints()
    {
        _urlPrefix = Configuration.GetAppSetting("UrlPrefix");
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost($"{_urlPrefix}/Decks", PostDeck)
            .Accepts<DeckWriteDTO>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete($"{_urlPrefix}/Decks/{{deckId:long}}", DeleteDeck)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddDbContext<mtg_v1Context>(options =>
            options.UseSqlServer(Configuration.GetAppSetting("ConnectionStrings:MgtV1Db")));
        services.AddScoped<IDeckRepository, SqlDeckRepository>();
        services.AddAutoMapper(new System.Type[]
            {
                typeof(Shared.Mappings.DecksProfile)
            });
    }

    private async Task<IResult> PostDeck(IDeckRepository deckRepository, IMapper mapper, DeckWriteDTO deckDTO)
    {
        try
        {
            return (await deckRepository.CreateDeckAsync(mapper.Map<Deck>(deckDTO)) is Deck createdDeck)
                ? Results.Created($"https://localhost:7103{_urlPrefix}/Decks/{createdDeck.Id}", mapper.Map<DeckReadDetailDTO>(createdDeck))
                : Results.BadRequest();
        }
        catch (Exception ex)
        {
            return Results.BadRequest();
        }
    }

    private async Task<IResult> DeleteDeck(IDeckRepository deckRepository, IMapper mapper, long deckId)
    {
        return (await deckRepository.ReadDeckAsync(deckId) is Deck deck)
            ? Results.Ok(mapper.Map<DeckReadDetailDTO>(await deckRepository.DeleteDeckAsync(deck)))
            : Results.NotFound($"Deck with {deckId} was not found.");
    }
}
