using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Persona : ICliente
    {

        public string Nombres {get; set;}
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public  DateTime FechaNacimiento { get; set; }

        public string NombreCompleto()
        {
            return this.Apellidos + " " + this.Nombres;
        }

        public string Identidad()
        {
            return this.DNI + "-" + this.NombreCompleto();
        }

        public virtual void Leer()
        {
            Console.Write("Nombres : ");
            this.Nombres = Console.ReadLine();
            Console.Write("Apellidos : ");
            this.Apellidos= Console.ReadLine();
            Console.Write("DNI : ");
            this.DNI = Console.ReadLine();
            Console.Write("Fecha Nacimiento(dd/mm/yy) : ");
            this.FechaNacimiento = DateTime.Parse (Console.ReadLine());
        }

    }
}
