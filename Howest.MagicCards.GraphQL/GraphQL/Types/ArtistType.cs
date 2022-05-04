namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class ArtistType : ObjectGraphType<Artist>
{
    public ArtistType(ICardRepository cardRepository)
    {
        Name = "Artist";
        Description = "Magic The Gathering artist";

        Field(artist => artist.Id, type: typeof(IdGraphType)).Description("The ID of the artist");
        Field(artist => artist.FullName, type: typeof(IdGraphType)).Description("The full name of the artist");
        Field<ListGraphType<CardType>>
        (
            "Cards",
            "The cards of the artist",
            resolve: context => cardRepository.ReadCards()
        );
    }
}
