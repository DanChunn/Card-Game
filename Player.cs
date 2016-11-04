using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    class Player
    {
        public string name { get; private set; } //name of player
        public int points { get; private set; } //points the player has
        public List<Card> hand { get; private set; } //cards in player's hand


        /// <summary>
        ///   Constructor that creates a player and initilizes its hand, points, and name.
        /// </summary>
        /// <param name="str">The player's name.</param>
        /// <exception cref="ArgumentNullException">The value is null, empty or whitespace.</exception>
        public Player(string str)
        {
            if(str == null || str.Length <= 0)
            {
                throw new ArgumentNullException();
            }
            name = str;
            points = 0;
            hand = new List<Card>();
        }


        /// <summary>
        ///   Adds a card to the player's hand.
        /// </summary>
        /// <param name="c">The card to be added to the hand.</param>
        /// <exception cref="ArgumentNullException">The value is null.</exception>
        public void AddCard(Card c)
        {
            if(c == null)
            {
                throw new ArgumentNullException();
            }
            hand.Add(c);
        }


        /// <summary>
        ///   Retrieves the first card in the player's hand.
        /// </summary>
        /// <returns>First card in player's hand.</returns>
        /// <exception cref="InvalidOperationException">The value is null.</exception>
        public Card RetrieveFirstCard()
        {
            if(hand.Count < 1)
            {
                throw new InvalidOperationException();
            }
            Card c = hand[0];
            hand.RemoveAt(0);
            return c;
        }


        /// <summary>
        ///   Adjusts the players points. If below zero, set to zero.
        /// </summary>
        /// <param name="n">The points to be added. Can be negative to subtract.</param>
        public void AdjustPoints(int n)
        {
            points += n;
            if(points < 0)
            {
                points = 0;
            }
        }


        /// <summary>
        ///     Gets the numbers of cards in the player's hand.
        /// </summary>
        /// <returns>Count of the card list.</returns>
        public int CardCount()
        {
            return hand.Count();
        }
    }
}
