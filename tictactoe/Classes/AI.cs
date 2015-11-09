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
        public struct Move
        {
            private int x;
            private int y;
            private int value;
        }

        //TODO TEST STUFF, DELETE AFTER IMPLEMENTATION
        private readonly Random _rnd = new Random();

        public Ai(Tictactoe tictactoe)
        {
            _tictactoe = tictactoe;
        }

        public void PerformMove(Move move)
        {

        }

        public Move ComputeMoveValue(char[,] bpard)
        {
            return new Move();
        }
    }
}
