namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckRepository
{
    Task<Deck> ReadDeckAsync(long id);
    Task<Deck?> CreateDeckAsync(Deck deck);
    Task<Deck?> DeleteDeckAsync(Deck deck);
}
