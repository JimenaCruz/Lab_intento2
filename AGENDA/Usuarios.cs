using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGENDA
{
    class Usuarios
    {
        /* Campos para usuarios
           Usuario = id único para cada usuario
           Contraseña = para ingresar al sistema
           Nombre = nombre y apellido del usuario a registrar
           Tipo = si es tipo 0 es administrador, si es tipo 1 es trabajador 
           Estado = 1 si está activo, 0 si está inactivo */

        private string rutaUsuarios = "usuarios.txt",
                       rutaTemp = "temp.txt",
                       User, Password, Nombre,
                       modificar = "", linea = "",
                       usuarioA = "", passA = "";
        private string[] datos = new string[5];
        private int Estado = 0, Tipo = 0, cont = 0;
        Menus m = new Menus();
        private StreamReader leer;
        private StreamWriter escribir;
        private string LlenarCampos(string campo)
        {
            Console.Write("Ingrese " + campo + ": ");
            return Console.ReadLine();
        }
        private void Guardar(string user, string pass, string nombre, int tipo, int estado)
        {
            escribir = File.AppendText(rutaUsuarios);
            escribir.WriteLine(user + "," + pass + "," + nombre + "," + tipo + "," + estado);
            escribir.Close();
            Console.WriteLine("Usuario guardado.");
        }
        public void Insertar(int operacion)   
        {
            try
            {
                do
                {
                    Console.WriteLine("0. Administrador\n1. Empleado");
                    Tipo = Convert.ToInt32(Console.ReadLine());

                } while (Tipo != 1 && Tipo != 0);

                if (operacion == 1)
                {
                    do
                    {
                        User = LlenarCampos("usuario");
                    } while (User == "" || User == string.Empty || User.Length == 0);
                }

                if (operacion == 2)
                    User = modificar;
                do
                {
                    Password = LlenarCampos("contraseña (máx 5 caracteres)");
                } while (Password.Length != 5);


                Nombre = LlenarCampos("nombre y apellido");

                do
                {
                    Estado = Convert.ToInt32(LlenarCampos("estado (1=Activo; 0=Inactivo)"));
                } while (Estado != 0 && Estado != 1);

                Guardar(User, Password, Nombre, Tipo, Estado);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
        public void Editar()
        {
            modificar = "";
            Console.Clear();
            Console.WriteLine("Editar usuarios");
            Listar();
            Console.Write("\nIngrese usuario a modificar: ");
            modificar = Console.ReadLine();
            using (escribir = new StreamWriter(rutaTemp))
            {
                using (leer = new StreamReader(rutaUsuarios))
                {
                    while ((linea = leer.ReadLine()) != null)
                    {
                        datos = linea.Split(',');
                        if (datos[0] != modificar)
                            escribir.WriteLine(linea);
                    }
                }
            }
            File.Delete(rutaUsuarios);
            File.Move(rutaTemp, rutaUsuarios);
            Console.WriteLine("Datos Nuevos");
            Insertar(2);
            Console.ReadKey();
        }
        public void Eliminar()
        {
            string borrar = ""; linea = ""; cont = 0;
            Console.Clear();
            Console.WriteLine("Eliminar usuarios");
            Listar();
            Console.Write("\nIngrese nombre del usuario a eliminar: ");
            borrar = Console.ReadLine();
            using (escribir = new StreamWriter(rutaTemp))
            {
                using (leer = new StreamReader(rutaUsuarios))
                {
                    while ((linea = leer.ReadLine()) != null)
                    {
                        datos = linea.Split(',');
                        if (datos[0] != borrar)
                            escribir.WriteLine(linea);
                        else cont++;
                    }
                }
            }
            if (cont == 1)
            {
                File.Delete(rutaUsuarios);
                File.Move(rutaTemp, rutaUsuarios);
                Console.WriteLine("Usuario borrado");
            }
            else Console.WriteLine("Usuario no encontrado");
            Console.ReadKey();

        }
        public void Listar()
        {
            Console.WriteLine("\t\tUsuario,Contraseña,Nombre,Tipo,Estado");
            using (leer = new StreamReader(rutaUsuarios))
            {
                while ((linea = leer.ReadLine()) != null)
                {
                    Console.WriteLine("\t\t" + linea);
                }
            }
            Console.ReadKey();
        }
        public void Login()
        {
            cont = 0;
            do
            {
                linea = "";
                Console.Clear();
                Console.WriteLine("----------------------------- LOGIN -----------------------------");
                Console.Write("Ingrese usuario: ");
                usuarioA = Console.ReadLine();
                Console.Write("Ingrese contraseña: ");
                passA = Console.ReadLine();
                using (leer = new StreamReader(rutaUsuarios))
                {
                    while ((linea = leer.ReadLine()) != null)
                    {
                        datos = linea.Split(',');
                        if (datos[0] == usuarioA && datos[1] == passA && datos[4] == 1.ToString())
                        { cont++; break; }
                    }
                }
                if (cont == 1)
                {
                    Console.WriteLine("Bienvenido, " + usuarioA);
                }
                else
                    Console.WriteLine("Acceso denegado");
                Console.ReadKey();
            } while (cont != 1);

            m.MenuGeneral(Convert.ToInt32(datos[3]));
        }


    }

}
