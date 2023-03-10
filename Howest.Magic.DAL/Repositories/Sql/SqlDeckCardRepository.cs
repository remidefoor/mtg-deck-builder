namespace Howest.MagicCards.DAL.Repositories;

public class SqlDeckCardRepository : IDeckCardRepository
{
    private readonly mtg_v1Context _db;

    public SqlDeckCardRepository(mtg_v1Context mtg_v1DbContext)
    {
        _db = mtg_v1DbContext;
    }

    public IQueryable<DeckCard> ReadDeckCards(long deckId)
    {
        return _db.DeckCards
            .Where(deckCard => deckCard.DeckId == deckId);
    }

    public async Task<DeckCard?> ReadDeckCardAsync(long deckId, long cardId)
    {
        return await _db.DeckCards
            .FindAsync(deckId, cardId);

    }

    public async Task<DeckCard?> CreateDeckCardAsync(DeckCard deckCard)
    {
        await _db.DeckCards
            .AddAsync(deckCard);
        await SaveAsync();

        return await ReadDeckCardAsync(deckCard.DeckId, deckCard.CardId);
    }

    private async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() > 0;
    }
}
