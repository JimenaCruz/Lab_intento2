using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGENDA
{
    class Inventario
    {
        private string rutaInventario = "inventario.txt",
                       rutaTemp = "temp.txt",
                       Nombre = "", Codigo = "", modificar = "", linea = "",
                       fechaRegistro = DateTime.Now.ToShortDateString();
            public string nombreTemp="", fechaTemp="";
        private int cont = 0, Cantidad = 0; public int cantidadTemp = 0;
        private double Precio = 0; public double precioTemp = 0; 
        private string[] datos = new string[5];
        private StreamReader leer;
        private StreamWriter escribir;
        Menus m = new Menus();
        Factura f = new Factura();
        public string producto = "";
        public double monto = 0, precio = 0, subtotal=0;
        public int cant = 0;
        private string LlenarCampos(string campo)
        {
            Console.Write("Ingrese " + campo + ": ");
            return Console.ReadLine();
        }
        private void Guardar(string codigo, string nombre, string registro, int cantidad, double precio)
        {
            escribir = File.AppendText(rutaInventario);
            escribir.WriteLine(codigo + "-" + nombre + "-" + registro + "-" + cantidad + "-" + precio.ToString("N2"));
            escribir.Close();
            Console.WriteLine("Producto guardado.");
        }
        public void Insertar(int operacion)
        {
            try
            {
                if (operacion == 1)
                {
                    do
                    {
                        Codigo = LlenarCampos("código del producto (máx 5 caracteres)");
                    } while (Codigo.Length != 5 || Codigo == string.Empty || Codigo == "");
                    do
                    {
                        Nombre = LlenarCampos("descripción");
                    } while (Nombre.Length == 0 || Nombre == "" || Nombre == string.Empty);

                    do
                    {
                        Cantidad = Convert.ToInt32(LlenarCampos("cantidad"));
                    } while (Cantidad <= 0 || Cantidad.ToString() == string.Empty);

                    do
                    {
                        Precio = Convert.ToDouble(LlenarCampos("precio"));
                    } while (Precio <= 0 || Precio.ToString() == string.Empty);
                    Guardar(Codigo, Nombre, fechaRegistro, Cantidad, Precio);
                }
                else if (operacion == 2)
                {

                    Codigo = modificar;
                    Nombre = nombreTemp;
                    fechaRegistro = fechaTemp;
                    Precio = precioTemp;
                    do
                    {
                        Console.Write("\nCantidad actual del producto {0}: {1}\n", Codigo, cantidadTemp);
                        Cantidad = Convert.ToInt32(LlenarCampos("cantidad a abastecer"));
                    } while (Cantidad <= 0 || Cantidad.ToString() == string.Empty);
                    Guardar(Codigo, Nombre, fechaRegistro, cantidadTemp + Cantidad, Precio);
                }
                else if(operacion == 3)
                {
                    Codigo = modificar;
                    Nombre = nombreTemp;
                    fechaRegistro = fechaTemp;
                    Precio = precioTemp;
                    do
                    {
                        Console.Write("\nCantidad actual de {0}: {1}\n", Codigo, cantidadTemp);
                        Cantidad = Convert.ToInt32(LlenarCampos("cantidad a retirar"));
                    } 
                    while (Cantidad <= 0 || Cantidad > cantidadTemp || Cantidad.ToString() == string.Empty);
                    if(Cantidad < cantidadTemp )
                    {
                        Guardar(Codigo, Nombre, fechaRegistro, cantidadTemp - Cantidad, Precio);

                        subtotal = Convert.ToDouble((Cantidad * Precio).ToString("N2"));
                        cantidadTemp = Cantidad;
                        precioTemp = Precio;
                        monto += Convert.ToDouble(subtotal.ToString("N2"));
                        Console.WriteLine("Producto cargado a factura");
                    }
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void Llenar()
        {
            try
            {
                modificar = ""; cont = 0;
                Console.Clear();
                Console.WriteLine("Abastecer inventario\n");
                Listar();
                Console.Write("Ingrese código de producto a abastecer: ");
                modificar = Console.ReadLine();
                using (escribir = new StreamWriter(rutaTemp))
                {
                    using (leer = new StreamReader(rutaInventario))
                    {
                        while ((linea = leer.ReadLine()) != null)
                        {
                            datos = linea.Split('-');
                            if (datos[0] != modificar)
                                escribir.WriteLine(linea);
                            else
                            {
                                cont++;
                                nombreTemp = datos[1];
                                fechaTemp = datos[2];
                                cantidadTemp = Convert.ToInt32(datos[3]);
                                precioTemp = Convert.ToDouble(datos[4]);
                            }
                        }
                    }
                }
                if (cont == 1)
                {
                    File.Delete(rutaInventario);
                    File.Move(rutaTemp, rutaInventario);
                    Insertar(2);
                }
                else Console.WriteLine("\nProducto no encontrado");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void Retirar(int tipoRetiro)
        {
            try
            {
                modificar = ""; cont = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Retirar productos\n");
                    Listar();
                    Console.Write("\nIngrese código de producto a retirar: ");
                    modificar = Console.ReadLine();
                    using (escribir = new StreamWriter(rutaTemp))
                    {
                        using (leer = new StreamReader(rutaInventario))
                        {
                            while ((linea = leer.ReadLine()) != null)
                            {
                                datos = linea.Split('-');
                                if (datos[0] != modificar)
                                    escribir.WriteLine(linea);
                                else
                                {
                                    cont++; 
                                    nombreTemp = datos[1];
                                    fechaTemp = datos[2];
                                    cantidadTemp = Convert.ToInt32(datos[3]);
                                    precioTemp = Convert.ToDouble(datos[4]);
                                }
                            }
                        }
                    }
                    if (cont == 1)
                    {
                        File.Delete(rutaInventario);
                        File.Move(rutaTemp, rutaInventario);
                        if (tipoRetiro == 1)
                        {
                            Console.WriteLine("\nProducto retirado");
                        }
                        else if(tipoRetiro == 2)
                        {
                            Insertar(3);
                            
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nProducto no encontrado");
                        modificar = "0";
                    }
                    
                    Console.ReadKey();
                } while (modificar == "" || modificar == string.Empty);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void Listar()
        {
            Console.WriteLine("\t\tCódigo - Descripción - Fecha registro - Cantidad - Precio");
            using (leer = new StreamReader(rutaInventario))
            {
                while ((linea = leer.ReadLine()) != null)
                {
                    Console.WriteLine("\t\t" + linea);
                }
            }
            Console.ReadKey();
        }
    }
}
