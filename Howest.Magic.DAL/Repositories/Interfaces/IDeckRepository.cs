namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckRepository
{
    IQueryable<Deck> ReadDecks();
    Task<Deck> ReadDeckAsync(long id);
    Task<Deck?> CreateDeckAsync(Deck deck);
    Task<Deck?> DeleteDeckAsync(Deck deck);
}
