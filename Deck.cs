using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpecialCardGame
{
    class Deck
    {
        protected Stack<Card> cardStack; //Stack of cards for the deck.

        public Deck()
        {
            cardStack = new Stack<Card>();
        }


        /// <summary>
        ///     Populates the deck with the 52 standard deck cards.
        /// </summary>
        public virtual void Populate()
        {
            Console.WriteLine("STANDARD DECK CREATED");
            for (Suit i = Suit.CLUBS; i <= Suit.SPADES; i++)
            {
                for (Rank j = Rank.TWO; j <= Rank.ACE; j++)
                {
                    Card c = new Card(i, j);
                    cardStack.Push(c);
                }
            }
        }


        /// <summary>
        ///     Shuffles the deck.
        /// </summary>
        public void Shuffle()
        {
            var rnd = new Random();
            var values = cardStack.ToArray();
            cardStack.Clear();
            foreach (var value in values.OrderBy(x => rnd.Next()))
            {
                cardStack.Push(value);
            }
        }


        /// <summary>
        ///     Draws a card from the top of the deck.
        /// </summary>
        /// <returns>Returns top card of deck.</returns>
        /// <exception cref = "InvalidOperationException" >No cards in the stack.</exception>
        public Card DrawCardFromTop()
        {
            if (cardStack.Count <= 0)
            {
                throw new InvalidOperationException();
            }
            Card c = cardStack.Pop();
            return  c;
        }


        /// <summary>
        ///     Draws a card from the top of the deck.
        /// </summary>
        /// <param name="c">The card to be added to the deck.</param>
        /// <exception cref="ArgumentNullException">Null Argument</exception>
        public void PushCard(Card c)
        {
            if(c == null)
            {
                throw new ArgumentNullException();
            }
            cardStack.Push(c);
        }


        /// <summary>
        ///     Gets the numbers of cards in the deck.
        /// </summary>
        /// <returns>Count of the card stack.</returns>
        public int CardCount()
        {
            return cardStack.Count();
        }
    }
}
