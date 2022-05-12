using System;
using System.Collections.Generic;

#nullable disable   

namespace Howest.MagicCards.DAL.Entities
{
    public partial class Deck
    {
        public Deck()
        {
            DeckCards = new HashSet<DeckCard>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<DeckCard> DeckCards { get; set; }
    }
}
