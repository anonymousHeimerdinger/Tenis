using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data;
using Tenis.data.abstractClasses;

namespace Tenis.controller{
    class GameManager{
        
        // testing Git to delete

        private DataManager dataManager;
        private const int numberPlayersTenis = 2;
        private Boolean isReadyInitGame;

        public GameManager(){
            this.isReadyInitGame = false;
            this.dataManager = new DataManager();
        }

        public int getNumberPlayers(){
            return this.dataManager.NumberPlayers;
        }

        private void initPlayers(List<String> namePlayers)
        {
            if(numberPlayersTenis == namePlayers.Count)
            {
                foreach(String name in namePlayers){
                    this.addPlayer(name);
                }
            }else{
                throw new Exception("error codigo 001, desigualdad en la cantidad de jugadores que jugaran la partida");
            }
        }

        private void addPlayer(String name)
        {
            this.dataManager.createPlayer(name);
        }

        private int getSets(){
            return this.dataManager.Sets;
        }

        private void setSets(int sets){
            this.dataManager.Sets = sets;
        }

        public List<TennisPlayer> getPlayers(){
            return this.dataManager.Players;
        }

        public TennisReferee getTennisReferee(){
            return this.dataManager.TennisReferee;
        }


        public void initComponents(String namePlayer1,String namePlayer2,int sets, EventReferee eventReferee)
        {
            List<String> namePlayers = new List<string>();
            namePlayers.Add(namePlayer1);
            namePlayers.Add(namePlayer2);
            this.initPlayers(namePlayers);
            this.setSets(sets);
            this.dataManager.setEventReferee(eventReferee);
            this.isReadyInitGame = true;
        }

        public void initGame(){
            if (this.isReadyInitGame == true)
            {
                TennisGame game = new TennisGame(this.getPlayers(),this.getTennisReferee());
                game.initGame(this.getSets());
            }else{
                throw new Exception("error interno: el controlador no esta listo para iniciar el juego");
            }
        }

        public Boolean IsReadyInitGame{
            get => this.isReadyInitGame;
        }


    }
}
