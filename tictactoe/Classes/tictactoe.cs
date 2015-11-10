using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using static tictactoe.Classes.Ai;

namespace tictactoe.Classes
{
    internal class Tictactoe : IGame
    {
        #region class members

        private const string EmptyField = " ";
        private const int BoardSizeHorizontal = 3;
        private const int BoardSizeVertical = 3;

        public bool Turn { get; private set; }
        public bool GameInProgress { get; private set; }
        public int BoardFieldsLeftCounter { get; private set; } = 9;
        public Ai ComputerPlayerAi { get; private set; }

        public string[,] Board { get; } = new string[BoardSizeHorizontal, BoardSizeVertical];
        public IEnumerable<Button> ButtonCollection { get; private set; }

        #endregion

        #region class methods

        /// <summary>
        /// Creates a new instance of the Tictactoe class.
        /// </summary>
        /// <param name="buttonList">Button collection representing the playing board.</param>
        public Tictactoe(IEnumerable<Button> buttonList)
        {
            // Assign the button collection
            ButtonCollection = buttonList;

            // Create AI player
            ComputerPlayerAi = new Ai(Board);

            // Start a new game
            NewGame();
        }

        /// <summary>
        /// Gets the maximum horizontal size of the game board.
        /// </summary>
        /// <returns></returns>
        public static int GetBoardMaxHorizontalSize()
        {
            return BoardSizeHorizontal;
        }

        /// <summary>
        /// Gets the maximum vertical size of the game board.
        /// </summary>
        /// <returns></returns>
        public static int GetBoardMaxVerticalSize()
        {
            return BoardSizeVertical;
        }

        /// <summary>
        /// Displays a dialog asking if the user wants to play a new game.
        /// </summary>
        /// <returns>Bool result true if the user selected yes.</returns>
        public bool AskForNewGame()
        {
            var continueBoxResult = MessageBox.Show("Do you want to start a new game?", "New game?",
                MessageBoxButton.YesNo);

            return continueBoxResult == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Disables all buttons in the games board button collection.
        /// </summary>
        private void DisableButtons()
        {
            foreach (var button in ButtonCollection)
            {
                button.IsEnabled = false;
            }
        }

        /// <summary>
        /// Checks if the board field is empty given the correspondings board field button.
        /// </summary>
        /// <param name="button">Button which board field is checked.</param>
        /// <returns>Bool result true if the field is empty.</returns>
        public bool IsBoardFieldEmpty(Button button)
        {
            return button.Content.ToString() == " ";
        }

        /// <summary>
        /// Checks if the board field is empty at the [x,y] coordinates.
        /// </summary>
        /// <param name="x">X coordinate of the board field.</param>
        /// <param name="y">Y coordinate of the board field.</param>
        /// <returns>Bool result true if the field is empty.</returns>
        public bool IsBoardFieldEmpty(int x, int y)
        {
            return Board[x, y] == " ";
        }

        /// <summary>
        /// Gets the string representation of the currents player marker.
        /// </summary>
        /// <returns>String representation of a players marker.</returns>
        public string GetCurrentTurnPlayer()
        {
            return Turn ? "X" : "O";
        }

        /// <summary>
        /// Resets game parameters, board fields and buttons representing the board.
        /// </summary>
        public void NewGame()
        {
            // Start the game and reset the empty fields counter
            GameInProgress = true;
            BoardFieldsLeftCounter = 9;

            // Reset all buttons and board fields
            foreach (var button in ButtonCollection)
            {
                // Reset board
                int buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                int buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                Board[buttonHorizontalPosition, buttonVerticalPosition] = EmptyField;

                // Reset buttons
                button.Content = EmptyField;
                button.IsEnabled = true;
            }

            // Set the turn to true so that X starts
            Turn = true;

            // If the AI is the first on turn, let it make the first move
            if (Properties.Settings.Default.PlayerStartsFirst || !Properties.Settings.Default.VsComputer) return;

            Move computerMove = ComputerPlayerAi.ComputeMoveValue(GetCurrentTurnPlayer());
            PlaceMarker(computerMove.X, computerMove.Y);

            // Update the UI and check the state of the game
            UpdateUi();
            GameStateCheck();
        }

        /// <summary>
        /// Restarts the game and updates the UI.
        /// </summary>
        public void RestartGame()
        {
            NewGame();
            UpdateUi();
        }

        /// <summary>
        /// Updates the board buttons.
        /// </summary>
        public void UpdateUi()
        {
            // Redraw the board buttons
            foreach (var button in ButtonCollection)
            {
                int x = GetButtonHorizontalCoordinate(button);
                int y = GetButtonVerticalCoordinate(button);

                button.Content = Board[x, y];

                // Disable the button if its marked by a player
                if (button.Content.ToString() != " ")
                {
                    button.IsEnabled = false;
                }
            }
        }

        /// <summary>
        /// Advances to the next turn.
        /// </summary>
        public void NextTurn()
        {
            Turn = !Turn;
        }

        /// <summary>
        /// Places a marker at a specified position into the board.
        /// </summary>
        /// <param name="x">The horizontal coordinate within the board.</param>
        /// <param name="y">The vertical coordinate within the board.</param>
        public void PlaceMarker(int x, int y)
        {
            // Adjust the fields left counter and mark the board field
            BoardFieldsLeftCounter--;
            Board[x, y] = GetCurrentTurnPlayer();
        }

        /// <summary>
        /// Places a marker at the specified position on a button and into the board.
        /// </summary>
        /// <param name="button">Pressed button indicating the position on the board.</param>
        public void PlaceMarker(Button button)
        {
            // Return immediately when the game ended or havent started yet
            if (!GameInProgress) return;

            string currentPlayer = GetCurrentTurnPlayer();

            // Check if the button is not taken yet, place marker if not
            if (IsBoardFieldEmpty(button))
            {
                // Asign the currents player mark as the button content
                button.Content = currentPlayer;
                button.IsEnabled = false;

                int buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                int buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                // Place the marker
                PlaceMarker(buttonHorizontalPosition, buttonVerticalPosition);
                return;
            }

            // Otherwise show a warning dialog
            MessageBox.Show("You can't place your marker to an already marked field!", "Warning.", MessageBoxButton.OK);
        }

        /// <summary>
        /// Checks if the last move resulted in a player winning the game.
        /// </summary>
        /// <param name="board">Board representing the playing field array.</param>
        /// <returns>Bool result true if there is a winner.</returns>
        public static bool CheckWinner(string[,] board, string mark)
        {
            // Check board rows, columns and diagonals. Also check for empty fields
            // Check board rows - 00=01=02 | 10=11=12 | 20=21=22
            for (int i = 0; i < BoardSizeVertical; i++)
            {
                if (board[i, 0] != board[i, 1] || board[i, 1] != board[i, 2] || board[i, 0] != mark) continue;
                return true;
            }

            // Check board columns - 00=10=20 | 01=11=21 | 02=12=22
            for (int i = 0; i < BoardSizeHorizontal; i++)
            {
                if (board[0, i] != board[1, i] || board[1, i] != board[2, i] || board[0, i] != mark) continue;
                return true;
            }

            // Check diagonals - 00=11=22 | 02=11=20
            return (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] ||
                    board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) && board[1, 1] == mark;
        }

