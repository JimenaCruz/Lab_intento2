using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGENDA
{
    class Menus
    {
        static Usuarios u = new Usuarios();
        private int op = 0;
        static Inventario i = new Inventario();
        static Factura f = new Factura();
        public void MenuGeneral(int tipoUsuario)
        {
            try
            {
                op = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Sistema  de ventas");
                    if (tipoUsuario == 0)
                    {
                        Console.WriteLine("1. Usuarios\n2. Inventario\n3. Facturación\n4. Salir");
                        op = Convert.ToInt32(Console.ReadLine());
                        if (op == 1) MenuUsuario();
                        if (op == 2) MenuInventario();
                        if (op == 3) MenuFactura();
                    }
                    else
                    {
                        Console.WriteLine("1. Inventario\n2. Facturación\n3. Salir");
                        op = Convert.ToInt32(Console.ReadLine());
                        if (op == 1) MenuInventario();
                        if (op == 2) MenuFactura();
                        if (op == 3) op = 4;
                    }

                } while (op != 4);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void MenuUsuario()
        {
            try
            {
                op = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Menú Usuarios");
                    Console.WriteLine("1. Nuevo\n2. Editar\n3. Eliminar\n4. Ver Listado\n5. Regresar");
                    op = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (op == 1) u.Insertar(1);
                    if (op == 2) u.Editar();
                    if (op == 3) u.Eliminar();
                    if (op == 4) u.Listar();
                } while (op != 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void MenuInventario()
        {
            try
            {
                op = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Menú Inventario");
                    Console.WriteLine("1. Nuevo producto\n2. Abastecer inventario\n3. Retirar productos\n4. Listar productos\n5. Regresar");
                    op = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (op == 1) i.Insertar(1);
                    if (op == 2) i.Llenar();
                    if (op == 3) i.Retirar(1);
                    if (op == 4) i.Listar();
                } while (op != 5);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return;
            }
        }
        public void MenuFactura()
        {
            try
            {
                op = 0;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Menú Factura");
                    Console.WriteLine("1. Generar factura\n2. Listado de facturas\n3. Regresar");
                    op = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    if (op == 1) f.Generar();
                    if (op == 2) f.Listar();
                } while (op != 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
                return;
            }
        }
    }
}
