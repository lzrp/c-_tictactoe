using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe.Classes
{
    internal class Tictactoe : IGame
    {
        #region class members
        private const char Emptyfield = ' ';
        private const int BoardSizeHorizontal = 3;
        private const int BoardSizeVertical = 3;

        public bool Turn { get; private set; } = true;
        public bool GameInProgress { get; private set; } = false;
        public bool PlayerStartsFirst { get; private set; } = true;

        public int BoardFieldsLeftCounter { get; private set; } = 9;

        public char[,] Board { get; private set; } = new char[BoardSizeHorizontal,BoardSizeVertical];
        //public List<Button> ButtonCollection { get; private set; }
        public IEnumerable<Button> ButtonCollection { get; private set; }
        #endregion

        #region class methods

        public Tictactoe(IEnumerable<Button> buttonList)
        {
            ButtonCollection = buttonList;
            NewGame();
            //ButtonCollection = buttonList as List<Button>;
        }

        /// <summary>
        /// Displays a dialog asking if the user wants to play a new game.
        /// </summary>
        /// <returns>Bool result true if the user selected yes.</returns>
        public bool AskForNewGame()
        {
            var continueBoxResult = MessageBox.Show("Do you want to start a new game?", "New game?", MessageBoxButton.YesNo);

            return continueBoxResult == MessageBoxResult.Yes;
        }

        /// <summary>
        /// Disables all buttons.
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
        /// <returns>Bool result indicating whether the field is empty.</returns>
        public bool IsBoardFieldEmpty(Button button)
        {
            return button.Content.ToString() == " ";
        }

        /// <summary>
        /// Checks if the board field is empty at the [x,y] coordinates.
        /// </summary>
        /// <param name="x">X coordinate of the board field.</param>
        /// <param name="y">Y coordinate of the board field.</param>
        /// <returns></returns>
        public bool IsBoardFieldEmpty(int x, int y)
        {
            return Board[x, y] == ' ';
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
        /// <param name="buttonList">Buttons used to represent the board.</param>
        public void NewGame()
        {
            GameInProgress = true;
            BoardFieldsLeftCounter = 9;
            //ButtonCollection = buttonList as List<Button>;

            // Reset all buttons and board fields
            foreach (var button in ButtonCollection)
            {
                var buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                var buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                Board[buttonHorizontalPosition, buttonVerticalPosition] = Emptyfield;

                button.Content = Emptyfield;
                button.IsEnabled = true;
            }

            //set the turn to true so that X starts
            Turn = true;
        }

        /// <summary>
        /// Advance to the next turn.
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
            BoardFieldsLeftCounter--;
            Board[x, y] = char.Parse(GetCurrentTurnPlayer());
        }

        /// <summary>
        /// Places a marker at the specified position on a button and into the board.
        /// </summary>
        /// <param name="button">Pressed button indicating the position on the board.</param>
        public void PlaceMarker(Button button)
        {
            //Return immediately when the game ended or havent started yet.
            if (!GameInProgress) return;

            var currentPlayer = GetCurrentTurnPlayer();

            //Check if the button is not taken yet, place marker if not
            if (IsBoardFieldEmpty(button))
            {
                //Asign the currents player mark as the button content
                button.Content = currentPlayer;
                button.IsEnabled = false;

                var buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                var buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                PlaceMarker(buttonHorizontalPosition, buttonVerticalPosition);
                return;

            }

            //Otherwise show a warning dialog
            MessageBox.Show("You can't place your marker to an already marked field!", "Warning.", MessageBoxButton.OK);
        }

        /// <summary>
        /// Checks if the last move resulted in a player winning the game.
        /// </summary>
        /// <param name="board">Board representing the playing field array.</param>
        /// <returns>Bool result true if there is a winner.</returns>
        public bool CheckWinner(char[,] board)
        {
            //Check if a game is in progress
            if (!GameInProgress) return false;

            //Check board rows - 00=01=02 | 10=11=12 | 20=21=22
            for (var i = 0; i < BoardSizeVertical; i++)
            {
                if (Board[i, 0] != Board[i, 1] || Board[i, 1] != Board[i, 2] || Board[i, 0] == ' ') continue;
                return true;
            }

            //Check board columns - 00=10=20 | 01=11=21 | 02=12=22
            for (var i = 0; i < BoardSizeHorizontal; i++)
            {
                if (Board[0, i] != Board[1, i] || Board[1, i] != Board[2, i] || Board[0, i] == ' ') continue;
                return true;
            }

            //Check diagonals - 00=11=22 | 02=11=20
            return (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] || Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0]) && Board[1, 1] != ' ';
        }

        public bool GameStateCheck()
        {
            //Check for a winner
            if (CheckWinner(Board))
            {
                AnnounceWinner(GetCurrentTurnPlayer());

                if (AskForNewGame())
                {
                    NewGame();
                }

                return false;
            }

            //Else advance to the next turn
            else
            {
                //If there are no empty fields left, end the game in a draw
                if (BoardFieldsLeftCounter != 0) return true;
                AnnounceDraw();

                if (AskForNewGame())
                {
                    NewGame();
                }

                return false;
            }
        }

        /// <summary>
        /// Shows a message with the name of the winning player.
        /// </summary>
        /// <param name="player"></param>
        public void AnnounceWinner(string player)
        {
            //Stop the game and disable the buttons.
            StopGame();
            DisableButtons();
            MessageBox.Show("Congratulations, player " + player + " wins!", "Winner!", MessageBoxButton.OK);
        }

        /// <summary>
        /// Shows a message indicating that the players draw.
        /// </summary>
        public void AnnounceDraw()
        {
            //Stop the game and disable the buttons.
            StopGame();
            DisableButtons();
            MessageBox.Show("Players draw!","Draw.", MessageBoxButton.OK);
        }

        /// <summary>
        /// Stops the game.
        /// </summary>
        public void StopGame()
        {
            GameInProgress = false;
        }

        /// <summary>
        /// Get the buttons horizontal coordinate within the board given its tag.
        /// </summary>
        /// <param name="button">Button with a tag in a specific pattern. Tag = (int)horizontalCoordinate(int)verticalCoordinate. Example button.Tag = 01</param>
        /// <returns>Integer horizontal coordinate value</returns>
        public int GetButtonHorizontalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(0, 1));
        }

        /// <summary>
        /// Get the buttons vertical coordinate within the board given its tag.
        /// </summary>
        /// <param name="button">Button with a tag in a specific pattern. Tag = (int)horizontalCoordinate(int)verticalCoordinate. Example button.Tag = 01</param>
        /// <returns>Integer vertical coordinate value</returns>
        public int GetButtonVerticalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(1, 1));
        }
        #endregion
    }
}
