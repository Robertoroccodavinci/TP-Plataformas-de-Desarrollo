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
        public const int MaxCategorias=10; //POR AHORA
        public int CantCategorias;
        public Categoria[] Categorias = new Categoria[MaxCategorias];
        public List <Compra> Compras = new List<Compra>();
        
        
        //public Dictionary<string, int> prueba = new Dictionary<string, int>();
        


        // METODOS DE PRODUCTO
        public bool AgregarProducto(string nombre, double precio, int cantidad, int ID_Categoria) 
        {
            int n = Productos.Count();
            foreach (Producto p  in  Productos) 
            {
                if (p.nNombre == nombre || nombre == "") 
                {
                    return false;  
                
                }
            }
            
            Productos.Add(new Producto(n++, nombre, precio, cantidad, Categorias[ID_Categoria]));
            return true;
        }
        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            if (Productos[ID] != null /*|| Productos[ID] == 'undefined'*/) {  //falta probar
                Productos[ID].nNombre = nombre;
                Productos[ID].nPrecio = precio;
                Productos[ID].nCantidad = cantidad;
                Productos[ID].nCategoria = Categorias[ID_Categoria];
                return true;
            }
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

        //METODOS DE USUARIO
        public bool AgregarUsuario(int DNI,string nombre, string apellido, string Mail,string password, int CUIT_CUIL, bool EsEmpresa)
        {
            int n = Usuarios.Count();
            if (EsEmpresa){
                Usuarios.Add(new Empresa(n++, DNI, nombre, apellido, Mail, password, CUIT_CUIL)); 
            } else { 
                Usuarios.Add(new ClienteFinal(n++, DNI, nombre, apellido, Mail, password, CUIT_CUIL));
            }
            
            return true;//solo para que no tire error
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            if (Usuarios[ID] != null)
            {
                if (EsEmpresa)
                {
                    Empresa e = (Empresa) Usuarios[ID];
                    e.nDNI = DNI;
                    e.nNombre = nombre;
                    e.nApellido = apellido;
                    e.nMail = Mail;
                    e.nPassword = password;
                    e.nCUIT=CUIT_CUIL;
                    return true;
                }
                else
                {
                    ClienteFinal c = (ClienteFinal) Usuarios[ID];
                    c.nDNI = DNI;
                    c.nNombre = nombre;
                    c.nApellido = apellido;
                    c.nMail = Mail;
                    c.nPassword = password;
                    c.nCUIL = CUIT_CUIL; 
                    return true;
                }
            }
                return false;
        }

        public bool EliminarUsuario(int ID)
        {
            if (Usuarios.Remove(Usuarios[ID])) {
                return true;
            }

            return false;
        }
        
        public void MostrarUsuarios()
        {
            Usuarios.Sort();
            foreach (Usuario u in Usuarios) {
                Console.WriteLine(u.ToString());
            }
        }

        //METODOS DE CATEGORIA

        public bool AgregarCategoria(string nombre)
        {
            if (CantCategorias < MaxCategorias) {
                CantCategorias++;
                Categorias[CantCategorias] = new Categoria(CantCategorias, nombre);
                return true;
            }
            return false;
        }
        public bool ModificarCategoria(int ID,string nombre)
        {
            if (Categorias[ID] != null) {
                Categorias[ID].nNombre = nombre;
                return true;
            }
            
            return false;
        }
        public bool EliminarCategoria(int ID) // a probar
        {
            if (Categorias[ID] != null)
            {
                Array.Clear(Categorias, ID, 1);
                Categorias[ID] = null;
                return true;
            }
                
            return false;
        }
        public void MostrarCategoria()
        {
            Array.Sort(Categorias);
            foreach (Categoria c in Categorias) 
            {
                Console.WriteLine(c.ToString());
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
        public bool VaciarCarro(int ID_Usuario) //revisar 
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
