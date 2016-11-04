using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("---------W-E-L-C-O-M-E---------");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine(" - Card Game by Daniel Chunn - ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("           - RULES -           ");
            Console.WriteLine(" Each round, each player draws ");
            Console.WriteLine(" a card. The player with the   ");
            Console.WriteLine(" highest scoring card is then  ");
            Console.WriteLine(" awarded points for that round.");
            Console.WriteLine(" Players that draw a penalty   ");
            Console.WriteLine(" card are penalized. Once a    ");
            Console.WriteLine(" player reaches 21 points with ");
            Console.WriteLine(" a 2 point lead, he or she is  ");
            Console.WriteLine(" declared the winner.          ");
            Console.WriteLine();
            Console.WriteLine();
            //Asks for game settings and then starts the game.
            try
            {
                int numOfPlayers = GameSettings.GetNumberOfPlayers();
                string[] names = GameSettings.GetArrayOfNames(numOfPlayers);
                SpecialCardGame game = new SpecialCardGame(names);
                game.PlayGame();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine();
            Console.WriteLine(" Hit any key to exit.");
            Console.ReadLine();
        }
    }
}
