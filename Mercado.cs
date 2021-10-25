using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TP2_PlataformasDeDesarrollo
{
    class Mercado
    {
        private List<Producto> Productos;
        private List<Usuario> Usuarios;
        private const int MaxCategorias = 10;
        private int CantCategorias = 0;
        private Categoria[] Categorias;
        private List<Compra> Compras = new List<Compra>();

        public string[] fileName = { "1productos.txt", "2usuario.txt", "3carro.txt", "4compras.txt", "5categoria.txt" };
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
            llenarListas();
/*
            nSourcePath = Directory.GetCurrentDirectory() + "/../../Archivos";
            nTargetPath = "C:/ArchivosMercado";

            string[] lines = File.ReadAllLines(nSourcePath+"/"+ fileName[1]);
            
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
                Usuarios.Add(new Usuario(n, int.Parse(parts[1]), parts[2], parts[3], parts[4], parts[5], long.Parse(parts[6]), int.Parse(parts[7])));
            }*/

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

            bool nom = Productos.Exists(x => x.nNombre == nombre);

            if (nom || nombre == "" || nombre == null)
            {
                MessageBox.Show("ERROR: ya existe ese producto");
                return false;
            }

            int resultadoQuery = 0;
            int idNuevoProducto = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "INSERT INTO dbo.Producto (nombre, precio, cantidad, idCategoria) VALUES (@nombre, @precio, @cantidad, @idCategoria)";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int));
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@precio"].Value = precio;
                command.Parameters["@cantidad"].Value = cantidad;
                command.Parameters["@idCategoria"].Value = ID_Categoria;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([idProducto]) FROM [dbo].[Producto]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoProducto = reader.GetInt16(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery==1) 
            {
                int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x.nID == ID_Categoria);
                if (Productos.Contains(null))
                {
                    int idProd = Productos.IndexOf(null);
                    nProductos[idProd] = new Producto(idNuevoProducto, nombre, precio, cantidad, nCategorias[indiceCategoriaArray]);
                    MessageBox.Show("Producto agregado correctamente");
                    return true;
                }
                nProductos.Add(new Producto(idNuevoProducto,nombre,precio,cantidad, nCategorias[indiceCategoriaArray]));
                MessageBox.Show("Producto agregado correctamente!");
                return true;            
            }

            MessageBox.Show("ERROR: no se pudo agregar el producto");
            return false;
        }

        public bool ModificarProducto(int ID, string nombre, double precio, int cantidad, int ID_Categoria)
        {
            int resultadoQuery = 0;
            int idNuevoProducto = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "UPDATE dbo.Producto SET nombre = @nombre, precio = @precio, cantidad = @cantidad, idCategoria = @idCategoria WHERE idProducto = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */

                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)); 
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@precio", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@idCategoria", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@precio"].Value = precio;
                command.Parameters["@cantidad"].Value = cantidad;
                command.Parameters["@idCategoria"].Value = ID_Categoria;

                try
                {
                    //Abro la conexión
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (resultadoQuery == 1)
            {
                int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x.nID == ID_Categoria);
                int indiceProductosLista = nProductos.FindIndex(x => x.nIDProd == ID);

                nProductos[indiceProductosLista].nNombre = nombre;
                nProductos[indiceProductosLista].nPrecio = precio;
                nProductos[indiceProductosLista].nCantidad = cantidad;
                nProductos[indiceProductosLista].nCategoria = nCategorias[indiceCategoriaArray];
                MessageBox.Show("Producto modificado correctamente!");
                return true;
            }
            MessageBox.Show("ERROR: producto no encontrado.");
            return false;
        }

        public bool EliminarProducto(int ID)
        {
            int resultadoQuery = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "DELETE FROM dbo.Producto WHERE idProducto = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) 
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); 
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery == 1)
            {
                try
                {
                    int indiceProductosLista = nProductos.FindIndex(x => x.nIDProd == ID);
                    nProductos.RemoveAt(indiceProductosLista);
                    MessageBox.Show("Producto eliminado correctamente.");
                    return true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Producto eliminado correctamente.");
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                MessageBox.Show("ERROR: No se encontro el producto con ese ID");
                return false;
            }
        }


        public List<Producto> BuscarProducto(string Query)
        {
            
            List<Producto> p = new List<Producto>();
            foreach (Producto pro in Productos) 
            {
                if (pro.nNombre.ToUpper().Contains(Query.ToUpper()) ) 
                {
                    p.Add(pro);
                }
            }
                p.Sort();
                return p;
        }


        public List<Producto> BuscarProductoPorPrecio(string Query)
        {
            
            List<Producto> p = new List<Producto>();
            foreach (Producto pro in Productos)
            {
                if (pro.nPrecio <= int.Parse(Query))
                {
                    p.Add(pro);
                }
            }
            p.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio); });
            return p;
        }


        public List<Producto> BuscarProductoPorCategoria(string nombre)
        {
            List<Producto> p = new List<Producto>();

            foreach (Producto pro in Productos)
            {
                if (pro.nCategoria.nNombre== nombre)
                {
                     p.Add(pro); 
                }
            }
            p.Sort();
            return p;
        }


        public List<Producto> MostrarTodosProductosPorPrecio()
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nPrecio.CompareTo(b.nPrecio); });
            return Productos;
        }

        public List<Producto> MostrarTodosProductosPorCategoria()
        {
            Productos.Sort(delegate (Producto a, Producto b) { return a.nCategoria.nID.CompareTo(b.nCategoria.nID); });
            return Productos;
        }

        // ######################################################################################
        //                                  METODOS DE USUARIO
        // ######################################################################################
        //                                  AGREGAR USUARIO
        //                                  MODIFICAR USUARIO
        //                                  ELIMINAR USUARIO
        //                                  MOSTRAR USUARIO
        // ######################################################################################
        public bool AgregarUsuario(int DNI, string nombre, string apellido, string Mail, string password, long CUIT_CUIL, int rol)
        {
            int resultadoQuery = 0;
            int idNuevoUsuario = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "INSERT INTO dbo.Usuario (dni, nombre, apellido, mail, password, cuitCuil, rol)"+
                                  "VALUES (@dni, @nombre, @apellido, @mail, @password, @cuit_cuil, @rol)";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@cuit_cuil", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@rol", SqlDbType.Int));
                command.Parameters["@dni"].Value = DNI;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@apellido"].Value = apellido;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = password;
                command.Parameters["@cuit_cuil"].Value = CUIT_CUIL;
                command.Parameters["@rol"].Value = rol;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();

                    string ConsultaID = "SELECT MAX([idUsuario]) FROM [dbo].[Usuario]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuario = reader.GetInt16(0);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery == 1)
            {
                if (nUsuarios.Contains(null))
                {
                    int idUser = nUsuarios.IndexOf(null);
                    nUsuarios[idUser] = new Usuario(idNuevoUsuario, DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol);
                    MessageBox.Show("Usuario agregado correctamente");
                    return true;
                }
                nUsuarios.Add(new Usuario(idNuevoUsuario,DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol));
                MessageBox.Show("Usuario agregado correctamente!");
                return true;
            }
            MessageBox.Show("ERROR: No se pudo agregar el Usuario!");
            return false;
        }

        public bool ModificarUsuario(int ID, int DNI, string nombre, string apellido, string Mail, string password, long CUIT_CUIL, int rol)
        {

            int resultadoQuery = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "UPDATE dbo.Usuario SET dni = @dni, nombre = @nombre, apellido = @apellido, " +
                "mail = @mail, password = @password, cuitCuil = @cuit_cuil, rol = @rol WHERE idUsuario = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@mail", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar));
                command.Parameters.Add(new SqlParameter("@cuit_cuil", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@rol", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;
                command.Parameters["@dni"].Value = DNI;
                command.Parameters["@nombre"].Value = nombre;
                command.Parameters["@apellido"].Value = apellido;
                command.Parameters["@mail"].Value = Mail;
                command.Parameters["@password"].Value = password;
                command.Parameters["@cuit_cuil"].Value = CUIT_CUIL;
                command.Parameters["@rol"].Value = rol;

                try
                {
                    //Abro la conexión
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery == 1)
            {
                int indiceUsuarioLista = nUsuarios.FindIndex(x=> x.nID == ID);
                nUsuarios[indiceUsuarioLista].nDNI= DNI;
                nUsuarios[indiceUsuarioLista].nNombre = nombre;
                nUsuarios[indiceUsuarioLista].nApellido = apellido;
                nUsuarios[indiceUsuarioLista].nMail = Mail;
                nUsuarios[indiceUsuarioLista].nPassword = password;
                nUsuarios[indiceUsuarioLista].nCUIT_CUIL = CUIT_CUIL;
                nUsuarios[indiceUsuarioLista].nRol = rol;
                MessageBox.Show("Usuario modificado con exito!");
                return true;
            }
            MessageBox.Show("ERROR: no hay Usuario con ese ID: " + ID);
            return false;
        }

        public bool EliminarUsuario(int ID) // FALTA PROGRAMARLA BIEN, JUNTO A LA BASE DE DATOS
        {
            int resultadoQuery = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "DELETE FROM dbo.Usuario WHERE idUsuario = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;
       
                try
                {
                    //Abro la conexión
                    connection.Open();
                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery == 1)
            {
                int indiceUsuarioLista = nUsuarios.FindIndex(x => x.nID == ID);

                nUsuarios.RemoveAt(indiceUsuarioLista);
                MessageBox.Show("Usuario eliminado con exito!");
                return true;
            }
            MessageBox.Show("ERROR: ID: " + ID + " usuario no encontrado");
            return false;
        }

        public List<Usuario> MostrarUsuarios()
        {
            nUsuarios.Sort(delegate (Usuario a, Usuario b) { return a.nID.CompareTo(b.nID); });
            //Usuarios.Sort();
            return nUsuarios;
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
                int resultadoQuery = 0;
                int idNuevoCategoria = 0;
                //Cargo la cadena de conexión desde el archivo de properties
                string connectionString = Properties.Resources.ConnectionString;

                //Defino el string con la consulta que quiero realizar
                string queryString = "INSERT INTO dbo.Categoria (nombre) VALUES (@nombre)";

                // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
                using (SqlConnection connection =
                    new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
                {
                    // Defino el comando a enviar al motor SQL con la consulta y la conexión
                    SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                    command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                    command.Parameters["@nombre"].Value = nombre;

                    try
                    {
                        //Abro la conexión
                        connection.Open();

                        resultadoQuery = command.ExecuteNonQuery();

                        string ConsultaID = "SELECT MAX([idCategoria]) FROM [dbo].[Categoria]";
                        command = new SqlCommand(ConsultaID, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        idNuevoCategoria = reader.GetInt16(0);
                        reader.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }

                if (resultadoQuery == 1)
                {
                    int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x == null);

                    nCategorias[indiceCategoriaArray] = new Categoria(idNuevoCategoria, nombre);
                    CantCategorias++;
                    MessageBox.Show("Categoria agregada con exito!");
                    return true;
                }
                else 
                {
                    MessageBox.Show("ERROR: no se pueden agregar mas categorias");
                    return false;
                }
            }
            return false;
        }

        public bool ModificarCategoria(int ID, string nombre)
        {

            int resultadoQuery = 0;

            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "UPDATE dbo.Categoria SET nombre = @nombre WHERE idCategoria = @id ";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar));
                command.Parameters["@id"].Value = ID;
                command.Parameters["@nombre"].Value = nombre;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (resultadoQuery == 1)
                {
                    int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x.nID == ID);

                    nCategorias[indiceCategoriaArray].nNombre = nombre;

                    MessageBox.Show("Categoria modificada con exito!");
                    return true;
                }
                else
                {
                    MessageBox.Show("ERROR: no hay categoria con ID: " + ID);
                    return false;
                }
            } 
        }

        public bool EliminarCategoria(int ID)
        {
            int resultadoQuery=0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "DELETE FROM dbo.Categoria WHERE idCategoria = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters["@id"].Value = ID;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (resultadoQuery == 1)
            {
                try
                {
                    int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x.nID == ID);

                    nCategorias[indiceCategoriaArray] = null;
                    CantCategorias--;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }

        public Categoria[] MostrarCategoria()
        {
            
            return nCategorias;
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
            int indiceProd = nProductos.FindIndex(x => x.nIDProd == ID_Producto);
            int indiceUsuario = nUsuarios.FindIndex(x => x.nID == ID_Usuario);
            if (nProductos[indiceProd].nCantidad >= Cantidad && Cantidad > 0)
            {
                nUsuarios[indiceUsuario].nCarro.AgregarProducto(nProductos[indiceProd], Cantidad);
                return true;
            }
             
            return false;
        }
        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario) /*  MODIFICADO, PREGUNTAR OPINION DE LOS DEMAS  */
        {
            int indiceProd = nProductos.FindIndex(x => x.nIDProd == ID_Producto);
            int indiceUsuario = nUsuarios.FindIndex(x => x.nID == ID_Usuario);
            if (nUsuarios[indiceUsuario].nCarro.QuitarProducto(nProductos[indiceProd], Cantidad))
            {
                MessageBox.Show("El producto no se encuentra en la lista");
                return true;
            }
            else
            {
                MessageBox.Show("ERROR: no se encontro producto con el ID " + ID_Producto + " en el Carro.");
                return false;
            }
        }
        public bool VaciarCarro(int ID_Usuario)
        {
            int indiceUsuario = nUsuarios.FindIndex(x => x.nID == ID_Usuario);
            nUsuarios[indiceUsuario].nCarro.Vaciar();
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
        {
            double total = 0;
            int n = Compras.Count();
            int indiceUsuario = Usuarios.FindIndex(x => x.nID == ID_Usuario);
            foreach (KeyValuePair<Producto, int> kvp in Usuarios[indiceUsuario].nCarro.nProductos)
            {
                total += kvp.Key.nPrecio * kvp.Value;
            }
            if (total > 0)
            {
                Dictionary<Producto, int> Prod = new Dictionary<Producto, int>();
                Prod = Usuarios[indiceUsuario].nCarro.nProductos;

                Compras.Add(new Compra(n, Usuarios[indiceUsuario], Usuarios[ID_Usuario].nCarro.nProductos, total));

                /*                      ***********************************  ELIMINAR ****************************************
                foreach (KeyValuePair<Producto, int> kvp in Prod) 
                {
                    Compras[n].nProductos.Add(kvp.Key,kvp.Value);
                }
                                        *************************************************************************************** */
                
                int indice = nCompras.FindIndex(x => x.nComprador == Usuarios[indiceUsuario]);

                foreach (KeyValuePair<Producto, int> kvp in nCompras[indice].nProductos)
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
                MessageBox.Show("Compraste con exito!");
                return true;
            }
            MessageBox.Show("ERROR: no se pudo efectuar la compra");
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
            int resultadoQuery=0;
            int idUsuario=0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * from dbo.Usuario WHERE dni = @dni AND password = @password";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar));
                command.Parameters["@dni"].Value = DNI;
                command.Parameters["@password"].Value = pass;

                try
                {
                    //Abro la conexión
                    connection.Open();

                    resultadoQuery = command.ExecuteNonQuery();
                    
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader(); /*ExecuteReader para SELECT*/

                    
                    Usuario aux;
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    if (reader.Read())
                    {
                        idUsuario = (reader.GetInt16(0));
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            if (idUsuario > 0)
            {
                int indiceUsuario = Usuarios.FindIndex(x => x.nID == idUsuario);
                return nUsuarios[indiceUsuario];
            }
            return null;
        }
        // #######################################################################################
        //                                  ES ADMIN
        // #######################################################################################
        public Boolean esAdmin(int ID)
        {
            int indiceUsuario = Usuarios.FindIndex(x => x.nID == ID);

            if (nUsuarios[indiceUsuario] != null && nUsuarios[indiceUsuario].nRol == 1) //preguntamos si usuario con ID es admin
            { return true; }
            else
            { return false; }
        }

        // #######################################################################################
        //                                  LLENAR LISTAS
        // #######################################################################################

        public void llenarListas()
        {
            // MODIFICAR RELLENANDO LAS LISTAS CON LOS DATOS DE LA BASE DE DATOS
            
            //LIMPIAMOS LAS LISTAS
            
            CantCategorias = 0;
            string queryString;
            string connectionString;

            // ######################################
            //      LLENAMOS LISTA CATEGORIAS
            // ######################################

            //Cargo la cadena de conexión desde el archivo de properties
            connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            queryString = "SELECT * from dbo.Categoria";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    Categoria aux;
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        aux = new Categoria(reader.GetInt16(0), reader.GetString(1));
                        nCategorias[CantCategorias] = aux;
                        CantCategorias++;
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            // ######################################
            //      LLENAMOS LISTA PRODUCTOS
            // ######################################

            //Cargo la cadena de conexión desde el archivo de properties
            //connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            queryString = "SELECT * from dbo.Producto";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    Producto aux;
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        int indiceCategoria = Array.FindIndex(nCategorias, x => x.nID == reader.GetInt16(4));
                        
                        //      PROBAR SI ESTO FUNCIONA BIEN
                        aux = new Producto(reader.GetInt16(0), reader.GetString(1), ((double)reader.GetDecimal(2)), reader.GetInt32(3), nCategorias[indiceCategoria]);
                        nProductos.Add(aux);
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // ######################################
            //      LLENAMOS LISTA USUARIOS
            // ######################################

            //Cargo la cadena de conexión desde el archivo de properties
            //connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            queryString = "SELECT * from dbo.Usuario";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader(); /*ExecuteReader para SELECT*/
                    Usuario aux;
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read()) /* Devuelve true, si no hay nada devuelve false*/
                    {

                        aux = new Usuario(reader.GetInt16(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), ((int)reader.GetByte(7)));
                        nUsuarios.Add(aux);
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            // ######################################
            //      LLENAMOS LISTA CARROS
            // ######################################

            //Defino el string con la consulta que quiero realizar
            queryString = "SELECT * from dbo.Carro INNER JOIN Carro_Producto ON dbo.Carro.idCarro = dbo.carro_Producto.idCarro";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) /*Se crea el objeto apuntando a esa BD*/
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); /* Comando listo para disparar */

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader(); /*ExecuteReader para SELECT*/
                    
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read()) /* Devuelve true, si no hay nada devuelve false*/
                    {
                        ///*ESTO SÓLO FUNCIONA*/ Console.WriteLine("{0} {1}", reader.GetInt16(0), reader.GetInt32(1));
                        int indiceUsuario = Usuarios.FindIndex(x => x.nID == reader.GetInt16(1));
                        int indiceProducto = nProductos.FindIndex(x => x.nIDProd == reader.GetInt16(4));

                        nUsuarios[indiceUsuario].nCarro.Productos.Add(nProductos[indiceProducto], reader.GetByte(5));
                        
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // ######################################
            //      LLENAMOS LISTA COMPRAS
            // ######################################

        }
        /* 
        public int compare(Categoria a, Categoria b) {
            if (a is Categoria && b is Categoria)
            {
                char[] arr = (a.nNombre.ToUpper()).ToCharArray();
                char first = arr[0];

        //        char[] arr2 = (b.nNombre.ToUpper()).ToCharArray();
        //        int cant = (arr.Length > arr2.Length) ? arr2.Length : arr.Length;

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