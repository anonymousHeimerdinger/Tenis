using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data
{
    class ProbabilityWinLauncher : ProbabilityWin{

        public ProbabilityWinLauncher() : base(){ }    

        public override int getProbabilityWin(){
            return 62;
        }
    }
}
