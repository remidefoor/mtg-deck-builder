namespace Howest.MagicCards.DAL.Repositories;

public class SqlArtistRepository : IArtistRepository
{
    private readonly mtg_v1Context _db;

    public SqlArtistRepository(mtg_v1Context mtg_v1DbContext)
    {
        _db = mtg_v1DbContext;
    }

    public IQueryable<Artist> ReadArtists()
    {
        return _db.Artists
            .Select(artist => artist);
    }

    public Artist? ReadArtist(long id)
    {
        return _db.Artists
            .SingleOrDefault(artist => artist.Id == id);
    }
}
