namespace Howest.MagicCards.Shared.Mappings;

public class CardsProfile : Profile
{
    public CardsProfile()
    {
        CreateMap<Card, CardReadDTO>()
            .ForMember(dto => dto.ImageUrl,
            opt => opt.MapFrom(card => card.OriginalImageUrl));
        CreateMap<CardReadDTO, DeckCardReadDetailDTO>()
            .ForMember(deckCard => deckCard.CardId,
                opt => opt.MapFrom(card => card.Id));
    }
}
