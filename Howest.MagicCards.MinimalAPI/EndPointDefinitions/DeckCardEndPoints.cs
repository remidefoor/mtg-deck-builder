using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions;

public class DeckCardEndPoints : IEndpointDefinition
{
    private readonly string _urlPrefix;

    public DeckCardEndPoints()
    {
        _urlPrefix = Configuration.GetAppSetting("UrlPrefix");
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost($"{_urlPrefix}/Decks/{{id:long}}/DeckCards", PostDeckCard)
            .Accepts<DeckCardWriteDTO>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddDbContext<mtg_v1Context>(options =>
            options.UseSqlServer(Configuration.GetAppSetting("ConnectionStrings:MgtV1Db")));
        services.AddScoped<IDeckRepository, SqlDeckRepository>();
        services.AddAutoMapper(new System.Type[]
            {
                typeof(Shared.Mappings.DeckCardsProfile)
            });
    }

    private async Task<IResult> PostDeckCard()
    {
        throw new NotImplementedException();
    }
}
