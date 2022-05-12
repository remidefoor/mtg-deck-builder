namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions;

public class DeckEndPoints : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("Decks", PostDeck)
            .Accepts<DeckWriteDTO>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        app.MapDelete("Decks/{id:long}", DeleteDeck)
            .Produces(StatusCodes.Status204NoContent);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddScoped<IDeckRepository, SqlDeckRepository>();
        services.AddAutoMapper(new System.Type[]
            {
                typeof(Howest.MagicCards.Shared.Mappings.DecksProfile),
                typeof(Howest.MagicCards.Shared.Mappings.DeckCardsProfile)
            });
    }

    private async Task<IResult> PostDeck(IDeckRepository deckRepository, IMapper mapper, DeckWriteDTO deck)
    {
        return (await deckRepository.CreateDeckAsync(mapper.Map<Deck>(deck)) is Deck createdDeck)
            ? Results.Created($"https://localhost:7103/api/Decks/{createdDeck.Id}", mapper.Map<DeckReadDetailDTO>(createdDeck))
            : Results.BadRequest();
    }

    private async Task<IResult> DeleteDeck(IDeckRepository deckRepository, IMapper mapper, long id)
    {
        return (await deckRepository.ReadDeckAsync(id) is Deck deck)
            ? Results.Ok(mapper.Map<DeckReadDetailDTO>(await deckRepository.DeleteDeckAsync(deck)))
            : Results.NotFound($"Deck with {id} was not found.");
    }
}
