namespace Howest.MagicCards.DAL.Repositories;

public class SqlCardRepository : ICardRepository
{
    private readonly mtg_v1Context _db;

    public SqlCardRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public IQueryable<Card> ReadCards()
    {
        return _db.Cards
            .Select(card => card);
    }
}
