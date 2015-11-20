using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace tictactoe.Classes
{
    public class Ai
    {
        private const string EmptyField = " ";
        private const string CrossMark = "X";
        private const string CircleMark = "O";

        private readonly Random _random = new Random();
        public string[,] Board { get; }
        public enum AiDifficulty
        {
            Easy = 0,
            Medium = 1,
            Impossible = 2,
            NotSet = 3
        }
        public struct Move
        {
            public int X;
            public int Y;
            public int Value;
        }
        
        public Ai(string[,] ticTacToeBoard)
        {
            Board = ticTacToeBoard;
        }

        /// <summary>
        /// Checks if the board field is empty.
        /// </summary>
        /// <param name="board">Board which to check.</param>
        /// <param name="x">Horizontal coordinate within the board.</param>
        /// <param name="y">Vertical coordinate within the board.</param>
        /// <returns></returns>
        public static bool IsBoardFieldEmpty(string[,] board, int x, int y)
        {
            return board[x, y] == EmptyField;
        }

        /// <summary>
        /// Performs the computers move.
        /// </summary>
        /// <returns>A move structure with the coordinates of the move and its value based on difficulty.</returns>
        public Move PerformMove(string playerMark, int aiDifficulty)
        {
            int x = 0;
            int y = 0;

            // If the AI is set to easy
            // This AI uses always the same strategy, filling the board from bottom left to the right and upwards.
            if (aiDifficulty == (int)AiDifficulty.Easy)
            {
                // Loop through all board fields, pick the first one which is empty
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check for an empty field
                        if (!IsBoardFieldEmpty(Board, i, j)) continue;

                        // Assign coordinates if found
                        x = i;
                        y = j;
                        break;
                    }
                }
            }

            // If the AI is set to medium
            // This AI uses random and unpredictable moves.
            if (aiDifficulty == (int)AiDifficulty.Medium)
            {
                bool moveFound = false;

                while (!moveFound)
                {
                    // Generate random coordinates
                    x = _random.Next(0, 3);
                    y = _random.Next(0, 3);

                    // Check if the field is empty
                    if (IsBoardFieldEmpty(Board, x, y))
                    {
                        // Indicate that a move is found and exit the loop
                        moveFound = true;
                    }
                }

            }

            // If the AI is set to impossible
            // This AI uses negamax algorithm to find the optimal move. Playing against this AI will ALWAYS result in a lost game or a draw.
            if (aiDifficulty == (int)AiDifficulty.Impossible)
            {
                return Negamax(Board, playerMark);
            }

            // Display an error message if no AI difficulty is set
            if (aiDifficulty == (int)AiDifficulty.NotSet)
            {
                MessageBox.Show(
                    "The AI player doesn't have its difficulty set. Please choose an AI difficulty from the settings.",
                    "AI difficulty not set", MessageBoxButton.OK);
            }

            return new Move() { X = x, Y = y };
        }

        /// <summary>
        /// Computes the best move available through the negamax algorithm.
        /// </summary>
        /// <param name="board">Board on which to do the calculations.</param>
        /// <param name="playerMark">Mark of the player who is calling the function.</param>
        /// <returns>An optimal move for the given board and player.</returns>
        /// Can be upgraded with alpha-beta pruning to increase performance.
        private static Move Negamax(string[,] board, string playerMark)
        {
            // Set the oponent players mark
            string oponentPlayerMark = playerMark == CrossMark ? CircleMark : CrossMark;
            

            // Check if the player calling the function won
            if (Tictactoe.CheckWinner(board, playerMark))
            {
                //move.Value = 1;
                //return move;
                return new Move() {Value = 1};
            }

            // Check if the player calling the function lost
            if (Tictactoe.CheckWinner(board, oponentPlayerMark))
            {
                //move.Value = -1;
                //return move;
                return new Move() {Value = -1};
            }

            // Check for a draw
            if (Tictactoe.CheckForDraw(board))
            {
                //move.Value = 0;
                //return move;
                return new Move() {Value = 0};
            }

            // Initialize helper variables, set maxValue to -2 because thats lower than the possible value you can get from this implementation (which is -1)
            var bestMove = new Move() { Value = -2 };

            // Loop through the board
            for (int i = 0; i < Tictactoe.GetBoardSizeHorizontal(); i++)
            {
                for (int j = 0; j < Tictactoe.GetBoardSizeVertical(); j++)
                {
                    if (IsBoardFieldEmpty(board, i, j))
                    {
                        // Mark the board field
                        board[i, j] = playerMark;

                        // Assign a new value to the opposite of a recursive function call
                        // Needs to be negative because thats how the algorithm
                        // differentiates between two players - one who maximizes and the
                        // second one who minimizes
                        int value = -Negamax(board, oponentPlayerMark).Value;
                        
                        // Store move information if it has better value
                        if (value > bestMove.Value)
                        {
                            bestMove.Value = value;
                            bestMove.X = i;
                            bestMove.Y = j;
                        }

                        // Change the board field back to empty
                        board[i, j] = EmptyField;
                    }
                }
            }

            // Return the move with the best value
            return bestMove;
        }
        
    }
}
