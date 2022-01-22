using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basicas
{
    class Funciones
    {

        public static int LeerEntero(string msje, int min, int max, string error)
        {
            int num;

            do
            {
                Console.Write(msje);
                num = Int16.Parse(Console.ReadLine());
                if (num < min || num > max)
                {
                    Console.WriteLine(error);
                }
            } while (num < min || num > max);

            return num;
        }


        public static int LeerMenu(string Titulo, string [] Opciones, string Msje, string MsjeError)
        {
            int op;

            Console.WriteLine("\n" + Titulo);
            for (int i = 0; i < Opciones.Length; i++)
            {
                Console.WriteLine( (i+1) + ". " + Opciones[i]);
            }
           
            op = LeerEntero(Msje, 1, Opciones.Length, MsjeError);

            return op;
        }

        public static string LeerMenuClases(string Titulo, Type tipo, string Msje, string MsjeError)
        {
            int op;
            string opcion = "";
            int i = 1;

            Console.WriteLine("\n" + Titulo);
            foreach (var info in tipo.GetProperties())
            {
                Console.WriteLine(i + ". " + info.Name);
                i++;
            }

            op = Funciones.LeerEntero(Msje, 1, i - 1, MsjeError);

            i = 1;
            foreach (var info in tipo.GetProperties())
            {
                if (i == op)
                {
                    opcion = info.Name;
                }
                i++;
            }

            return opcion;
        }
    }
}
