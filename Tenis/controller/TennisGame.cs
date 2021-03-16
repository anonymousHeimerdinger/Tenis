using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.data;
using Tenis.controller.interfaces;
using Tenis.data.abstractClasses;

namespace Tenis.controller{
    class TennisGame : InterfaceGame{

        private List<TennisPlayer> players; // List of players
        
        private TennisReferee referee; // referee
        private ProbabilityManager probabilityManager;
        private Marker marker;

        private TennisPlayer launcher;
        private TennisPlayer receiver;

        enum pointsTennis { zero, fifteen, thirty, forty, Ad };
        private const int NP = 2;
        public int[] VpositionPlayers;

        private int numGame;
        private int numSet;

        private int countChangesRol;

        public TennisGame(){
            players.Add(new TennisPlayer("player1"));
            players.Add(new TennisPlayer("player2"));
            this.referee = new TennisReferee();
            
            this.initComponents();
        }

        public TennisGame(EventReferee eventReferee)
        {
            players.Add(new TennisPlayer("player1"));
            players.Add(new TennisPlayer("player2"));
            this.referee = new TennisReferee(eventReferee);

            this.initComponents();
        }

        public TennisGame(List<TennisPlayer> players, TennisReferee referee)
        {
            this.players = players;
            this.referee = referee;

            this.initComponents();
        }

        private void initComponents(){
            this.probabilityManager = new ProbabilityManager(); 
        }

        public void indicateEventReferee(String textMatchState)
        {
            this.referee.refereeIndicateNewEvent(textMatchState);
        }

        private void resetAndFillStats(){
            if (this.isFirstPlayerTheLauncher()){
                this.launcher = new Launcher(this.players[0].Name, this.players[0].NumberPlayer);
                this.receiver = new Receiver(this.players[1].Name, this.players[1].NumberPlayer);
            }
            else{
                this.launcher = new Launcher(this.players[1].Name, this.players[1].NumberPlayer);
                this.receiver = new Receiver(this.players[0].Name, this.players[0].NumberPlayer);
            }          
        }

        public void initMarker(int numSets){
            this.marker = new Marker(numSets);
            this.VpositionPlayers = new int[NP];
            if (!((this.launcher.NumberPlayer == 0 && this.receiver.NumberPlayer == 1) || (this.launcher.NumberPlayer == 1 && this.receiver.NumberPlayer == 0))){
                this.launcher.NumberPlayer = 0;
                this.receiver.NumberPlayer = 1;
            }
            this.VpositionPlayers[this.launcher.NumberPlayer] = 0;
            this.VpositionPlayers[this.receiver.NumberPlayer] = 1;
        }

        public int getNumSets(){
            if(this.marker != null){
                return this.marker.NumSets;
            }
            return 0;
        }

        public void initGame(int numSets){

            //preparing components that it will be used during the game
            
            this.resetAndFillStats();
            this.initMarker(numSets);
            this.indicateEventReferee("\n" + this.getLauncher().Name + " iniciará el partido sacando\n");
            this.numSet = 1;
            int countSetsLastWinner;
            // begins the game
            do {
                this.numGame = 1;
                this.marker.HasFinishedLastSet = false;
                do {
                    this.indicateEventReferee("** Juego " + this.numGame + " - Set " + this.numSet);
                    this.playOneGame();
                } while (!this.marker.HasFinishedLastSet);
                if (((this.numGame-1) + this.countChangesRol) % 2 == 0){
                    this.changeRoles();
                }
                countSetsLastWinner = this.marker.getCountSetsLastWinner();
                this.countChangesRol = 0;
                ++this.numSet; 
            } while (!((countSetsLastWinner == 2 && this.marker.NumSets == 3) || (countSetsLastWinner == 3 && this.marker.NumSets == 5)));

            int positionWinner = this.marker.PositionPlayerWonLastGame;
            String name = positionWinner == this.VpositionPlayers[this.launcher.NumberPlayer] ? this.launcher.Name : this.receiver.Name; 
            String resultSets = this.marker.showResultSets();
            this.indicateEventReferee("!!" + name +  " GANA EL PARTIDO¡¡\t\t" + resultSets);

        }

