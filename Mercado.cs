using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_2_PlataformasDeDesarrollo
{
    class Mercado
    {
        private List<Producto> Productos;
        private List<Usuario> Usuarios;
        private const int MaxCategorias = 10;
        private int CantCategorias = 0;
        private Categoria[] Categorias;
        private List<Compra> Compras;

        private string[] fileName = { "productos.txt", "usuario.txt", "carro.txt", "compras.txt", "categoria.txt" };
        private string sourcePath ;
        private string targetPath ;



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
            

            nSourcePath = System.IO.Directory.GetCurrentDirectory()+"/../../Archivos";
            nTargetPath = "C:/ArchivosMercado";
            Console.WriteLine(nTargetPath);

            //PREGUNTAMOS SI EXISTE EL DIRECTORIO TARGET
            if (System.IO.Directory.Exists(nTargetPath))
            {
                string[] files = System.IO.Directory.GetFiles(nTargetPath);
                foreach (string s in fileName)
                {
                    int cont = 0;
                    //PREGUNTAMOS SI EN EL DIRECTORIO TARGET, SE ENCUENTRAN LOS ARCHIVOS
                    if (!files.Contains(s))
                    {
                        //SI NO EXISTEN LOS ARCHIVOS, LOS COPIAMOS DE SOURCE
                        System.IO.File.Copy(System.IO.Path.Combine(nSourcePath, fileName[cont]), Dest(cont), true);
                        cont++;
                    }
                }
            }
            else 
            {
                //SI NO EXITE EL DIRECTORIO TARGET, LO CREAMOS
                System.IO.Directory.CreateDirectory(nTargetPath);
                //COPIAMOS TODOS LOS ARCHIVOS DE SOURCE A TARGET
                for (int i = 0 ; i < fileName.Length ; i++)
                {
                    System.IO.File.Copy(System.IO.Path.Combine(nSourcePath, fileName[i]), Dest(i));
                }
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
                    List<string> texto;
                    System.IO.File.ReadAllLines(Dest(0));
                    /* ***************************************** */
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
        public bool AgregarUsuario(int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            int n = Usuarios.Count();
            n++;
            if (EsEmpresa)
            {
              //Usuarios.Add(new Empresa(n, DNI, nombre, apellido, Mail, password, CUIT_CUIL));
                Console.WriteLine("Usuario Empresa agregado con exito!");
                return true;
            }
            else
            {
              //Usuarios.Add(new ClienteFinal(n, DNI, nombre, apellido, Mail, password, CUIT_CUIL));
                Console.WriteLine("Usuario Cliente Final agregado con exito!");
                return true;
            }
            //Console.WriteLine("ERROR: No se pudo agregar el usuario");
            //return false;
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            foreach (Usuario u in Usuarios)
            {
                /*
                 if (u.nID == ID)
                 {
                     if (u is Empresa && EsEmpresa == true)
                     {
                         Empresa e = (Empresa)u;
                         e.nDNI = DNI;
                         e.nNombre = nombre;
                         e.nApellido = apellido;
                         e.nMail = Mail;
                         e.nPassword = password;
                         e.nCUIT = CUIT_CUIL;
                         Console.WriteLine("Usuario Empresa modificado con exito!");
                         return true;
                     }
                     else if (u is ClienteFinal && EsEmpresa == false)
                     {
                         ClienteFinal c = (ClienteFinal)Usuarios[ID];
                         c.nDNI = DNI;
                         c.nNombre = nombre;
                         c.nApellido = apellido;
                         c.nMail = Mail;
                         c.nPassword = password;
                         c.nCUIL = CUIT_CUIL;
                         Console.WriteLine("Usuario Cliente Final modificado con exito!");
                         return true;
                     }

                 }
                */
            }
            if (EsEmpresa == true)
            {
                Console.WriteLine("ERROR: no hay Usuario Empresa con ese ID: " + ID);
                return false;
            }
            else
            {
                Console.WriteLine("ERROR: no hay Usuario Cliente Final con ese ID: " + ID);
                return false;
            }

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

        public bool EliminarCategoria(int ID) // a probar ****************************** MODIFICADA, PREGUNTAR OPINIONES DE LOS DEMAS
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

        } /* MODIFICADA, PREGUNTAR OPINIONES DE LOS DEMAS   */

        public void MostrarCategoria()
        {
            /* ************************************************************* MODIFICADO, LO PIDIO EL PROFE, CAMBIAR IS POR VERIFICACION DE NULL */

            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i] != null)
                {
                    Console.WriteLine(Categorias[i].ToString());
                }

            }
            /* ************************************************************* */
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
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario) /*  MODIFICADO, PREGUNTAR OPINION DE LOS DEMAS  */
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
               /* 
                if (Usuarios[ID_Usuario] is Empresa)
                {
                    total = (total * 21) / 100;
                }
               */
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
            return true;//solo para que no tire error
        }

        public bool EliminarCompra(int ID)
        {
            return true;//solo para que no tire error
        }

        // #######################################################################################
        //                                  INICIAR SESION
        // #######################################################################################


        public int IniciarSesion(int DNI, string pass) // PARA SEBA  
        {
            //SEBA VA A HACER ESTO, sino lo hace le pegamo!
            return 1;
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
