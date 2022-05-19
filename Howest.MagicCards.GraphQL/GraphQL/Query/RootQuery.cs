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
            arguments: new QueryArguments()
            {
                new QueryArgument<StringGraphType> { Name = "Power", DefaultValue = "All" },
                new QueryArgument<StringGraphType> { Name = "Toughness", DefaultValue = "All" }
            },
            resolve: context =>
            {
                CardFilter filter = new CardFilter()
                {
                    Power = context.GetArgument<string>("Power"),
                    Toughness = context.GetArgument<string>("Toughness")
                };

                return cardRepository.ReadCards()
                    .Filter(filter)
                    .ToList();
            }
        );

        Field<ListGraphType<ArtistType>>
        (
            "Artists",
            Description = "Get artists",
            arguments: new QueryArguments
            {
                new QueryArgument<IntGraphType> { Name = "limit", DefaultValue = 150 }
            },
            resolve: context =>
            {
                int limit = context.GetArgument<int>("limit");
                if (limit <= 0 || limit > 150) limit = 150;

                return artistRepository.ReadArtists()
                    .Take(limit)
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
