using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace tictactoe.Classes
{
    public interface IGame
    {
        void NewGame(IEnumerable<Button> buttonList);
        void NextTurn();
        void PlaceMarker(Button button);
        void RestartGame();

        bool CheckWinner(char[,] board);
        bool AskForNewGame();

        string GetCurrentTurnPlayer();


    }
}