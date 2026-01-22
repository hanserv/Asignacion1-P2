using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Utils
{
    internal static class ConsoleUtils
    {
        public static void PressAndContinue()
        {
            Console.Write("\nPresiona cualquier tecla para continuar...");
            Console.ReadLine();
        }
        public static void ClearAndWrite(string mensaje)
        {
            Console.Clear();
            Console.WriteLine(mensaje);
        }
    }
}
