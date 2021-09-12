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
        public List <Producto> Productos = new List<Producto>();
        public List<Usuario> Usuarios = new List<Usuario>();
        public const int MaxCategorias = 10; //POR AHORA
        public int CantCategorias;
        public Categoria[] Categorias = new Categoria[MaxCategorias];
        public List <Compra> Compras = new List<Compra>();
       
        //public Dictionary<string, int> prueba = new Dictionary<string, int>();
        
        public Mercado()
        {
           
        }

        // METODOS DE PRODUCTO
        public bool AgregarProducto(string nombre, double precio, int cantidad, int ID_Categoria) 
        {
            int n = Productos.Count();
            n++;
            foreach (Producto p  in  Productos) 
            {
                if (p.nNombre == nombre || nombre == "" || nombre == null) 
                {
                    Console.WriteLine("ERROR: ya existe ese producto");
                    return false;
                }
            }

            for (int i = 0; i <= MaxCategorias; i++)
            {
                if(Categorias[i] is Categoria) {
                    if (Categorias[i].nID == ID_Categoria) {
                            Productos.Add(new Producto(n, nombre, precio, cantidad, Categorias[i]));
                            Console.WriteLine("Producto agregado correctamente!");
                            return true;
                    }
                }
            }
            Console.WriteLine("ERROR: no se pudo agregar el producto");
            return false;
        }
        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            foreach (Producto p in Productos) 
            {
                if (p.nIDProd == ID) 
                {
                    p.nNombre = nombre;
                    p.nPrecio = precio;
                    p.nCantidad = cantidad;
                    for (int i = 0; i <= MaxCategorias;i++) 
                    {
                        if (Categorias[i].nID==ID_Categoria) 
                        {
                            p.nCategoria = Categorias[i];
                            Console.WriteLine("Producto modificado correctamente!");
                            return true;
                        }
                    }
                    
                }
            }
            Console.WriteLine("ERROR: producto no encontrado");
            return false;

        }
        public bool EliminarProducto(int ID)
        {
            if (Productos[ID] != null) {
                Productos.Remove(Productos[ID]);
                return true;
            }
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
            Console.WriteLine("ERROR");
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
                Console.WriteLine("ERROR: no hay Usuario Empresa con ese ID");
                return false;
            }
            else 
            {
                Console.WriteLine("ERROR: no hay Usuario Cliente Final con ese ID");
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

            Console.WriteLine("ERROR: ID usuario no encontrado");
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
                for  (int i = 0; i <= MaxCategorias; i++)
                {
                    if(Categorias[i] == null) {          
                        Categorias[i] = new Categoria(i, nombre);
                        CantCategorias++;
                        Console.WriteLine("Categoria agregada con exito!");
                        return true;
                    }
                    
                }
                
                
            }
            Console.WriteLine("ERROR: no se puede agregar mas categorias");
            return false;
        }
        public bool ModificarCategoria(int ID,string nombre)
        {
            for (int i = 0; i <= MaxCategorias; i++)
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
            for (int i = 0; i <= MaxCategorias; i++)
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
           
            foreach (Categoria c in Categorias) 
            {
                if  (c is Categoria) {

                    Console.WriteLine(c.ToString());
                }
            }

        }

        // METODOS DE CARRO

        public bool AgregarAlCarro(int ID_Producto,int Cantidad, int ID_Usuario)
        {
            if (Productos[ID_Producto].nCantidad >= Cantidad) {

                Usuarios[ID_Usuario].nCarro.AgregarProducto(Productos[ID_Producto], Cantidad);
                return true;
            }
            
            return false;
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            
            foreach (Producto p in Usuarios[ID_Usuario].nCarro.nProductos.Keys) {
                if (p.nIDProd == ID_Producto) {
                    if (Cantidad <= p.nCantidad )
                    {
                        Usuarios[ID_Usuario].nCarro.QuitarProducto(Productos[ID_Producto], Cantidad);
                        return true;
                    }
                }
            }
                
            return false;
        }
        public bool VaciarCarro(int ID_Usuario) 
        {
            Usuarios[ID_Usuario].nCarro.Vaciar();
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
                return true;
            }
            
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




    }
}
