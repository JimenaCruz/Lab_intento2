using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace AGENDA
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuarios u = new Usuarios();
            Menus m = new Menus();
            Factura f = new Factura();
            u.Login();
            
        }

    }
}
