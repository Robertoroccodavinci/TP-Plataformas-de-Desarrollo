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
        //private List<Producto> Productos;
        //private List<Usuario> Usuarios;
        //private List<Compra> Compras;
        //private Categoria[] Categorias;
        //#####################################
        
        //private const int MaxCategorias = 10;//   QUITAMOS ESTAS VARIABLES? 
        //private int CantCategorias = 0;
        
        

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


                // agregamos un carro con un producto
                Producto prod1 = contexto.productos.Where(p => p.idProducto == 46).FirstOrDefault();
                Producto prod2 = contexto.productos.Where(p => p.idProducto == 47).FirstOrDefault();
                Producto prod3 = contexto.productos.Where(p => p.idProducto == 48).FirstOrDefault();
                
                Carro carro = contexto.carros.Where(c => c.idCarro == 2).FirstOrDefault();
                CarroProducto cp1 = new CarroProducto(carro, prod1, 3);
                CarroProducto cp2 = new CarroProducto(carro, prod2, 2);
                CarroProducto cp3 = new CarroProducto(carro, prod3, 1);
                
                // para que se agregue una sola vez

                if ( carro.carroProducto != null && 
                    !carro.carroProducto.Where(cp => cp.producto.idProducto == 46).Any() &&
                    !carro.carroProducto.Where(cp => cp.producto.idProducto == 47).Any() &&
                    !carro.carroProducto.Where(cp => cp.producto.idProducto == 48).Any() ) 
                {
                    carro.carroProducto.Add(cp1);
                    carro.carroProducto.Add(cp2);
                    carro.carroProducto.Add(cp3);
                    contexto.carros.Update(carro);
                }

                // agregamos una compra de un producto
                Producto prod4 = contexto.productos.Where(p => p.idProducto == 21).FirstOrDefault();
                Producto prod5 = contexto.productos.Where(p => p.idProducto == 23).FirstOrDefault();
                Compra compra = contexto.compras.Where(c => c.idCompra == 1).FirstOrDefault();
                CompraProducto comProd1 = new CompraProducto(compra, prod4, 1);
                CompraProducto comProd2 = new CompraProducto(compra, prod5, 1);

                if (compra.compraProducto != null &&
                    !compra.compraProducto.Where(cp => cp.producto.idProducto == 21).Any() &&
                    !compra.compraProducto.Where(cp => cp.producto.idProducto == 23).Any() )
                {
                    compra.compraProducto.Add(comProd1);
                    compra.total += prod4.precio * 1;
                    compra.compraProducto.Add(comProd2);
                    compra.total += prod5.precio * 1;
                    contexto.compras.Update(compra);
                }

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
            /*<===REVISAR===>*/
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("ERROR: no hay Usuario con ese ID: " + ID);
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
            /*if (CantCategorias < MaxCategorias)
            { }*/

            try
            {
                Categoria aux = new Categoria(nombre);
                contexto.categorias.Add(aux);
                contexto.SaveChanges();
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
                Categoria aux = contexto.categorias.Where(c => c .idCategoria == ID).FirstOrDefault();
                aux.nombre = nombre;
                contexto.Update(aux);
                contexto.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: no hay categoria con ID: " + ID);
                return false;
            }
            MessageBox.Show("Categoria modificada con exito!");
            return true;
        }

        public bool EliminarCategoria(int ID)
        {
            try
            {
                Categoria aux = contexto.categorias.Where(c => c.idCategoria == ID).FirstOrDefault();
                //CantCategorias--;
                contexto.categorias.Remove(aux);
                contexto.SaveChanges();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
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
        //                                  METODOS DE COMPRA
        // #######################################################################################
        //                                  COMPRA
        //                                  MODIFICACION COMPRA
        //                                  ELIMINACION COMPRA
        // #######################################################################################

        public bool Comprar(int ID_Usuario)
        {                               // idCompra_producto - idProducto - cantidad
            /*double total = 0;
            int indiceUsuario = Usuarios.FindIndex(x => x.nID == ID_Usuario);
            foreach (KeyValuePair<Producto, int> kvp in nUsuarios[indiceUsuario].nCarro.nProductos)
            {
                total += kvp.Key.nPrecio * kvp.Value;
            }

            int idCarro = 0;
            int idCompra = 0;
            int resultadoQuery = 1;
            int resultadoQueryCompra = 1;
            //string queryStringIdCarro = "SELECT idCarro FROM Carro WHERE idUsuario = @idUsuario ";
            string queryStringInsertCompra = "INSERT INTO dbo.Compra (idUsuario, total) VALUES (@idUsuario,@total) ";
            string queryStringSelectCarroProducto = "SELECT * FROM carro_producto WHERE idCarro= @idCarro";
            string queryStringInsertCompraProducto = "INSERT INTO dbo.compra_producto (idCompra,idProducto, cantidad) VALUES (@idCompra, @idProducto, @cantidad)";
            string queryStringDelCarroProducto = "DELETE FROM carro_producto WHERE idCarro = @idCarro";
            string queryStringIdCompra = "SELECT MAX(idCompra) FROM dbo.Compra WHERE idUsuario = @idUsuario";


            string connectionString = Properties.Resources.ConnectionString;
            SqlCommand command;
            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection = new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                try
                {
                    //Abro la conexión
                    connection.Open();
                    SqlDataReader reader;
                    *//* //Obtenemos el ID del Carro
                     command = new SqlCommand(queryStringIdCarro, connection); *//* Comando listo para disparar *//*
                     command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                    command.Parameters["@idUsuario"].Value = nUsuarios[indiceUsuario].nID;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idCarro = reader.GetInt16(0);
                    reader.Close(); *//*
                    idCarro = nUsuarios[indiceUsuario].nCarro.nID;
                    //obtenemos productos y cantidades de carro_producto
                    command = new SqlCommand(queryStringSelectCarroProducto, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                    command.Parameters["@idCarro"].Value = idCarro;
                    SqlDataReader readerCarroProducto = command.ExecuteReader();
                    Dictionary<Producto, int> prod = new Dictionary<Producto, int>();
                    while (readerCarroProducto.Read())
                    {
                        int indiceProd = nProductos.FindIndex(x => x.nIDProd == readerCarroProducto.GetInt16(2));
                        prod.Add(nProductos[indiceProd], readerCarroProducto.GetByte(3));

                    }
                    readerCarroProducto.Close();


                    //Eliminamos el carro del usuario
                    command = new SqlCommand(queryStringDelCarroProducto, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                    command.Parameters["@idCarro"].Value = idCarro;

                    resultadoQuery = command.ExecuteNonQuery();

                    //Insertamos la compra del usuario
                    command = new SqlCommand(queryStringInsertCompra, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@total", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                    command.Parameters["@total"].Value = total;
                    command.Parameters["@idUsuario"].Value = nUsuarios[indiceUsuario].nID;

                    resultadoQueryCompra = command.ExecuteNonQuery();

                    //Obtenemos el ID del Compra
                    command = new SqlCommand(queryStringIdCompra, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                    command.Parameters["@idUsuario"].Value = nUsuarios[indiceUsuario].nID;
                    reader = command.ExecuteReader();
                    reader.Read();
                    idCompra = reader.GetInt16(0);
                    reader.Close();

                    //insertamos a compra_producto


                    foreach (KeyValuePair<Producto, int> kvp in prod)
                    {
                        command = new SqlCommand(queryStringInsertCompraProducto, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                        command.Parameters["@idCompra"].Value = idCompra;
                        command.Parameters["@idProducto"].Value = kvp.Key.nIDProd;
                        command.Parameters["@cantidad"].Value = kvp.Value;
                        resultadoQuery = command.ExecuteNonQuery();
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (resultadoQueryCompra == 1)
            {
                Compras.Add(new Compra(idCompra, nUsuarios[indiceUsuario], nUsuarios[indiceUsuario].nCarro.nProductos, total));
                nUsuarios[indiceUsuario].nCarro.nProductos = new Dictionary<Producto, int>();

                //VaciarCarro(ID_Usuario);
                
            }
            
            */
            /* try
             {
                 Usuario u = contexto.usuarios.Where(u => u.idUsuario == ID_Usuario).FirstOrDefault();
                 double total = 0;
                 List<Producto> prods = new List<Producto>();

                 foreach (CarroProducto cp in u.miCarro.carroProducto) 
                 {


                     total += (cp.producto.precio * cp.cantidad);
                 }
                 Compra c = new Compra();//u, list<CarroProducto>, total

             }
             catch (Exception) 
             {
                 MessageBox.Show("ERROR: no se pudo efectuar la compra");
                 return false;
             }
             MessageBox.Show("Compraste con exito!");
             */
            return true;
        }

        public bool ModificarCompra(int ID, double Total)
        {
            try
            {
                Compra aux = contexto.compras.Where(c => c.idCompra == ID).FirstOrDefault();
                aux.total = Total;
                contexto.compras.Update(aux);
                contexto.SaveChanges();
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
                Compra aux = contexto.compras.Where(c => c.idCompra == IDcompra).FirstOrDefault();
                contexto.compras.Remove(aux);
                contexto.SaveChanges();
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
            Usuario aux = contexto.usuarios.Where(U => U.dni == DNI && U.password == pass).FirstOrDefault();
            if (aux != null)
            {
                return aux;
            }
            else 
            {
                return null;
            }
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
