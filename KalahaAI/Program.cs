using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalahaAI
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean validMove;
            Board board = new Board();
            int chosenPocket;
            AI ai = new AI();

            while (true)
            {
                validMove = false;
                if (board.TerminalTest())
                {
                    board.PrintBoard();
                    Console.WriteLine("Game Over - Write 'exit' to quit the game");
                    while (true)
                    {
                        if(Console.ReadLine() == "exit") { break; }
                    }
                    break;
                }
                
                Console.WriteLine("\n---------------------------------------------------------------------");
                board.PrintBoard();

                //If player one (human): ask for input, check if valid, and apply the move if valid. Otherwise ask for a new move.
                if (board.GetIsPlayerOne())
                {
                    while (validMove == false)
                    {
                        Console.WriteLine("Turn of player one (human)");
                        Console.Write("Choose a pocket: ");
                        if (Int32.TryParse(Console.ReadLine(), out chosenPocket) && chosenPocket >= 0 &&
                            chosenPocket < 6 && board.GetPocketValue(chosenPocket) > 0)
                        {
                            board.Move(chosenPocket);
                            validMove = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid pocket index: Try again");
                        }
                    }
                }
                //If player 2 (AI): ask the MiniMax algorithm for a move and apply it.
                else
                {
                    Console.WriteLine("Turn of player two (AI)");
                    chosenPocket = ai.MiniMax(board, false, 13, int.MinValue, int.MaxValue).moveIndex;
                    Console.WriteLine("AI chooses pocket with index = " + chosenPocket);
                    board.Move(chosenPocket);
                }
            }
        }
    }
}
