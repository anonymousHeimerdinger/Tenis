using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data
{
    class Launcher : TennisPlayer{

        public Launcher() : base(new ProbabilityWinLauncher()) {}
        public Launcher(String name) : base(name, new ProbabilityWinLauncher()) { }
        public Launcher(String name, int numberPlayer) : base(name, numberPlayer, new ProbabilityWinLauncher()) { }
        public Launcher(String name, int numberPlayer, ProbabilityWin probabilityWin) : base(name, numberPlayer, probabilityWin) { }


    }
}
