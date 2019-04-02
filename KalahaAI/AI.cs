using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalahaAI
{
    //Class used as return type for the MiniMax algorithm. Contains move index and utility value.
    class AIReturn
    {
        public int utility { get; }
        public int moveIndex { get; }

        public AIReturn(int u, int m)
        {
            utility = u;
            moveIndex = m;
        }
    }

    class AI
    {
        //Returns the possible moves for the current player. (Not including the pockets with 0 stones)
        private List<int> Actions(Board board, Boolean maximizingPlayer)
        {
            List<int> actions = new List<int>();
            int lower;
            int upper;
            if (maximizingPlayer) { lower = 0; upper = 6; }
            else { lower = 7; upper = 13; }

            for (int i = lower; i < upper; i++)
            {
                if (board.GetPocketValue(i) > 0) actions.Add(i);
            }
            return actions;
        }

        //Takes a board copies it, applies the move a to the copy, and return the copy.
        private Board Result(Board board, int a)
        {
            Board rBoard = new Board(board);
            rBoard.Move(a);
            return rBoard;
        }

        // MiniMax with cutoff and alpha-beta pruning
        public AIReturn MiniMax(Board board, Boolean maximizingPlayer, int depth, int alpha, int beta)
        {
            int eval;
            int moveIndex = -1;

            // If game is done or at cutoff: return the Utility/Evaluation.
            if (board.TerminalTest() || depth == 0)
            {
                return new AIReturn(board.Utility(), -1);
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;
                foreach (int a in Actions(board, true))
                {
                    Board aBoard = Result(board, a);
                    eval = MiniMax(aBoard, aBoard.GetIsPlayerOne(), depth - 1, alpha, beta).utility;
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        moveIndex = a;
                    }
                    //Pruning
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha) break;
                }
                return new AIReturn(maxEval, moveIndex);
            }
            else // minimizing player
            {
                int minEval = int.MaxValue;
                foreach (int a in Actions(board, false))
                {
                    Board aBoard = Result(board, a);
                    eval = MiniMax(aBoard, aBoard.GetIsPlayerOne(), depth - 1, alpha, beta).utility;
                    if (eval < minEval)
                    {
                        minEval = eval;
                        moveIndex = a;
                    }
                    //Pruning
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) break;
                }
                return new AIReturn(minEval, moveIndex);
            }
        }
    }
}
