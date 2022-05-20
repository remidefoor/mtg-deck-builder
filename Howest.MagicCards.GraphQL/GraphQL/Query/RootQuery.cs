namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    private readonly string defaultFilter;
    private readonly int defaultEntityAmount;

    public RootQuery(ICardRepository cardRepository, IArtistRepository artistRepository)
    {
        defaultFilter = Configuration.GetAppSetting("defaultFilter");
        defaultEntityAmount = int.Parse(Configuration.GetAppSetting("defaultEntityAmount"));

        Name = "Query";
        Description = "Query the Magic The Gathering collection";

        Field<ListGraphType<CardType>>
        (
            "Cards",
            Description = "Get cards",
            arguments: new QueryArguments()
            {
                new QueryArgument<StringGraphType> { Name = "Power", DefaultValue = defaultFilter },
                new QueryArgument<StringGraphType> { Name = "Toughness", DefaultValue = defaultFilter }
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
                new QueryArgument<IntGraphType> { Name = "limit", DefaultValue = defaultEntityAmount }
            },
            resolve: context =>
            {
                int limit = context.GetArgument<int>("limit");
                if (limit <= 0 || limit > defaultEntityAmount) limit = defaultEntityAmount;

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
