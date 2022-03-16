using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utils.UUID
{
    public class UUID
    {
        public static string FIRST_KEY = "A0000";
        public static int INTERVAL_KEY = 9999;
        public static int INTERVAL_START = 0000;
        
        public static string generarUUID(string lastKey) {
            if (lastKey != null) {
                lastKey = lastKey.Trim();
                lastKey = lastKey.ToUpper();
                String letter = lastKey.Substring(0, 1);
                int number = int.Parse(lastKey.Substring(1, lastKey.Length - 1));

                //SI ES MAYOR AL INTERVALO SE DEBE AUMENTAR EL VALOR DE LA LETRA 
                if (number >= INTERVAL_KEY)
                {
                    return aumentarLetra(letter) + concatZero(INTERVAL_START);
                }
                //SINO SE DEBE AUMENTAR +1 AL VALOR DEL NUMERO
                else {
                    number++;
                    return letter + concatZero(number);
                }
            }
            return FIRST_KEY;
        }

        private static string concatZero(int number) {
            if (number < 10)
            {
                return  "000" + number;
            }
            else if (number < 100)
            {
                return "00" + number;
            }
            else if (number < 1000){
                return "0" + number;
            }
            return "" + number;
        }

        private static string aumentarLetra(string letra) {
            char caracter = char.Parse(letra);
            if (caracter >= 65 && caracter < 90) {
                caracter++;
                return caracter.ToString();
            }
            return null;
        }
    }
}
