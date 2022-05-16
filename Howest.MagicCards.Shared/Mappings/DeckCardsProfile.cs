namespace Howest.MagicCards.Shared.Mappings;

public class DeckCardsProfile : Profile
{
    public DeckCardsProfile()
    {
        CreateMap<DeckCard, DeckCardReadDTO>();
        CreateMap<DeckCard, DeckCardReadDetailDTO>()
            .ForMember(dto => dto.Name,
                opt => opt.MapFrom(deckCard => deckCard.Card.Name));
        CreateMap<DeckCardWriteDTO, DeckCard>();
        CreateMap<DeckCardReadDetailDTO, DeckCardWriteDTO>();
    }
}
