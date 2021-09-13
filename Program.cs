using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class Program
    {
        static void Main(string[] args)
        {

            Mercado m = new Mercado();

            //agregamos Categorias
            
            m.AgregarCategoria("Comida"); // 1
            m.AgregarCategoria("Bebida"); // 2
            //Array.Sort(m.Categorias);

            //agregamos Productos

            m.AgregarProducto("cerveza", 10, 20,2); // 1
            m.AgregarProducto("papas", 5, 20, 1); // 2

            //agregamos Usuarios
            m.AgregarUsuario(12345678,"Pepito","Fulano", "pepito@gmail.com", "123456", 2145687, false); // 1
            m.AgregarUsuario(12345678, "Jose", "Gomez", "jose@gmail.com", "123456", 2189587, false); // 2
            m.AgregarUsuario(12345678, "Carlos", "Lopez", "carlos@gmail.com", "123456", 158468, true); // 3
            m.AgregarUsuario(12345678, "Luis", "Mengano", "luis@gmail.com", "123456", 157852, true); // 4
            
            //limpiamos la consola
            Console.Clear();

            int flag = 1;
            int res  =  0;
            
            string a,b,d, e;
            double p;
            int c, id,n;

            

            while (flag == 1)
            {
                Console.Clear();
                Console.WriteLine("\nBienvenido, elija una de las opciones.\n");
                Console.WriteLine("1. Administrar \n2. Comprar \n");
                res = int.Parse(Console.ReadLine());
                if (res == 1)
                {
                    while (flag == 1)
                    {
                        Console.Clear();
                        Console.WriteLine("1. Alta de Categoria \n2. Baja de Categoria \n3. Modificacion de categoria  \n4. Mostrar categorías que existen en el sistema " +
                                          "\n5. Alta de usuario \n6. Baja de usuario \n7. Modificación de usuario \n8. Mostrar usuarios que existen en el sistema \n9. Alta de producto" +
                                          "\n10. Baja de producto \n11. Modificacion de producto \n12. Mostrar productos que existen en el sistema \n13. Mostrar productos que existen en el sistema ordenados por precio \n14. Mostrar productos que existen en el sistema ordenados por categorias \n15. Salir \n");

                         res = int.Parse(Console.ReadLine());

                        switch (res)
                        {
                            case 1:
                                Console.Clear();
                                Console.WriteLine("Alta de categoria");
                                Console.WriteLine("Ingrese el nombre de la categoria nueva: ");
                                e = Console.ReadLine();
                                m.AgregarCategoria(e);
                                //Array.Sort(m.Categorias);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 2:
                                Console.Clear();
                                Console.WriteLine("Baja de categoria");
                                m.MostrarCategoria();
                                Console.WriteLine("Elija el numero de la categoria a eliminar: ");
                                m.MostrarCategoria();
                                id = int.Parse(Console.ReadLine());
                                m.EliminarCategoria(id);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 3:
                                Console.Clear();
                                Console.WriteLine("Modificar Categoria: \n");
                                m.MostrarCategoria();
                                Console.WriteLine("Ingrese ID Categoria:");
                                c = int.Parse( Console.ReadLine());
                                Console.WriteLine("Ingrese nombre:");
                                a = Console.ReadLine();
                                m.ModificarCategoria(c,a);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 4:
                                Console.Clear();
                                Array.Sort(m.nCategorias,m.compare);
                                m.MostrarCategoria();

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 5:
                                Console.Clear();
                                Console.WriteLine("Agregar Usuario: \n");
                                Console.WriteLine("Ingrese DNI:");
                                c = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese Nombre:");
                                a = Console.ReadLine();
                                Console.WriteLine("Ingrese Apellido:");
                                b = Console.ReadLine();
                                Console.WriteLine("Ingrese Mail:");
                                e = Console.ReadLine();
                                Console.WriteLine("Ingrese Password:");
                                d = Console.ReadLine();
                                Console.WriteLine("Ingrese CUIL/CUIT:");
                                res = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese: \n" +
                                                  "1 - Cliente Final\n" +
                                                  "2 - Empresa");
                                n = int.Parse(Console.ReadLine());  ;
                                if (n == 1)
                                {
                                    m.AgregarUsuario(c, a, b, e, d, res, false);
                                }
                                else
                                {
                                    m.AgregarUsuario(c, a, b, e, d, res, true);
                                }

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 6:
                                Console.Clear();
                                Console.WriteLine("Eliminar Usuario: \n");
                                Console.WriteLine("\nPrecione enter para continuar");
                                m.MostrarUsuarios();
                                id = int.Parse(Console.ReadLine());
                                m.EliminarUsuario(id);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 7:
                                Console.Clear();
                                Console.WriteLine("Modificar Usuario: \n");
                                m.MostrarUsuarios();
                                Console.WriteLine("Ingrese ID del Usuario a modificar: \n");
                                id = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el nuevo DNI:");
                                c = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el nuevo Nombre:");
                                a = Console.ReadLine();
                                Console.WriteLine("Ingrese el nuevo Apellido:");
                                b = Console.ReadLine();
                                Console.WriteLine("Ingrese el nuevo Mail:");
                                e = Console.ReadLine();
                                Console.WriteLine("Ingrese la nueva Password:");
                                d = Console.ReadLine();
                                Console.WriteLine("Ingrese el nuevo CUIL/CUIT:");
                                res = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese: \n" +
                                                  "1 - Cliente Final\n" +
                                                  "2 - Empresa");
                                n = int.Parse(Console.ReadLine()); ;
                                if (n == 1)
                                {
                                    m.ModificarUsuario(id,c, a, b, e, d, res, false);
                                }
                                else
                                {
                                    m.ModificarUsuario(id, c, a, b, e, d, res, true);
                                }

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 8:
                                Console.Clear();
                                Console.WriteLine("\nUsuarios del sistema:");
                                m.MostrarUsuarios();

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 9:
                                Console.Clear();
                                Console.WriteLine("Alta de Producto: \n" +
                                                  "Debes ingresar nombre, precio, cantidad e ID de Categoria");
                                Console.WriteLine("Ingrese nombre de producto:");
                                a = Console.ReadLine();
                                Console.WriteLine("Ingrese precio de producto:");
                                p = double.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese cantidad de productos:");
                                c = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese ID de categoria:" );
                                m.MostrarCategoria();
                                
                                id = int.Parse(Console.ReadLine());
                                m.AgregarProducto(a, p, c, id);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 10:
                                Console.Clear();
                                Console.WriteLine("Baja de Producto: \n");
                                Console.WriteLine("Ingrese ID de producto:");
                                id = int.Parse(Console.ReadLine());
                                m.EliminarProducto(id);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 11:
                                Console.Clear();
                                Console.WriteLine("Modificacion de Producto: \n");
                                foreach (Producto pro in m.nProductos)
                                {
                                    Console.WriteLine(pro.ToString());
                                }

                                Console.WriteLine("Ingrese ID de producto:");
                                n = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese nombre de producto:");
                                a = Console.ReadLine();
                                Console.WriteLine("Ingrese precio de producto:");
                                p = double.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese cantidad de productos:");
                                c = int.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese ID de categoria:");
                                m.MostrarCategoria();
                                id = int.Parse(Console.ReadLine());
                                m.ModificarProducto(n,a,p,c,id);

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 12:
                                Console.Clear();
                                Console.WriteLine("\nProductos del sistema:");
                                foreach (Producto pro in m.nProductos)
                                {
                                    Console.WriteLine(pro.ToString());
                                }

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 13:
                                Console.Clear();
                                m.MostrarTodosProductosPorPrecio();

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 14:
                                Console.Clear();
                                m.MostrarTodosProductosPorCategoria();

                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;
                            case 15:
                                Console.Clear();
                                flag  =  0;
                                Console.WriteLine("Adios!");
                                Console.WriteLine("\nPrecione enter para continuar");
                                Console.ReadLine();
                                break;

                        }

                    }

                }
                else if (res == 2)
                {
                    Console.WriteLine("Bajo construcción, próximamente en TP2!");
                    Console.WriteLine("\nPrecione enter para salir");
                    Console.ReadLine();
                }

            }

        }
    }
}