        /// <summary>
        /// Checks for a draw.
        /// </summary>
        /// <param name="fieldsLeftCounter">Counter which keeps the number of free board fields.</param>
        /// <returns>A bool result true if there is a draw (zero remaining free fields).</returns>
        public static bool CheckForDraw(int fieldsLeftCounter)
        {
            return fieldsLeftCounter == 0;
        }

        /// <summary>
        /// Checks for a draw.
        /// </summary>
        /// <param name="board">A board which has to be checked.</param>
        /// <returns>A bool result true if the board state results in a draw (if there are no free fields left).</returns>
        public static bool CheckForDraw(string[,] board)
        {
            for (int i = 0; i < BoardSizeHorizontal; i++)
            {
                for (int j = 0; j < BoardSizeVertical; j++)
                {
                    if (board[i,j] != EmptyField)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    /// <summary>
        /// Checks for the state of the game.
        /// </summary>
        /// <returns>Returns a bool result true if the game state has not changed. (No wins or draws.)</returns>
        public bool GameStateCheck()
    {
        string currentPlayer = GetCurrentTurnPlayer();

            // Check for a winner
            if (CheckWinner(Board, currentPlayer))
            {
                StopGame();

                AnnounceWinner(currentPlayer);
                
                if (AskForNewGame())
                {
                    NewGame();
                }

                return false;
            }

            // If there are no empty fields left, end the game in a draw
            if (CheckForDraw(BoardFieldsLeftCounter))
            {
                StopGame();

                AnnounceDraw();

                if (AskForNewGame())
                {
                    NewGame();
                }

                return false;
            }

            // Else advance to the next turn
            NextTurn();
            return true;
        }

        /// <summary>
        /// Shows a message with the name of the winning player.
        /// </summary>
        /// <param name="player">The winning player.</param>
        public void AnnounceWinner(string player)
        {
            // Announce the winner
            MessageBox.Show("Congratulations, player " + player + " wins!", "Winner!", MessageBoxButton.OK);
        }

        /// <summary>
        /// Shows a message indicating that the players draw.
        /// </summary>
        public void AnnounceDraw()
        {
            // Announce draw
            MessageBox.Show("Players draw!","Draw.", MessageBoxButton.OK);
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        public void StopGame()
        {
            // Stop the game and disable all buttons.
            DisableButtons();
            GameInProgress = false;
        }
        /// <summary>
        /// Get the buttons horizontal coordinate within the board given its tag.
        /// </summary>
        /// <param name="button">Button with a Tag property in a specific pattern. Tag = (int)horizontalCoordinate(int)verticalCoordinate. Example: button.Tag = 01</param>
        /// <returns>Integer horizontal coordinate value</returns>
        public int GetButtonHorizontalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(0, 1));
        }
        /// <summary>
        /// Get the buttons vertical coordinate within the board given its tag.
        /// </summary>
        /// <param name="button">Button with a Tag property in a specific pattern. Tag = (int)horizontalCoordinate(int)verticalCoordinate. Example: button.Tag = 01</param>
        /// <returns>Integer vertical coordinate value</returns>
        public int GetButtonVerticalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(1, 1));
        }
        #endregion
    }
}
