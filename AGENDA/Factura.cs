using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace AGENDA
{
    class Factura
    {
        private string rutaFactura = "factura.txt", rutaTemp = "temp.txt", detalleFactura = "detalle.txt",
                       Correlativo = "", NombreCliente = "", Nit = "", fecha = DateTime.Now.ToShortDateString(),
                       linea = "", linea2=""; 
        public string resp="";
        private double costoU = 0, subtotal = 0, monto = 0, serie = 0;
        private string[] datos = new string[5];
        private string[] detalle = new string[5];
        private StreamReader leer, leerDetalle;
        private StreamWriter escribir;
        static Inventario i = new Inventario();
        private string LlenarCampos(string campo)
        {
            Console.Write("Ingrese " + campo + ": ");
            return Console.ReadLine();
        }
        private void GuardarFactura(string correlativo, string cliente, string nit, string fecha, double monto)
        {
            escribir = File.AppendText(rutaFactura);
            escribir.WriteLine(correlativo + "*" + cliente + "*" + nit + "*" + fecha + "*" + monto.ToString("N2"));
            escribir.Close();
            Console.WriteLine("Factura guardada");
        }
        private void GuardarDetalle(string correlativo, string producto, int cantidad, double precio, double subtotal)
        {
            escribir = File.AppendText(detalleFactura);
            escribir.WriteLine(correlativo + "-" + producto + "-" + cantidad + "-" + precio.ToString("N2") + "-" + subtotal.ToString("N2"));
            escribir.Close();
            Console.WriteLine("Detalle de factura guardado");
        }
        public void Generar()
        {
            try
            {
                serie = 0;
                Console.Clear();
                Console.WriteLine("Factura nueva\n");
                using (leer = new StreamReader(rutaFactura))
                {
                    while ((linea = leer.ReadLine()) != null)
                    {
                        datos = linea.Split('*');
                        serie = Convert.ToInt32(datos[0]);
                    }
                }

                if (serie == 0) serie = 1;
                else serie++;
                Correlativo = serie.ToString();
                NombreCliente = LlenarCampos("nombre a facturar");
                if (NombreCliente.Length == 0 || NombreCliente == "")
                {
                    NombreCliente = "C/F";
                    Nit = "";
                }
                else
                {
                    do
                    {
                        Nit = LlenarCampos("nit");
                    } while (Nit == "" || Nit.Length == 0 || Nit == string.Empty);
                }

                do
                {
                    i.Retirar(2);
                    if (i.monto != 0 && i.subtotal != 0)
                    {
                        monto = i.monto;
                        GuardarDetalle(Correlativo, i.nombreTemp, i.cantidadTemp, i.precioTemp, i.subtotal);
                    }
                    Console.WriteLine("¿Desea retirar otro producto? S/N");
                    resp = Console.ReadLine();
                } while (resp == "s" || resp == "S");
                if (i.monto != 0 && i.subtotal != 0)
                    GuardarFactura(Correlativo, NombreCliente, Nit, fecha, monto);
                monto = 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void Listar()
        {
            using (leer = new StreamReader(rutaFactura))
            {
                while ((linea = leer.ReadLine()) != null)
                {
                    datos = linea.Split('*');
                    Console.WriteLine("Fecha: {0}\t\tCorrelativo: {1}", datos[3], datos[0]);
                    Console.WriteLine("Nombre: {0}\t\t\t\tNit: {1}\n", datos[1], datos[2]);
                    using (leerDetalle = new StreamReader(detalleFactura))
                    {
                        Console.WriteLine("\tProducto\tCantidad\tPrecio U.\tSubtotal");
                        while ((linea2 = leerDetalle.ReadLine()) != null)
                        {
                            detalle = linea2.Split('-');
                            if(datos[0] == detalle[0])
                                Console.WriteLine("\t{0}\t{1}\t\t{2}\t{3}", detalle[1], detalle[2], detalle[3], detalle[4]);
                        }
                    }
                    Console.WriteLine("\n\t\t\t\t\t\t Monto: {0}\n", datos[4]);
                }
            }
            Console.ReadKey();
        }

    }
}