        private void tieBreak(){
            this.marker.resetTieBreak();
            this.indicateEventReferee("Se ha producido un empate    " + this.marker.showResultSets() + "\n"
                +"Se jugara un Tie Break para lograr desempatar el set\n\nSe inicia el Tie Break\n");
            int countPoints = 0;
            Boolean hasTieBreakFinished = false;
            String namePlayer;
            int numberPlayer;

            do{
                if (countPoints % 2 == 0){
                    this.changeRoles();
                    this.indicateEventReferee("-> " + this.getLauncher().Name + " pasa a ser el lanzador en los dos próximos juegos");
                }if (this.isLauncherWon()){
                    hasTieBreakFinished = this.manageTieBreak(this.getLauncher());
                    namePlayer = this.getLauncher().Name;
                    numberPlayer = this.getLauncher().NumberPlayer;
                    this.indicateEventReferee("Punto para el lanzador, punto para " + namePlayer + "\t" + this.showResultTieBreak());
                }else{
                    hasTieBreakFinished = this.manageTieBreak(this.getReceiver());
                    namePlayer = this.getReceiver().Name;
                    numberPlayer = this.getReceiver().NumberPlayer;
                    this.indicateEventReferee("OOhhhh!!!, punto para " + namePlayer + "\t\t\t" + this.showResultTieBreak());
                }
                
                ++countPoints;
            } while (!hasTieBreakFinished);

            int position = this.VpositionPlayers[numberPlayer];
            this.marker.addWonGame(position, this.numSet-1);
            this.marker.savetPointsLoserTieBreak(this.numSet - 1);

            this.countChangesRol = countPoints / 2;
        }

        private String showResultTieBreak(){
            return this.marker.tieBreak[0] + "-" + this.marker.tieBreak[1];
        }

        private Boolean manageTieBreak(TennisPlayer playerWonPoint){
            Boolean hasWon = false;
            int position = this.VpositionPlayers[playerWonPoint.NumberPlayer];
            int pointsTB = ++this.marker.tieBreak[position];
            if (pointsTB >= 7){
                int pointsTBEnemy = this.marker.tieBreak[this.marker.getPositionPlayerEnemy(position)];
                if(pointsTB -2 >= pointsTBEnemy){
                    hasWon = true;
                }
            }
            return hasWon;
        }

        private void playOneGame(){
            Boolean hasWonTheGame;
            do{
                if (this.isLauncherWon()){
                    hasWonTheGame = this.managePoints(this.getLauncher());
                }else{
                    hasWonTheGame = this.managePoints(this.getReceiver());
                }
            } while (!hasWonTheGame);
        }

        private Boolean managePoints(TennisPlayer playerWonPoint){
            String name = playerWonPoint.Name;
            Boolean hasWon = false;
            int position = this.VpositionPlayers[playerWonPoint.NumberPlayer]; 
            pointsTennis pointsPlayer = this.marker.Vpoints[position]++;
            switch (pointsPlayer){                
                case pointsTennis.Ad:
                    hasWon = true;
                    // add game won at the winner 
                    this.marker.addWonGame(position, this.numSet-1);
                    if (this.manageSets(name)){
                        String typeGame;
                        if (this.marker.WasThereTieBreak) {
                            typeGame = "Tie Break";
                            this.marker.WasThereTieBreak = false;
                            int positionWinnerTieBreak = this.marker.PositionPlayerWonLastGame;
                            String nameWinnerTieBreak;
                            if((this.VpositionPlayers[this.getLauncher().NumberPlayer]) == positionWinnerTieBreak){
                                nameWinnerTieBreak = this.getLauncher().Name;
                            }else{
                                nameWinnerTieBreak = this.getReceiver().Name;
                            }
                            this.createEventSet(nameWinnerTieBreak, positionWinnerTieBreak, typeGame);
                        }else{
                            typeGame = "juego";
                            this.createEventSet(name, position, typeGame);
                        }
                        
                        this.marker.HasFinishedLastSet = true;
                    }else{
                        this.createEventGame(name);
                    }
                    break;
                case pointsTennis.forty:
                    int positionPlayerEnemy = this.marker.getPositionPlayerEnemy(position);
                    if(this.marker.Vpoints[positionPlayerEnemy] == pointsTennis.Ad){
                        --this.marker.Vpoints[position];
                        --this.marker.Vpoints[positionPlayerEnemy];
                        this.createEventPoints(name);
                    }else{
                        if(this.marker.Vpoints[positionPlayerEnemy] != pointsTennis.forty){
                            hasWon = true;
                            // add game won at the winner
                            this.marker.addWonGame(position, this.numSet-1);
                            if(this.manageSets(name)){
                                String typeGame;
                                if (this.marker.WasThereTieBreak){
                                    typeGame = "Tie Break";
                                    this.marker.WasThereTieBreak = false;
                                    int positionWinnerTieBreak = this.marker.PositionPlayerWonLastGame;
                                    String nameWinnerTieBreak;
                                    if ((this.VpositionPlayers[this.getLauncher().NumberPlayer]) == positionWinnerTieBreak){
                                        nameWinnerTieBreak = this.getLauncher().Name;
                                    }
                                    else{
                                        nameWinnerTieBreak = this.getReceiver().Name;
                                    }
                                    this.createEventSet(nameWinnerTieBreak, positionWinnerTieBreak, typeGame);
                                }else{
                                    typeGame = "juego";
                                    this.createEventSet(name, position, typeGame);
                                }

                                this.marker.HasFinishedLastSet = true;
                            }
                            else{
                                this.createEventGame(name);
                            } 
                        }else{
                            this.createEventPoints(name);
                        }
                    }
                    break;
                default:
                    this.createEventPoints(name);
                    break;
            }
            return hasWon;
        }

