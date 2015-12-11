using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tictactoeTests.Properties;

namespace tictactoeTests.Classes
{
    public class BaseAiTest
    {
        private const string InvalidMark = "";

        private readonly string[,] _board =
        {
            {   Resources.BoardEmptyField,
                InvalidMark,
                Resources.BoardCrossMark
            },
            {   Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
            },
            {   Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                InvalidMark
            }
        };

        private readonly string[,] _boardPlayerStartsFirstOne =
        {
            {   Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardCrossMark
            }
            , { Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            }
            , { Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
            }
        };
        private readonly string[,] _boardPlayerStartsFirstTwo =
        {
            {   Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardCrossMark
            },
            {   Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            },
            {   Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
            }
        };
        private readonly string[,] _boardPlayerStartsFirstThree =
        { 
            {   Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardCrossMark
            },
            {   Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCrossMark
            },
            {   Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
            }
        };

        private readonly string[,] _boardAiStartsFirstOne =
        {
            { Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            },
            { Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
            },
            { Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            }
        };

        private readonly string[,] _boardAiStartsFirstTwo =
        {
            {   Resources.BoardCrossMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            },
            {   Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
            },
            {   Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
            }
        };

        private readonly string[,] _boardAiStartsFirstThree =
        {
            {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCircleMark
            },
            {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
            },
            {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField 
            }
        
        };

        private readonly Random _randomGenerator = new Random();
        private readonly MockRandom _mockRandomGenerator = new MockRandom();

        protected string[,] GetBoard()
        {
            return _board;
        }

        protected Random GetRandom()
        {
            return _randomGenerator;
        }

        protected MockRandom GetMockRandom()
        {
            return _mockRandomGenerator;
        }

        protected string[,] GetBoardPlayerStartsFirstOne()
        {
            return _boardPlayerStartsFirstOne;
        }

        protected string[,] GetBoardPlayerStartsFirstTwo()
        {
            return _boardPlayerStartsFirstTwo;
        }

        protected string[,] GetBoardPlayerStartsFirstThree()
        {
            return _boardPlayerStartsFirstThree;
        }

        protected string[,] GetBoardAiStartsFirstOne()
        {
            return _boardAiStartsFirstOne;
        }

        protected string[,] GetBoardAiStartsFirstTwo()
        {
            return _boardAiStartsFirstTwo;
        }

        protected string[,] GetBoardAiStartsFirstThree()
        {
            return _boardAiStartsFirstThree;
        }

    }
}
