using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_Plataformas_de_Desarrollo
{
    class Mercado
    {
        private List <Producto> Productos;
        private List<Usuario> Usuarios ;
        private const int MaxCategorias = 10;
        private int CantCategorias;
        private Categoria[] Categorias;
        private List <Compra> Compras;
       
        //public Dictionary<string, int> prueba = new Dictionary<string, int>();
        
        public Mercado()
        {
            nProductos = new List<Producto>();
            nUsuarios = new List<Usuario>();
            nCompras = new List<Compra>();
            nCategorias = new Categoria[MaxCategorias];

        }
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


        // METODOS DE PRODUCTO
        public bool AgregarProducto(string nombre, double precio, int cantidad, int ID_Categoria) 
        {
            /*int n = nProductos.Count();
            n++;*/
            
            foreach (Producto p  in  nProductos) 
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
                if(Categorias[i] != null && Categorias[i].nID == ID_Categoria) {
                    int idProd;
                    if (Productos.Contains(null)) {
                        idProd = Productos.IndexOf(null);
                    }
                    else
                    {
                        idProd = Productos.Count();
                    }
                    Productos.Add(new Producto(idProd, nombre, precio, cantidad, Categorias[i]));
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
            if (IDProd > 0)
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
        
            //int cont = 0;
            //while (Productos[cont].nIDProd != ID)
            //{
            //    if (Productos.Count <= cont) { 
            //        if (Productos[cont].nIDProd == ID)
            //        {
            //            Productos[cont].nNombre = nombre;
            //            Productos[cont].nPrecio = precio;
            //            Productos[cont].nCantidad = cantidad;
            //            for (int i = 0; i < MaxCategorias; i++)
            //            {
            //                if (Categorias[i].nID == ID_Categoria)
            //                {
            //                    Productos[i].nCategoria = Categorias[i];
            //                    Console.WriteLine("Producto modificado correctamente!");
            //                    return true;
            //                }
            //            }
            //        }
            //        cont++;
            //    }
            //    else
            //    {
                    
            //        return false;
            //    }
            //}




            //foreach (Producto p in Productos) 
            //{
            //    if (p.nIDProd == ID) 
            //    {
            //        p.nNombre = nombre;
            //        p.nPrecio = precio;
            //        p.nCantidad = cantidad;
            //        for (int i = 0; i < MaxCategorias;i++) 
            //        {
            //            if (Categorias[i].nID==ID_Categoria) 
            //            {
            //                p.nCategoria = Categorias[i];
            //                Console.WriteLine("Producto modificado correctamente!");
            //                return true;
            //            }
            //        }
                    
            //    }
            //}
            //Console.WriteLine("ERROR: producto no encontrado.");
            //return false;

        }
        public bool EliminarProducto(int ID)
        {
            if (Productos[ID] != null) {
                Productos[ID] = null;
                Console.WriteLine("Producto eliminado correctamente.");
                return true;
            }
            Console.WriteLine("ERROR: No se encontro el producto con ese ID");
            return false;


        }
        public void BuscarProducto(string Query)
        {
            Productos.Sort();
            foreach (Producto p in Productos) {
                if (p.nNombre == Query) {
                    Console.WriteLine(p.ToString());
                }
            }
        }
        public void BuscarProductoPorPrecio(string Query)
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio);});
            foreach (Producto p in Productos)
            {
                if (p.nNombre == Query)
                {
                    Console.WriteLine(p.ToString());
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
                    Console.WriteLine(p.ToString());
                }
            }
        }
        public void MostrarTodosProductosPorPrecio()
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio); });
            foreach (Producto p in Productos)
            {
                Console.WriteLine(p.ToString());
            }
        }
        public void MostrarTodosProductosPorCategoria()
        {
            foreach (Categoria c in Categorias) 
            {
                if  (c is Categoria) { 
                    Console.WriteLine(c.ToString());
                    foreach (Producto p in Productos)
                    {
                        if (c.nID == p.nCategoria.nID)
                        {
                            Console.WriteLine(p.ToString());
                        }
                    }
                }
                
            }
        }

        //METODOS DE USUARIO
        public bool AgregarUsuario(int DNI,string nombre, string apellido, string Mail,string password, int CUIT_CUIL, bool EsEmpresa)
        {
            int n = Usuarios.Count();
            n++;
            if (EsEmpresa){
                Usuarios.Add(new Empresa(n, DNI, nombre, apellido, Mail, password, CUIT_CUIL));
                Console.WriteLine("Usuario Empresa agregado con exito!");
                return true;
            } else { 
                Usuarios.Add(new ClienteFinal(n, DNI, nombre, apellido, Mail, password, CUIT_CUIL));
                Console.WriteLine("Usuario Cliente Final agregado con exito!");
                return true;
            }
            Console.WriteLine("ERROR: No se pudo agregar el usuario");
            return false;
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            foreach (Usuario u in Usuarios)
            {
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
                    else if(u is ClienteFinal && EsEmpresa == false)
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
            }
            if (EsEmpresa == true)
            {
                Console.WriteLine("ERROR: no hay Usuario Empresa con ese ID: " + ID);
                return false;
            }
            else 
            {
                Console.WriteLine("ERROR: no hay Usuario Cliente Final con ese ID: " +ID);
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
            foreach (Usuario u in Usuarios) {
                if (u is Empresa)
                {
                    Empresa e = (Empresa)u;
                    Console.WriteLine(e.ToString());
                }
                else 
                {
                    ClienteFinal c = (ClienteFinal)u;
                    Console.WriteLine(c.ToString());
                }
                
                
            }
        }

        //METODOS DE CATEGORIA

        public bool AgregarCategoria(string nombre)
        {
            
            if (CantCategorias < MaxCategorias) {
                for  (int i = 0; i < MaxCategorias; i++)
                {
                    if(Categorias[i] == null) {          
            
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
        public bool ModificarCategoria(int ID,string nombre)
        {
            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i].nID == ID)
                {
                    Categorias[i].nNombre = nombre;
                    
                    Console.WriteLine("Categoria modificada con exito!");
                    return true;
                }

            }
            Console.WriteLine("ERROR: no hay categoria con ID: "+ID);
            return false;
        }
        public bool EliminarCategoria(int ID) // a probar
        {
            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i].nID == ID)
                {

                    Array.Clear(Categorias, ID, 1);
                    Categorias[i] = null;
                    CantCategorias--;
                    
                    Console.WriteLine("Categoria eliminada con exito!");
                    return true;
                }

            }
            Console.WriteLine("ERROR: no hay categoria con ID: " + ID);
            return false;

        }
        
        public void MostrarCategoria()
        {
            /* ************************************************************* */
            for (int i = 0; i < MaxCategorias; i++)
            {
                if (Categorias[i] != null)
                {
                    Console.WriteLine(Categorias[i].ToString());
                }

            }
            /* ************************************************************* */
        }

        // METODOS DE CARRO

        public bool AgregarAlCarro(int ID_Producto,int Cantidad, int ID_Usuario)
        {
            if (Productos[ID_Producto].nCantidad >= Cantidad) {

                Usuarios[ID_Usuario].nCarro.AgregarProducto(Productos[ID_Producto], Cantidad);
                Console.WriteLine("Producto agregada con exito al Carro.");
                return true;
            }
            Console.WriteLine("ERROR: el Producto no se pudo agregar al carro al Carro.");
            return false;
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            
            foreach (Producto p in Usuarios[ID_Usuario].nCarro.nProductos.Keys) {
                if (p.nIDProd == ID_Producto) {
                    if (Cantidad <= p.nCantidad )
                    {
                        Usuarios[ID_Usuario].nCarro.QuitarProducto(Productos[ID_Producto], Cantidad);
                        Console.WriteLine("Producto eliminado del Carro.");
                        return true;
                    }
                }
            }
            Console.WriteLine("ERROR: no se encontro producto con el ID "+ID_Producto+" en el Carro.");
            return false;
        }
        public bool VaciarCarro(int ID_Usuario) 
        {
            Usuarios[ID_Usuario].nCarro.Vaciar();
            Console.WriteLine("Carro vaciado con exito!");
            return true;
        }


        //METODOS DE COMPRA
        public bool Comprar(int ID_Usuario)
        {
            double total=0;
            int n = Compras.Count();
            foreach (KeyValuePair<Producto, int> kvp in Usuarios[ID_Usuario].nCarro.nProductos)
            {
                total += kvp.Key.nPrecio * kvp.Value;
                
            }
            if (total > 0) {
                if (Usuarios[ID_Usuario] is Empresa)
                {
                    total = (total * 21) / 100;
                }
                Compras.Add(new Compra(n++, Usuarios[ID_Usuario], Usuarios[ID_Usuario].nCarro.nProductos,total));

                foreach (KeyValuePair<Producto, int> kvp in Compras[n].nProductos)
                {
                    foreach (Producto p in Productos) {
                        if (kvp.Key == p) {
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

        public int compare(Categoria a, Categoria b)
        {
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





    }
}