        private Boolean manageSets(String nameWinnerLastGame){
            Boolean hasWonSet = false;
            int wins = this.marker.getGamesWonOfTheLastWinner(this.numSet - 1);
            if (wins >= 6){
                if (wins == 6){
                    int winsLastLoser = this.marker.getGamesWonOfTheLastLoser(this.numSet - 1);
                    switch (winsLastLoser){
                        case 5:
                            // continue the set
                            this.changeRoles();
                            ++this.numGame;
                            break;
                        case 6:
                            this.createEventGame(nameWinnerLastGame);
                            this.tieBreak();
                            this.marker.WasThereTieBreak = true;
                            hasWonSet = true;
                            break;
                        default:
                            hasWonSet = true;
                            break;
                    }
                }else{
                    hasWonSet = true;
                }
            }else{
                // continue the set
                this.changeRoles();
                ++this.numGame;
            }
            return hasWonSet;
        }

        

        private void createEventPoints(String namePlayerWonPoints){
            String stringEvent = "Punto de " + namePlayerWonPoints + " " + this.marker.showResultGame() + "\t\t";
            stringEvent += this.marker.showResultSets();
            this.indicateEventReferee(stringEvent);
        }

        private void createEventGame(String namePlayerWonGame){
            String stringEvent = namePlayerWonGame + " gana el juego \t\t";
            stringEvent += this.marker.showResultSets();
            stringEvent += "\n";
            this.marker.resetPoints();
            this.indicateEventReferee(stringEvent);
        }

        private void createEventSet(String namePlayerWonGameAndSet, int positionPlayer, String typeWin){
            String stringEvent = namePlayerWonGameAndSet + " gana el " + typeWin + " y el set\t";
            stringEvent += this.marker.showResultSets();
            stringEvent += "\n";
            this.marker.HasFinishedLastSet = true;
            this.marker.addCountSetsWon(positionPlayer);
            this.marker.resetPoints();
            this.indicateEventReferee(stringEvent);
        }

        private void changeRoles(){
            TennisPlayer tennisPlayer = this.receiver;
            this.receiver = this.launcher;
            this.receiver.setProbabilityWin(new ProbabilityWinReceiver());
            this.launcher = tennisPlayer;
            this.launcher.setProbabilityWin(new ProbabilityWinLauncher());
        }

        private TennisPlayer getLauncher(){
            return this.launcher;
        }

        private TennisPlayer getReceiver(){
            return this.receiver;
        }

        private int getWinProbabilityOfTheLauncher(){
            return this.getLauncher().getWinProbability();  //using 'inyección de dependencias'
        }

        private Boolean isLauncherWon(){
            int probabilityWinLauncher = this.getWinProbabilityOfTheLauncher();
            return this.probabilityManager.isProbabilityWon(100, probabilityWinLauncher);
        }

        private Boolean isFirstPlayerTheLauncher() {
            return this.probabilityManager.isProbabilityWon(2,1);
        }


        class Marker{
            
            private int[][] Msets;
            public pointsTennis[] Vpoints;
            private int[] VcountSetsWon;
            private int numSets;
            private int positionPlayerWonLastGame;
            public int[] tieBreak;
            private String[] tieBreakBySets;
            private Boolean hasFinishedLastSet;
            private Boolean wasThereTieBreak;

            SortedDictionary<pointsTennis, String> pointsDictionary;

