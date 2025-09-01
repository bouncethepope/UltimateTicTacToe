using System.Linq;

namespace UltimateTicTacToe
{
    /// <summary>
    /// Represents a single 3x3 tic-tac-toe board.
    /// </summary>
    public class SmallBoard
    {
        private readonly Mark[] _cells = new Mark[9];

        /// <summary>Winner of the board, if any.</summary>
        public Mark Winner { get; private set; } = Mark.None;

        /// <summary>
        /// Returns true when all cells are filled.
        /// </summary>
        public bool IsFull => _cells.All(c => c != Mark.None);

        /// <summary>
        /// Attempts to place a mark in the specified cell.
        /// </summary>
        /// <param name="index">Cell index 0-8.</param>
        /// <param name="mark">Mark to place.</param>
        /// <returns>True if move is applied.</returns>
        public bool ApplyMove(int index, Mark mark)
        {
            if (index < 0 || index >= 9) return false;
            if (mark == Mark.None) return false;
            if (_cells[index] != Mark.None) return false;
            if (Winner != Mark.None) return false;

            _cells[index] = mark;
            if (CheckWin(mark))
            {
                Winner = mark;
            }
            return true;
        }

        private bool CheckWin(Mark mark)
        {
            int[][] lines = new int[][]
            {
                new [] {0,1,2}, new [] {3,4,5}, new [] {6,7,8}, // rows
                new [] {0,3,6}, new [] {1,4,7}, new [] {2,5,8}, // cols
                new [] {0,4,8}, new [] {2,4,6}                  // diagonals
            };
            foreach (var line in lines)
            {
                if (line.All(i => _cells[i] == mark))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the mark in a given cell.
        /// </summary>
        public Mark GetCell(int index) => _cells[index];
    }
}
