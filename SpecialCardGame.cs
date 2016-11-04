using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    /// <summary>
    ///     Handles a card game using a special ruleset.
    ///     Each round, go through a list of players and play their turn.
    ///     Each turn, prompt the player to enter a button to draw their card.
    ///     At the end of each round, points are calculated and awarded/deducted based on their hand.
    ///     Once a player reaches 21 points with a lead of atleast 2, the game ends.
    /// </summary>
    class SpecialCardGame
    {
        
        List<Player> playersList; //List of players participating in the game.
        bool winnerFound;  //Win Condition
        int round;  //Current round of game.
        int turn;  //Current turn of game.
        SpecialDeck deck;  //The deck used for the game.

        /* GAME SETTINGS THAT CAN BE CHANGED */
        int winPoints = 2; //Amount of points awarded to the winner of each round.
        int penaltyPoints = PenaltyCard.penalty; //Amount of points deducted to those with a penalty card.
        int requiredPointsToWin = 21; //Amount of points needed to win
        int requiredPointLead = 2; //Point difference needed to win


        /// <summary>
        ///     Constructor that initializes the card game using Special Tech's ruleset.
        ///     Creates a deck and the list of players participating in the game.
        /// </summary>
        /// <param name="players">An array of the players playing the game.</param>
        /// <exception cref="ArgumentNullException">The array is null</exception>
        /// <exception cref="ArgumentException">The amount of players is invalid.</exception>
        public SpecialCardGame(string[] players)
        {
            if(players == null)
            {
                throw new ArgumentNullException();
            }
            if(players.Length < 2 || players.Length > 4)
            {
                throw new ArgumentException();
            }

            winnerFound = false;
            turn = 0;
            round = 0;
            playersList = new List<Player>();

            for (int i = 0; i < players.Length; i++)
            {
                Player p = new Player(players[i]);
                playersList.Add(p);
                Console.WriteLine("PLAYER: " + playersList.ElementAt(playersList.Count - 1).name + " ADDED");  
            }

            deck = new SpecialDeck();
            deck.Populate();
        }


        /// <summary>
        ///     Plays rounds until a winner is found.
        ///     Rounds goes through the player list to find a round winner.
        /// </summary>
        public void PlayGame()
        {
            while (!winnerFound)
            {
                PlayRound();
            }
        }


        /// <summary>
        ///     Shuffles the deck, then plays a round of turns that cause each player to draw a card.
        ///     Player hands are then compared to each other and awards/deducts points.
        ///     Hands are then cleared and cards are placed back into the deck.
        /// </summary>
        void PlayRound()
        {
            deck.Shuffle();
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("   - Deck Ready and Shuffled. Card Count: " + deck.CardCount() + " | ROUND: " + round + " -");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();

            for (turn = 0; turn < playersList.Count; turn++)
            {
                Player currentPlayer = playersList[turn];
                PlayTurn(currentPlayer);
            }

            Player playerWithBestHand = FindRoundWinner(); //find the winner of the round
            Player winner = FindGameWinner(); //check if anyone can win the game

            if (winnerFound == true)
            {
                Console.WriteLine();
                Console.WriteLine(" !!! " + winner.name + "  HAS WON !!!");
                Console.WriteLine(" !!! " + winner.name + "  HAS WON !!!");
                return;
            }
            ClearHands();
            round++;
        }


        /// <summary>
        ///     Prompts the to user to enter a key to draw a card then prints their values.
        /// </summary>
        /// /// <param name="p">The current player.</param>
        void PlayTurn(Player p)
        {
            ConsoleKey key;
            do
            {
                Console.WriteLine(" Current turn: " + p.name + " | Press 'Enter' to draw a card from the deck(" + deck.CardCount() + ").");
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    Card c = deck.DrawCardFromTop();
                    p.AddCard(c);
                    if (c.GetType() == typeof(PenaltyCard))
                    {
                        Console.WriteLine("- " + p.name + " has added a PENALTY CARD (HandValue: " + penaltyPoints + ")");
                    }
                    else
                    {
                        Console.WriteLine(" - " + p.name + " has added a " + c.rank + " of " + c.suit + " (HandValue: " + GetHandValue(p) + ")");
                    }
                    Console.WriteLine();
                }
            } while (key != ConsoleKey.Enter);
        }


        /// <summary>
        ///     Compares every hand of each player and finds the player with the best hand.
        ///     Adjusts points of the round winner and any one who has a penalty card.
        /// </summary>
        /// <returns>Returns player with the best hand.</returns>
        Player FindRoundWinner()
        {
            Console.WriteLine();
            Player winnerOfRound = playersList[0];

            if (GetHandValue(winnerOfRound) < 0)
            {
                Console.WriteLine(" " + winnerOfRound.name + " has received a penalty! " + penaltyPoints + "(" + winnerOfRound.points + ")" );
                winnerOfRound.AdjustPoints(penaltyPoints);
            }
            //compare the potential winner against the rest of the players
            for(int i = 1; i < playersList.Count; i++)
            {
                Player p = playersList[i];

                if(GetHandValue(p) < 0)
                {
                    Console.WriteLine(" " + p.name + " has received a penalty! " + penaltyPoints + "(" + p.points + ")");
                    p.AdjustPoints(penaltyPoints);
                }
                if (GetHandValue(p) > GetHandValue(winnerOfRound))
                {
                    winnerOfRound = p;
                }
            }
            //if the winner has a handvalue of less than 0, then that means everyone got a penalty card
            if (GetHandValue(winnerOfRound) < 0)
            {
                Console.WriteLine();
                Console.WriteLine(" NO WINNERS, ONLY PENALTIES!");
                PrintScores();
                return null;
            }

            Console.WriteLine(" " + winnerOfRound.name + " has won the round! +" + winPoints + "(" + winnerOfRound.points + ")");
            winnerOfRound.AdjustPoints(winPoints);
            PrintScores();
            return winnerOfRound;
        }


        /// <summary>
        ///     Finds if theres a winner.
        ///     Checks if there's a player with >= 21 points 
        ///     and that they are atleast in the lead by atleast 2 points.
        /// </summary>
        /// /// <returns>Returns player that meets the conditions to win the game.</returns>
        Player FindGameWinner()
        {
            List<Player> copy = new List<Player>();

            foreach (Player p in playersList)
            {
                copy.Add(p);
            }
            
            copy.Sort((x, y) => y.points.CompareTo(x.points));
            Player first = copy[0];
            Player second = copy[1];

            if (first.points >= requiredPointsToWin)
            {
                int scoreDiff = first.points - second.points;
                if (scoreDiff >= requiredPointLead)
                {
                    winnerFound = true;
                }
            }
            return first;
        }


        /// <summary>
        ///     Clears the hands of all players and returns them to the deck.
        /// </summary>
        /// <exception cref="InvalidOperationException">Player has an invalid amount of cards.</exception>
        void ClearHands()
        {
            foreach (Player p in playersList)
            {
                if (p.CardCount() != 1)
                {
                    throw new InvalidOperationException();
                }
                Card c = p.RetrieveFirstCard();
                deck.PushCard(c);
            }
        }


        /// <summary>
        ///   Gets  the value of player's hand.
        /// </summary>
        /// <param name="player">The player you want to check.</param>
        /// <returns>Integer representation of the hand value.</returns>
        /// /// <exception cref="ArgumentNullException">Player is null.</exception>
        public int GetHandValue(Player p)
        {
            if(p == null)
            {
                throw new ArgumentNullException();
            }
            if (p.CardCount() <= 0)
            {
                return 0;
            }

            int value = 0;

            foreach (Card c in p.hand)
            {
                value += (int)c.rank * 10;
                value += (int)c.suit;
                if (c.GetType() == typeof(PenaltyCard))
                {
                    value = -1;
                }
            }

            return value;
        }


        /// <summary>
        ///     Prints the current scores of all the players.
        /// </summary>
        void PrintScores()
        {
            Console.WriteLine();
            foreach (Player p in playersList)
            {
                Console.WriteLine("  | " + p.name + " : " + p.points + " pts. ");
            }
        }
    }
}