            public Marker(int numSets){
                this.positionPlayerWonLastGame = -1;
                this.numSets = numSets;
                this.resetStatsMarker();
                this.wasThereTieBreak = false;

                this.pointsDictionary = new SortedDictionary<pointsTennis, String>();
                this.fillPointsDictionary();
            }
            
            private void fillPointsDictionary(){
                this.pointsDictionary.Add(pointsTennis.zero, "0");
                this.pointsDictionary.Add(pointsTennis.fifteen, "15");
                this.pointsDictionary.Add(pointsTennis.thirty, "30");
                this.pointsDictionary.Add(pointsTennis.forty, "40");
                this.pointsDictionary.Add(pointsTennis.Ad, "Ad");
            }

            private void resetStatsMarker(){
                this.Vpoints = new pointsTennis[NP];
                this.VcountSetsWon = new int[NP];
                this.tieBreak = new int[NP];
                this.Msets = new int[NP][];
                this.initTieBreakBySets();

                for (int row = 0; row < NP; ++row){
                    this.Vpoints[row] = pointsTennis.zero;
                    this.tieBreak[row] = 0;
                    this.VcountSetsWon[row] = 0;
                    this.Msets[row] = new int[this.numSets];
                    for (int column = 0; column < this.numSets; ++column){
                        this.Msets[row][column] = 0;
                    }
                }
            }

            private void initTieBreakBySets(){
                this.tieBreakBySets = new string[this.numSets];
                for (int column = 0; column < this.numSets; ++column){
                    this.tieBreakBySets[column] = "";
                }
            }

            public void resetPoints(){
                for(int row = 0; row < NP; ++row){
                    this.Vpoints[row] = pointsTennis.zero;
                }
            }

            public void resetTieBreak(){
                for (int row = 0; row < NP; ++row){
                    this.tieBreak[row] = 0;
                }
            }

            public int getCountSetsLastWinner(){
                return this.VcountSetsWon[this.positionPlayerWonLastGame];
            }

            public int getCountSetsWon(int position){
                return this.VcountSetsWon[position];
            }

            public void addCountSetsWon(int position){
                ++this.VcountSetsWon[position];
            }

            public int getPositionPlayerEnemy(int positionplayer){
                return positionplayer == 0 ? 1 : 0;     
            }

            public int NumSets{
                get => this.numSets;
            }

            public void addWonGame(int positionPlayer, int positionSet){
                ++this.Msets[positionPlayer][positionSet];
                this.positionPlayerWonLastGame = positionPlayer;
            }

            public int getGamesWon(int positionPlayer, int positionSet){
                return this.Msets[positionPlayer][positionSet];
            }

            public int getGamesWonOfTheLastWinner(int positionSet){
                return this.Msets[this.positionPlayerWonLastGame][positionSet];
            }

            public int getGamesWonOfTheLastLoser(int positionSet){
                return this.Msets[this.getPositionPlayerEnemy(this.positionPlayerWonLastGame)][positionSet];
            }

            public int PositionPlayerWonLastGame{
                get => this.positionPlayerWonLastGame;
            }

            public Boolean HasFinishedLastSet{
                get => this.hasFinishedLastSet;
                set => this.hasFinishedLastSet = value;
            }

            public Boolean WasThereTieBreak{
                get => this.wasThereTieBreak;
                set => this.wasThereTieBreak = value;
            }

            public void savetPointsLoserTieBreak(int positionSet){
                this.tieBreakBySets[positionSet] = " (" + this.tieBreak[this.getPositionPlayerEnemy(this.PositionPlayerWonLastGame)] + ")";
            }

            public String showResultSets(){
                String result;
                result = "(";
                for (int j = 0; j < this.numSets-1; ++j){
                    result += this.Msets[0][j];
                    for (int i = 1; i < NP; ++i){
                        result += "-";
                        result += this.Msets[i][j];
                        
                    }
                    result += this.tieBreakBySets[j];
                    result += ", ";
                }
                result += this.Msets[0][this.numSets - 1];
                for (int i = 1; i < NP; ++i){
                    result += "-";
                    result += this.Msets[i][this.numSets - 1];

                }
                result += this.tieBreakBySets[this.numSets - 1];
                result += ")";

                return result;
            }

            public String showResultGame(){
                String result;
                result = this.pointsDictionary[this.Vpoints[0]] + "-" + this.pointsDictionary[this.Vpoints[1]];
                return result;
            }

        }

    }
}
