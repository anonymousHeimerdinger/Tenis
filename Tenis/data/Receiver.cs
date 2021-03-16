using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data
{
    class Receiver:TennisPlayer
    {

        public Receiver() : base(new ProbabilityWinReceiver()) { }
        public Receiver(String name) : base(name, new ProbabilityWinReceiver()) { }
        public Receiver(String name, int numberPlayer) : base(name, numberPlayer, new ProbabilityWinReceiver()) { }
        public Receiver(String name, int numberPlayer, ProbabilityWin probabilityWin) : base(name, numberPlayer, probabilityWin) { }
    }
}
