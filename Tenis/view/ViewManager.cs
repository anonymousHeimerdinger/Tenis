using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenis.controller;
using Tenis.data.abstractClasses;  // in order to get both classes StringEVentArgs and EventReferee

namespace Tenis.view
{
    class ViewManager
    {

        private GameManager controller;
        private ScreenManager screenManager;
        private String namePlayer1, namePlayer2;
        private Boolean isReadyInitController;
        private EventReferee eventReferee;

        public ViewManager(){

            this.initComponents();
            this.initPresentation();
            this.coutEmptyLines(2);
            this.getNamePlayers();
            int sets = this.getNumberSets();

            try {
                this.controller.initComponents(namePlayer1, namePlayer2, sets, this.eventReferee);
                this.isReadyInitController = true;
            } catch (Exception ex) {
                this.screenManager.coutLine(ex.Message);
            }

            if(this.isReadyInitController)
            {
                try{
                    this.controller.initGame();
                }
                catch(Exception ex)
                {
                    this.screenManager.coutLine(ex.Message);
                }  
            }
            
        }

        private void initComponents(){
            this.screenManager = new ScreenManager();
            this.isReadyInitController = false;

            // init Event will be added to class TennisReferee in order to get what happen during the match
            this.eventReferee = new EventReferee();
            // Any event during the match will be showed by the console
            this.eventReferee.refereeEvent += (object sender, StringEventArgs args) =>
            {
                this.screenManager.coutLine(args.Text);
            };
            this.controller = new GameManager();
        }

        private void coutEmptyLines(int numberOfLines)
        {
            for (int line = 0; line < numberOfLines; ++line)
            {
                this.screenManager.coutLine();
            }
        }

        private void initPresentation()
        {
            String textTitle = "###################################################";
            String title = "#######              TENNIS                 #######";
            this.screenManager.coutLine(textTitle);
            this.screenManager.coutLine(title);
            this.screenManager.coutLine(textTitle);
        }

        
        private void getNamePlayers(){
            namePlayer1 = this.introducePlayer(1);
            namePlayer2 = this.introducePlayer(2);
        }


        private String introducePlayer(int num)
        {
            String name = this.screenManager.introducePlayer(num);
            return name;
        }

        private int getNumberSets()
        {
            int sets = this.introduceSets();
            while (sets != 3 && sets != 5)
            {
                sets = this.introduceSets();
            }
            return sets;
        }

        private int introduceSets()
        {
            int sets;
            try
            {
                sets = this.screenManager.introduceSets();
            }
            catch (FormatException fe)
            {
                this.screenManager.coutLine(fe.Message);
                sets = 0;
            }
            return sets;
        }

    }
}
