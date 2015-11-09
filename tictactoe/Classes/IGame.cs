using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace tictactoe.Classes
{
    public interface IGame
    {
        void NewGame();
        void NextTurn();
        void AnnounceWinner(string player);
        void AnnounceDraw();
        void PlaceMarker(Button button);
        void PlaceMarker(int x, int y);

        bool CheckWinner(char[,] board);
        bool AskForNewGame();
        bool IsBoardFieldEmpty(Button button);
        bool IsBoardFieldEmpty(int x, int y);

        string GetCurrentTurnPlayer();


    }
}