namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
    {
        Name = "Query";
        Description = "Query the Magic The Gathering collection";

        Field<ListGraphType<CardType>>
        (
            "Cards",
            Description = "Get cards",
            resolve: context =>
            {
                return cardRepository.ReadCards()
                    .ToList();
            }
        );

        Field<ListGraphType<ArtistType>>
        (
            "Artists",
            description: "Get artists",
            resolve: context =>
            {
                return artistRepository.ReadArtists()
                    .Take(10)
                    .ToList();
            }
        );
    }
}
