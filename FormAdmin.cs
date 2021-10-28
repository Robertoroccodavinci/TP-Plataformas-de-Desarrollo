using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
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
            //borro los datos
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            dataGridView2.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIAS
            dataGridView3.Rows.Clear(); //LIMPIAMOS TABLA USUARIOS
            dataGridView4.Rows.Clear(); //LIMPIAMOS TABLA COMPRAS
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO
            dataGridView6.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIA DE PRODUCTOS

            foreach (Categoria c in data.nCategorias)
            {
                if (c != null)
                {
                    string[] cate = { c.nID.ToString(),
                                        c.nNombre };
                    dataGridView2.Rows.Add(cate);
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

            // LISTADO DE CATEGORIAS DE LA LISTA DE PRODUCTOS
            foreach (Categoria c in merc.nCategorias)
            {
                if (c != null)
                {
                    //Rellenamos la lista de categorias(SOLO NOMBRES) en la Grilla de la izquierda en la pestaña de PRODUCTOS
                    //Para poder usar la grilla como filtro
                    dataGridView6.Rows.Add(c.nNombre);
                }
            }

            //data.nProductos.Sort();
            foreach (Producto p in data.nProductos)
            {
                if (p != null)
                {
                    string[] prods = { p.nIDProd.ToString(),
                                p.nNombre,
                                p.nPrecio.ToString(),
                                p.nCantidad.ToString(),
                                p.nCategoria.nID.ToString() };
                    dataGridView1.Rows.Add(prods);
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
            
            foreach (Usuario u in data.MostrarUsuarios())
            {
                if (u != null)
                {
                    string r;
                    if (u.nRol == 1) r = "Administrador";
                    else if (u.nRol == 2) r = "Cliente";
                    else r = "Empresa";     //Preguntar si seguimos necesitando empresa

                    string[] users = { u.nID.ToString(),
                                u.nDNI.ToString(),
                                u.nNombre,
                                u.nApellido,
                                u.nMail,
                                u.nPassword,
                                u.nCUIT_CUIL.ToString(),
                                r };
                    dataGridView3.Rows.Add(users);

                    if (r == "Cliente"|| r == "Empresa")
                    {
                        
                        string prods = "";
                        double total = 0;
                        foreach (KeyValuePair<Producto, int> kvp in u.nCarro.nProductos)
                        { if (kvp.Key != null)
                            prods += kvp.Key.nNombre + "*" + kvp.Value + ", ";
                            total += kvp.Key.nPrecio * kvp.Value;

                        }
                        string[] carro = { u.nID.ToString(),
                                           prods.ToString(),
                                           total.ToString() 
                                         };
                        if (total != 0 && prods != "") {
                            dataGridView5.Rows.Add(carro);
                        }
                    }
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


            foreach (Compra c in data.nCompras)
            {
                if (c != null)
                {
                    string prods = "";
                    foreach (KeyValuePair<Producto, int> kvp in c.nProductos)
                    {
                        prods += kvp.Key.nNombre + "*" + kvp.Value + ", ";
                    }
                    string[] comp = { c.nIDCompra.ToString(),
                                      c.nComprador.nID.ToString(),
                                      prods.ToString(),
                                      c.nTotal.ToString()
                                    };
                    dataGridView4.Rows.Add(comp);
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
 
                        int indice = merc.nProductos.FindIndex(x => x.nIDProd == int.Parse(dataGridView1[0, e.RowIndex].Value.ToString()));
                        textBox9.Text = merc.nProductos[indice].nIDProd.ToString();
                        textBox5.Text = merc.nProductos[indice].nNombre;
                        textBox6.Text = merc.nProductos[indice].nPrecio.ToString();
                        textBox7.Text = merc.nProductos[indice].nCantidad.ToString();
                        textBox8.Text = merc.nProductos[indice].nCategoria.nID.ToString();
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
                    string[] prods = { p.nIDProd.ToString(),
                                       p.nNombre,
                                       p.nPrecio.ToString(),
                                       p.nCantidad.ToString(),
                                       p.nCategoria.nID.ToString() };
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
                                string[] prods = { p.nIDProd.ToString(),
                                               p.nNombre,
                                               p.nPrecio.ToString(),
                                               p.nCantidad.ToString(),
                                               p.nCategoria.nID.ToString() };
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

                    if (merc.BuscarProducto(textBox34.Text).Count == 0)
                    {
                        MessageBox.Show("No existe el producto: " + textBox34.Text);
                        refreshData(merc);
                        button1.Text = "Actualizar Datos";
                    }
                    else
                    {
                        foreach (Producto p in merc.BuscarProducto(textBox34.Text))
                        {
                            if (p != null)
                            {
                                string[] prods = { p.nIDProd.ToString(),
                                               p.nNombre,
                                               p.nPrecio.ToString(),
                                               p.nCantidad.ToString(),
                                               p.nCategoria.nID.ToString() };
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
        private void OrdenNPC() //Se repite en ambos eventos COMBOBOX entonces hago una sola funcion
        {
            if (comboBox1.Text == "Nombre")
            {
                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                merc.nProductos.Sort();
                foreach (Producto p in merc.nProductos)
                {
                    if (p != null)
                    {
                        string[] prods = { p.nIDProd.ToString(),
                                           p.nNombre,
                                           p.nPrecio.ToString(),
                                           p.nCantidad.ToString(),
                                           p.nCategoria.nID.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Precio")
            {
                
                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorPrecio())
                {
                    if (p != null)
                    {
                        string[] prods = { p.nIDProd.ToString(),
                                           p.nNombre,
                                           p.nPrecio.ToString(),
                                           p.nCantidad.ToString(),
                                           p.nCategoria.nID.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
            else if (comboBox1.Text == "Categoria")
            {
               
                dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
                foreach (Producto p in merc.MostrarTodosProductosPorCategoria())
                {
                    if (p != null)
                    {
                        string[] prods = { p.nIDProd.ToString(),
                                           p.nNombre,
                                           p.nPrecio.ToString(),
                                           p.nCantidad.ToString(),
                                           p.nCategoria.nID.ToString() };
                        dataGridView1.Rows.Add(prods);
                    }
                }
            }
        }

        // Variable que arregla error del REVERSE, si COMBOBOX es DESC (1), no vuelve a ejecutar el REVERSE
        int cambio = 0;  
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrdenNPC();
            //Permite ejecutar el DESC
            cambio = 1;
            
            // SI ESTA SELECCIONADO EL ORDEN DESCENDENTE
            if (comboBox2.SelectedIndex == 1 && cambio == 1)
            {
                merc.nProductos.Reverse();
                refreshData(merc);
                //Impide volver a ejecutar DESC, que ejecuta devuelta el reverse que haria un loop
                cambio = 0; 
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                //EN CASO DE ESTAR PREVIAMENTE SELECCIONADO EL ORDEN DESCENDENTE, SE VUELVE A ORDENAR
                OrdenNPC();
                //Permite ejecutar el DESC
                cambio = 1;
            }
            else if (comboBox2.SelectedIndex == 1 && cambio == 1)
            {
                merc.nProductos.Reverse();
                refreshData(merc);
                //Impide volver a ejecutar DESC, que ejecuta devuelta el reverse que haria un loop
                cambio = 0; 
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA CATEGORIAS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indiceBaseCategoria = int.Parse(dataGridView2[0, e.RowIndex].Value.ToString());
            int indiceArrayCategoria = Array.FindIndex(merc.nCategorias, x => x.nID == indiceBaseCategoria);
            if (e.ColumnIndex == dataGridView2.Columns["botonBorrar"].Index && merc.nCategorias[indiceArrayCategoria] != null)
            {
                // ELIMINAR
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar esta Categoria?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes && merc.EliminarCategoria(indiceBaseCategoria))
                {
                    dataGridView2.Rows.RemoveAt(e.RowIndex);
                }
                else
                {
                    MessageBox.Show("No se puede eliminar esta categoria, porque hay productos que dependen de ella");
                }
                
                
            }
            else
            {
                //MODIFICAR
                textBox11.Text = merc.nCategorias[indiceArrayCategoria].nID.ToString();
                textBox12.Text = merc.nCategorias[indiceArrayCategoria].nNombre;
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
                int indice = merc.nUsuarios.FindIndex(x => x.nID == ID);
                //int indice = int.Parse(dataGridView3[0, e.RowIndex].Value.ToString());
                textBox20.Text = merc.nUsuarios[indice].nID.ToString();
                textBox21.Text = merc.nUsuarios[indice].nDNI.ToString();
                textBox22.Text = merc.nUsuarios[indice].nNombre;
                textBox23.Text = merc.nUsuarios[indice].nApellido;
                textBox24.Text = merc.nUsuarios[indice].nMail;
                textBox25.Text = merc.nUsuarios[indice].nPassword;
                textBox26.Text = merc.nUsuarios[indice].nCUIT_CUIL.ToString();
                button3.Show();
                comboBoxModRol.SelectedIndex = int.Parse(merc.nUsuarios[indice].nRol.ToString()) - 1;

            }
        }
        //######################################################
        //            BOTON MODIFICAR USUARIO
        //######################################################
        private void buttonModificarUsuario_Click(object sender, EventArgs e)
        {
            if (merc.ModificarUsuario(int.Parse(textBox20.Text), int.Parse(textBox21.Text), textBox22.Text, textBox23.Text,
                                      textBox24.Text, textBox25.Text, long.Parse(textBox26.Text), comboBoxModRol.SelectedIndex + 1))
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
                                    textPass.Text, long.Parse(textCUILCUIT.Text), comboBoxRol.SelectedIndex + 1))
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
                int indice = merc.nCompras.FindIndex(x=>x.nIDCompra == int.Parse(dataGridView4[0, e.RowIndex].Value.ToString()) );
                //MODIFICAR
                textBox28.Text = merc.nCompras[indice].nIDCompra.ToString();
                textBox29.Text = merc.nCompras[indice].nComprador.nID.ToString();
                textBox30.Text = merc.nCompras[indice].nTotal.ToString();
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
        //                                       PESTAÑA MI CARRO
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //######################################################
        //                  MOSTRAR CARRO
        //######################################################

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
            refreshData(merc); //RECARGA LAS LISTAS
        }


        //######################################################
        //             BOTON EXIT
        //######################################################
        private void iconButton2_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Seguro que desea salir?", "", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {               
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
