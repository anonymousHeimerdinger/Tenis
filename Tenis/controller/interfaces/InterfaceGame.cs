using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.controller.interfaces{
    interface InterfaceGame{

        void initGame(int sets); // init game

        void indicateEventReferee(String text); // event that indicate any change in the Game
    }
}
