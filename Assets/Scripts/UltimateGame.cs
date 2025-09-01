namespace UltimateTicTacToe
{
    /// <summary>
    /// High level game state for Ultimate Tic-Tac-Toe.
    /// </summary>
    public class UltimateGame
    {
        public Mark CurrentPlayer { get; private set; } = Mark.X;
        public UltimateBoard Board { get; } = new UltimateBoard();

        /// <summary>
        /// Attempts to play a move for the current player.
        /// </summary>
        /// <returns>True if the move is valid.</returns>
        public bool Play(int boardIndex, int cellIndex)
        {
            if (!Board.ApplyMove(boardIndex, cellIndex, CurrentPlayer))
            {
                return false;
            }

            // Swap turn
            CurrentPlayer = (CurrentPlayer == Mark.X) ? Mark.O : Mark.X;
            return true;
        }
    }
}
