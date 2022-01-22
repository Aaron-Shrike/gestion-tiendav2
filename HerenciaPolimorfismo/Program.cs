using Entidades;
using Basicas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerenciaPolimorfismo
{
    class Program
    {
        private static List<ICliente> DatosClientes = new List<ICliente>();
        private static List<Producto> DatosProductos = new List<Producto>();
        private static List<Venta> DatosVentas = new List<Venta>();

        private static string[] Opciones = { "Gestionar Cliente", "Gestionar Producto", "Gestionar Venta", "Reportes", "Salir" };
        private static string[] OpcionesBool = { "SI", "NO" };
        private static string[] OpcionesCliente = { "Registrar Cliente", "Modificar Cliente", "Listar Clientes", "Eliminar Cliente", "Retornar" };
        private static string[] TipoCliente = { "Persona", "Empresa", "Trabajador"};
        private static string[] OpcionesProducto = { "Registrar Producto", "Modificar Producto", "Listar Productos", "Eliminar Producto", "Retornar" };
        private static string[] OpcionesVenta = { "Registrar Venta", "Dar de baja una Venta", "Listar Ventas", "Retornar" };
        private static string[] OpcionesReporte = { "Listado detallado de las ventas realizadas a empresas",
                                                      "Listado de los clientes de tipo trabajador a los que se les ha emitido una boleta",
                                                      "Mostrar los datos de la factura con el monto de venta más alto",
                                                      "Retornar" };
    
        static void Main(string[] args)
        {
            int opcion;

            do{
                opcion = Funciones.LeerMenu("- MENU PRINCIPAL -", Opciones, "Ingrese opcion: ", "* Opcion incorrecta");

                switch (opcion) {
                    case 1: GestionarCliente();
                        break;
                    case 2: GestionarProducto();
                        break;
                    case 3: GestionarVenta();
                        break;
                    case 4: ListadoReportes();
                        break;
                }
            }while(opcion != Opciones.Length);
        }

        private static void ListadoReportes()
        {
            int opcion;

            do
            {
                opcion = Funciones.LeerMenu("REPORTES", OpcionesReporte, "Ingrese opcion: ", "* Opcion incorrecta");

                switch (opcion)
                {
                    case 1: ListadoDetalladoVentasEmpresa();
                        break;
                    case 2: ListadoTrabajadoresConBoleta();
                        break;
                    case 3: FacturaConMontoMasAlto();
                        break;
                }
            } while (opcion != OpcionesReporte.Length);
        }

        private static void FacturaConMontoMasAlto()
        {
            int i = 1;
            IEnumerable<Venta> resultado = null;
            double montoMasAlto;

            resultado = DatosVentas.Where(venta => venta.TipoDocumento.ToUpper().Equals("FACTURA"));
            try
            {
                montoMasAlto = resultado.Max(venta => venta.Monto());
                resultado = resultado.Where(venta => venta.Monto() == montoMasAlto);
            }catch(Exception e){
            }
            

            Console.WriteLine("\nLISTADO DE FACTURAS CON MONTO MAS ALTO");
            foreach (var venta in resultado)
            {
                Console.WriteLine(i + ".- " + venta.Serie + " - " + venta.Numero + " - " + venta.Fecha + " - " + venta.Cliente.Identidad());
                i++;
            }
            Console.WriteLine(".................................................");
        }

        private static void ListadoTrabajadoresConBoleta()
        {
            int i = 1;
            IEnumerable<Venta> resultado;

            resultado = DatosVentas.Where(venta => venta.Cliente is Trabajador && venta.TipoDocumento.ToUpper().Equals("BOLETA"));

            Console.WriteLine("\nLISTADO DE TRABAJADORES CON BOLETA");
            foreach (var venta in resultado)
            {
                Console.WriteLine(i + ".- " + ((Trabajador)venta.Cliente).NombreCompleto() + " - " + ((Trabajador)venta.Cliente).DNI + " - " + ((Trabajador)venta.Cliente).Sueldo);
                i++;
            }
            Console.WriteLine(".................................................");
        }

        private static void ListadoDetalladoVentasEmpresa()
        {
            int i = 1;
            IEnumerable<Venta> resultado;

            resultado = DatosVentas.Where(venta => venta.Cliente is Empresa);
            Console.WriteLine("\nLISTADO DETALLADO DE VENTAS REALIZADAS A EMPRESAS");
            foreach (var venta in resultado)
            {
                Console.WriteLine(i + ".- " + ((Empresa)venta.Cliente).RazonSocial + " - " + venta.Fecha + " - S/." + venta.Monto());
                i++;
            }
            Console.WriteLine(".................................................");
        }

        private static void GestionarVenta()
        {
            int opcion;

            do
            {
                opcion = Funciones.LeerMenu("GESTIONAR VENTA", OpcionesVenta, "Ingrese opcion: ", "* Opcion incorrecta");

                switch (opcion)
                {
                    case 1: RegistrarVenta();
                        break;
                    case 2: DarBajaVenta();
                        break;
                    case 3: ListarVenta();
                        break;
                }
            } while (opcion != OpcionesVenta.Length);
        }

        private static void ListarVenta()
        {
            int i = 1;
            Console.WriteLine("\nLISTADO DE VENTAS");
            foreach (var venta in DatosVentas)
            {
                Console.WriteLine(i + ".- " + venta.DetalleVenta());
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void DarBajaVenta()
        {
            Venta venta;

            venta = BuscarVenta();
            if (venta != null)
            {
                venta.Vigente = false;

                Console.WriteLine("Venta dada de baja correctamente");
                Console.ReadLine();
            }
        }

        private static Venta BuscarVenta()
        {
            string serie;

            Console.Write("Serie : ");
            serie = Console.ReadLine();

            foreach (var venta in DatosVentas)
            {
                if (venta.Serie.Equals(serie))
                {
                    return venta;
                }
            }

            Console.WriteLine("* Venta no encontrado");
            Console.ReadLine();

            return null;
        }

        private static void RegistrarVenta()
        {
            Venta venta;
            ICliente cliente;

            cliente = BuscarCliente();

            if (cliente != null)
            {
                venta = new Venta();

                venta.Leer();
                venta.Cliente = cliente;
                AgregarProducto(venta);

                DatosVentas.Add(venta);
            }
        }

        private static void AgregarProducto(Venta venta)
        {
            int opcion;
            Producto producto;

            do
            {
                do
                {
                    producto = BuscarProducto();
                } while (producto == null);

                if (producto != null)
                {
                    producto.Cantidad = Funciones.LeerEntero("Ingrese cantidad : ", 0, 1000, "* Cantidad incorrecta");

                    venta.Productos.Add(producto);
                }
                
                opcion = Funciones.LeerMenu("Desea agregar otro producto?", OpcionesBool, "Ingrese opcion: ", "* Opcion incorrecta");
            } while (opcion != OpcionesBool.Length);
        }

        private static Producto BuscarProducto()
        {
            string nombre;
            
            Console.Write("Nombre del producto : ");
            nombre = Console.ReadLine();

            foreach (var producto in DatosProductos) { 
                if (producto.Nombre.Equals(nombre)){
                    return producto;
                }
            }

            Console.WriteLine("* Producto no encontrado");
            Console.ReadLine();

            return null;
        }

        private static ICliente BuscarCliente()
        {
            int opcion;
            string[] TipoDocumento = {"DNI", "RUC", "DNI"};
            string documento;

            opcion = Funciones.LeerMenu("TIPO DE CLIENTE", TipoCliente, "Opcion : ", "* Opcion no valida");
            Console.Write(TipoDocumento[opcion-1] + " : ");
            documento = Console.ReadLine();

            foreach(var cliente in DatosClientes){
                if (opcion == 2 && cliente is Empresa && ((Empresa)cliente).RUC.Equals(documento))
                {
                    return cliente;
                }
                else {
                    if (opcion != 2 && cliente is Persona && ((Persona)cliente).DNI.Equals(documento))
                    {
                        return cliente;
                    }
                }
            }

            Console.WriteLine("* Cliente no encontrado");
            Console.ReadLine();

            return null;
        }

        private static void GestionarProducto()
        {
            int opcion;

            do
            {
                opcion = Funciones.LeerMenu("GESTIONAR PRODUCTO", OpcionesProducto, "Ingrese opcion: ", "* Opcion incorrecta");

                switch (opcion)
                {
                    case 1: RegistrarProducto();
                        break;
                    case 2: ModificarProducto();
                        break;
                    case 3: ListarProducto();
                        break;
                    case 4: EliminarProducto();
                        break;
                }
            } while (opcion != OpcionesProducto.Length);
        }

        private static void EliminarProducto()
        {
            Producto producto;

            producto = BuscarProducto();
            if (producto != null)
            {
                DatosProductos.Remove(producto);
            }
        }

        private static void ListarProducto()
        {
            int i = 1;
            Console.WriteLine("\nLISTADO DE PRODUCTOS");
            foreach (var producto in DatosProductos)
            {
                Console.WriteLine(i + ".- " + producto.Nombre + " - S/." + producto.Precio);
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void ModificarProducto()
        {
            Producto producto;
            string opcion;
            string valor;

            producto = BuscarProducto();
            if (producto != null)
            {
                opcion = Funciones.LeerMenuClases("MODIFICAR PRODUCTO", producto.GetType(), "Ingrese opcion: ", "* Opcion incorrecta");

                Console.Write(opcion + " : ");
                valor = Console.ReadLine();

                ModificarPropiedadProducto(opcion, valor, producto);
            }
        }

        private static void ModificarPropiedadProducto(string opcion, string valor, Producto producto)
        {
            switch (opcion)
            {
                case "Nombre": producto.Nombre = valor;
                    break;
                case "Precio": producto.Precio = Double.Parse(valor);
                    break;
            }
        }

        private static void RegistrarProducto()
        {
            Producto producto;

            producto = new Producto();
            producto.Leer();
            DatosProductos.Add(producto);
        }

        private static void GestionarCliente()
        {
            int opcion;

            do
            {
                opcion = Funciones.LeerMenu("GESTIONAR CLIENTE", OpcionesCliente, "Ingrese opcion: ", "* Opcion incorrecta");

                switch (opcion)
                {
                    case 1: RegistrarCliente();
                        break;
                    case 2: ModificarCliente();
                        break;
                    case 3: ListarCliente();
                        break;
                    case 4: EliminarCliente();
                        break;
                }
            } while (opcion != OpcionesCliente.Length);
        }

        private static void EliminarCliente()
        {
            ICliente cliente;

            cliente = BuscarCliente();
            if (cliente != null)
            {
                DatosClientes.Remove(cliente);
            }
        }

        private static void ListarCliente()
        {
            int opcion;
            string[] ListadoCliente = { "Persona", "Empresa", "Trabajador", "Todos" };

            opcion = Funciones.LeerMenu("TIPO CLIENTE", ListadoCliente, "Ingrese opcion: ", "* Opcion incorrecta");

            switch (opcion)
            {
                case 1: ListarClientePersona();
                    break;
                case 2: ListarClienteEmpresa();
                    break;
                case 3: ListarClienteTrabajador();
                    break;
                case 4: ListarClientes();
                    break;
            }
        }

        private static void ListarClientes()
        {
            int i = 1;
            Console.WriteLine("\nLISTADO DE TODOS LOS CLIENTES");
            foreach (var cliente in DatosClientes)
            {
                Console.WriteLine(i + ".- " + cliente.Identidad() + " (" + cliente.GetType().Name.ToUpper() + ")");
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void ListarClienteTrabajador()
        {
            int i = 1;
            IEnumerable<ICliente> resultado;

            resultado = DatosClientes.Where(cliente => cliente is Trabajador);

            Console.WriteLine("\nLISTADO DE TRABAJADORES");
            foreach (var cliente in resultado)
            {
                Console.WriteLine(i + ".- " + ((Trabajador)cliente).DatosTrabajador());
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void ListarClienteEmpresa()
        {
            int i = 1;
            IEnumerable<ICliente> resultado;

            resultado = DatosClientes.Where(cliente => cliente is Empresa);

            Console.WriteLine("\nLISTADO DE EMPRESAS");
            foreach (var cliente in resultado)
            {
                Console.WriteLine(i + ".- " + cliente.Identidad());
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void ListarClientePersona()
        {
            int i = 1;
            IEnumerable<ICliente> resultado;

            resultado = DatosClientes.Where(cliente => cliente is Persona && !(cliente is Trabajador) );

            Console.WriteLine("\nLISTADO DE PERSONAS");
            foreach (var cliente in resultado)
            {
                Console.WriteLine(i + ".- " + cliente.Identidad());
                i++;
            }
            Console.WriteLine("..............................");
        }

        private static void ModificarCliente()
        {
            ICliente cliente;
            string opcion;
            string valor;

            cliente = BuscarCliente();
            if (cliente != null) {
                opcion = Funciones.LeerMenuClases("MODIFICAR " + cliente.GetType().Name.ToUpper(), cliente.GetType(), "Ingrese opcion: ", "* Opcion incorrecta");

                Console.Write(opcion + " : ");
                valor = Console.ReadLine();

                ModificarPropiedadCliente(opcion, valor, cliente);
            }
        }

        private static void ModificarPropiedadCliente(string opcion, string valor, ICliente cliente)
        {
            switch (cliente.GetType().Name)
            {
                case "Persona": ModificarPropiedadPersona(opcion, valor, (Persona)cliente);
                    break;
                case "Empresa": ModificarPropiedadEmpresa(opcion, valor, (Empresa)cliente);
                    break;
                case "Trabajador": ModificarPropiedadTrabajador(opcion, valor, (Trabajador)cliente);
                    break;
            }
        }

        private static void ModificarPropiedadTrabajador(string opcion, string valor,Trabajador cliente)
        {
            switch (opcion)
            {
                case "Cargo": cliente.Cargo = valor;
                    break;
                case "Sueldo": cliente.Sueldo = Double.Parse(valor);
                    break;
                case "Nombres": cliente.Nombres = valor;
                    break;
                case "Apellidos": cliente.Apellidos = valor;
                    break;
                case "DNI": cliente.DNI = valor;
                    break;
                case "FechaNacimiento": cliente.FechaNacimiento = DateTime.Parse(valor);
                    break;
            }
        }

        private static void ModificarPropiedadEmpresa(string opcion, string valor, Empresa cliente)
        {
            switch (opcion)
            {
                case "RazonSocial": cliente.RazonSocial = valor;
                    break;
                case "RUC": cliente.RUC = valor;
                    break;
            }
        }

        private static void ModificarPropiedadPersona(string opcion, string valor, Persona cliente)
        {
            switch(opcion){
                case "Nombres": cliente.Nombres = valor;
                    break;
                case "Apellidos": cliente.Apellidos = valor;
                    break;
                case "DNI": cliente.DNI = valor;
                    break;
                case "FechaNacimiento": cliente.FechaNacimiento = DateTime.Parse(valor);
                    break;
            }
        }

        private static void RegistrarCliente()
        {
            ICliente cliente;
            int opcion;

            opcion = Funciones.LeerMenu("TIPO CLIENTE", TipoCliente, "Ingrese opcion: ", "* Opcion incorrecta");
            cliente = CrearCliente(opcion);
            cliente.Leer();
            DatosClientes.Add(cliente);
        }

        private static ICliente CrearCliente(int opcion)
        {
            ICliente cliente = null;

            switch (opcion)
            {
                case 1: cliente = new Persona();
                    break;
                case 2: cliente = new Empresa();
                    break;
                case 3: cliente = new Trabajador();
                    break;
            }

            return cliente;
        }
    }
}