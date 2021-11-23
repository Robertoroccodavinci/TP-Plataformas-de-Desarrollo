using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace TP_Plataformas_de_Desarrollo
{
    class Mercado
    {
        private const int MaxCategorias = 10;//   QUITAMOS ESTAS VARIABLES? 
        private int CantCategorias = 0;

        private MyContext contexto;

        public Mercado()
        {
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            try
            {
                // inicio el contexto
                nContexto = new MyContext();

                // cargo las entidades
                contexto.categorias.Load();
                contexto.productos.Include(p => p.compras).Include(p=> p.carros).Load();
                contexto.usuarios.Load();
                contexto.carros.Load();
                contexto.compras.Load();

                CantCategorias = contexto.categorias.Count();

                // guardamos
                contexto.SaveChanges();
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR AL INICIALIZAR ATRIBUTOS");
            }
        }

        public MyContext nContexto
        {
            get { return contexto; }
            set { contexto = value; }
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
            try
            {
                if (contexto.productos.Where(P => P.nombre == nombre && P.cat.idCategoria == ID_Categoria).FirstOrDefault() == null &&
                    contexto.categorias.Where(C => C.idCategoria == ID_Categoria).FirstOrDefault() != null)
                {
                    Categoria cat = contexto.categorias.Where(C => C.idCategoria == ID_Categoria).FirstOrDefault();
                    Producto aux = new Producto(nombre, precio, cantidad, cat);
                    contexto.productos.Add(aux);

                    contexto.SaveChanges();
                }
                else
                {
                    MessageBox.Show("ERROR en los datos ingresados");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no se pudo agregar el producto");
                return false;
            }
            MessageBox.Show("Producto agregado exitosamente!");
            return true;
        }

        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            try
            {
                if (contexto.productos.Where(P => P.idProducto == ID).FirstOrDefault() != null &&
                    contexto.categorias.Where(C => C.idCategoria == ID_Categoria).FirstOrDefault() != null)
                {
                    Producto aux = contexto.productos.Where(P => P.idProducto == ID).FirstOrDefault();
                    aux.nombre = nombre;
                    aux.precio = precio;
                    aux.cantidad = cantidad;
                    aux.idCategoria = ID_Categoria;


                    contexto.productos.Update(aux);
                    contexto.SaveChanges();
                }
                else
                {
                    MessageBox.Show("ERROR en los datos ingresados");
                    return false;
                }
            }
            catch (Exception )
            {
                MessageBox.Show("ERROR: no pudo modoficar el Producto ");
                return false;
            }

            MessageBox.Show("Producto modificado con exito!");
            return true;
            
        }

        public bool EliminarProducto(int ID)
        {
            try
            {
                if (nContexto.productos.Where(p => p.idProducto == ID).FirstOrDefault() != null)
                {
                    Producto p = nContexto.productos.Where(p => p.idProducto == ID).FirstOrDefault();
                    nContexto.productos.Remove(p);

                    nContexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: No se encontro el producto con ese ID");
                    return false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no se pudo eliminar el producto");
                return false;
            }
            
            MessageBox.Show("Producto eliminado correctamente.");
            return true;
        }

        public List<Producto> BuscarProducto(string Query,int orden)
        {
            if (Query!="") 
            {
                var query = from prod in nContexto.productos
                            where prod.nombre.Contains(Query)
                            orderby prod.nombre
                            select prod;
                return query.ToList();
            }
            else if (orden == 1)
            {
                var query = from prod in nContexto.productos
                            orderby prod.nombre ascending
                            select prod;
                return query.ToList();
            }
            else
            {
                var query = from prod in nContexto.productos
                            orderby prod.nombre descending 
                            select prod;
                return query.ToList();
            }
        }

        public List<Producto> BuscarProductoPorPrecio(string Query)
        {
            var query = from prod in nContexto.productos
                        where prod.precio <= int.Parse(Query)
                        orderby prod.precio
                        select prod;
            return query.ToList();
        }


        public List<Producto> BuscarProductoPorCategoria(string nombre)
        {
            var query = from prod in nContexto.productos
                        where prod.cat.nombre.Contains(nombre)
                        orderby prod.cat.nombre
                        select prod;
            return query.ToList();
        }


        public List<Producto> MostrarTodosProductosPorPrecio(int orden)
        {
            if (orden == 1)
            {
                var query = from prod in nContexto.productos
                            orderby prod.precio ascending
                            select prod;
                return query.ToList();
            }
            else
            {
                var query = from prod in nContexto.productos
                            orderby prod.precio descending
                            select prod;
                return query.ToList();
            }

            
        }

        public List<Producto> MostrarTodosProductosPorCategoria(int orden)
        {
            
            if (orden == 1)
            {
                var query = from prod in nContexto.productos
                            orderby prod.idCategoria ascending
                            select prod;
                return query.ToList();
            }
            else 
            {
                var query = from prod in nContexto.productos
                            orderby prod.idCategoria descending
                            select prod;
                return query.ToList();
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
            try 
            {
                if (contexto.usuarios.Where(U => U.dni == DNI && U.password == password).FirstOrDefault() == null) 
                {
                    Usuario aux = new Usuario(DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol);
                    contexto.usuarios.Add(aux);
                    Carro auxC = new Carro(aux);
                    contexto.carros.Add(auxC);

                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR ya existe el usuario");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR no se pudo agregar el usuarios");
                return false;
            }

            return true;
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, int CUIT_CUIL, int rol)
        {
            try
            {
                if (contexto.usuarios.Where(u => u.idUsuario == ID).FirstOrDefault() != null)
                {
                    Usuario aux = contexto.usuarios.Where(u => u.idUsuario == ID).FirstOrDefault();
                    aux.dni = DNI;
                    aux.nombre = nombre;
                    aux.apellido = apellido;
                    aux.mail = Mail;
                    aux.password = password;
                    aux.cuit_cuil = CUIT_CUIL;
                    aux.rol = rol;

                    contexto.usuarios.Update(aux);
                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: no hay Usuario con ese ID: " + ID);
                    return false;
                }
                
            }
            catch (Exception )
            {
                MessageBox.Show("ERROR: no se pudo modificar el usuario");
                return false;
            }

            MessageBox.Show("Usuario modificado con exito!");
            return true;
        }

        public bool EliminarUsuario(int ID) // FALTA PROGRAMARLA BIEN, JUNTO A LA BASE DE DATOS
        {
            try
            {
                Usuario aux = contexto.usuarios.Where(u => u.idUsuario == ID).FirstOrDefault();
                contexto.usuarios.Remove(aux);
                
                contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //invalid null exception
                MessageBox.Show("ERROR: ID: " + ID + " usuario no encontrado");
                return false;
            }
            MessageBox.Show("Usuario eliminado con exito!");
            return true;
        }

        public List<Usuario> MostrarUsuarios()
        {
            var query = from users in nContexto.usuarios
                        orderby users.idUsuario
                        select users;
            return query.ToList();
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
            try
            {
                if (CantCategorias < MaxCategorias)
                {
                    if (contexto.categorias.Where(c => c.nombre == nombre).FirstOrDefault() == null)
                    {
                        Categoria aux = new Categoria(nombre);
                        contexto.categorias.Add(aux);
                        contexto.SaveChanges();
                        CantCategorias++;
                    }
                    else
                    {
                        MessageBox.Show("ERROR: ya existe la categoria");
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("ERROR: No se pueden agregar mas categorias");
                    return false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no pudo agregar la categoria");
                return false;
            }
            MessageBox.Show("Categoria agregada con exito!");
            return true;
        }

        public bool ModificarCategoria(int ID, string nombre)
        {
            try
            {
                if (contexto.categorias.Where(c => c.idCategoria == ID).FirstOrDefault() != null)
                {
                    Categoria aux = contexto.categorias.Where(c => c.idCategoria == ID).FirstOrDefault();
                    aux.nombre = nombre;
                    contexto.Update(aux);
                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: no existe la categoria ");
                    return false;
                }
              
            }
            catch (Exception )
            {
                MessageBox.Show("ERROR: no se pudo modificar la categoria");
                return false;
            }
            MessageBox.Show("Categoria modificada con exito!");
            return true;
        }

        public bool EliminarCategoria(int ID)
        {
            try
            {
                if (contexto.categorias.Where(c => c.idCategoria == ID).FirstOrDefault() != null)
                {
                    Categoria aux = contexto.categorias.Where(c => c.idCategoria == ID).FirstOrDefault();
                    //CantCategorias--;
                  
                    contexto.Remove(aux);
                                        
                    contexto.SaveChanges();
                    
                    CantCategorias--;
                }
                else 
                {
                    MessageBox.Show("ERROR: no existe la Categoriase");
                    return false;
                }
                
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: la Categoria no se pudo eliminadar");
                return false;
            }
            MessageBox.Show("Categoria eliminada con exito");
            return true;
        }

        public List<Categoria> MostrarCategoria()
        {
            var query = from cat in nContexto.categorias
                        orderby cat.nombre
                        select cat;
            return query.ToList();
        }

        // #######################################################################################
        //                                  METODOS DE CARRO
        // #######################################################################################
        //                                  AGREGAR AL CARRO
        //                                  MODIFICAR CARRO
        //                                  VACIAR CARRO
        // #######################################################################################

        public bool AgregarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            try
            {
                Carro aux = contexto.usuarios.Where(U => U.idUsuario == ID_Usuario).FirstOrDefault().miCarro;
                aux.productos.Add(contexto.productos.Where(P => P.idProducto == ID_Producto).FirstOrDefault());
                contexto.carros.Update(aux);

                contexto.SaveChanges();

                foreach (CarroProducto CarProd in aux.carroProducto)
                {
                    if (CarProd.idProducto == ID_Producto)
                    {
                        CarProd.cantidad = Cantidad;
                        contexto.carros.Update(aux);
                        Producto prod = contexto.productos.Where(P => P.idProducto == ID_Producto).FirstOrDefault();
                        prod.cantidad -= Cantidad;
                        contexto.productos.Update(prod);

                        contexto.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: No se puede agregar producto al Carro");
                return false;
            }
            MessageBox.Show("Producto agregado al Carro correctamente");
            return true;
        }


        public bool ModificarCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            try
            {
                if (contexto.carros.Where(c => c.idUsuario == ID_Usuario).FirstOrDefault() != null)
                {
                    Carro aux = contexto.carros.Where(c => c.idUsuario == ID_Usuario).FirstOrDefault();
                    Producto p = contexto.productos.Where(p => p.idProducto == ID_Producto).FirstOrDefault();

                    if (Cantidad == 0)
                    {
                        p.cantidad += aux.carroProducto.Where(cp => cp.producto.idProducto == ID_Producto).FirstOrDefault().cantidad;
                        aux.productos.Remove(aux.productos.Where(p => p.idProducto == ID_Producto).FirstOrDefault());
                        contexto.carros.Update(aux);
                        contexto.productos.Update(p);
                        contexto.SaveChanges();
                    }
                    else
                    {
                        int Cant = aux.carroProducto.Where(p => p.idProducto == ID_Producto).FirstOrDefault().cantidad;
                        p.cantidad += Cant;
                        aux.carroProducto.Where(p => p.idProducto == ID_Producto).FirstOrDefault().cantidad = Cantidad;
                        p.cantidad -= Cantidad;
                        contexto.carros.Update(aux);
                        contexto.productos.Update(p);
                        contexto.SaveChanges();
                    }
                }
                else 
                {
                    MessageBox.Show("ERROR: no se encontro el carro");
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: el Producto no se pudo modificar del carro");
                return false;
            }
            MessageBox.Show("Producto modificado del carro");
            return true;
        }

        public bool VaciarCarro(int ID_Usuario)
        {
            try
            {
                if (contexto.carros.Where(c => c.idUsuario == ID_Usuario).FirstOrDefault() != null)
                {
                    Carro aux = contexto.carros.Where(c => c.idUsuario == ID_Usuario).FirstOrDefault();

                    foreach (CarroProducto cp in aux.carroProducto) 
                    {
                        Producto p = contexto.productos.Where(p => p.idProducto == cp.producto.idProducto).FirstOrDefault();
                        p.cantidad += cp.cantidad;
                        contexto.productos.Update(p);

                    }
                    aux.carroProducto = new List<CarroProducto>();
                    contexto.carros.Update(aux);

                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: no existe el carro");
                    return false;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no se pudo vaciar el carro");
                return false;
            }
            MessageBox.Show("Carro vaciado con exito!");
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
        {                               // idCompra_producto - idProducto - cantidad
            try
            {
                double total = 0;
                Carro car = contexto.usuarios.Where(U => U.idUsuario == ID_Usuario).FirstOrDefault().miCarro;
                foreach (CarroProducto carProd in car.carroProducto)
                {
                    total += (carProd.cantidad * carProd.producto.precio);
                }

                Usuario user = contexto.usuarios.Where(U => U.idUsuario == ID_Usuario).FirstOrDefault();
                Compra aux = new Compra(user, total);
                contexto.compras.Add(aux);

                contexto.SaveChanges();

                foreach (Producto prod in car.productos)
                {
                    int cant = car.carroProducto.Where(CP => CP.idProducto == prod.idProducto).FirstOrDefault().cantidad;
                    CompraProducto cp = new CompraProducto(aux, prod, cant);
                    aux.compraProducto.Add(cp);
                    
                   
                }
                car.carroProducto = new List<CarroProducto>();
                contexto.carros.Update(car);

                contexto.compras.Update(aux);

                contexto.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no se pudo realizar la compra");
                return false;
            }
            MessageBox.Show("Compra realizada con exito!!");
            return true;
        }

        public bool ModificarCompra(int ID, double Total)
        {
            try
            {
                if (contexto.compras.Where(c => c.idCompra == ID).FirstOrDefault() != null)
                {
                    Compra aux = contexto.compras.Where(c => c.idCompra == ID).FirstOrDefault();
                    aux.total = Total;
                    contexto.compras.Update(aux);
                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: no existe la Compra ");
                    return false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: la Compra no se pudo modificar");
                return false;
            }
            MessageBox.Show("Compra modificada con exito!");
            return true;
        }

        public bool EliminarCompra(int IDcompra)
        {
            try
            {
                if (contexto.compras.Where(c => c.idCompra == IDcompra).FirstOrDefault() != null)
                {
                    Compra aux = contexto.compras.Where(c => c.idCompra == IDcompra).FirstOrDefault();
                    contexto.compras.Remove(aux);
                    contexto.SaveChanges();
                }
                else 
                {
                    MessageBox.Show("ERROR: no existe esta compra");
                    return false;
                }
                
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: no se puedo eliminar esta compra");
                return false;
            }

            MessageBox.Show("La compra se elimino exitosamente");
            return true;
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //
        //                                  OTROS METODOS
        //
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // #######################################################################################
        //                                  INICIAR SESION
        // #######################################################################################

        public Usuario IniciarSesion(int DNI, string pass)
        {
            if (contexto.usuarios.Where(U => U.dni == DNI && U.password == pass).FirstOrDefault() != null) 
            {
                Usuario aux = contexto.usuarios.Where(U => U.dni == DNI && U.password == pass).FirstOrDefault();
                return aux;
            }
            return null;
        }

        // #######################################################################################
        //                                  ES ADMIN
        // #######################################################################################
        
        public bool esAdmin(int ID)
        {
            bool res = false;
            if (nContexto.usuarios.Where(u => u.idUsuario == ID).FirstOrDefault().rol == 1) //preguntamos si usuario con ID es admin
            { 
                res= true; 
            }
            return res;
        }


        // #######################################################################################
        //                                  CERRAR CONTEXTO
        // #######################################################################################
        public void cerrarContexto()
        {
            contexto.Dispose();
        }
    }
}
