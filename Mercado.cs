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
        public int MaxCategorias;
        public int CantCategorias;
        public List <Categoria> Categorias = new List<Categoria>();
        public List <Compra> Compras = new List<Compra>();
        
        
        //public Dictionary<string, int> prueba = new Dictionary<string, int>();
        


        // METODOS DE PRODUCTO
        public bool AgregarProducto(string nombre, double precio, int cantidad, int ID_Categoria) 
        {
            return true;//solo para que no tire error
        }
        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            return true;//solo para que no tire error
        }
        public bool EliminarProducto(int ID)
        {
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
            return true;//solo para que no tire error
        }
        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, bool EsEmpresa)
        {
            return true;//solo para que no tire error
        }

        public bool EliminarUsuario(int ID)
        {
            return true;//solo para que no tire error
        }

        public void MostrarUsuarios()
        {
            
        }

        //METODOS DE CATEGORIA

        public bool AgregarCategoria(string nombre)
        {
            return true;//solo para que no tire error
        }
        public bool ModificarCategoria(int ID,string nombre)
        {
            return true;//solo para que no tire error
        }
        public bool EliminarCategoria(int ID)
        {
            return true;//solo para que no tire error
        }
        public void MostrarCategoria()
        {
            
        }

        // METODOS DE CARRO

        public bool AgregarAlCarro(int ID_Producto,int Cantidad, int ID_Usuario)
        {
            return true;//solo para que no tire error
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            return true;//solo para que no tire error
        }
        public bool VaciarCarro(int ID_Usuario)
        {
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
