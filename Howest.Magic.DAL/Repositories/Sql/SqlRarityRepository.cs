namespace Howest.MagicCards.DAL.Repositories;

public class SqlRarityRepository : IRarityRepository
{
    private readonly mtg_v1Context _db;

    public SqlRarityRepository(mtg_v1Context mtg_v1DbContext)
    {
        _db = mtg_v1DbContext;
    }

    public IQueryable<Rarity> ReadRarities()
    {
        return _db.Rarities
            .Select(rarity => rarity);
    }
}
