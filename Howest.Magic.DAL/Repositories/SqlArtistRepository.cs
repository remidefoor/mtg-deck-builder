namespace Howest.MagicCards.DAL.Repositories;

public class SqlArtistRepository : IArtistRepository
{
    private readonly mtg_v1Context _db;

    public SqlArtistRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public IQueryable<Artist> GetArtists()
    {
        return _db.Artists
            .Select(artist => artist);
    }

    public Artist GetArtist(long id)
    {
        return _db.Artists
            .SingleOrDefault(artist => artist.Id == id);
    }
}
