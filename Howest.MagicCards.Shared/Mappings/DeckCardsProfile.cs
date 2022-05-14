namespace Howest.MagicCards.Shared.Mappings;

public class DeckCardsProfile : Profile
{
    public DeckCardsProfile()
    {
        CreateMap<DeckCard, DeckCardReadDTO>();
        CreateMap<DeckCardWriteDTO, DeckCard>();
    }
}
