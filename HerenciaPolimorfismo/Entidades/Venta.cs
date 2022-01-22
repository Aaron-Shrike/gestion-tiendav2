using Basicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Venta
    {

        public string TipoDocumento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public bool Vigente { get; set; }

        public ICliente Cliente { get; set; }

        public List<Producto> Productos = new List<Producto>();

        public void Leer()
        {
            Console.Write("Tipo de Documento[BOLETA/FACTURA] : ");
            this.TipoDocumento = Console.ReadLine();
            Console.Write("Serie : ");
            this.Serie = Console.ReadLine();
            Console.Write("Numero : ");
            this.Numero = Console.ReadLine();
            Console.Write("Fecha(dd/mm/yy) : ");
            this.Fecha = DateTime.Parse(Console.ReadLine());
            this.Vigente = true;
        }

        public double Monto()
        {
            double monto = 0;

            foreach (var pro in Productos) {
                monto += pro.Precio * pro.Cantidad;
            }
            
            return monto;
        }

        public string DetalleVenta()
        {
            string text = Cliente.Identidad() + " - " + Fecha + " - " + Monto() + " (" + Vigente.ToString().ToUpper() + ")";

            return text;
        }
    }
}
