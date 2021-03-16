using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.data.abstractClasses{
    abstract class Referee: Person{
        private EventReferee eventReferee;

        public Referee():base(){
            this.eventReferee = new EventReferee();
        }

        public Referee(EventReferee eventReferee):base(){
            this.eventReferee = eventReferee;
        }

        public Referee(String name) :base(name){
            this.eventReferee = new EventReferee();
        }

        public Referee(String name, EventReferee eventReferee):base(name)
        {
            this.eventReferee = eventReferee;
        }

        public void refereeIndicateNewEvent(String text)
        {
            this.eventReferee.refereeIndicate(text);
        }

        public EventReferee EventReferee{
            get => this.eventReferee;
            set => this.eventReferee = value;
        }

    }

    class StringEventArgs : EventArgs
    {
        public String Text { get; set; }
    }

    /*
        Event used to sending text through the Class StringEventArgs from a guest class.
    */

    class EventReferee
    {

        public delegate void refereeEventHandler(object sender, StringEventArgs args);

        public event refereeEventHandler refereeEvent;

        public void refereeIndicate(String textString)
        {
            refereeEvent(this, new StringEventArgs { Text = textString });
        }

        public EventReferee()
        {

        }

    }
}
