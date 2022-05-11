using System;
using System.Collections.Generic;

namespace Howest.MagicCards.DAL.Entities
{
    public partial class Deck
    {
        public long CardId { get; set; }

        public virtual Card card { get; set; }
    }
}
