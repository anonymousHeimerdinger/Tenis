using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.controller
{
    class SingletonUtilities
    {

        private static volatile SingletonUtilities instance = null;
        private static Random random;

        private SingletonUtilities(){
            fixedSeedRandoms();
        }

        public static SingletonUtilities Instance(){
            if (instance == null)
            {
                lock (typeof(SingletonUtilities))
                {
                    if (instance == null)
                    {
                        instance = new SingletonUtilities();
                    }
                }
            }
            return instance;
        }

        private void fixedSeedRandoms()
        {
            int seed = Environment.TickCount & Int32.MaxValue; // pending testing with number 345348534650436656
            random = new Random(seed);
        }


        public int getNumberRandom(int num){
            return random.Next(num);
        }

        public int getNumberRandom(int numInit, int numFinal)
        {
            return random.Next(numInit, numFinal);
        }


    }
}
