using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.data.abstractClasses{
    abstract class Player: Person{

        private int numberPlayer;    

        public Player() : base(){
            this.numberPlayer = 0;
        }

        public Player(String name):base(name){
            this.numberPlayer = 0;
        }

        public Player(String name, int numberPlayer):base(name){
            this.numberPlayer = numberPlayer;
        }

        public int NumberPlayer{
            get => this.numberPlayer;
            set => this.numberPlayer = value;
        }
    }
}
