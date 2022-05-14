using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories;

public class SqlCardRepository : ICardRepository
{
    private readonly mtg_v1Context _db;

    public SqlCardRepository(mtg_v1Context mtg_V1DbContext)
    {
        _db = mtg_V1DbContext;
    }

    public IQueryable<Card> ReadCards()
    {
        return _db.Cards
            .Select(card => card);
    }

    public IQueryable<Card> ReadCardsByArtist(long artistId)
    {
        return _db.Cards
            .Where(card => card.Artist.Id == artistId);
    }
}
