using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TP2_PlataformasDeDesarrollo
{
    public partial class FormCliente : Form
    {
        private Mercado merc;
        private int ID;
        public delegate void TransfDelegado2(); // Metodo
        public TransfDelegado2 TrasfEvento;

        public FormCliente(int ID, string nombre, Object m)
        {

            InitializeComponent();
            this.ID = ID;
            label2.Text = nombre;
            merc = (Mercado)m;
            refreshData(merc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            tabControl1.SelectedIndexChanged += new EventHandler(ocultarMostrar);
            button15.Hide();
            button14.Hide();

            //CREAMOS EVENTOS EN LAS TABLAS PARA DAR MAS ACCIONES
            dataGridView1.CellClick += dataGridView1_CellClick; //EVENTO TABLA PRODUCTOS
            dataGridView5.CellClick += dataGridView5_CellClick; //EVENTO TABLA MI CARRO

            //EVENTO PARA LOS BOTONES DE LA LISTA DE CATEGORIAS
            dataGridView6.CellClick += dataGridView6_CellClick;
        }
        //######################################################
        //           ACTUALIZAR DATOS DE LAS TABLAS
        //######################################################
        private void refreshData(Mercado data)
        {
            //borro los datos
            dataGridView1.Rows.Clear(); //LIMPIAMOS TABLA PRODUCTOS
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO
            dataGridView6.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIAS DE PRODUCTOS

            foreach (Categoria c in merc.nCategorias)
            {
                if (c != null)
                {
                    dataGridView6.Rows.Add(c.nNombre);
                }
            }
            
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

            foreach (KeyValuePair<Producto, int> kvp in data.nUsuarios[ID].nCarro.nProductos)
            {
                if (kvp.Key != null)
                {
                    string[] prods = { kvp.Key.nIDProd.ToString(),
                                       kvp.Key.nNombre,
                                       kvp.Key.nPrecio.ToString(),
                                       kvp.Value.ToString(),
                                      (kvp.Key.nPrecio * kvp.Value).ToString() };
                    dataGridView5.Rows.Add(prods);
                }
            }
            if (dataGridView5.Columns["botonBorrarDelCarro"] == null)
            {
                DataGridViewButtonColumn borrarDelCarro = new DataGridViewButtonColumn();
                borrarDelCarro.HeaderText = "Borrar";
                borrarDelCarro.Text = "Borrar";
                borrarDelCarro.Name = "botonBorrarDelCarro";
                borrarDelCarro.UseColumnTextForButtonValue = true;
                dataGridView5.Columns.Add(borrarDelCarro);
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA PRODUCTOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //######################################################
        //                      MOSTRAR 
        //######################################################
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indice = merc.nProductos.FindIndex(x => x.nIDProd == int.Parse(dataGridView1[0, e.RowIndex].Value.ToString()));

            label20.Text = merc.nProductos[indice].nIDProd.ToString();
            label7.Text = merc.nProductos[indice].nNombre;
            label8.Text = merc.nProductos[indice].nPrecio.ToString();
            label9.Text = merc.nProductos[indice].nCantidad.ToString();
            label10.Text = merc.nProductos[indice].nCategoria.nID.ToString();
            numericUpDown1.Maximum = merc.nProductos[indice].nCantidad;
            tabControl2.SelectedTab = MostrarProducto;
            
            
        }
       
        //######################################################
        //             AGREGAR PRODUCTO AL CARRO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
         
            int indice = merc.nProductos.FindIndex(x => x.nIDProd == int.Parse(label20.Text));
            if (merc.AgregarAlCarro(indice, int.Parse(numericUpDown1.Value.ToString()), ID))
            {
                MessageBox.Show("Producto agregada con exito al Carro.");
                refreshData(merc);
                tabControl2.SelectedTab = ListaProductos;
            }
            else 
            {
                MessageBox.Show("ERROR: el Producto no se pudo agregar al Carro.");
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
        //             OCULTAR MOSTRAR
        //######################################################
        private void ocultarMostrar(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                comboBox1.Show();
                comboBox2.Show();
                label46.Show();
                button13.Show();
                textBox34.Show();
                button15.Hide();
                button14.Hide();
            }
            else
            {
                comboBox1.Hide();
                comboBox2.Hide();
                label46.Hide();
                button13.Hide();
                textBox34.Hide();
                button15.Show();
                button14.Show();
            }
        }

        //######################################################
        //             BOTON BUSCAR PRODUCTO
        //######################################################
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox34.Text != "") { 
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
        
        int cambio = 0; // Variable que arregla error del REVERSE, si COMBOBOX es DESC (1), no vuelve a ejecutar el REVERSE 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrdenNPC();
            cambio = 1;//Permite ejecutar el DESC
            
            // SI ESTA SELECCIONADO EL ORDEN DESCENDENTE
            if (comboBox2.SelectedIndex == 1 && cambio == 1)
            {
                merc.nProductos.Reverse();// El reverse hace que ande mal la segunda vez que lo elegimos
                refreshData(merc);
                cambio = 0;//Impide volver a ejecutar DESC, que ejecuta devuelta el reverse que haria un loop
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                //EN CASO DE ESTAR PREVIAMENTE SELECCIONADO EL ORDEN DESCENDENTE, SE VUELVE A ORDENAR
                OrdenNPC();
                cambio = 1;//Permite ejecutar el DESC
            }
            else if (comboBox2.SelectedIndex == 1 && cambio == 1)
            {
                merc.nProductos.Reverse();// El reverse hace que ande mal la segunda vez que lo elegimos
                refreshData(merc);
                cambio = 0; //Impide volver a ejecutar DESC, que ejecuta devuelta el reverse que haria un loop
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA MI CARRO
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView5.Columns["botonBorrarDelCarro"].Index)
            {
                // ELIMINAR PRODUCTO DEL CARRO
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar el producto de tu Carro?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes)
                {
                    int idprod = int.Parse(dataGridView5[0, e.RowIndex].Value.ToString());
                    int cantidad = int.Parse(dataGridView5[2, e.RowIndex].Value.ToString());
                    merc.nUsuarios[ID].nCarro.QuitarProducto(merc.nProductos[idprod], cantidad);
                    dataGridView5.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                //MODIFICAR CANTIDAD DE PRODUCTO DEL CARRO
                textBox31.Text = dataGridView5[0, e.RowIndex].Value.ToString();
                textBox32.Text = dataGridView5[1, e.RowIndex].Value.ToString();
                textBox33.Text = dataGridView5[2, e.RowIndex].Value.ToString();
                tabControl6.SelectedTab = ModificarCarro;
            }
        }
        //######################################################
        //             MODIFICAR CARRO
        //######################################################
        private void button12_Click(object sender, EventArgs e)
        {
            if (merc.QuitarAlCarro(int.Parse(textBox31.Text), int.Parse(textBox33.Text), ID)) 
            {
                tabControl6.SelectedTab = ListaCarro;
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
            DialogResult respuesta = MessageBox.Show("Antes de Salir, ¿Deseas guardar todos los cambios?", "", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                merc.guardarTodo();
            }
            this.TrasfEvento();
            this.Close();
        }

    }
}
