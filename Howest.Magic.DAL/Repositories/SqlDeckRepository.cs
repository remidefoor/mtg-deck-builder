﻿using Microsoft.EntityFrameworkCore;

namespace Howest.MagicCards.DAL.Repositories;
public class SqlDeckRepository : IDeckRepository
{
    private readonly mtg_v1Context _db;

    public SqlDeckRepository(mtg_v1Context mtg_V1DBContext)
    {
        _db = mtg_V1DBContext;
    }

    public async Task<Deck?> ReadDeckAsync(long id)
    {
        return await _db.Decks
            .SingleOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Deck?> CreateDeckAsync(Deck deck)
    {
        await _db.Decks.AddAsync(deck);
        await SaveAsync();

        await _db.DeckCards.AddRangeAsync(deck.DeckCards);
        await SaveAsync();

        return await ReadDeckAsync(deck.Id);
    }

    public async Task<Deck?> DeleteDeckAsync(Deck deck)
    {
        Deck? foundDeck = await ReadDeckAsync(deck.Id);

        if (foundDeck is Deck)
        {
            _db.Decks.Remove(deck);
            await SaveAsync();
        }

        return foundDeck;
    }

    private async Task<bool> SaveAsync()
    {
        return await _db.SaveChangesAsync() > 0;
    }
}
