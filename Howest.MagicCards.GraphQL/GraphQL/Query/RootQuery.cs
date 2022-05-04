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
            Description = "Get artists",
            resolve: context =>
            {
                return artistRepository.ReadArtists()
                    .ToList();
            }
        );

        Field<ArtistType>
        (
            "Artist",
            Description = "Get artist",
            arguments: new QueryArguments
            {
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "Id" }
            },
            resolve: context =>
            {
                long id = context.GetArgument<long>("Id");
                return artistRepository.ReadArtist(id);
            }
        );
    }
}
