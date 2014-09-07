using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivia_testv1
{
    internal class Program
    {
       public static bool notAWinner;
        private static void Main(string[] args)
        {
           

            Game aGame = new Game();
            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");
            Random rand = new Random();
            do
            {
                aGame.roll(rand.Next(5) + 1);
                if (rand.Next(9) == 7)
                {
                    notAWinner = aGame.wrongAnswer();
                }
                else
                {
                    notAWinner = aGame.wasCorrectlyAnswered();
                }

            } while (notAWinner);
            Console.ReadLine();
        }


    }

    }
