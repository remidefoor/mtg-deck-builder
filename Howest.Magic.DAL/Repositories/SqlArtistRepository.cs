namespace Howest.MagicCards.DAL.Repositories;

public class SqlArtistRepository : IArtistRepository
{
    private readonly mtg_v1Context _db;

    public SqlArtistRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public IQueryable<Artist> ReadArtists()
    {
        return _db.Artists // TODO include set, rarity & artist?
            .Select(artist => artist);
    }

    public Artist? ReadArtist(long id)
    {
        return _db.Artists
            .SingleOrDefault(artist => artist.Id == id);
    }
}
