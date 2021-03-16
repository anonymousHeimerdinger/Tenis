using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data{
    class TennisPlayer: Player{

        ProbabilityWin probabilityWin;
        
        public TennisPlayer() : base() {
            this.probabilityWin = new ProbabilityWinPlayerDefault();
        }

        public TennisPlayer(ProbabilityWin probabilityWin) : base(){
            this.probabilityWin = probabilityWin;
        }

        public TennisPlayer(String name) : base(name){
            this.probabilityWin = new ProbabilityWinPlayerDefault();
        }

        public TennisPlayer(String name, ProbabilityWin probabilityWin) : base(name){
            this.probabilityWin = probabilityWin;
        }

        public TennisPlayer(String name, int numberPlayer): base(name, numberPlayer){
            this.probabilityWin = new ProbabilityWinPlayerDefault();
        }

        public TennisPlayer(String name, int numberPlayer, ProbabilityWin probabilityWin) : base(name, numberPlayer){
            this.probabilityWin = probabilityWin;
        }
        

        public int getWinProbability(){
            return this.probabilityWin.getProbabilityWin();                      //using 'inyección de dependencias'
        }

        public void setProbabilityWin(ProbabilityWin probabilityWin){
            this.probabilityWin = probabilityWin;                       
        }

    }
    
}
