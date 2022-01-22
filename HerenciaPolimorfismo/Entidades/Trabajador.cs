using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Trabajador : Persona
    {

        public string Cargo { get; set; }
        public double Sueldo { get; set; }

        public string DatosTrabajador()
        {
            return this.Identidad() + " - " + Cargo;
        }
        
        public override void Leer()
        {
            base.Leer();
            Console.Write("Cargo : ");
            this.Cargo = Console.ReadLine();
            Console.Write("Sueldo : ");
            this.Sueldo = Double.Parse(Console.ReadLine());
        }
    }
}
