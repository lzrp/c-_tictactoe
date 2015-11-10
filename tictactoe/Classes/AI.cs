using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe.Classes
{
    internal class Ai
    {
        #region class members

        private const string EmptyValue = " ";
        private readonly Random _random = new Random();
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
        #endregion


        public Ai(string[,] ticTacToeBoard)
        {
            Board = ticTacToeBoard;
        }

        public bool IsBoardFieldEmpty(int x, int y)
        {
            return Board[x, y] == EmptyValue;
        }

        /// <summary>
        /// Computes the move value from its coordinates.
        /// </summary>
        /// <returns>A move structure with the coordinates of the move and its value based on difficulty.</returns>
        public Move ComputeMoveValue()
        {
            int x = 0;
            int y = 0;

            // If the AI is set to easy
            // This AI uses always the same strategy, filling the board from bottom left to the right and upwards.
            if (Properties.Settings.Default.DifficultySetting == (int)AiDifficulty.Easy)
            {
                // Loop through all board fields, pick the first one which is empty
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check for an empty field
                        if (IsBoardFieldEmpty(i, j)) continue;

                        // Assign coordinates if found
                        x = i;
                        y = j;
                        break;
                    }
                }
            }

            // If the AI is set to medium
            // This AI uses random and unpredictable moves.
            if (Properties.Settings.Default.DifficultySetting == (int)AiDifficulty.Medium)
            {
                bool moveFound = false;

                while (!moveFound)
                {
                    // Generate random coordinates
                    x = _random.Next(0, 3);
                    y = _random.Next(0, 3);

                    // Check if the field is empty
                    if (IsBoardFieldEmpty(x, y))
                    {
                        // Indicate that a move is found and exit the loop
                        moveFound = true;
                    }
                }

            }

            // If the AI is set to impossible
            // This AI uses negamax algorithm to find the optimal move. Playing against this AI will ALWAYS result in a lost game or a draw.
            if (Properties.Settings.Default.DifficultySetting == (int)AiDifficulty.Impossible)
            {

            }

            return new Move() {X = x, Y = y};
        }
    }
}
