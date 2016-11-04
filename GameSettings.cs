using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    /// <summary>
    ///     Handles the settings used to create SpecialCardGame.
    /// </summary>
    class GameSettings
    {

        /// <summary>
        ///     Asks the user for the number of players.
        ///     Input must be a number value inbetween 2 and 4. Loops if not.
        /// </summary>
        /// <returns>Returns number of players.</returns>
        public static int GetNumberOfPlayers()
        {
            int numOfPlayers = 0;
            do
            {
                Console.WriteLine("How many players will be playing? Enter a value between 2 and 4.");
                string str = Console.ReadLine();
                if (int.TryParse(str, out numOfPlayers))
                {
                    if(numOfPlayers > 4)
                    {
                        Console.WriteLine("Number is too large. Try again.");
                    }
                    else if(numOfPlayers < 2)
                    {
                        Console.WriteLine("Number is too small. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("That is not a number! Try again.");
                }
                Console.WriteLine();
            } while (!(numOfPlayers >= 2 && numOfPlayers <= 4));

            return numOfPlayers;
        }


        /// <summary>
        ///     Asks the user for the names of the players. Trims and capitalizes names.
        ///     Input must be a unique string. Loops if not.
        /// </summary>
        /// <param name="numOfPlayers">The number of players playing the game.</param>
        /// <returns>Returns array of player names.</returns>
        public static string[] GetArrayOfNames(int numOfPlayers)
        {
            Console.WriteLine();
            Console.WriteLine("Enter the names of your players. Names will be capitalized.");
            Console.WriteLine();

            HashSet<string> nameSet = new HashSet<string>();
            int maxChars = 14;

            for (int i = 0; i < numOfPlayers; i++)
            {
                string str = "";
                do
                {
                    Console.WriteLine("Please enter a UNIQUE name for PLAYER " + (i) + ". Must be between 1 to " + maxChars + " characters.");
                    str = Console.ReadLine().Trim().ToUpper();

                    if (str.Length < 0)
                    {
                        Console.WriteLine("Name is too long! Try again.");
                    }
                    else if (str.Length > maxChars)
                    {
                        Console.WriteLine("Name is too long! Try again.");
                    }
                    else if (nameSet.Contains(str))
                    {
                        Console.WriteLine("A name like that already exists! Try again.");
                    }
                    Console.WriteLine();
                } while (nameSet.Contains(str) || str.Length <= 0 || str.Length > maxChars);

                nameSet.Add(str);
            }

            return nameSet.ToArray();
        }
    }
}
