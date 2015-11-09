using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe.Classes
{
    internal class Ai
    {
        //TODO DESIGN CLASS
        private readonly Tictactoe _tictactoe;

        //TODO TEST STUFF, DELETE AFTER IMPLEMENTATION
        private readonly Random _rnd = new Random();

        public Ai(Tictactoe tictactoe)
        {
            _tictactoe = tictactoe;
        }

        public void PerformMove(int x, int y)
        {
            while (_tictactoe.IsBoardFieldEmpty(x, y))
            {
                _tictactoe.PlaceMarker(x, y);
                break;
            }
        }
    }
}
