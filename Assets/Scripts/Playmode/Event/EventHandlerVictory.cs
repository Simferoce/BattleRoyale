using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playmode.Event
{
    class EventHandlerVictory : EventChannel<VictoryData>
    {
    }

    public struct VictoryData
    {
        public string WinnerName { get; private set; }
        public VictoryData(string winnerName)
        {
            this.WinnerName = winnerName;
        }
    }
}
