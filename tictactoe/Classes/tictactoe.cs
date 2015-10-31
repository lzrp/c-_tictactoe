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

        public string GetCurrentTurnPlayer()
        {
            return Turn ? "X" : "O";
        }

        public void NewGame(IEnumerable<Button> buttonList )
        {
            foreach (var button in buttonList)
            {
                    button.Content = "";
            }

            Turn = true;
        }

        public void NextTurn()
        {
            Turn = !Turn;
        }
        

    }
}
