using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace tictactoe.Classes
{
    internal class Ai
    {
        //TODO DESIGN CLASS
        private readonly Tictactoe _tictactoe;
        private readonly Random _random = new Random();

        public enum AiDifficulty
        {
            Easy = 0,
            Medium = 1,
            Impossible = 2
        }
        public struct Move
        {
            public int X;
            public int Y;
            public int Value;
        }

        //TODO TEST STUFF, DELETE AFTER IMPLEMENTATION
        private readonly Random _rnd = new Random();

        public Ai(Tictactoe tictactoe)
        {
            _tictactoe = tictactoe;
        }

        public void PerformMove(Move move)
        {
            if (_tictactoe.GameInProgress)
            {
                _tictactoe.PlaceMarker(move.X, move.Y);
            }
            
        }

        public Move ComputeMoveValue(string[,] board)
        {
            var x = 0;
            var y = 0;

            for (var i = 0; i < 3; i++)
            {
                for (var j = 0; j < 3; j++)
                {
                    if (_tictactoe.IsBoardFieldEmpty(i,j))
                    {
                        x = i;
                        y = j;
                        break;
                    }
                }
            }

            return new Move() {X = x, Y = y};
        }
    }
}
