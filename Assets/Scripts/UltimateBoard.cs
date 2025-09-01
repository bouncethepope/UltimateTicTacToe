using System.Linq;
using System.Collections.Generic;

namespace UltimateTicTacToe
{
    /// <summary>
    /// Represents the 3x3 grid of small boards for Ultimate Tic-Tac-Toe.
    /// </summary>
    public class UltimateBoard
    {
        public SmallBoard[] Boards { get; } = new SmallBoard[9];

        /// <summary>Winner of the overall game.</summary>
        public Mark Winner { get; private set; } = Mark.None;

        /// <summary>
        /// Index of the board the next player must play in. Null if any board is allowed.
        /// </summary>
        public int? ActiveBoard { get; private set; }

        public UltimateBoard()
        {
            for (int i = 0; i < Boards.Length; i++)
            {
                Boards[i] = new SmallBoard();
            }
        }

        /// <summary>
        /// Attempts to play on a specific small board and cell.
        /// </summary>
        /// <returns>true if the move was applied.</returns>
        public bool ApplyMove(int boardIndex, int cellIndex, Mark mark)
        {
            if (mark == Mark.None) return false;
            if (boardIndex < 0 || boardIndex >= 9) return false;
            if (cellIndex < 0 || cellIndex >= 9) return false;

            if (ActiveBoard.HasValue && ActiveBoard.Value != boardIndex) return false;

            var board = Boards[boardIndex];
            if (!board.ApplyMove(cellIndex, mark)) return false;

            if (board.Winner != Mark.None)
            {
                UpdateWinner();
            }

            // Determine next active board
            var targetBoard = Boards[cellIndex];
            ActiveBoard = (targetBoard.Winner == Mark.None && !targetBoard.IsFull) ? cellIndex : (int?)null;
            return true;
        }

        private void UpdateWinner()
        {
            // Evaluate winners of each small board as a 3x3 grid
            Mark[] marks = Boards.Select(b => b.Winner).ToArray();
            Winner = EvaluateLines(marks);
        }

        private Mark EvaluateLines(Mark[] marks)
        {
            int[][] lines = new int[][]
            {
                new [] {0,1,2}, new [] {3,4,5}, new [] {6,7,8},
                new [] {0,3,6}, new [] {1,4,7}, new [] {2,5,8},
                new [] {0,4,8}, new [] {2,4,6}
            };
            foreach (var line in lines)
            {
                var first = marks[line[0]];
                if (first != Mark.None && line.All(i => marks[i] == first))
                {
                    return first;
                }
            }
            return Mark.None;
        }

        /// <summary>
        /// Returns the indices of boards a player may play on this turn.
        /// </summary>
        public IEnumerable<int> GetPlayableBoards()
        {
            if (ActiveBoard.HasValue)
            {
                yield return ActiveBoard.Value;
            }
            else
            {
                for (int i = 0; i < Boards.Length; i++)
                {
                    if (Boards[i].Winner == Mark.None && !Boards[i].IsFull)
                    {
                        yield return i;
                    }
                }
            }
        }
    }
}
