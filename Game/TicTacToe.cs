using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lea.Game
{
    class TicTacToe
    {
        static int[][] winningCombinations = new int[8][];
        static Dictionary<int, int> mostPickedNumbers = new Dictionary<int, int>();
        static private List<string> learnedCombinations = new List<string>();

        public static void StartGame()
        {
            for (int i = 1; i < 10; i++)
            {
                mostPickedNumbers.Add(i, 0);
            }
            //horizontal
            winningCombinations[0] = new int[3] { 1, 2, 3 };
            winningCombinations[1] = new int[3] { 4, 5, 6 };
            winningCombinations[2] = new int[3] { 7, 8, 9 };
            //vertical
            winningCombinations[3] = new int[3] { 1, 4, 7 };
            winningCombinations[4] = new int[3] { 2, 5, 8 };
            winningCombinations[5] = new int[3] { 3, 6, 9 };
            //diagonals
            winningCombinations[6] = new int[3] { 1, 5, 9 };
            winningCombinations[7] = new int[3] { 3, 5, 7 };

            while (true)
            {
                int choiceNumber = 0, markedOptions = 0;
                int computerWins = 0,
                    humanWins = 0;
                int[] userCombination = new int[5];
                int[] computerCombination = new int[5];
                int combNumber = 0;
                int input;

                while (markedOptions < 9)
                {

                    Console.WriteLine("human - computer: {0} - {1}", humanWins, computerWins);

                    Console.Write("Use inputs: ");
                    for (int i = 0; i < choiceNumber; i++)
                    {
                        Console.Write(userCombination[i]);
                        Console.Write(' ');
                    }
                    Console.WriteLine();

                    Console.Write("AI inputs: ");
                    for (int i = 0; i < choiceNumber; i++)
                    {
                        Console.Write(computerCombination[i]);
                        Console.Write(' ');
                    }
                    Console.WriteLine();

                    Console.WriteLine("Enter a number(1-9)");
                    do
                    {
                        Console.WriteLine("Remember: You can't input something that has already been taken.");
                        input = int.Parse(Console.ReadLine());
                    } while ((userCombination.Contains(input)) || (computerCombination.Contains(input)));

                    userCombination[choiceNumber] = input;
                    mostPickedNumbers[input]++;

                    Console.Clear();

                    //Check combinations
                    if (choiceNumber >= 2)
                    {
                        combNumber = isWinner(userCombination);
                        if (combNumber != -1)
                        {
                            Console.Clear();
                            Console.WriteLine("User is winner!");
                            AddCombination(learnedCombinations, winningCombinations[combNumber], combNumber);
                            humanWins++;
                            break;
                        }
                        else
                        {
                            combNumber = isWinner(computerCombination);
                            if (combNumber != -1)
                            {
                                Console.Clear();
                                Console.WriteLine("Computer is winner!");
                                computerWins++;
                                break;
                            }
                        }
                    }
                    preventWin(learnedCombinations, userCombination, computerCombination, choiceNumber);

                    choiceNumber++;
                    markedOptions += 2;
                }
                Console.WriteLine("Learned combinations:");
                for (int i = 0; i < learnedCombinations.Count; i++)
                {
                    Console.WriteLine(learnedCombinations[i]);
                }

                Console.WriteLine();
                Console.WriteLine("Do you want to play again?\n\t-- press N to exit");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    return;
                }
            }
        }
        private static void preventWin(List<string> learnedCombinations, int[] userCombination, int[] computerCombination, int choiceNumber)
        {
            int prevContained = 0, contained = 0, combNumber = -1;
            for (int i = 0; i < learnedCombinations.Count; i++)
            {
                contained = 0;
                for (int j = 0; j < userCombination.Length; j++)
                {
                    if (learnedCombinations[i].Contains(userCombination[j].ToString()))
                    {
                        contained++;
                    }
                }

                if (contained > prevContained)
                {
                    combNumber = i;
                    prevContained = contained;
                }
            }

            bool acted = false;
            if (combNumber > -1)
            {
                Console.Write("User is trying: ");

                for (int i = 0; i < learnedCombinations[combNumber].Length; i++)
                {
                    Console.Write(learnedCombinations[combNumber][i] + " ");
                    if (!userCombination.Contains(learnedCombinations[combNumber][i] - '0')
                        && !computerCombination.Contains(learnedCombinations[combNumber][i] - '0')
                        && !acted)
                    {
                        computerCombination[choiceNumber] = learnedCombinations[combNumber][i] - '0';
                        acted = true;
                    }
                }
                Console.WriteLine();
            }
            if (mostPickedNumbers.Count > 0 && !acted)
            {
                int timesPicked = 0, mostPickedIndex = 0;
                for (int i = 1; i <= mostPickedNumbers.Count; i++)
                {
                    if (mostPickedNumbers[i] > timesPicked
                        && !computerCombination.Contains(i)
                        && !userCombination.Contains(i))
                    {
                        timesPicked = mostPickedNumbers[i];
                        mostPickedIndex = i;
                    }
                }

                if (mostPickedIndex > 0)
                {
                    computerCombination[choiceNumber] = mostPickedIndex;
                    Console.WriteLine("MOST_PICKED_NUMBER: {0}", mostPickedIndex);
                    acted = true;
                }
            }
            if (!acted)
            {
                while (!acted)
                {
                    Random rand = new Random();
                    int selected = rand.Next(1, 10);
                    if (!userCombination.Contains(selected)
                        && !computerCombination.Contains(selected))
                    {
                        computerCombination[choiceNumber] = selected;
                        acted = true;
                    }
                }
            }
        }
        private static int isWinner(int[] combArray)
        {
            bool winning = true;
            int combNumber = 0;
            for (int i = 0; i < 8; i++)
            {
                winning = true;
                for (int j = 0; j < 3; j++)
                {
                    if (!combArray.Contains(winningCombinations[i][j]))
                    {
                        winning = false;
                    }
                }

                if (winning)
                {
                    combNumber = i;
                    break;
                }
            }

            if (winning)
            {
                return combNumber;
            }

            return -1;
        }
        private static void AddCombination(List<string> learnedCombinations, int[] combination, int combNumber)
        {
            if (!IsLearnedComb(learnedCombinations, combination))
            {
                learnedCombinations.Add(string.Join("", combination));
            }
        }
        private static bool IsLearnedComb(List<string> learnedCombinations, int[] combination)
        {
            bool learned = false;
            if (learnedCombinations.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < learnedCombinations.Count; i++)
            {
                if (learnedCombinations[i] == string.Join("", combination))
                {
                    learned = true;
                    break;
                }
            }

            return learned;
        }
    }

    /*learnedCombinations.Add(string.Join("",winningCombinations[0]));
    learnedCombinations.Add(string.Join("", winningCombinations[1]));
    learnedCombinations.Add(string.Join("", winningCombinations[2]));
    learnedCombinations.Add(string.Join("", winningCombinations[3]));
    learnedCombinations.Add(string.Join("", winningCombinations[4]));
    learnedCombinations.Add(string.Join("", winningCombinations[5]));
    learnedCombinations.Add(string.Join("", winningCombinations[6]));
    learnedCombinations.Add(string.Join("", winningCombinations[7]));*/

}
