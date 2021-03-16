using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenis.view {
    class ScreenManager {
        public ScreenManager() {

        }

        public int introduceSets(){

            int sets;

            Console.Write("Partido a 3 ó 5 sets?: ");

            try{
                sets = Convert.ToInt32(Console.ReadLine());
            }
            catch(FormatException)
            {
                throw new FormatException("comando introducido no válido, por favor, introduzca un número entero");
            }

            return sets;

        }

        public String introducePlayer(int num)
        {
            Console.Write("Nombre jugador " + num + ": ");
            return Console.ReadLine();
        }

        public void cout(String text){
            Console.Write(text);
        }

        public void coutLine()
        {
            Console.WriteLine();
        }

        public void coutLine(String text)
        {
            Console.WriteLine(text);
        }

        public void cout(int num)
        {
            Console.Write(num);
        }

        public void coutLine(int num)
        {
            Console.WriteLine(num);
        }

        public void cout(double num)
        {
            Console.Write(num);
        }

        public void coutLine(double num)
        {
            Console.WriteLine(num);
        }

        public void cout(float num)
        {
            Console.Write(num);
        }

        public void coutLine(float num)
        {
            Console.WriteLine(num);
        }

    }
}
