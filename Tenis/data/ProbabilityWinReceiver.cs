using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data
{
    class ProbabilityWinReceiver : ProbabilityWin{
         
        public ProbabilityWinReceiver() : base(){ }
        
        public override int getProbabilityWin(){
            return 38;
        }
    }
}
