using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
{
    class Mercado
    {
        private List<Producto> Productos;
        private List<Usuario> Usuarios;
        private const int MaxCategorias = 10;
        private int CantCategorias = 0;
        private Categoria[] Categorias;
        private List<Compra> Compras;

        public string[] fileName = { "1productos.txt", "2usuario.txt", "3carro.txt", "4compras.txt", "5categoria.txt" };// A CORREGIR
        private string sourcePath;
        private string targetPath;



        // #######################################################################################
        //                                  CONSTRUCTOR
        // ######################################################################################
        //          SETEAMOS LAS LISTAS Y RUTAS DE ARCHIVOS
        //          SETEAMOS LAS LISTAS, RUTAS
        //          VERIFICAMOS QUE EXISTA LA RUTA TARGET Y SUS ARCHIVOS
        //          SI NO EXISTE LA RUTA TARGET SE CREA, LO MISMO PARA LOS ARCHIVOS
        // ######################################################################################
        public Mercado()
        {
            nProductos = new List<Producto>();
            nUsuarios = new List<Usuario>();
            nCompras = new List<Compra>();
            nCategorias = new Categoria[MaxCategorias];

            nSourcePath = System.IO.Directory.GetCurrentDirectory() + "/../../Archivos";
            nTargetPath = "C:/ArchivosMercado";

            string[] lines = System.IO.File.ReadAllLines(nSourcePath+"/"+ fileName[1]);
            
            foreach (string s in lines)
            {
                string[] parts = s.Split(',');
                // 0 - ID
                // 1 - DNI
                // 2 - NOMBRE
                // 3 - APELLIDO
                // 4 - MAIL
                // 5 - PASSWORD
                // 6 - CUIT_CUIL
                // 7 - ROL
                int n = nUsuarios.Count();
                Usuarios.Add(new Usuario(n, int.Parse(parts[1]), parts[2], parts[3], parts[4], parts[5], int.Parse(parts[6]), int.Parse(parts[7])));
            }

        }
        // ######################################################################################
        //                                  METODOS SET Y GET
        // ######################################################################################

        //METODO DEST
        //INDICAR CON EL INDEX QUE ARCHIVO SE CONSULTARA
        // 0 - Productos
        // 1 - Usuarios
        // 2 - Carro
        // 3 - Compras
        // 4 - Categorias
        public string Dest(int index)
        {
            return System.IO.Path.Combine(nTargetPath, fileName[index]);
        }

        //GET Y SET DE RUTAS
        public string nSourcePath
        {
            get { return sourcePath; }
            set { sourcePath = value; }
        }
        public string nTargetPath
        {
            get { return targetPath; }
            set { targetPath = value; }
        }

        // GET Y SET DE LAS LISTAS
        // PRODUCTOS
        // USUARIOS
        // CATEGORIAS
        // COMPRAS
        public List<Producto> nProductos
        {
            get { return Productos; }
            set { Productos = value; }
        }

        public List<Usuario> nUsuarios
        {
            get { return Usuarios; }
            set { Usuarios = value; }
        }

        public Categoria[] nCategorias
        {
            get { return Categorias; }
            set { Categorias = value; }
        }

        public List<Compra> nCompras
        {
            get { return Compras; }
            set { Compras = value; }
        }

        // ######################################################################################
        //                                  METODOS DE PRODUCTO
        // ######################################################################################
        //                                  AGREGAR PRODUCTO
        //                                  MODIFICAR PRODUCTO
        //                                  ELIMINAR PRODUCTO
        //                                  BUSCAR PRODUCTO
        //                                  BUSCAR PRODUCTO POR PRECIO
        //                                  BUSCAR PRODUCTO POR CATEGORIA
        //                                  MOSTRAR TODOS LOS PRODUCTOS POR PRECIO
        //                                  MOSTRAR TODOS LOS PRODUCTOS POR CATEGORIA
        // ######################################################################################

        public bool AgregarProducto(string nombre, double precio, int cantidad, int ID_Categoria)
        {

            foreach (Producto p in nProductos)
            {   /* **********************************************************************      Cambiamos el foreach por el metodo Exists */
                bool nom = Productos.Exists(x => x.nNombre == nombre);

                if (p != null && (nom || nombre == "" || nombre == null))
                {/* ********************************************************************* */
                    Console.WriteLine("ERROR: ya existe ese producto");
                    return false;
                }
            }
            /* *****************************************   VER QUE PASA SI NO HAY NINGUN ***NULL***         */
            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i] != null && Categorias[i].nID == ID_Categoria)
                {
                    int idProd;
                    if (Productos.Contains(null))
                    {
                        idProd = Productos.IndexOf(null);
                    }
                    else
                    {
                        idProd = Productos.Count();
                    }
                    Productos.Add(new Producto(idProd, nombre, precio, cantidad, Categorias[i]));

                    Console.WriteLine("Producto agregado correctamente!");
                    return true;
                }
            }
            Console.WriteLine("ERROR: no se pudo agregar el producto");
            return false;
        }

        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            int IDProd = Productos.FindIndex(x => x.nIDProd == ID);
            if (IDProd >= 0)
            {
                Productos[IDProd].nNombre = nombre;
                Productos[IDProd].nPrecio = precio;
                Productos[IDProd].nCantidad = cantidad;
                for (int i = 0; i < MaxCategorias; i++)
                {
                    if (Categorias[i].nID == ID_Categoria)
                    {
                        Productos[i].nCategoria = Categorias[i];
                        Console.WriteLine("Producto modificado correctamente!");
                        return true;
                    }
                }
            }
            Console.WriteLine("ERROR: producto no encontrado.");
            return false;
        }


        public bool EliminarProducto(int ID)
        {
            if (Productos[ID] != null)
            {
                Productos[ID] = null;
                Console.WriteLine("Producto eliminado correctamente.");
                return true;
            }
            Console.WriteLine("ERROR: No se encontro el producto con ese ID");
            return false;
        }


        public void BuscarProducto(String Query)
        {
            Productos.Sort();
            /* FALTA CORREGIR, DE TODAS FORMAS TODAVIA NO LO IMPLEMENTAMOS ************************************************** */
            if (Productos.Equals(Query))
            {

                Console.WriteLine("Existe");
            }
            /* FALTA CORREGIR, DE TODAS FORMAS TODAVIA NO LO IMPLEMENTAMOS ************************************************** */
        }


        public void BuscarProductoPorPrecio(string Query)
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio); });
            foreach (Producto p in Productos)
            {
                if (p.nNombre == Query)
                {
                    Console.WriteLine(p);
                }
            }
        }


        public void BuscarProductoPorCategoria(int ID_Categoria)
        {
            Productos.Sort();
            foreach (Producto p in Productos)
            {
                if (p.nCategoria.nID == ID_Categoria)
                {
                    Console.WriteLine(p);
                }
            }
        }


        public void MostrarTodosProductosPorPrecio()
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio); });
            foreach (Producto p in Productos)
            {
                Console.WriteLine(p);
            }
        }

        public void MostrarTodosProductosPorCategoria()
        {
            foreach (Categoria c in Categorias)
            {
                if (c != null)
                {
                    Console.WriteLine(c);
                    foreach (Producto p in Productos)
                    {
                        if (c.nID == p.nCategoria.nID)
                        {
                            Console.WriteLine(p);
                        }
                    }
                }
            }
        }

        // ######################################################################################
        //                                  METODOS DE USUARIO
        // ######################################################################################
        //                                  AGREGAR USUARIO
        //                                  MODIFICAR USUARIO
        //                                  ELIMINAR USUARIO
        //                                  MOSTRAR USUARIO
        // ######################################################################################
        public bool AgregarUsuario(int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, int rol)
        {
            int n = Usuarios.Count();
            Usuarios.Add(new Usuario(n, DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol));

            if (Usuarios[n] != null)
            {
               
                StreamWriter file2 = new StreamWriter(System.IO.Path.Combine(nSourcePath, fileName[1]), true);
                file2.WriteLine(n + "," + DNI + "," + nombre + "," + apellido + "," + Mail + "," + password + "," + CUIT_CUIL + "," + rol);
                file2.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, int rol)
        {
            foreach (Usuario u in Usuarios)
            {
                if (Usuarios[ID] != null)
                {
                    Usuario e = (Usuario)u;
                    e.nDNI = DNI;
                    e.nNombre = nombre;
                    e.nApellido = apellido;
                    e.nMail = Mail;
                    e.nPassword = password;
                    e.nCUIT_CUIL = CUIT_CUIL;
                    e.nRol = rol;
                    Console.WriteLine("Usuario modificado con exito!");
                    return true;
                }
            }
            Console.WriteLine("ERROR: no hay Usuario con ese ID: " + ID);
            return false;
        }

        public bool EliminarUsuario(int ID)
        {
            foreach (Usuario u in Usuarios)
            {
                if (u.nID == ID)
                {
                    Usuarios.Remove(u);
                    Console.WriteLine("Usuario eliminado con exito!");
                    return true;
                }
            }
            Console.WriteLine("ERROR: ID: " + ID + " usuario no encontrado");
            return false;
        }

        public void MostrarUsuarios()
        {
            Usuarios.Sort();
            foreach (Usuario u in Usuarios)
            {
                Console.WriteLine(u);
            }
        }

        // #######################################################################################
        //                                  METODOS DE CATEGORIA
        // #######################################################################################
        //                                  AGREGAR CATEGORIA
        //                                  MODIFICAR CATEGORIA
        //                                  ELIMINAR CATEGORIA
        //                                  MOSTRAR CATEGORIA
        // ######################################################################################
        public bool AgregarCategoria(string nombre)
        {
            if (CantCategorias < MaxCategorias)
            {
                for (int i = 0; i < MaxCategorias; i++)
                {
                    if (Categorias[i] == null)
                    {
                        Categorias[i] = new Categoria(i, nombre);
                        CantCategorias++;

                        Console.WriteLine("Categoria agregada con exito!");
                        return true;
                    }
                }
            }
            Console.WriteLine("ERROR: no se pueden agregar mas categorias");
            return false;
        }

        public bool ModificarCategoria(int ID, string nombre) /********* A VECES TOMA OTRO VALOR DE ID, DIFERENTE AL QUE LE PASAMOS ********/
        {
            Console.WriteLine(Categorias[ID].nID);
            Console.ReadLine();
            if (Categorias[ID].nID == ID) /* ALGUNA PARTE DE ACA ANDA MAL */
            {
                Categorias[ID].nNombre = nombre;
                Console.WriteLine("Categoria modificada con exito!" + Categorias[ID].nID);
                return true;
            }
            Console.WriteLine("ERROR: no hay categoria con ID: " + ID);
            return false;
        }                   /* MODIFICADO, COMPROBAR QUE LES PARECE A LOS DEMAS, DEL GRUPO */

        public bool EliminarCategoria(int ID)
        {
            if (Categorias[ID] != null)
            {
                Categorias[ID] = null;
                CantCategorias--;

                Console.WriteLine("Categoria eliminada con exito!");
                return true;
            }
            Console.WriteLine("ERROR: no hay categoria con ID: " + ID);
            return false;
        }

        public void MostrarCategoria()
        {
            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i] != null)
                {
                    Console.WriteLine(Categorias[i].ToString());
                }
            }
        }

        // #######################################################################################
        //                                  METODOS DE CARRO
        // #######################################################################################
        //                                  AGREGAR AL CARRO
        //                                  QUITAR AL CARRO
        //                                  VACIAR CARRO
        // #######################################################################################

        public bool AgregarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            if (Productos[ID_Producto].nCantidad >= Cantidad)
            {
                Usuarios[ID_Usuario].nCarro.AgregarProducto(Productos[ID_Producto], Cantidad);
                Console.WriteLine("Producto agregada con exito al Carro.");
                return true;
            }
            Console.WriteLine("ERROR: el Producto no se pudo agregar al carro al Carro.");
            return false;
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            if (Usuarios[ID_Usuario].nCarro.QuitarProducto(Productos[ID_Producto], Cantidad))
            {
                return true;
            }
            else
            {
                Console.WriteLine("ERROR: no se encontro producto con el ID " + ID_Producto + " en el Carro.");
                return false;
            }
        }
        public bool VaciarCarro(int ID_Usuario)
        {
            Usuarios[ID_Usuario].nCarro.Vaciar();
            Console.WriteLine("Carro vaciado con exito!");
            return true;
        }

        // #######################################################################################
        //                                  METODOS DE COMPRA
        // #######################################################################################
        //                                  COMPRA
        //                                  MODIFICACION COMPRA
        //                                  ELIMINACION COMPRA
        // #######################################################################################

        public bool Comprar(int ID_Usuario)
        {
            double total = 0;
            int n = Compras.Count();
            foreach (KeyValuePair<Producto, int> kvp in Usuarios[ID_Usuario].nCarro.nProductos)
            {
                total += kvp.Key.nPrecio * kvp.Value;
            }
            if (total > 0)
            {
                Compras.Add(new Compra(n++, Usuarios[ID_Usuario], Usuarios[ID_Usuario].nCarro.nProductos, total));

                foreach (KeyValuePair<Producto, int> kvp in Compras[n].nProductos)
                {
                    foreach (Producto p in Productos)
                    {
                        if (kvp.Key == p)
                        {
                            p.nCantidad = p.nCantidad - kvp.Value;
                        }
                    }
                }
                VaciarCarro(ID_Usuario);
                Compras[n].ToString();
                Console.WriteLine("Compraste con exito!");
                return true;
            }
            Console.WriteLine("ERROR: no se pudo efectuar la compra");
            return false;
        }

        public bool ModificarCompra(int ID, double Total)
        {
            if (Compras[ID] != null)
            {
                Compras[ID].nTotal = Total;
                return true;
            }
            return false;
        }

        public bool EliminarCompra(int ID)
        {
            if (Compras[ID] != null)
            {
                Compras[ID] = null;
                return true;
            }
            return false;
        }

        // #######################################################################################
        //                                  INICIAR SESION
        // #######################################################################################
        public Usuario IniciarSesion(int DNI, string pass)
        {
            
            foreach (Usuario u in nUsuarios)
            {
                
                if (u.nDNI == DNI && u.nPassword == pass)
                {

                    llenarListas();
                    return nUsuarios[u.nID];
                }
            }
            return null;
        }
        // #######################################################################################
        //                                  ES ADMIN
        // #######################################################################################
        public Boolean esAdmin(int ID)
        {
            if (Usuarios[ID] != null && Usuarios[ID].nRol == 3) //preguntamos si usuario con ID es admin
            { return true; }
            else
            { return false; }
        }
        // #######################################################################################
        //                                  GUARDAR ARCHIVOS
        // #######################################################################################

        public void guardarTodo()
        {
            //GUARDAR PRODUCTOS
            StreamWriter file0 = new StreamWriter(Dest(0));
            foreach (Producto p in Productos)
            {
                file0.WriteLine(p.nIDProd + "," + p.nNombre + "," + p.nPrecio + "," + p.nCantidad + "," + p.nCategoria);
            }
            file0.Close();

            //GUARDAR USUARIOS
            StreamWriter file1 = new StreamWriter(Dest(1));
            foreach (Usuario u in Usuarios)
            {
                file1.WriteLine(u.nID + "," + u.nDNI + "," + u.nNombre + "," + u.nApellido + "," + u.nMail + "," + u.nPassword + "," + u.nCUIT_CUIL + "," + u.nRol);
            }
            file1.Close();

            //GUARDAR CARROS
            StreamWriter file2 = new StreamWriter(Dest(2));
            foreach (Usuario u in Usuarios)
            {
                foreach (KeyValuePair<Producto, int> kvp in u.nCarro.nProductos)
                {
                    file2.WriteLine(u.nID + "," + kvp.Key.nIDProd + "," + kvp.Value);
                }
            }
            file2.Close();

            //GUARDAR COMPRAS
            StreamWriter file3 = new StreamWriter(Dest(3));
            foreach (Compra c in Compras)
            {
                foreach (KeyValuePair<Producto, int> kvp in c.nProductos)
                {
                    file3.WriteLine(c.nComprador + "," + kvp.Key.nIDProd + "," + kvp.Value + "," + c.nTotal);
                }
            }
            file3.Close();

            //GUARDAR CATEGORIAS
            StreamWriter file4 = new StreamWriter(Dest(4));
            foreach (Categoria c in Categorias)
            {
                if (c != null)
                {
                    file4.WriteLine(c.nID + "," + c.nNombre);
                }
            }
            file4.Close();

        }
        // #######################################################################################
        //                                  GUARDAR ARCHIVOS
        // #######################################################################################

        public void llenarListas ()
        {
            //###########################################
            //  COMPROBACION DE DIRECTORIOS Y ARCHIVOS 
            //###########################################

            //PREGUNTAMOS SI EXISTE EL DIRECTORIO TARGET
            if (System.IO.Directory.Exists(nTargetPath))
            {
                string[] files = System.IO.Directory.GetFiles(nTargetPath);
                int cont = 0;
                foreach (string s in fileName)
                {

                    //PREGUNTAMOS SI EN EL DIRECTORIO TARGET, SE ENCUENTRAN LOS ARCHIVOS
                    if (!files.Contains(s)) 
                    {
                        Console.WriteLine(s+" --- "+files[cont]);
                        //SI NO EXISTEN LOS ARCHIVOS, LOS COPIAMOS DE SOURCE
                        System.IO.File.Copy(System.IO.Path.Combine(nSourcePath, s), Dest(cont),true);
                        cont++;
                    }
                }
            }
            else
            {
                //SI NO EXITE EL DIRECTORIO TARGET, LO CREAMOS
                System.IO.Directory.CreateDirectory(nTargetPath);
                //COPIAMOS TODOS LOS ARCHIVOS DE SOURCE A TARGET
                for (int i = 0; i < fileName.Length; i++)
                {
                    System.IO.File.Copy(System.IO.Path.Combine(nSourcePath, fileName[i]), Dest(i));
                }
            }
            //###########################################
            //   PASAMOS DE LOS ARCHIVOS A LAS LISTAS 
            //###########################################

            string[] lines;

            // 4 - Categorias -> ANTES DE PRODUCTOS
            if (System.IO.File.Exists(Dest(4)))
            {
                lines = System.IO.File.ReadAllLines(@"" + Dest(4));
                foreach (string s in lines)
                {
                    string[] parts = s.Split(',');
                    // 0 - ID
                    // 1 - Nombre
                    //nCategorias += new Categoria(parts[1]);
                    AgregarCategoria(parts[1]);
                }
            }

            // 0 - Productos
            if (System.IO.File.Exists(Dest(0)))
            {
                lines = System.IO.File.ReadAllLines(@"" + Dest(0));
                foreach (string s in lines)
                {
                    string[] parts = s.Split(',');
                    // 0 - ID PRODUCTO
                    // 1 - NOMBRE
                    // 2 - PRECIO
                    // 3 - CANTIDAD
                    // 4 - ID CATEGORIA
                    AgregarProducto(parts[1], double.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));
                }
            }
            // 1 - Usuarios
            /*if (System.IO.File.Exists(Dest(1)))
            {
                lines = System.IO.File.ReadAllLines(@"" + Dest(1));
                foreach (string s in lines)
                {
                    string[] parts = s.Split(',');
                    // 0 - ID
                    // 1 - DNI
                    // 2 - NOMBRE
                    // 3 - APELLIDO
                    // 4 - MAIL
                    // 5 - PASSWORD
                    // 6 - CUIT_CUIL
                    // 7 - ROL
                    AgregarUsuario(int.Parse(parts[1]), parts[2], parts[3], parts[4], parts[5], int.Parse(parts[6]), int.Parse(parts[7]));
                }
            }*/

            // 2 - Carro
            if (System.IO.File.Exists(Dest(2)))
            {
                lines = System.IO.File.ReadAllLines(@"" + Dest(2));
                foreach (string s in lines)
                {
                    string[] parts = s.Split(',');
                    // 0 - ID PRODUCTO
                    // 1 - CANTIDAD
                    // 2 - ID USUARIO
                    AgregarAlCarro(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
                }
            }

            // 3 - Compras
            if (System.IO.File.Exists(Dest(3)))
            {
                lines = System.IO.File.ReadAllLines(@"" + Dest(3));
                foreach (string s in lines)
                {
                    string[] parts = s.Split(',');
                    // 0 - ID COMPRA
                    // 1 - ID USUARIO
                    // 3 - PRODUCTO
                    // 4 - TOTAL
                    Comprar(int.Parse(parts[1]));
                }
            }
        }
        /* 
        public int compare(Categoria a, Categoria b) {
            if (a is Categoria && b is Categoria)
            {
                char[] arr = (a.nNombre.ToUpper()).ToCharArray();
                char first = arr[0];

                char[] arr2 = (b.nNombre.ToUpper()).ToCharArray();
                int cant = (arr.Length > arr2.Length) ? arr2.Length : arr.Length;

                for (int i = 0; i < cant; i++) 
                { 
                    if (arr[i] < arr2[i])
                    {
                        return -1;
                    }
                    else if (arr[i] > arr2[i])
                    {
                        return 1;
                    }
                }
                return 0;
            }
            return 0;
        }
        */



    }
}
