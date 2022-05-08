namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions;

public class DeckEndPoints : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("Deck", PostDeck)
            .Accepts<DeckWriteDTO>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("Deck", DeleteDeck)
            .Produces(StatusCodes.Status204NoContent);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IDeckRepository, SqlDeckRepository>();
    }

    private IResult PostDeck(IDeckRepository deckRepository, DeckWriteDTO deck)
    {
        throw new NotImplementedException();
    }

    private IResult DeleteDeck(IDeckRepository deckRepository)
    {
        throw new NotImplementedException();
    }
}
