using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    class SpecialDeck : Deck
    {
        public SpecialDeck()
        {
        }


        /// <summary>
        ///     Populates the deck with 52 standard deck cards along with 4 penalty cards.
        /// </summary>
        public override void Populate()
        {
            Console.WriteLine("Special DECK CREATED");
            for (Suit i = Suit.CLUBS; i <= Suit.SPADES; i++)
            {
                for (Rank j = Rank.TWO; j <= Rank.ACE; j++)
                {
                    Card c = new Card(i, j);
                    PushCard(c);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                PenaltyCard c = new PenaltyCard();
                PushCard(c);
            }
        }
    }
}
