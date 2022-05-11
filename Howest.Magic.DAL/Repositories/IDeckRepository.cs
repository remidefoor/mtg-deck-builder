namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckRepository
{
    Task CreateDeck(IEnumerable<DeckCard> deck);
    Task DeleteDeck();
}
