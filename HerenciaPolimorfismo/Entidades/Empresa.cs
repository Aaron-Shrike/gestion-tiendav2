using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Empresa : ICliente
    {

        public string RazonSocial { get; set; }
        public string RUC { get; set; }

        public void Leer()
        {
            Console.Write("Razon social : ");
            this.RazonSocial = Console.ReadLine();
            Console.Write("RUC : ");
            this.RUC = Console.ReadLine();
        }

        public string Identidad()
        {
           return this.RUC + "-" + this.RazonSocial;
        }

    }
}
