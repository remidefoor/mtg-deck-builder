namespace Howest.MagicCards.DAL.Repositories;

internal interface ICardRepository
{
    IQueryable<Card> ReadCards();
}
