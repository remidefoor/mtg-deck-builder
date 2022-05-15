namespace Howest.MagicCards.DAL.Repositories;

public interface IArtistRepository
{
    IQueryable<Artist> ReadArtists();
    Artist? ReadArtist(long id);
}
