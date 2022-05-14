﻿using Microsoft.EntityFrameworkCore;

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

        app.MapDelete($"{_urlPrefix}/Decks/{{id:long}}", DeleteDeck)
            .Produces(StatusCodes.Status204NoContent);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddDbContext<mtg_v1Context>(options =>
            options.UseSqlServer(Configuration.GetAppSetting("ConnectionStrings:MgtV1Db")));
        services.AddScoped<IDeckRepository, SqlDeckRepository>();
        services.AddAutoMapper(new System.Type[]
            {
                typeof(Howest.MagicCards.Shared.Mappings.DecksProfile)
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
