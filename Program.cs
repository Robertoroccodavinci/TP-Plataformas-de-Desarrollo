using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TP_2_PlataformasDeDesarrollo
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());

            //despues se quita
            

            //agregamos Categorias
            m.AgregarCategoria("Comida"); // 1
            m.AgregarCategoria("Bebida"); // 2
            Array.Sort(m.Categorias);

            //agregamos Productos
            
            m.AgregarProducto("cerveza", 10, 20,1); // 1
            m.AgregarProducto("papas", 5, 20, 0); // 2

            //agregamos Usuarios
            m.AgregarUsuario(12345678,"Pepito","Fulano", "pepito@gmail.com", "123456", 2145687, false); // 1
            m.AgregarUsuario(12345678, "Jose", "Gomez", "jose@gmail.com", "123456", 2189587, false); // 2
            m.AgregarUsuario(12345678, "Carlos", "Lopez", "carlos@gmail.com", "123456", 158468, true); // 3
            m.AgregarUsuario(12345678, "Luis", "Mengano", "luis@gmail.com", "123456", 157852, true); // 4


        }
    }
}
