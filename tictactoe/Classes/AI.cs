﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace tictactoe.Classes
{
    public class Ai
    {
        private const string EmptyField = " ";
        private const string CrossMark = "X";
        private const string CircleMark = "O";
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
            if (ticTacToeBoard == null || randomGenerator == null)
            {
                throw new ArgumentNullException(nameof(ticTacToeBoard), nameof(randomGenerator));
            }

            Board = ticTacToeBoard;
            RandomGenerator = randomGenerator;
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
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            return board[x, y] == EmptyField;
        }

        /// <summary>
        /// Performs the computers move.
        /// </summary>
        /// <returns>A move structure with the coordinates of the move and its value based on difficulty.</returns>
        public Move GetMove(string playerMark, int aiDifficulty)
        {

            // Throw an exception if the players mark doesnt match a cross or a circle mark
            if (playerMark != CircleMark && playerMark != CrossMark)
            {
                throw new ArgumentException();
            }

            switch (aiDifficulty)
            {
                case (int) AiDifficulty.Easy:
                    return GenerateEasyDifficultyMove();
                case (int) AiDifficulty.Medium:
                    return GenerateMediumDifficultyMove();
                case (int) AiDifficulty.Impossible:
                    return GenerateImpossibleDifficultyMove(Board, playerMark);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Generate a predictable move.
        /// </summary>
        /// <returns>Returns a move object with defined X, Y coordinates</returns>
        private Move GenerateEasyDifficultyMove()
        {
            // This AI uses always the same strategy, filling the board from bottom right to the left and upwards.
            // Loop through all board fields, pick the first one which is empty
            Move easyMove = new Move();
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check for an empty field
                    if (!IsBoardFieldEmpty(Board, i, j)) continue;

                    // Assign coordinates if found
                    easyMove.X = i;
                    easyMove.Y = j;
                }
            }

            return easyMove;
        }

        /// <summary>
        /// Generates a random, unpredictable move.
        /// </summary>
        /// <returns>Returns a move object with defined X, Y coordinates.</returns>
        private Move GenerateMediumDifficultyMove()
        {
            bool moveIsFound = false;
            Move mediumMove = new Move();

            while (!moveIsFound)
            {
                // Generate random coordinates
                mediumMove.X = RandomGenerator.Next(0, 3);
                mediumMove.Y = RandomGenerator.Next(0, 3);

                // Check if the field is empty
                if (IsBoardFieldEmpty(Board, mediumMove.X, mediumMove.Y))
                {
                    // Indicate that a move is found and exit the loop
                    moveIsFound = true;
                }
            }

            return mediumMove;
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
            string oponentPlayerMark = (playerMark == CrossMark) ? CircleMark : CrossMark;
            

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
                        int value = -GenerateImpossibleDifficultyMove(board, oponentPlayerMark).Value;
                        
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
