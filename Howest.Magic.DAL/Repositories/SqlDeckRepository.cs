namespace Howest.MagicCards.DAL.Repositories;
public class SqlDeckRepository : IDeckRepository
{
    private readonly mtg_v1Context _db;

    public SqlDeckRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public void CreateDeck(List<long> cards)
    {
        // TODO implement
    }

    public void DeleteDeck()
    {
        // TODO implement
    }
}
