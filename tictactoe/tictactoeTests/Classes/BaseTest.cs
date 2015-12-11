using System.Collections.Generic;
using System.Windows.Controls;
using tictactoeTests.Properties;

namespace tictactoeTests.Classes
{
    public class BaseTest
    {
        protected List<Button> BoardButtons = new List<Button>
        {
            new Button() {Tag = "00"},
            new Button() {Tag = "01"},
            new Button() {Tag = "02"},
            new Button() {Tag = 10},
            new Button() {Tag = 11},
            new Button() {Tag = 12},
            new Button() {Tag = 20},
            new Button() {Tag = 21},
            new Button() {Tag = 22}
        };

        protected string [,] BoardFirstRowAllCross =
        {
                {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardSecondRowAllCross =
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardThirdRowAllCross =
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                }
        };

        protected string[,] BoardFirstRowAllCircle = 
        {
                {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardSecondRowAllCircle =
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardThirdRowAllCircle =
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                }
        };

        protected string[,] BoardFirstColumnAllCross =
        {
            {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardSecondColumnAllCross =
        {
            {
                Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardThirdColumnAllCross =
        {
            {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
                }
        };

        protected string[,] BoardFirstColumnAllCircle =
        {
            {
                Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardSecondColumnAllCircle =
        {
            {
                Resources.BoardEmptyField,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardThirdColumnAllCircle =
        {
            {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
                }
        };

        protected string [,] BoardTopLeftBottomRightDiagonalAllCross =
        {
                {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
                }
        };

        protected string[,] BoardTopRightBottomLeftDiagonalAllCross =
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCrossMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardTopLeftBottomRightDiagonalAllCircle =
        {
                {
                Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
                }
        };

        protected string[,] BoardTopRightBottomLeftDiagonalAllCircle = 
        {
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardCircleMark,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardAllEmptyField = 
        {
            {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                },
                {
                Resources.BoardEmptyField,
                Resources.BoardEmptyField,
                Resources.BoardEmptyField
                }
        };

        protected string[,] BoardAllCross =
        {
            {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                },
                {
                Resources.BoardCrossMark,
                Resources.BoardCrossMark,
                Resources.BoardCrossMark
                }
        };

        protected string[,] BoardAllCircle =
        {
            {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                },
                {
                Resources.BoardCircleMark,
                Resources.BoardCircleMark,
                Resources.BoardCircleMark
                }
        };

        protected List<Button> GetBoardButtons()
        {
            return BoardButtons;
        }

        protected string[,] GetBoardFirstRowAllCross()
        {
            return BoardFirstRowAllCross;
        }

        protected string[,] GetBoardSecondRowAllCross()
        {
            return BoardSecondRowAllCross;
        }

        protected string[,] GetBoardThirdRowAllCross()
        {
            return BoardThirdRowAllCross;
        }

        protected string[,] GetBoardFirstRowAllCircle()
        {
            return BoardFirstRowAllCircle;
        }
        protected string[,] GetBoardSecondRowAllCircle()
        {
            return BoardSecondRowAllCircle;
        }
        protected string[,] GetBoardThirdRowAllCircle()
        {
            return BoardThirdRowAllCircle;
        }

        protected string[,] GetBoardFirstColumnAllCross()
        {
            return BoardFirstColumnAllCross;
        }

        protected string[,] GetBoardSecondColumnAllCross()
        {
            return BoardSecondColumnAllCross;
        }

        protected string[,] GetBoardThirdColumnAllCross()
        {
            return BoardThirdColumnAllCross;
        }

        protected string[,] GetBoardFirstColumnAllCircle()
        {
            return BoardFirstColumnAllCircle;
        }

        protected string[,] GetBoardSecondColumnAllCircle()
        {
            return BoardSecondColumnAllCircle;
        }

        protected string[,] GetBoardThirdColumnAllCircle()
        {
            return BoardThirdColumnAllCircle;
        }

        protected string[,] GetBoardTopLeftBottomRightDiagonalAllCross()
        {
            return BoardTopLeftBottomRightDiagonalAllCross;
        }

        protected string[,] GetBoardTopLeftBottomRightDiagonalAllCircle()
        {
            return BoardTopLeftBottomRightDiagonalAllCircle;
        }

        protected string[,] GetBoardTopRightBottomLeftDiagonalAllCross()
        {
            return BoardTopRightBottomLeftDiagonalAllCross;
        }

        protected string[,] GetBoardTopRightBottomLeftDiagonalAllCircle()
        {
            return BoardTopRightBottomLeftDiagonalAllCircle;
        }

        protected string[,] GetBoardAllEmptyField()
        {
            return BoardAllEmptyField;
        }

        protected string[,] GetBoardAllCross()
        {
            return BoardAllCross;
        }

        protected string[,] GetBoardAllCircle()
        {
            return BoardAllCircle;
        }
    }
}
