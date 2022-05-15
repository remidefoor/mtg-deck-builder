namespace Howest.MagicCards.DAL.Repositories;

public interface ICardRepository
{
    IQueryable<Card> ReadCards();
    IQueryable<Card> ReadCardsByArtist(long artistId);
}
