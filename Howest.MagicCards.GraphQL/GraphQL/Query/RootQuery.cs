namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class RootQuery : ObjectGraphType
{
    public RootQuery(ICardRepository cardRepository)
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
    }
}
