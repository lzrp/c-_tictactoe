using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe.Classes
{
    class Tictactoe : IGame
    {
        public bool Turn { get; private set; } = true;
        public bool GameInProgress { get; private set; } = false;
        public bool PlayerStartsFirst { get; private set; } = true;

        public char[,] Board { get; private set; } = new char[3,3];

        public bool AskForNewGame()
        {
            throw new NotImplementedException();
        }

        public string GetCurrentTurnPlayer()
        {
            return Turn ? "X" : "O";
        }

        public void NewGame(IEnumerable<Button> buttonList )
        {
            foreach (var button in buttonList)
            {
                var buttonHorizontalPosition = GetButtonHorizontalCoordinate(button);
                var buttonVerticalPosition = GetButtonVerticalCoordinate(button);

                Board[buttonHorizontalPosition, buttonVerticalPosition] = char.Parse("");

                button.Content = "";
                button.IsEnabled = true;
            }

            Turn = true;
        }

        public void NextTurn()
        {
            Turn = !Turn;
        }

        public void PlaceMarker(Button button)
        {
            throw new NotImplementedException();
        }

        public void RestartGame()
        {
            throw new NotImplementedException();
        }

        public bool CheckWinner(char[,] board)
        {
            throw new NotImplementedException();
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
