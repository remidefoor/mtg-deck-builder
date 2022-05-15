namespace Howest.MagicCards.Shared.Mappings;

public class RaritiesProfile : Profile
{
    public RaritiesProfile()
    {
        CreateMap<Rarity, RarityReadDTO>();
    }
}
