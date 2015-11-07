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
        private readonly Random _randomNumberGenetator = new Random();
        private readonly Tictactoe _tictactoe;

        public Ai(Tictactoe tictactoe)
        {
            _tictactoe = tictactoe;
        }

        public void PerformMove(int x, int y)
        {
            _tictactoe.PlaceMarker(x,y);
        }
    }
}
