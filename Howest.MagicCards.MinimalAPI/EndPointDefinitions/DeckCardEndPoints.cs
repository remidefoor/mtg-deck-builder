using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.MinimalAPI.EndPointDefinitions;

public class DeckCardEndPoints : IEndpointDefinition
{
    private readonly string _urlPrefix;
    private readonly int _deckSize;

    public DeckCardEndPoints()
    {
        _urlPrefix = Configuration.GetAppSetting("UrlPrefix");
        _deckSize = int.Parse(Configuration.GetAppSetting("DeckSize"));
    }

    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost($"{_urlPrefix}/Decks/{{deckId:long}}/DeckCards", PostDeckCard)
            .Accepts<DeckCardWriteDTO>("application/json")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddDbContext<mtg_v1Context>(options =>
            options.UseSqlServer(Configuration.GetAppSetting("ConnectionStrings:MgtV1Db")));
        services.AddScoped<IDeckCardRepository, SqlDeckCardRepository>();
        services.AddAutoMapper(new System.Type[]
            {
                typeof(Shared.Mappings.DeckCardsProfile)
            });
    }

    private async Task<IResult> PostDeckCard(
        IDeckCardRepository deckCardRepository,
        IMapper mapper,
        long deckId,
        DeckCardWriteDTO deckCardDTO
    )
    {
        DeckCard deckCard = mapper.Map<DeckCard>(deckCardDTO);
        deckCard.DeckId = deckId;
        if (deckCardFits(deckCardRepository, deckCard))
        {
            try
            {
                DeckCard? createdDeckCard = await deckCardRepository.CreateDeckCardAsync(deckCard);
                if (createdDeckCard is DeckCard)
                {
                    return Results.Created($"https://localhost:7103{_urlPrefix}/Decks/{deckId}/DeckCards",
                        mapper.Map<DeckCardReadDTO>(createdDeckCard));
                }
            }
            catch (Exception ex)
            {
            }
        }
        return Results.BadRequest();
    }

    private bool deckCardFits(IDeckCardRepository deckCardRepository, DeckCard deckCard)
    {
        int deckCardAmount = deckCardRepository.ReadDeckCards(deckCard.DeckId)
            .Sum(deckCard => deckCard.Amount);
        return deckCardAmount + deckCard.Amount <= _deckSize;
    }
}
