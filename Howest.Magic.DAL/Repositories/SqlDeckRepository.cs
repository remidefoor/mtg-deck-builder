using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories;
public class SqlDeckRepository : IDeckRepository
{
    private readonly mtg_v1Context _db;

    public SqlDeckRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public async Task CreateDeck(IEnumerable<DeckCard> deck)
    {
        _db.Deck.AddRangeAsync(deck);
        await SaveAsync();
    }

    public async Task DeleteDeck()
    {
        _db.Deck.FromSqlRaw("DELETE FROM deck");
        await SaveAsync();
    }

    private async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() > 0;
    }
}
