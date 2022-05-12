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

    private async Task<IResult> PostDeck(IDeckRepository deckRepository, DeckWriteDTO deck)
    {
        
    }

    private async Task<IResult> DeleteDeck(IDeckRepository deckRepository)
    {
        try
        {
            await deckRepository.DeleteDeck();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.StatusCode(500);
        }
    }
}
