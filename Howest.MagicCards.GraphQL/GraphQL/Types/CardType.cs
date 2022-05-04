namespace Howest.MagicCards.GraphQL.GraphQLTypes;

public class CardType : ObjectGraphType<Card>
{
    public CardType(IArtistRepository artistRepository)
    {
        Name = "Card";
        Description = "Magic The Gathering card";

        Field(card => card.Id, type: typeof(IdGraphType)).Description("The ID of the card");
        Field(card => card.Name, type: typeof(StringGraphType)).Description("The name of the card");
        Field(card => card.ManaCost, type: typeof(StringGraphType)).Description("The mana cost of the card");
        Field(card => card.ConvertedManaCost, type: typeof(StringGraphType)).Description("The total mana cost of the card")
            .Name("TotalManaCost");
        Field(card => card.Type, type: typeof(StringGraphType)).Description("The type of the card");
        Field(card => card.Rarity, type: typeof(StringGraphType)).Description("The rarity code of the card");
        Field(card => card.SetCode, type: typeof(StringGraphType)).Description("The set code of the card");
        Field(card => card.Text, type: typeof(StringGraphType)).Description("The description of the card")
            .Name("Description");
        Field(card => card.Flavor, type: typeof(StringGraphType)).Description("The flavor of the card");
        Field(card => card.ArtistId, type: typeof(NonNullGraphType<StringGraphType>)).Description("The artist ID of the card");
        Field(card => card.Number, type: typeof(StringGraphType)).Description("The number of the card");
        Field(card => card.Power, type: typeof(StringGraphType)).Description("The power of the card");
        Field(card => card.Toughness, type: typeof(StringGraphType)).Description("The toughness of the card");
        Field(card => card.Layout, type: typeof(StringGraphType)).Description("The layout of the card");
        Field(card => card.MultiverseId, type: typeof(IntGraphType)).Description("The multiverse id of the card");
        Field(card => card.OriginalImageUrl, type: typeof(StringGraphType)).Description("The image URL of the card")
            .Name("ImageUrl");
        Field(card => card.Image, type: typeof(StringGraphType)).Description("The imge of the card");
        Field(card => card.OriginalText, type: typeof(StringGraphType)).Description("The original description of the card")
            .Name("OriginalDescription");
        Field(card => card.OriginalType, type: typeof(StringGraphType)).Description("The original type of the card");
        Field(card => card.MtgId, type: typeof(StringGraphType)).Description("The MTG ID of the card");
        Field(card => card.Variations, type: typeof(StringGraphType)).Description("The variations of the card");
        Field<ArtistType>
        (
            "Artist",
            "The artist of the card",
            resolve: context => artistRepository.ReadArtist(context.Source.Id)
        );
    }
}
