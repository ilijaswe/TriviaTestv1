﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Trivia_testv1
{
    public static class TriviaExtension
    {
        public static bool In<T>(this T t, params T[] values)
        { return values.Contains(t); }
    }


    public class Game
    {
 
        

        public static List<string> players = new List<string>();
        int[] places = new int[6];
        int[] purses = new int[6];
        private bool[] inPenaltyBox = new bool[6];
        public static List<string> popQuestions = new List<string>();
        public static List<string> scienceQuestions = new List<string>();
        public static List<string> sportsQuestions = new List<string>();
        private static List<string> rockQuestions = new List<string>();
        int currentPlayer;
        bool isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.Add(("Pop Question " + i));
                scienceQuestions.Add(("Science Question " + i));
                sportsQuestions.Add(("Sports Question " + i));
                rockQuestions.Add(("Rock Question" + i));
            }
        }

       

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(String playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;
            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count());
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count();
        }

        public void roll(int roll)
        {
            
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);
            if (inPenaltyBox[currentPlayer])
            {
                if (roll%2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;
                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;
                    Console.WriteLine(players[currentPlayer]
                                      + "'s new location is "
                                      + places[currentPlayer]);
                    Console.WriteLine("The category is " + currentCategory());
                    askQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;
                Console.WriteLine(players[currentPlayer]
                                  + "'s new location is "
                                  + places[currentPlayer]);
                Console.WriteLine("The category is " + currentCategory());
                askQuestion();
            }
        }

        private void askQuestion()
        {
            if (currentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions[0]);
                popQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions[0]);
                scienceQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions[0]);
                sportsQuestions.RemoveAt(0);
            }
            if (currentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions[0]);
                rockQuestions.RemoveAt(0);
            }
        }

        private String currentCategory()
        {
            if (places[currentPlayer].In(0, 4, 8))
                return "Pop";
            else if (places[currentPlayer].In(1, 5, 9))
                return "Science";
            else if (places[currentPlayer].In(2, 6, 10))
                return "Sports";
            else
                return "Rock";
        }

        public bool wasCorrectlyAnswered()
        {
                if (inPenaltyBox[currentPlayer])
                {
            
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                                      + " now has "
                                      + purses[currentPlayer]
                                      + " Gold Coins.");
                    bool winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Answer was correct!!!!");
                purses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer] + " now has " + purses[currentPlayer].ToString() + " Gold Coins.");
                bool winner = didPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;
                return winner;
            }
        }

        public bool wrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");

            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }

        private bool didPlayerWin()
        {
            return !(purses[currentPlayer] == 6);
        }
    }
}

