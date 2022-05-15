namespace Howest.MagicCards.DAL.Repositories;

public interface IRarityRepository
{
    IQueryable<Rarity> ReadRarities();
}
