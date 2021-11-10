using System;
using System.Collections.Generic;
using System.Text;

namespace TP_Plataformas_de_Desarrollo
{
    class MercadoOld
    {
        private List<Producto> Productos;
        private List<UsuarioOLD> Usuarios;
        private const int MaxCategorias = 10;
        private int CantCategorias = 0;
        private Categoria[] Categorias;
        private List<Compra> Compras;

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
        public MercadoOld()
        {
            //nProductos = new List<Producto>();
            //nUsuarios = new List<UsuarioOLD>();
            //nCompras = new List<Compra>();
            //nCategorias = new Categoria[MaxCategorias];
           // llenarListas();
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
        /*// ######################################################################################
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

        public List<UsuarioOLD> nUsuarios
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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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
            if (resultadoQuery == 1)
            {
                int indiceCategoriaArray = Array.FindIndex(nCategorias, x => x.nID == ID_Categoria);
                if (Productos.Contains(null))
                {
                    int idProd = Productos.IndexOf(null);
                    nProductos[idProd] = new Producto(idNuevoProducto, nombre, precio, cantidad, nCategorias[indiceCategoriaArray]);
                    MessageBox.Show("Producto agregado correctamente");
                    return true;
                }
                nProductos.Add(new Producto(idNuevoProducto, nombre, precio, cantidad, nCategorias[indiceCategoriaArray]));
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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*

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
                //MessageBox.Show("Producto modificado correctamente!");
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
                if (pro.nNombre.ToUpper().Contains(Query.ToUpper()))
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
                if (pro.nCategoria.nNombre == nombre)
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
            int resultadoQueryCarro = 0;
            //Defino el string con la consulta que quiero realizar
            string queryString = "INSERT INTO dbo.Usuario (dni, nombre, apellido, mail, password, cuitCuil, rol)" +
                                  "VALUES (@dni, @nombre, @apellido, @mail, @password, @cuit_cuil, @rol)";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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

                    // INSERT INTO dbo.Carro(idUsuario) VALUES((SELECT MAX([idUsuario]) FROM[dbo].[Usuario]))

                    string crearCarro = "INSERT INTO dbo.Carro (idUsuario) VALUES (@idUser)";

                    command = new SqlCommand(crearCarro, connection);
                    command.Parameters.Add(new SqlParameter("@idUser", SqlDbType.Int));
                    command.Parameters["@idUser"].Value = idNuevoUsuario;
                    resultadoQueryCarro = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                // Aca agregamos un carro nuevo, que corresponde al Usuario

            }
            if (resultadoQuery == 1 && resultadoQueryCarro == 1)
            {
                if (nUsuarios.Contains(null))
                {
                    int idUser = nUsuarios.IndexOf(null);
                    nUsuarios[idUser] = new Usuario(idNuevoUsuario, DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol);
                    MessageBox.Show("Usuario agregado correctamente");
                    return true;
                }
                nUsuarios.Add(new Usuario(idNuevoUsuario, DNI, nombre, apellido, Mail, password, CUIT_CUIL, rol));
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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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
                int indiceUsuarioLista = nUsuarios.FindIndex(x => x.nID == ID);
                nUsuarios[indiceUsuarioLista].nDNI = DNI;
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
            int resultadoQueryUsuario = 0;
            int idCarro = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryDelUsuario = "DELETE FROM dbo.Usuario WHERE idUsuario = @id";
            string queryDelCarro = "DELETE FROM Carro WHERE idCarro = @id";


            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                SqlCommand command;
                try
                {
                    connection.Open();
                    string ConsultaIDCarro = "SELECT C.idCarro FROM dbo.Usuario U INNER JOIN dbo.Carro C ON U.idUsuario = C.idUsuario WHERE U.idUsuario = @id";
                    command = new SqlCommand(ConsultaIDCarro, connection);
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters["@id"].Value = ID;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idCarro = reader.GetInt16(0);
                    reader.Close();

                    int indiceUsuarioLista = nUsuarios.FindIndex(x => x.nID == ID);
                    // Defino el comando a enviar al motor SQL con la consulta y la conexión
                    if (nCompras.Exists(x => x.nComprador == nUsuarios[indiceUsuarioLista]))
                    {
                        int indiceCompra = nCompras.FindIndex(x => x.nComprador == nUsuarios[indiceUsuarioLista]);
                        string queryEliminarComprasProducto = "DELETE FROM compra_producto WHERE idCompra = @idCompra";
                        command = new SqlCommand(queryEliminarComprasProducto, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                        command.Parameters["@idCompra"].Value = nCompras[indiceCompra].nIDCompra;
                        int resultadoQueryCompraProducto = command.ExecuteNonQuery();
                        if (resultadoQueryCompraProducto >= 1)
                        {
                            string queryEliminarCompras = "DELETE FROM Compra WHERE idCompra = @idCompra";
                            command = new SqlCommand(queryEliminarCompras, connection); *//* Comando listo para disparar *//*
                            command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                            command.Parameters["@idCompra"].Value = nCompras[indiceCompra].nIDCompra;
                            command.ExecuteNonQuery();
                        }
                    }
                    if (nUsuarios[indiceUsuarioLista].nCarro.nProductos.Count() != 0)
                    {
                        string queryDelCarroProducto = "DELETE FROM carro_producto WHERE idCarro = @id";
                        command = new SqlCommand(queryDelCarroProducto, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                        command.Parameters["@id"].Value = idCarro;
                        command.ExecuteNonQuery();
                    }

                    command = new SqlCommand(queryDelCarro, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                    command.Parameters["@id"].Value = idCarro;
                    resultadoQuery = command.ExecuteNonQuery();
                    if (resultadoQuery == 1)
                    {
                        command = new SqlCommand(queryDelUsuario, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                        command.Parameters["@id"].Value = ID;
                        resultadoQueryUsuario = command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQueryUsuario == 1)
            {
                int indiceUsuarioLista = nUsuarios.FindIndex(x => x.nID == ID);

                nUsuarios.RemoveAt(indiceUsuarioLista);
                MessageBox.Show("Usuario eliminado con exito!");
                return true;
            }
            MessageBox.Show("ERROR: ID: " + ID + " usuario no encontrado");
            return false;
        }

        public List<UsuarioOLD> MostrarUsuarios()
        {
            nUsuarios.Sort(delegate (UsuarioOLD a, UsuarioOLD b) { return a.nID.CompareTo(b.nID); });
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
                    new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
                {
                    // Defino el comando a enviar al motor SQL con la consulta y la conexión
                    SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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
            int resultadoQuery = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "DELETE FROM dbo.Categoria WHERE idCategoria = @id";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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

                nProductos[indiceProd].nCantidad -= Cantidad;

                int resultadoQuery = 0;
                int resultadoQueryCantidad = 0;
                int idCarro = 0;
                //Cargo la cadena de conexión desde el archivo de properties


                //Defino el string con la consulta que quiero realizar
                string queryStringIdCarro = "SELECT C.idCarro FROM dbo.Carro C INNER JOIN dbo.Usuario U ON C.idUsuario = U.idUsuario WHERE U.idUsuario = @idUsuario";
                string queryStringCarroProducto = "";
                string queryStringStockProducto = "UPDATE dbo.Producto SET cantidad = @cantidad WHERE idProducto = @idProducto ";
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

                        command = new SqlCommand(queryStringIdCarro, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                        command.Parameters["@idUsuario"].Value = ID_Usuario;
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        idCarro = reader.GetInt16(0);
                        reader.Close();

                        string query = "SELECT COUNT(idCarroProducto) FROM dbo.carro_producto WHERE idProducto = @idProducto AND idCarro = @idCarro";
                        command = new SqlCommand(query, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int));
                        command.Parameters["@idProducto"].Value = ID_Producto;
                        command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                        command.Parameters["@idCarro"].Value = idCarro;
                        //resultadoQuery = command.exe.ExecuteNonQuery();
                        reader = command.ExecuteReader();
                        reader.Read();
                        int cant = reader.GetInt32(0);
                        reader.Close();

                        if (cant == 1)
                        {
                            queryStringCarroProducto = "UPDATE dbo.carro_producto SET cantidad = @cantidad WHERE idCarro = @idCarro AND idProducto = @idProducto";
                            // A la cantidad actual del producto en el carro, le sumamos la cantidad que agregamos
                            Cantidad += nUsuarios[indiceUsuario].nCarro.nProductos[nProductos[indiceProd]];
                        }
                        else
                        {
                            queryStringCarroProducto = "INSERT INTO dbo.carro_producto (idCarro,idProducto,cantidad) " +
                                                       "VALUES (@idCarro, @idProducto, @cantidad)";
                        }



                        command = new SqlCommand(queryStringCarroProducto, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                        command.Parameters["@idCarro"].Value = idCarro;
                        command.Parameters["@idProducto"].Value = ID_Producto;
                        command.Parameters["@cantidad"].Value = Cantidad;

                        resultadoQuery = command.ExecuteNonQuery();

                        command = new SqlCommand(queryStringStockProducto, connection); *//* Comando listo para disparar *//*
                        command.Parameters.Add(new SqlParameter("@idProducto", SqlDbType.Int));
                        command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                        command.Parameters["@idProducto"].Value = ID_Producto;
                        command.Parameters["@cantidad"].Value = nProductos[indiceProd].nCantidad;

                        resultadoQueryCantidad = command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                if (resultadoQuery == 1 && resultadoQueryCantidad == 1)
                {

                    nUsuarios[indiceUsuario].nCarro.AgregarProducto(nProductos[indiceProd], Cantidad);
                    return true;
                }
            }
            return false;
        }


        public bool QuitarAlCarro(int ID_Producto, int Cantidad, int ID_Usuario)
        {
            int indiceProd = nProductos.FindIndex(x => x.nIDProd == ID_Producto);
            int indiceUsuario = nUsuarios.FindIndex(x => x.nID == ID_Usuario);
            int idCarro = 0;
            string queryString = "";
            string queryStringIdCarro = "";
            int resultadoQuery = 0;
            bool update = false;
            queryStringIdCarro = "SELECT idCarro FROM Carro WHERE idUsuario = @idUsuario";
            // Si la cantidad Actual es mayor a la ingresada, se descuenta de la cantidad actual
            if (nUsuarios[indiceUsuario].nCarro.nProductos[nProductos[indiceProd]] > Cantidad)
            {
                queryString = "UPDATE carro_producto SET cantidad = @cantidad WHERE idCarro = @idCarro AND idProducto = @idProd";
                update = true;
            } // Si la cantidad Actual es igual a la ingresada, se borra el producto del carro
            else if (nUsuarios[indiceUsuario].nCarro.nProductos[nProductos[indiceProd]] == Cantidad)
            {
                queryString = "DELETE FROM carro_producto WHERE idCarro = @idCarro AND idProducto = @idProd";
                // DELETE FROM carro_producto WHERE idCarro = @id
            }
            else
            {
                MessageBox.Show("La cantidad ingresada es incorrecta");
                return false;
            }


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

                    command = new SqlCommand(queryStringIdCarro, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                    command.Parameters["@idUsuario"].Value = ID_Usuario;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idCarro = reader.GetInt16(0);
                    reader.Close();

                    command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                    command.Parameters["@idCarro"].Value = idCarro;
                    command.Parameters.Add(new SqlParameter("@idProd", SqlDbType.Int));
                    command.Parameters["@idProd"].Value = nProductos[indiceProd].nIDProd;
                    if (update)
                    {
                        command.Parameters.Add(new SqlParameter("@cantidad", SqlDbType.Int));
                        command.Parameters["@cantidad"].Value = Cantidad;
                    }
                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (resultadoQuery == 1 && nUsuarios[indiceUsuario].nCarro.QuitarProducto(nProductos[indiceProd], Cantidad))
            {
                if (update)
                {
                    MessageBox.Show("Se desconto la cantidad del producto del carro");
                    return true;
                }
                else
                {
                    MessageBox.Show("Se quito el Producto del Carro");
                    return true;
                }
            }
            MessageBox.Show("ERROR: no se encontro el producto con el ID " + ID_Producto + " en el Carro.");
            return false;
        }

        public bool VaciarCarro(int ID_Usuario)
        {
            int idCarro = 0;
            int resultadoQuery = 0;
            string queryStringIdCarro = "SELECT idCarro FROM Carro WHERE idUsuario = @idUsuario";
            string queryStringDelCarroProducto = "DELETE FROM carro_producto WHERE idCarro = @idCarro";
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

                    command = new SqlCommand(queryStringIdCarro, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idUsuario", SqlDbType.Int));
                    command.Parameters["@idUsuario"].Value = ID_Usuario;
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idCarro = reader.GetInt16(0);
                    reader.Close();

                    command = new SqlCommand(queryStringDelCarroProducto, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCarro", SqlDbType.Int));
                    command.Parameters["@idCarro"].Value = idCarro;

                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (resultadoQuery >= 1)
            {
                int indiceUsuario = nUsuarios.FindIndex(x => x.nID == ID_Usuario);

                foreach (KeyValuePair<Producto, int> kvp in nUsuarios[indiceUsuario].nCarro.nProductos)
                {
                    int indice = nProductos.FindIndex(x => x.nIDProd == kvp.Key.nIDProd);
                    int cantTotal = kvp.Value + nProductos[indice].nCantidad;
                    ModificarProducto(kvp.Key.nIDProd, kvp.Key.nNombre, kvp.Key.nPrecio, cantTotal, kvp.Key.nCategoria.nID);
                }
                nUsuarios[indiceUsuario].nCarro.Vaciar();
                MessageBox.Show("Carro vaciado con exito!");
                return true;
            }
            MessageBox.Show("ERROR: no se pudo vaciar el Carro");
            return false;
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
            double total = 0;
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
                     reader.Close();*//*
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
                MessageBox.Show("Compraste con exito!");
                return true;
            }
            MessageBox.Show("ERROR: no se pudo efectuar la compra");
            return false;
        }

        public bool ModificarCompra(int ID, double Total)
        {
            int resultadoQuery = 0;
            string queryStringUpdateCompra = "UPDATE dbo.Compra SET total = @total WHERE idCompra = @idCompra";
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

                    //Modificamos el total de la compra correspondiente al ID
                    command = new SqlCommand(queryStringUpdateCompra, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@total", SqlDbType.Int));
                    command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                    command.Parameters["@total"].Value = Total;
                    command.Parameters["@idCompra"].Value = ID;

                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            if (resultadoQuery == 1)
            {
                int indiceCompra = nCompras.FindIndex(x => x.nIDCompra == ID);
                nCompras[indiceCompra].nTotal = Total;
                MessageBox.Show("Compra modificada con exito!");
                return true;
            }
            MessageBox.Show("ERROR: la Compra no se pudo modificar");
            return false;
        }

        public bool EliminarCompra(int IDcompra)
        {
            int resultadoQuery = 0;
            string queryStringDelCompra = "DELETE FROM dbo.Compra WHERE idCompra = @idCompra";
            string queryStringDelCompraProducto = "DELETE FROM dbo.compra_producto WHERE idCompra = @idCompra";
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

                    command = new SqlCommand(queryStringDelCompraProducto, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                    command.Parameters["@idCompra"].Value = IDcompra;

                    resultadoQuery = command.ExecuteNonQuery();

                    //Modificamos el total de la compra correspondiente al ID
                    command = new SqlCommand(queryStringDelCompra, connection); *//* Comando listo para disparar *//*
                    command.Parameters.Add(new SqlParameter("@idCompra", SqlDbType.Int));
                    command.Parameters["@idCompra"].Value = IDcompra;

                    resultadoQuery = command.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

            if (resultadoQuery == 1)
            {
                int indiceCompra = nCompras.FindIndex(x => x.nIDCompra == IDcompra);
                nCompras[indiceCompra] = null;
                MessageBox.Show("La compra se elimino exitosamente");
                return true;
            }
            MessageBox.Show("ERROR: no se puedo eliminar esta compra");
            return false;
        }

        // #######################################################################################
        //                                  INICIAR SESION
        // #######################################################################################
        public UsuarioOLD IniciarSesion(int DNI, string pass)
        {
            int resultadoQuery = 0;
            int idUsuario = 0;
            //Cargo la cadena de conexión desde el archivo de properties
            string connectionString = Properties.Resources.ConnectionString;

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * from dbo.Usuario WHERE dni = @dni AND password = @password";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*
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
                    SqlDataReader reader = command.ExecuteReader(); *//*ExecuteReader para SELECT*//*


                    UsuarioOLD aux;
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
            queryString = "SELECT * from dbo.Categoria ORDER BY nombre";

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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader(); *//*ExecuteReader para SELECT*//*
                    UsuarioOLD aux;
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read()) *//* Devuelve true, si no hay nada devuelve false*//*
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
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection); *//* Comando listo para disparar *//*

                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader(); *//*ExecuteReader para SELECT*//*

                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read()) *//* Devuelve true, si no hay nada devuelve false*//*
                    {
                        ///*ESTO SÓLO FUNCIONA*/ /*Console.WriteLine("{0} {1}", reader.GetInt16(0), reader.GetInt32(1));
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

            //Defino el string con la consulta que quiero realizar
            string queryStringIdsCompra = "SELECT * FROM Compra C JOIN compra_producto CP ON C.idCompra = CP.idCompra";
            Compra compraAuxiliar;

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString)) *//*Se crea el objeto apuntando a esa BD*//*
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command; *//* Comando listo para disparar *//*
                try
                {
                    //Abro la conexión
                    connection.Open();
                    command = new SqlCommand(queryStringIdsCompra, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) *//* Devuelve true, si no hay nada devuelve false*//*
                    {
                        int indiceUsuario = Usuarios.FindIndex(x => x.nID == reader.GetInt16(1));
                        int indiceCompra = Compras.FindIndex(x => x.nIDCompra == reader.GetInt16(0));
                        int indiceProd = nProductos.FindIndex(x => x.nIDProd == reader.GetInt16(5));


                        if (Compras.Exists(x => x.nIDCompra == reader.GetInt16(0)))
                        {
                            Compras[indiceCompra].Agregar(nProductos[indiceProd], reader.GetByte(6));
                        }
                        else
                        {
                            Dictionary<Producto, int> produc = new Dictionary<Producto, int>();
                            produc.Add(nProductos[indiceProd], reader.GetByte(6));
                            compraAuxiliar = new Compra(reader.GetInt16(0), Usuarios[indiceUsuario], produc, reader.GetInt32(2));
                            nCompras.Add(compraAuxiliar);

                        }

                    }
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



        *//*  
           
         *** METODO PARA ORDENAR CATEGORIAS ***
         
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