namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckCardRepository
{
    IQueryable<DeckCard> ReadDeckCards(long deckId);
    Task<DeckCard?> ReadDeckCardAsync(long deckId, long cardId);
    Task<DeckCard?> CreateDeckCardAsync(DeckCard deckCard);
}
