using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TP_Plataformas_de_Desarrollo
{
    public partial class FormAdmin : Form
    {
        private Mercado merc;
        private int ID;
        public delegate void TransfDelegado2(); // Metodo
        public TransfDelegado2 TrasfEvento;

        public FormAdmin(int ID, string nombre, Object m)
        {
            InitializeComponent();
            this.ID = ID;
            label2.Text = nombre;
            merc = (Mercado)m;
            refreshData(merc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBoxRol.SelectedIndex = 1;
            button3.Hide();
            //comboBoxModRol.SelectedIndex = 0;

            //tabControl1.SelectedIndexChanged += new EventHandler(ocultarMostrar);

            //CREAMOS EVENTOS EN LAS TABLAS PARA DAR MAS ACCIONES
            dataGridView1.CellClick += dataGridView1_CellClick; //EVENTO TABLA PRODUCTOS
            dataGridView2.CellClick += dataGridView2_CellClick; //EVENTO TABLA CATEGORIAS
            dataGridView3.CellClick += dataGridView3_CellClick; //EVENTO TABLA USUARIOS
            dataGridView4.CellClick += dataGridView4_CellClick; //EVENTO TABLA COMPRAS
            //dataGridView5.CellClick += dataGridView5_CellClick; //EVENTO TABLA MI CARRO

            //EVENTO PARA LOS BOTONES DE LA LISTA DE CATEGORIAS
            dataGridView6.CellClick += dataGridView6_CellClick;
        }

        private void refreshData(Mercado data)
        {
            // LIMPIAMOS LAS TABLAS
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            dataGridView2.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIAS
            dataGridView3.Rows.Clear(); //LIMPIAMOS TABLA USUARIOS
            dataGridView4.Rows.Clear(); //LIMPIAMOS TABLA COMPRAS
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO
            dataGridView6.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIA DE PRODUCTOS

            // LLENAMOS LISTADO DE CATEGORIAS
            foreach (Categoria c in data.MostrarCategoria())
            {
                if (c != null)
                {
                    string[] cate = { c.idCategoria.ToString(),
                                      c.nombre };
                    dataGridView2.Rows.Add(cate);
                    // LLENAMOS LISTADO DE CATEGORIAS DE LA PESTAÑA DE PRODUCTOS
                    dataGridView6.Rows.Add(c.nombre);
                }
            }
            if (dataGridView2.Columns["botonBorrar"] == null)
            {
                DataGridViewButtonColumn borrarCategoria = new DataGridViewButtonColumn();
                borrarCategoria.HeaderText = "Borrar";
                borrarCategoria.Text = "Borrar";
                borrarCategoria.Name = "botonBorrar";
                borrarCategoria.UseColumnTextForButtonValue = true;
                dataGridView2.Columns.Add(borrarCategoria);
            }

            // LLENAMOS LISTADO DE PRODUCTOS

            foreach (Producto p in data.nContexto.productos)
            {
                if (p != null)
                {
                    string[] pro = { p.idProducto.ToString(),
                                     p.nombre,
                                     p.precio.ToString(),
                                     p.cantidad.ToString(),
                                     p.cat.nombre };
                    dataGridView1.Rows.Add(pro);
                }
            }
            if (dataGridView1.Columns["botonBorrar"] == null)
            {
                DataGridViewButtonColumn borrarProducto = new DataGridViewButtonColumn();
                borrarProducto.HeaderText = "Borrar";
                borrarProducto.Text = "Borrar";
                borrarProducto.Name = "botonBorrar";
                borrarProducto.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(borrarProducto);
            }

            // LLENAMOS LISTADO DE USUARIOS
            foreach (Usuario u in data.MostrarUsuarios())
            {
                if (u != null)
                {
                    string r;
                    if (u.rol == 1) r = "Administrador";
                    else if (u.rol == 2) r = "Cliente";
                    else r = "Empresa";     //Preguntar si seguimos necesitando empresa

                    string[] users = { u.idUsuario.ToString(),
                                       u.dni.ToString(),
                                       u.nombre,
                                       u.apellido,
                                       u.mail,
                                       u.password,
                                       u.cuit_cuil.ToString(),
                                       r };
                    dataGridView3.Rows.Add(users);
                }
            }
            if (dataGridView3.Columns["botonBorrar"] == null)
            {
                DataGridViewButtonColumn borrarUsuario = new DataGridViewButtonColumn();
                borrarUsuario.HeaderText = "Borrar";
                borrarUsuario.Text = "Borrar";
                borrarUsuario.Name = "botonBorrar";
                borrarUsuario.UseColumnTextForButtonValue = true;
                dataGridView3.Columns.Add(borrarUsuario);
            }
            // LLENAMOS LISTADO DE CARROS
            foreach (Carro c in data.nContexto.carros)
            {
                string prods = "";
                double total = 0;
                if (c.carroProducto != null)
                {
                    foreach (CarroProducto cp in c.carroProducto)
                    {
                        prods += cp.producto.nombre + "*" + cp.cantidad + ", ";
                        total += cp.producto.precio * cp.cantidad;
                    }
                    string[] carroproducto = { c.usuario.idUsuario.ToString(),
                                                prods,
                                                total.ToString()
                                              };
                    if (total != 0 && prods != "")
                    {
                        dataGridView5.Rows.Add(carroproducto);
                    }
                }
            }

            // LLENAMOS LISTADO DE COMPRAS
            foreach (Compra c in data.nContexto.compras)
            {
                string prods = "";
                double total = 0;
                if (c.compraProducto != null)
                {
                    foreach (CompraProducto cp in c.compraProducto)
                    {
                        prods += cp.producto.nombre + "*" + cp.cantidad + ", ";
                        
                    }
                    total += c.total;
                    string[] carroproducto = { c.idCompra.ToString(),
                                               c.usuario.idUsuario.ToString(),
                                               prods,
                                               total.ToString() };
                    if (total != 0 && prods != "")
                    {
                        dataGridView4.Rows.Add(carroproducto);
                    }
                }
            }
            if (dataGridView4.Columns["botonBorrar"] == null)
            {
                DataGridViewButtonColumn borrarCompra = new DataGridViewButtonColumn();
                borrarCompra.HeaderText = "Borrar";
                borrarCompra.Text = "Borrar";
                borrarCompra.Name = "botonBorrar";
                borrarCompra.UseColumnTextForButtonValue = true;
                dataGridView4.Columns.Add(borrarCompra);
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA PRODUCTOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //######################################################
        //           MOSTRAR Y MODIFICAR PRODUCTOS
        //######################################################
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (merc.esAdmin(ID))
                {
                    if (e.ColumnIndex == dataGridView1.Columns["botonBorrar"].Index)
                    {
                        // ELIMINAR
                        DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar Producto?", "", MessageBoxButtons.YesNo);
                        if (resutl == DialogResult.Yes)
                        {
                            merc.EliminarProducto(int.Parse(dataGridView1[0, e.RowIndex].Value.ToString()));
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    else
                    {
                        //MODIFICAR 
                        Producto p = merc.nContexto.productos.Where(p => p.idProducto == int.Parse(dataGridView1[0, e.RowIndex].Value.ToString())).FirstOrDefault();
                        textBox9.Text = p.idProducto.ToString();
                        textBox5.Text = p.nombre;
                        textBox6.Text = p.precio.ToString();
                        textBox7.Text = p.cantidad.ToString();
                        textBox8.Text = p.idCategoria.ToString();
                        button3.Show();
                        tabControl2.SelectedTab = ModificarProducto;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("no se puede seleccionar las columnas");
            }
        }
        //######################################################
        //             MODIFICAR PRODUCTOS
        //######################################################
        private void button7_Click(object sender, EventArgs e)
        {
            if (merc.ModificarProducto(int.Parse(textBox9.Text), textBox5.Text, double.Parse(textBox6.Text),
                                       int.Parse(textBox7.Text), int.Parse(textBox8.Text)))
            {
                textBox9.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                refreshData(merc);
                tabControl2.SelectedTab = ListaProductos;
            }
        }
        //######################################################
        //             AGREGAR PRODUCTO NUEVO
        //######################################################
        private void button6_Click(object sender, EventArgs e)
        {
            if (merc.AgregarProducto(textBox1.Text, double.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text)))
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                refreshData(merc);
                tabControl2.SelectedTab = ListaProductos;
            }
        }
        //######################################################
        //              SELECCIONAR CATEGORIA 
        //######################################################

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Text = "Restablecer datos";
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            foreach (Producto p in merc.BuscarProductoPorCategoria(dataGridView6[0, e.RowIndex].Value.ToString()))
            {
                if (p != null)
                {
                    string[] prods = { p.idProducto.ToString(),
                                       p.nombre,
                                       p.precio.ToString(),
                                       p.cantidad.ToString(),
                                       p.idCategoria.ToString() };
                    dataGridView1.Rows.Add(prods);
                }
            }
        }

        //######################################################
        //             BOTON BUSCAR PRODUCTO
        //######################################################
        private void buttonB_Click(object sender, EventArgs e)
        {
            if (textBox34.Text != "")
            {
                //Se intenta parsear el texto, si lo logra, busca Producto por precio.
                if (int.TryParse(textBox34.Text, out int result))
                {
                    button1.Text = "Restablecer datos";
                    dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS

                    if (merc.BuscarProductoPorPrecio(textBox34.Text).Count == 0)
                    {
                        MessageBox.Show("No existe el producto: " + textBox34.Text);
                        refreshData(merc);
                        button1.Text = "Actualizar Datos";
                    }
                    else
                    {
                        foreach (Producto p in merc.BuscarProductoPorPrecio(textBox34.Text))
                        {
                            if (p != null)
                            {
                                string[] prods = { p.idProducto.ToString(),
                                                   p.nombre,
                                                   p.precio.ToString(),
                                                   p.cantidad.ToString(),
                                                   p.idCategoria.ToString() };
                                dataGridView1.Rows.Add(prods);
                            }
                        }
                    }
                }
                //Busca producto por Nombre
                else
                {
                    button1.Text = "Restablecer datos";
                    dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS

                    if (merc.BuscarProducto(textBox34.Text,1).Count == 0)
                    {
                        MessageBox.Show("No existe el producto: " + textBox34.Text);
                        refreshData(merc);
                        button1.Text = "Actualizar Datos";
                    }
                    else
                    {
                        foreach (Producto p in merc.BuscarProducto(textBox34.Text,1))
                        {
                            if (p != null)
                            {
                                string[] prods = { p.idProducto.ToString(),
                                                   p.nombre,
                                                   p.precio.ToString(),
                                                   p.cantidad.ToString(),
                                                   p.idCategoria.ToString() };
                                dataGridView1.Rows.Add(prods);
                            }
                        }
                    }
                }
            }
        }
        //######################################################
        //                COMBO BOX DE ORDEN
        //   COMBO BOX 1 -> ORDEN POR NOMBRE, CATEGORIA O PRECIO
        //   COMBO BOX 2 -> ORDEN ASCENDENTE O DESCENDENTE
        //######################################################
        private void OrdenNPC(int cambio) //Se repite en ambos eventos COMBOBOX entonces hago una sola funcion
        {
            if (comboBox1.Text == "Nombre")
            {
                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                var query = from prod in merc.nContexto.productos
                            orderby prod.nombre
                            select prod;

                foreach (Producto p in merc.BuscarProducto("",cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Precio")
            {

                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorPrecio(cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Categoria")
            {

                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorCategoria(cambio))
                {
                    if (p != null)
                    {
                        string[] prods = { p.idProducto.ToString(),
                                           p.nombre,
                                           p.precio.ToString(),
                                           p.cantidad.ToString(),
                                           p.idCategoria.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
        }

        // Variable que arregla error del REVERSE, si COMBOBOX es DESC (1), no vuelve a ejecutar el REVERSE
        int cambio = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // SI ESTA SELECCIONADO EL ORDEN DESCENDENTE
            if (comboBox2.SelectedIndex == 1 )
            {
                cambio = 0;
                OrdenNPC(cambio);
            }
            else 
            {
                cambio = 1;
                OrdenNPC(cambio);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                cambio = 0;
                OrdenNPC(cambio);
            }
            else
            {
                cambio = 1;
                OrdenNPC(cambio);
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA CATEGORIAS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = int.Parse(dataGridView2[0, e.RowIndex].Value.ToString());
            Categoria aux = merc.nContexto.categorias.Where(c => c.idCategoria == indice).FirstOrDefault();
            
            if (e.ColumnIndex == dataGridView2.Columns["botonBorrar"].Index)
            {
                // ELIMINAR
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar esta Categoria?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes && merc.EliminarCategoria(aux.idCategoria))
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                    refreshData(merc);
                }
                else
                {
                    MessageBox.Show("No se puede eliminar esta categoria, porque hay productos que dependen de ella");
                }
            }
            else
            {
                //MODIFICAR
                textBox11.Text = aux.idCategoria.ToString();
                textBox12.Text = aux.nombre;
                button3.Show();
                tabControl3.SelectedTab = ModificarCategoria;
            }
        }

        //######################################################
        //             MODIFICAR CATEGORIA
        //######################################################
        private void button8_Click(object sender, EventArgs e)
        {
            if (merc.ModificarCategoria(int.Parse(textBox11.Text), textBox12.Text))
            {
                textBox11.Text = "";
                textBox12.Text = "";
                refreshData(merc);
                tabControl3.SelectedTab = ListaCategoria;
            }
        }
        //######################################################
        //             AGREGAR CATEGORIA NUEVA
        //######################################################
        private void button4_Click(object sender, EventArgs e)
        {
            if (merc.AgregarCategoria(textBox10.Text))
            {
                textBox10.Text = "";
                refreshData(merc);
                tabControl3.SelectedTab = ListaCategoria;
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA USUARIOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //######################################################
        //           MOSTRAR Y ELIMINAR USUARIOS
        //######################################################
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView3.Columns["botonBorrar"].Index)
            {
                // ELIMINAR
                int indice = int.Parse(dataGridView3[0, e.RowIndex].Value.ToString());
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar el Usuario?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes && merc.EliminarUsuario(indice))
                {
                    dataGridView3.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                //MOSTRAR
                tabControl4.SelectedTab = ModificarUsuario;
                int ID = int.Parse(dataGridView3[0, e.RowIndex].Value.ToString());
                Usuario aux = merc.nContexto.usuarios.Where(u => u.idUsuario ==ID).FirstOrDefault();
              
                textBox20.Text = aux.idUsuario.ToString();
                textBox21.Text = aux.dni.ToString();
                textBox22.Text = aux.nombre;
                textBox23.Text = aux.apellido;
                textBox24.Text = aux.mail;
                textBox25.Text = aux.password;
                textBox26.Text = aux.cuit_cuil.ToString();
                button3.Show();
                comboBoxModRol.SelectedIndex = aux.rol - 1;
            }
        }
        //######################################################
        //            BOTON MODIFICAR USUARIO
        //######################################################
        private void buttonModificarUsuario_Click(object sender, EventArgs e)
        {
             if (merc.ModificarUsuario(int.Parse(textBox20.Text), int.Parse(textBox21.Text), textBox22.Text, textBox23.Text,
                                       textBox24.Text, textBox25.Text, int.Parse(textBox26.Text), comboBoxModRol.SelectedIndex + 1))
             {
                 textBox20.Text = "";
                 textBox21.Text = "";
                 textBox22.Text = "";
                 textBox23.Text = "";
                 textBox24.Text = "";
                 textBox25.Text = "";
                 textBox26.Text = "";
                 refreshData(merc);
                 tabControl4.SelectedTab = ListaUsuarios;
             }
        }
        //######################################################
        //             AGREGAR USUARIO NUEVO
        //######################################################
        private void buttonAgregar_Click(object sender, EventArgs e)
        {
              if (merc.AgregarUsuario(int.Parse(textDNI.Text), textNombre.Text, textApellido.Text, textMail.Text,
                                      textPass.Text, int.Parse(textCUILCUIT.Text), comboBoxRol.SelectedIndex + 1))
              {
                  refreshData(merc);
                  tabControl4.SelectedTab = ListaUsuarios;
              }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA COMPRAS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                        if (e.ColumnIndex == dataGridView4.Columns["botonBorrar"].Index)
                        {
                            // ELIMINAR
                            DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar la Compra?", "", MessageBoxButtons.YesNo);
                            if (resutl == DialogResult.Yes)
                            {
                                int indice = int.Parse(dataGridView4[0, e.RowIndex].Value.ToString());
                                merc.EliminarCompra(indice);
                                dataGridView4.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                        else
                        {
                            Compra aux = merc.nContexto.compras
                                        .Where(c => c.idCompra == int.Parse(dataGridView4[0, e.RowIndex].Value.ToString()) ).FirstOrDefault();
                            //MODIFICAR
                            textBox28.Text = aux.idCompra.ToString();
                            textBox29.Text = aux.idUsuario.ToString();
                            textBox30.Text = aux.total.ToString();
                            button3.Show();
                            tabControl5.SelectedTab = ModificarCompra;
                        }

        }
        //######################################################
        //             MODIFICAR COMPRA
        //######################################################
        private void button11_Click(object sender, EventArgs e)
        {
             if (merc.ModificarCompra(int.Parse(textBox28.Text), double.Parse(textBox30.Text)))
             {
                 textBox28.Text = "";
                 textBox29.Text = "";
                 textBox30.Text = "";
                 refreshData(merc);
                 tabControl5.SelectedTab = ListaCompras;
             }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                           OTROS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //################################4######################
        //                 OCULTAR MOSTRAR
        //######################################################
        private void ocultarMostrar(object sender, EventArgs e)
        {
            //Pestaña Usuario
            if (tabControl1.SelectedTab == tabPage1)
            {
                // Mostrar Panel de Orden
                comboBox1.Show();
                comboBox2.Show();
                label46.Show();
                buttonB.Show();
                textBox34.Show();
                // Mostrar Boton Agregar 
                button2.Show();
            }
            else
            {
                //Ocultar Panel de Orden
                comboBox1.Hide();
                comboBox2.Hide();
                label46.Hide();
                buttonB.Hide();
                textBox34.Hide();
                // Mostrar Boton Agregar 
                button2.Show();
                //Pestaña Carro y Compra
                if (tabControl1.SelectedTab == tabPage4 || tabControl1.SelectedTab == tabPage5)
                {
                    // Quitar Boton Agregar
                    button2.Hide();
                }
            }
        }

        //######################################################
        //                  BOTON AGREGAR
        //######################################################
        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Productos")
            {
                button3.Show();
                tabControl2.SelectedTab = AgregarProducto;
            }
            else if (tabControl1.SelectedTab.Text == "Categorias")
            {
                button3.Show();
                tabControl3.SelectedTab = AgregarCategoria;
            }
            else if (tabControl1.SelectedTab.Text == "Usuarios")
            {
                button3.Show();
                tabControl4.SelectedTab = AgregarUsuario;
            }
        }

        //######################################################
        //             BOTON ACTUALIZAR DATOS
        //######################################################
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Actualizar Datos";
            textBox34.Text = "";
            refreshData(merc); 
        }

        //######################################################
        //             BOTON EXIT
        //######################################################
        private void iconButton2_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Seguro que desea salir?", "", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                merc.cerrarContexto();
                this.TrasfEvento();
                this.Close();
            }
        }

        //######################################################
        //             BOTON VOLVER
        //######################################################
        private void button3_Click(object sender, EventArgs e)
        {
            button3.Hide();
            if (tabControl1.SelectedTab.Text == "Productos" && (tabControl2.SelectedTab == AgregarProducto || tabControl2.SelectedTab == ModificarProducto))
            {
                tabControl2.SelectedTab = ListaProductos;
            }
            else if (tabControl1.SelectedTab.Text == "Categorias" && (tabControl3.SelectedTab == AgregarCategoria || tabControl3.SelectedTab == ModificarCategoria))
            {
                tabControl3.SelectedTab = ListaCategoria;
            }
            else if (tabControl1.SelectedTab.Text == "Usuarios" && (tabControl4.SelectedTab == AgregarUsuario || tabControl4.SelectedTab == ModificarUsuario))
            {
                tabControl4.SelectedTab = ListaUsuarios;
            }
            else if (tabControl1.SelectedTab.Text == "Compras" && tabControl5.SelectedTab == ModificarCompra)
            {
                tabControl5.SelectedTab = ListaCompras;
            }
        }
    
    }
}
