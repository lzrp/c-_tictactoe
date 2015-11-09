using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tictactoe.Events
{
    public delegate void ComputersMoveEventHandler(object source, ComputersMoveEventArgs e);

    public class ComputersMoveEventArgs : EventArgs
    {
        private readonly string _computerPlayerMark;

        public ComputersMoveEventArgs(string computerPlayerMark)
        {
            _computerPlayerMark = computerPlayerMark;
        }

        public string GetEventInfo()
        {
            return _computerPlayerMark;
        }
    }
}
