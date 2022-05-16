using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Entities
{
    public partial class DeckCard
    {
        public long DeckId { get; set; }
        public long CardId { get; set; }
        public int Amount { get; set; }

        public virtual Card Card { get; set; }
        public virtual Deck Deck { get; set; }
    }
}
