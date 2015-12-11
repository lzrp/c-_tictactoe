using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using tictactoe.Properties;

namespace tictactoe.Classes
{
    public class Ai
    {
        private Random RandomGenerator { get; }

        public string[,] Board { get; }
        public enum AiDifficulty
        {
            Easy = 0,
            Medium = 1,
            Impossible = 2
        }
        public struct Move
        {
            public int X;
            public int Y;
            public int Value;
        }
        
        public Ai(string[,] ticTacToeBoard, Random randomGenerator)
        {
            if (ticTacToeBoard == null)
            {
                throw new ArgumentNullException(nameof(ticTacToeBoard));
            }

            if (randomGenerator == null)
            {
                throw new ArgumentNullException(nameof(randomGenerator));
            }

            Board = ticTacToeBoard;
            RandomGenerator = randomGenerator;
        }

        /// <summary>
        /// Checks if the board field is empty.
        /// </summary>
        /// <param name="board">String[,] board which to check.</param>
        /// <param name="x">Horizontal coordinate within the board.</param>
        /// <param name="y">Vertical coordinate within the board.</param>
        /// <returns></returns>
        public static bool IsBoardFieldEmpty(string[,] board, int x, int y)
        {
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            return board[x, y] == Resources.BoardEmptyField;
        }

        /// <summary>
        /// Gets the move to perform by the AI with a specific difficulty setting.
        /// </summary>
        /// <returns>A move structure with the coordinates of the move.</returns>
        public Move GetMove(string playerMark, int aiDifficulty)
        {
            // Throw an exception if the players mark doesnt match a cross or a circle mark
            if (playerMark != Resources.BoardCircleMark && playerMark != Resources.BoardCrossMark)
            {
                throw new ArgumentException();
            }

            // Generate a move according to the difficulty setting
            switch (aiDifficulty)
            {
                case (int) AiDifficulty.Easy:
                    return GenerateEasyDifficultyMove();
                case (int) AiDifficulty.Medium:
                    return GenerateMediumDifficultyMove();
                case (int) AiDifficulty.Impossible:
                    return GenerateImpossibleDifficultyMove(Board, playerMark);
                default:
                    throw new ArgumentOutOfRangeException(nameof(aiDifficulty));
            }
        }

        /// <summary>
        /// Generate a predictable move.
        /// </summary>
        /// <returns>Returns a move structure with defined X, Y coordinates</returns>
        private Move GenerateEasyDifficultyMove()
        {
            // This AI uses always the same strategy, filling the board from top left to the right and downwards.
            // Loop through all board fields, pick the first one which is empty
            var easyDifficultyMove = new Move();
            
            for (int i = 0; i < Tictactoe.GetBoardHorizontalSize(); i++)
            {
                for (int j = 0; j < Tictactoe.GetBoardVerticalSize(); j++)
                {
                    // Check for an empty field
                    if (!IsBoardFieldEmpty(Board, i, j)) continue;

                    // Assign coordinates if found
                    easyDifficultyMove.X = i;
                    easyDifficultyMove.Y = j;
                    return easyDifficultyMove;
                }
            }

            return easyDifficultyMove;
        }

        /// <summary>
        /// Generates a random, unpredictable move.
        /// </summary>
        /// <returns>Returns a move structure with defined X, Y coordinates.</returns>
        private Move GenerateMediumDifficultyMove()
        {
            bool isValidMoveFound = false;
            Move mediumDifficultyMove = new Move();

            while (!isValidMoveFound)
            {
                // Generate random coordinates
                mediumDifficultyMove.X = RandomGenerator.Next(0, Tictactoe.GetBoardHorizontalSize());
                mediumDifficultyMove.Y = RandomGenerator.Next(0, Tictactoe.GetBoardVerticalSize());

                // Check if the field is empty
                if (IsBoardFieldEmpty(Board, mediumDifficultyMove.X, mediumDifficultyMove.Y))
                {
                    isValidMoveFound = true;
                }
            }

            return mediumDifficultyMove;
        }

        /// <summary>
        /// Generates the best move available using the negamax algorithm.
        /// </summary>
        /// <param name="board">Board on which to do the calculations.</param>
        /// <param name="playerMark">Mark of the player who is calling the function.</param>
        /// <returns>An optimal move for the given board and player.</returns>
        /// Can be upgraded with alpha-beta pruning to increase performance.
        private  Move GenerateImpossibleDifficultyMove(string[,] board, string playerMark)
        {
            // Set the oponent players mark
            string oponentPlayerMark = (playerMark == Resources.BoardCrossMark) ? Resources.BoardCircleMark : Resources.BoardCrossMark;

            // Check if the player calling the function has won
            if (Tictactoe.HasPlayerWon(board, playerMark))
            {
                return new Move() {Value = 1};
            }

            // Check if the player calling the function has lost
            if (Tictactoe.HasPlayerWon(board, oponentPlayerMark))
            {
                return new Move() {Value = -1};
            }

            // Check for a draw
            if (Tictactoe.HavePlayersDraw(board))
            {
                return new Move() {Value = 0};
            }

            // Set initial bestMove value to -2 because thats lower than the lowest possible value you can generate for a valid move from this implementation (which is -1)
            var bestMove = new Move() { Value = -2 };

            // Loop through the board
            for (int i = 0; i < Tictactoe.GetBoardHorizontalSize(); i++)
            {
                for (int j = 0; j < Tictactoe.GetBoardVerticalSize(); j++)
                {
                    if (IsBoardFieldEmpty(board, i, j))
                    {
                        // Mark the board field
                        board[i, j] = playerMark;

                        // Assign a new value to the opposite of a recursive function call
                        int value = -GenerateImpossibleDifficultyMove(board, oponentPlayerMark).Value;
                        
                        // Store move information if it has better value than the current best move
                        if (value > bestMove.Value)
                        {
                            bestMove.Value = value;
                            bestMove.X = i;
                            bestMove.Y = j;
                        }

                        // Change the board field back to empty
                        board[i, j] = Resources.BoardEmptyField;
                    }
                }
            }
            
            return bestMove;
        }
    }
}
