namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckCardRepository
{
    Task<DeckCard?> ReadDeckCardAsync(long deckId, long cardId);
    Task<DeckCard?> CreateDeckCardAsync(DeckCard deckCard);
}
