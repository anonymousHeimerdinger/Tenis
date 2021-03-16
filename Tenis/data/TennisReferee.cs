using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data{
    class TennisReferee: Referee{
        public TennisReferee() : base(){ }
        public TennisReferee(EventReferee eventReferee) : base(eventReferee) { }
        public TennisReferee(String name) : base(name) { }
        public TennisReferee(String name, EventReferee eventReferee) : base(name, eventReferee) { }
    }
}
