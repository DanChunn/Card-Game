using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    public class Card
    {
        public Suit suit { get; }
        public Rank rank { get; }

        public Card(Suit s, Rank r)
        {
            suit = s;
            rank = r;
        }

        public Card()
        {
        }
    }
}
