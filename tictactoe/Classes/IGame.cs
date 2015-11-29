using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace tictactoe.Classes
{
    public interface IGame
    {
        void StartNewGame();
        void NextTurn();
        void AnnounceWinner(string player);
        void AnnounceDraw();
        void PlaceMarker(Button button);
        void PlaceMarker(int x, int y);
        
        bool UserWantsToStartNewGame();
        bool IsBoardFieldEmpty(Button button);
        bool IsBoardFieldEmpty(int x, int y);

        string GetCurrentTurnPlayerMark();


    }
}