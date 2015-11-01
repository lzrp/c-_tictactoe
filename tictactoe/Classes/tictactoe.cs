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
    class Tictactoe : IGame
    {
        private const char Emptyfield = ' ';

        public bool Turn { get; private set; } = true;
        public bool GameInProgress { get; private set; } = false;
        public bool PlayerStartsFirst { get; private set; } = true;

        private const int BoardSizeHorizontal = 3;
        private const int BoardSizeVertical = 3;

        public int BoardFieldsLeftCounter { get; private set; } = 9;

        public char[,] Board { get; private set; } = new char[BoardSizeHorizontal,BoardSizeVertical];
        public List<Button> ButtonCollection { get; private set; } 

        public bool AskForNewGame()
        {
            var continueBoxResult = MessageBox.Show("Do you want to start a new game?", "New game?", MessageBoxButton.YesNo);

            return continueBoxResult == MessageBoxResult.Yes;
        }

        public string GetCurrentTurnPlayer()
        {
            return Turn ? "X" : "O";
        }

        public void NewGame(IEnumerable<Button> buttonList )
        {
            GameInProgress = true;
            BoardFieldsLeftCounter = 9;
            ButtonCollection = buttonList as List<Button>;

            // Reset all buttons and board fields
            foreach (var button in buttonList)
            {
                var buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                var buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                Board[buttonHorizontalPosition, buttonVerticalPosition] = Emptyfield;

                button.Content = Emptyfield;
                button.IsEnabled = true;
            }

            Turn = true;
        }

        public void NextTurn()
        {
            Turn = !Turn;
        }

        public void PlaceMarker(int x, int y)
        {
            
        }

        public void PlaceMarker(Button button)
        {
            if (!GameInProgress) return false;

            var currentPlayer = GetCurrentTurnPlayer();

            //Check if the button is not taken yet
            if (button.Content.ToString() == " ")
            {
                button.Content = currentPlayer;
                button.IsEnabled = false;
                BoardFieldsLeftCounter--;

                var buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                var buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                Board[buttonHorizontalPosition, buttonVerticalPosition] = char.Parse(currentPlayer);

                return true;
            }

            MessageBox.Show("You can't place your marker to an already marked field!", "Warning.", MessageBoxButton.OK);
            return false;
        }

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

        public void AnnounceWinner(string player)
        {
            MessageBox.Show("Congratulations, player " + player + " wins!", "Winner!", MessageBoxButton.OK);
        }

        public void AnnounceDraw()
        {
            MessageBox.Show("Players draw!","Draw.", MessageBoxButton.OK);
        }

        public void StopGame()
        {
            GameInProgress = false;
        }
        
        public int GetButtonHorizontalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(0, 1));
        }

        public int GetButtonVerticalCoordinate(Button button)
        {
            return int.Parse(button.Tag.ToString().Substring(1, 1));
        }
    }
}
