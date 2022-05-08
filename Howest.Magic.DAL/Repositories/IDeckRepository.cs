namespace Howest.MagicCards.DAL.Repositories;

public interface IDeckRepository
{
    void CreateDeck(List<long> cards);
    void DeleteDeck();
}
