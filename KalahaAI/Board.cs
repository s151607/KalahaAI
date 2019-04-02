using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalahaAI
{
    // This class holds the state information and several methods used to play the game.
    class Board
    {
        private int[] pockets;
        private Boolean isPlayerOne;

        // Constructor for a board in initial state.
        public Board()
        {
            pockets = new int[] { 4, 4, 4, 4, 4, 4, 0, 4, 4, 4, 4, 4, 4, 0 };
            isPlayerOne = true;
        }

        //Copy constructor
        public Board(Board previousBoard)
        {
            pockets = (int[])previousBoard.pockets.Clone();
            isPlayerOne = previousBoard.isPlayerOne;
        }

        public Boolean GetIsPlayerOne()
        {
            return isPlayerOne;
        }

        //Returns the amount of stones in the given pocket
        public int GetPocketValue(int pocket)
        {
            return pockets[pocket];
        }

        //Prints the current state of the board to console
        public void PrintBoard()
        {
            Console.WriteLine(" ---------------------------------");
            Console.Write("|     ");
            for (int i = 12; i > 6; i--)
            {
                Console.Write("[" + pockets[i] + "] ");
            }

            Console.Write("    |\n| [" + pockets[13] + "]");
            Console.Write("                       ");
            if (pockets[6] < 10)
                Console.Write(" ");
            if (pockets[13] < 10)
                Console.Write(" ");
            Console.Write("[" + pockets[6] + "] |\n");
            Console.Write("|     ");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("[" + pockets[i] + "] ");
            }
            Console.WriteLine("    |\n ---------------------------------");
        }

        //Test if the game is done. Returns true if done. Done is defined as all pockets being empty
        public Boolean TerminalTest()
        {
            Boolean terminal = true;
            for (int i = 0; i < 6; i++) if (pockets[i] > 0) terminal = false;
            for (int i = 7; i < 13; i++) if (pockets[i] > 0) terminal = false;
            return terminal;
        }

        //Utility function defined as the number of stones in player one's store
        public int Utility()
        {
            return pockets[6];
        }

        //Returns the pocket index opposite to the input
        public int Opposite(int pocket)
        {
            if (pocket == 6) return 13;
            else if (pocket == 13) return 6;
            else return 12 - pocket;
        }

        //Logic for moving a piece. Including what happends based on how the turn ends.
        public void Move(int pocket)
        {
            int i = pocket;

            while (pockets[pocket] > 0) //While the chosen pocket is not empty: take one out of it and place it in the next....
            {
                if (isPlayerOne)
                {
                    pockets[pocket]--;
                    if (i == 12) i = 0;
                    else i++;
                    pockets[i]++;
                }
                else
                {
                    pockets[pocket]--;
                    if (i == 5) i = 7;
                    else if (i == 13) i = 0;
                    else i++;
                    pockets[i]++;
                }
            }

            // If you end up in a empty pocket on your side.. take that and the opposite
            if (isPlayerOne && pockets[i] == 1 && ((i >= 0) && (i <= 5)) && pockets[Opposite(i)] > 0)
            {
                pockets[6] += pockets[i] + pockets[Opposite(i)];
                pockets[i] = 0;
                pockets[Opposite(i)] = 0;
            }
            else if (!isPlayerOne && pockets[i] == 1 && ((i >= 7) && (i <= 12)) && pockets[Opposite(i)] > 0)
            {
                pockets[13] += pockets[i] + pockets[Opposite(i)];
                pockets[i] = 0;
                pockets[Opposite(i)] = 0;
            }

            // If the other side is empty: capture all stones on your side
            int sum1 = 0;
            for (int j = 0; j < 6; j++) sum1 += pockets[j];
            int sum2 = 0;
            for (int j = 7; j < 13; j++) sum2 += pockets[j];
            if (sum1 == 0)
            {
                pockets[13] += sum2;
                for (int j = 7; j < 13; j++)
                {
                    pockets[j] = 0;
                }
            }
            else if (sum2 == 0)
            {
                pockets[6] += sum1;
                for (int j = 0; j < 6; j++)
                {
                    pockets[j] = 0;
                }
            }

            // passes turn when you don't end up in your own store
            if (isPlayerOne && i != 6) isPlayerOne = false;
            else if (!isPlayerOne && i != 13) isPlayerOne = true;
        }
    }
}
