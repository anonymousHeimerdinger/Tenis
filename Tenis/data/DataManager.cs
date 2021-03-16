using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data.abstractClasses;

namespace Tenis.data
{
    class DataManager
    {
        private List<TennisPlayer> players;
        private int sets;
        private TennisReferee tennisReferee;
        private int countNumPlayers;

        public DataManager() {
            this.players = new List<TennisPlayer>();
            this.sets = 0;
            this.tennisReferee = new TennisReferee();
            this.countNumPlayers = 0;
        }

        public void createPlayer(String name) {
            this.players.Add(new TennisPlayer(name, this.countNumPlayers++)); // players with number 0 and 1
        }

        public int Sets {
            get => this.sets;
            set => this.sets = value;
        }

        public int NumberPlayers {
            get => this.players.Count;
        }

        public TennisReferee TennisReferee
        {
            get => this.tennisReferee;
        }

        public void setEventReferee(EventReferee eventReferee)
        {
            this.tennisReferee.EventReferee = eventReferee;
        }

        public List<TennisPlayer> Players {
            get => this.players;
        }

        public int CountNumPlayers{
            get => this.countNumPlayers;
            set => this.countNumPlayers = value;
        }

    }
}
