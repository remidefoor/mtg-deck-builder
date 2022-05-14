namespace Howest.MagicCards.Shared.Mappings;

public class DecksProfile : Profile
{
    public DecksProfile()
    {
        CreateMap<Deck, DeckReadDetailDTO>();
        CreateMap<DeckWriteDTO, Deck>();
    }
}
