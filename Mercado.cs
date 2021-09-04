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
            Productos.Add(new Producto(n++, nombre, precio, cantidad, ID_Categoria)) ;
            return true;//solo para que no tire error
        }
        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            Productos[ID].nNombre = nombre;
            Productos[ID].nPrecio = precio;
            Productos[ID].nCantidad = cantidad;
            Productos[ID].nCategoria = Categorias[ID_Categoria];
            return true;//solo para que no tire error
        }
        public bool EliminarProducto(int ID)
        {
            Productos.Remove(Productos[ID]);
            return true;//solo para que no tire error


        }
        public void BuscarProducto(string Query)
        {
            
        }
        public void BuscarProductoPorPrecio(string Query)
        {

        }
        public void BuscarProductoPorCategoria(int ID_Categoria)
        {
           
        }
        public void MostrarTodosProductosPorPrecio()
        {
            
        }
        public void MostrarTodosProductosPorCategoria()
        {

        }

        //METODOS DE USUARIO
        public bool AgregarUsuario(int DNI,string nombre, string apellido, string Mail,string password, int CUIT_CUIL, bool EsEmpresa)
        {
            if(EsEmpresa){
                Empresa empresa = new Empresa(nombre, apellido, Mail, password, CUIT_CUIL); 
            } else { 
                ClienteFinal cliente = new ClienteFinal(CUIT_CUIL, nombre, apellido, Mail, password);
            }
            
            return true;//solo para que no tire error
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            return true;//solo para que no tire error
        }

        public bool EliminarUsuario(int ID)
        {
            // INDICE A ELIMINAR
            int indiceAEliminar = Usuarios.findIndex(usuario => usuario.nID == ID);
            
            // ELIMINAR EL USUARIO 
            Usuarios.Remove(indiceAEliminar, 1);
            
            return true;
        }
        
        public void MostrarUsuarios()
        {
            
        }

        //METODOS DE CATEGORIA

        public bool AgregarCategoria(string nombre)
        {
            if (CantCategorias < MaxCategorias) { 
                Categorias[CantCategorias] = new Categoria(CantCategorias, nombre);
                CantCategorias++;
                return true;
            }
            return false;
        }
        public bool ModificarCategoria(int ID,string nombre)
        {
            Categorias[ID].nNombre=nombre;
            return true;//solo para que no tire error
        }
        public bool EliminarCategoria(int ID)
        {
            Array.Clear(Categorias, 0, ID);
            return true;//solo para que no tire error
        }
        public void MostrarCategoria()
        {
            for(int i= 0; i <= MaxCategorias; i++) {
                Console.WriteLine(Categorias[i]);
            }
        }

        // METODOS DE CARRO

        public bool AgregarAlCarro(int ID_Producto,int Cantidad, int ID_Usuario)
        {
            Usuarios[ID_Usuario].nCarro.AgregarProducto(Productos[ID_Producto], Cantidad);
            return true;//solo para que no tire error
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            Usuarios[ID_Usuario].nCarro.QuitarProducto(Productos[ID_Producto], Cantidad);
                
            return true;//solo para que no tire error
        }
        public bool VaciarCarro(int ID_Usuario)
        {
            Usuarios[ID_Usuario].nCarro.Vaciar();
            return true;//solo para que no tire error
        }

        //METODOS DE COMPRA
        public bool Comprar(int ID_Usuario)
        {
            return true;//solo para que no tire error
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
