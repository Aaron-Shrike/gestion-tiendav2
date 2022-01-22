using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Producto
    {

        public string Nombre { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }

        public void Leer()
        {
            Console.Write("Nombre : ");
            this.Nombre = Console.ReadLine();
            Console.Write("Precio : ");
            this.Precio = Double.Parse( Console.ReadLine());
            this.Cantidad = 0;
        }

    }
}
