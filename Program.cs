using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
{
    public static class Program
    {
        static Mercado m = new Mercado();
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new Form1());

           
           

            //agregamos Categorias
           /* m.AgregarCategoria("Comida");                // 1
            m.AgregarCategoria("Bebida");                // 2
            m.AgregarCategoria("Ropa");                  // 3
            m.AgregarCategoria("Articulos de limpieza"); // 4
            m.AgregarCategoria("Electrodomesticos");     // 5
            m.AgregarCategoria("Informatica");           // 6*/
            
            //agregamos Productos
           /* m.AgregarProducto("Cerveza", 10, 20, 1); // 1
            m.AgregarProducto("Papas", 5, 20, 0);   // 2
            m.AgregarProducto("Palitos", 5, 20, 0); // 3
            m.AgregarProducto("Cheetos", 5, 20, 0); // 4
            m.AgregarProducto("TV", 5, 20, 4);      // 5
            m.AgregarProducto("PC", 5, 20, 5);      // 6*/

            //agregamos Usuarios
          /*  m.AgregarUsuario(12345678, "Pepito", "Fulano", "pepito@gmail.com", "123456", 2145687, 1);   // 1
            m.AgregarUsuario(12345678, "Jose", "Gomez", "jose@gmail.com", "123456", 2189587, 2);        // 2
            m.AgregarUsuario(12345678, "Carlos", "Lopez", "carlos@gmail.com", "123456", 158468, 3);     // 3
            m.AgregarUsuario(12345678, "Luis", "Mengano", "luis@gmail.com", "123456", 157852, 1);       // 4
            m.AgregarUsuario(12345678, "Pepito", "Fulano", "pepito@gmail.com", "123456", 2145687, 2);   // 5
            m.AgregarUsuario(12345678, "Jose", "Gomez", "jose@gmail.com", "123456", 2189587, 3);        // 6
            m.AgregarUsuario(12345678, "Carlos", "Lopez", "carlos@gmail.com", "123456", 158468, 1);     // 7
            m.AgregarUsuario(12345678, "Luis", "Mengano", "luis@gmail.com", "123456", 157852, 2);       // 8*/

        }
    }
}
