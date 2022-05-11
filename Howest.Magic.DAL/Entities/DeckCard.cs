using System;
using System.Collections.Generic;

#nullable disable

namespace Howest.MagicCards.DAL.Entities
{
    public partial class DeckCard
    {
        public long CardId { get; set; }

        public virtual Card card { get; set; }
    }
}
