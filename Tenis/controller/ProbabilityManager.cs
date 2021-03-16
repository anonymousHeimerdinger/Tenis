using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.controller{

    /*
     * Manage the probability of the tennis game
     */    

    class ProbabilityManager{
        public ProbabilityManager(){
            
        } 

        /*
         return a boolean indicating if the numbers of the probability has won or has lost.
         @numMax indicate the maximum number about the amount of the probabilities.
         @numProbability indicate the amount of the probabilities
         */

        public Boolean isProbabilityWon(int numMax, int numProbabiblity){
            int numRandom = SingletonUtilities.Instance().getNumberRandom(numMax);
            Boolean boolean = false;

            if (numRandom < numProbabiblity)
            {
                boolean = true;
            }

            return boolean;
        }
    }
}
