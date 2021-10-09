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
    public partial class Form3 : Form
    {
        Mercado merc;
        int ID;

        public delegate void TransfDelegado2();
        public TransfDelegado2 TrasfEvento;
        
        

        public Form3(int ID, string nombre,Object m)
        {
            
            InitializeComponent();
            this.ID = ID;
            label2.Text = nombre;
            merc = (Mercado) m;
            refreshData(merc);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            tabControl1.SelectedIndexChanged += new EventHandler(ocultarComboBox);
            
            
            foreach (Categoria c in merc.nCategorias) 
            {
                if (c != null) 
                {
                    dataGridView6.Rows.Add(c.nNombre);
                }
            }
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
            dataGridView2.Rows.Clear(); //LIMPIAMOS TABLA CATEGORIAS
            dataGridView3.Rows.Clear(); //LIMPIAMOS TABLA USUARIOS
            dataGridView4.Rows.Clear(); //LIMPIAMOS TABLA COMPRAS
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO

            //CREAMOS EVENTOS EN LAS TABLAS PARA DAR MAS ACCIONES
            dataGridView1.CellClick += dataGridView1_CellClick; //EVENTO TABLA PRODUCTOS
            dataGridView2.CellClick += dataGridView2_CellClick; //EVENTO TABLA CATEGORIAS
            dataGridView3.CellClick += dataGridView3_CellClick; //EVENTO TABLA USUARIOS
            dataGridView4.CellClick += dataGridView4_CellClick; //EVENTO TABLA COMPRAS
            dataGridView5.CellClick += dataGridView5_CellClick; //EVENTO TABLA MI CARRO
            
            if (merc.esAdmin(ID))
            {
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
                foreach (Usuario u in data.nUsuarios)
                {
                    if (u != null)
                    {
                        string r;
                        if (u.nRol == 1) r = "Cliente";
                        else
                        if (u.nRol == 2) r = "Empresa";
                        else r = "Admin";

                        string[] users = { u.nID.ToString(),
                                   u.nDNI.ToString(),
                                   u.nNombre,
                                   u.nApellido,
                                   u.nMail,
                                   u.nPassword,
                                   u.nCUIT_CUIL.ToString(),
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

                foreach (Compra c in data.nCompras)
                {
                    if (c != null) 
                    {
                        string[] comp = { c.nIDCompra.ToString(),
                                          c.nComprador.ToString() };
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
            
            // SI ES CLIENTE O EMPRESA
            else 
            {
                tabPage2.Dispose();
                tabPage3.Dispose();
                tabPage4.Dispose();
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
                                           kvp.Value.ToString(),
                                           kvp.Key.nPrecio.ToString() };
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
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA PRODUCTOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //######################################################
        //           MOSTRAR Y MODIFICAR PRODUCTOS
        //######################################################
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (merc.esAdmin(ID))
            {
                if (e.ColumnIndex == dataGridView1.Columns["botonBorrar"].Index)
                {
                    // ELIMINAR
                    DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar Producto?","",MessageBoxButtons.YesNo);
                    if (resutl == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (e.RowIndex == row.Index)
                            {
                                merc.EliminarProducto(int.Parse(row.Cells[0].Value.ToString()));
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                    }
                }
                else 
                {
                   
                  //MODIFICAR 
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (e.RowIndex == row.Index)
                        {
                            int indice = int.Parse(row.Cells[0].Value.ToString());
                            textBox9.Text = merc.nProductos[indice].nIDProd.ToString();
                            textBox5.Text = merc.nProductos[indice].nNombre;
                            textBox6.Text = merc.nProductos[indice].nPrecio.ToString();
                            textBox7.Text = merc.nProductos[indice].nCantidad.ToString();
                            textBox8.Text = merc.nProductos[indice].nCategoria.nID.ToString();
                            tabControl2.SelectedTab = ModificarProducto;
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (e.RowIndex == row.Index)
                    {
                        int indice = int.Parse(row.Cells[0].Value.ToString());
                        label20.Text = merc.nProductos[indice].nIDProd.ToString();
                        label7.Text = merc.nProductos[indice].nNombre;
                        label8.Text = merc.nProductos[indice].nPrecio.ToString();
                        label9.Text = merc.nProductos[indice].nCantidad.ToString();
                        label10.Text = merc.nProductos[indice].nCategoria.nID.ToString();
                        numericUpDown1.Maximum= merc.nProductos[indice].nCantidad;
                        tabControl2.SelectedTab = MostrarProducto;
                    }
                }
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
                tabControl2.SelectedTab = ListaProductos;
            }
        }
        //######################################################
        //             AGREGAR PRODUCTO AL CARRO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Valor");
            Console.WriteLine(numericUpDown1.Value);
            
            if (merc.AgregarAlCarro(int.Parse(label20.Text), int.Parse(numericUpDown1.Value.ToString()), ID)) 
            {
                
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
                tabControl2.SelectedTab = ListaProductos;
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA CATEGORIAS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (merc.esAdmin(ID))
            {
                if (e.ColumnIndex == dataGridView2.Columns["botonBorrar"].Index)
                {
                    // ELIMINAR
                    DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar esta Categoria?", "", MessageBoxButtons.YesNo);
                    if (resutl == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (e.RowIndex == row.Index)
                            {
                                int indice = int.Parse(row.Cells[0].Value.ToString());
                                merc.EliminarCategoria(indice);
                                dataGridView2.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                    }
                }
                else
                {
                    //MODIFICAR

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (e.RowIndex == row.Index)
                        {
                            int indice = int.Parse(row.Cells[0].Value.ToString());
                            textBox11.Text = merc.nCategorias[indice].nID.ToString();
                            textBox12.Text = merc.nCategorias[indice].nNombre;
                            tabControl3.SelectedTab = ModificarCategoria;
                        }
                    }
                   

                }
            }
            
        }
        //######################################################
        //             MODIFICAR CATEGORIA
        //######################################################
        private void button8_Click(object sender, EventArgs e)
        {
            if (merc.ModificarCategoria(int.Parse(textBox11.Text), textBox12.Text))
            {
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
                tabControl3.SelectedTab = ListaCategoria;
            }
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA USUARIOS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //######################################################
        //           MOSTRAR Y MODIFICAR PRODUCTOS
        //######################################################
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (merc.esAdmin(ID))
            {
                if (e.ColumnIndex == dataGridView3.Columns["botonBorrar"].Index)
                {
                    // ELIMINAR
                    DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar el Usuario?", "", MessageBoxButtons.YesNo);
                    if (resutl == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView3.Rows)
                        {
                            if (e.RowIndex == row.Index)
                            {
                                int indice = int.Parse(row.Cells[0].Value.ToString());
                                merc.EliminarUsuario(indice);
                                dataGridView3.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                     }
                }
                else
                {

                    foreach (DataGridViewRow row in dataGridView3.Rows)
                    {
                        if (e.RowIndex == row.Index)
                        {
                            int indice = int.Parse(row.Cells[0].Value.ToString());
                            //MODIFICAR
                            textBox20.Text = merc.nUsuarios[indice].nID.ToString();
                            textBox21.Text = merc.nUsuarios[indice].nDNI.ToString();
                            textBox22.Text = merc.nUsuarios[indice].nNombre;
                            textBox23.Text = merc.nUsuarios[indice].nApellido;
                            textBox24.Text = merc.nUsuarios[indice].nMail;
                            textBox25.Text = merc.nUsuarios[indice].nPassword;
                            textBox26.Text = merc.nUsuarios[indice].nCUIT_CUIL.ToString();
                            textBox27.Text = merc.nUsuarios[indice].nRol.ToString();
                            tabControl3.SelectedTab = ModificarUsuario;
                        }
                    }

                   

                }
            }
        }
        //######################################################
        //             MODIFICAR USUARIO
        //######################################################
        private void button10_Click(object sender, EventArgs e)
        {
            if (merc.ModificarUsuario(int.Parse(textBox20.Text), int.Parse(textBox21.Text), textBox22.Text, textBox23.Text,
                                      textBox24.Text, textBox25.Text, int.Parse(textBox26.Text), int.Parse(textBox21.Text)))
            {
                tabControl4.SelectedTab = ListaUsuarios;
            }
        }
        //######################################################
        //             AGREGAR USUARIO NUEVO
        //######################################################
        private void button9_Click(object sender, EventArgs e)
        {
            if (merc.AgregarUsuario(int.Parse(textBox13.Text), textBox14.Text, textBox15.Text, textBox16.Text,
                                    textBox17.Text, int.Parse(textBox18.Text), int.Parse(textBox19.Text)))
            {
                tabControl4.SelectedTab = ListaUsuarios;
            }
        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA COMPRAS
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (merc.esAdmin(ID))
            {
                if (e.ColumnIndex == dataGridView4.Columns["botonBorrar"].Index)
                {
                    // ELIMINAR
                    DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar la Compra?", "", MessageBoxButtons.YesNo);
                    if (resutl == DialogResult.Yes)
                    {
                        foreach (DataGridViewRow row in dataGridView4.Rows)
                        {
                            if (e.RowIndex == row.Index)
                            {
                                int indice = int.Parse(row.Cells[0].Value.ToString());
                                merc.EliminarCompra(indice);
                                dataGridView4.Rows.RemoveAt(e.RowIndex);
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {
                        if (e.RowIndex == row.Index)
                        {
                            int indice = int.Parse(row.Cells[0].Value.ToString());
                            //MODIFICAR
                            textBox28.Text = merc.nCompras[indice].nIDCompra.ToString();
                            textBox21.Text = merc.nCompras[indice].nComprador.nID.ToString();
                            textBox22.Text = merc.nCompras[indice].nTotal.ToString();
                            tabControl5.SelectedTab = ModificarCompra;
                        }
                    }
                }
            }
        }
        //######################################################
        //             MODIFICAR COMPRA
        //######################################################
        private void button11_Click(object sender, EventArgs e)
        {
            if (merc.ModificarCompra(int.Parse(textBox28.Text),double.Parse(textBox22.Text)))
            {
                tabControl5.SelectedTab = ListaCompras;
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //                                       PESTAÑA MI CARRO
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == dataGridView5.Columns["botonBorrar"].Index)
            {
                // ELIMINAR PRODUCTO DEL CARRO
                DialogResult resutl = MessageBox.Show("¿Seguro que desea eliminar el producto de tu Carro?", "", MessageBoxButtons.YesNo);
                if (resutl == DialogResult.Yes)
                {
                        
                    int idprod = int.Parse(dataGridView5.CurrentRow.Cells[0].Value.ToString());
                    int cantidad = int.Parse(dataGridView5.CurrentRow.Cells[2].Value.ToString());
                    merc.nUsuarios[ID].nCarro.QuitarProducto(merc.nProductos[idprod],cantidad);
                    dataGridView5.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                //MODIFICAR CANTIDAD DE PRODUCTO DEL CARRO
                textBox31.Text = dataGridView5.CurrentRow.Cells[0].Value.ToString();
                textBox32.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();
                textBox33.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString();
                    
                tabControl6.SelectedTab = ModificarCarro;

            }
            
        }
        //######################################################
        //             MODIFICAR CARRO
        //######################################################
        private void button12_Click(object sender, EventArgs e)
        {
            /*if (merc.QuitarAlCarro(int.Parse(textBox31.Text), )) 
            {
                tabControl6.SelectedTab = ListaCarro;
            }*/
        }


        //######################################################
        //             OCULTAR COMBO BOX
        //######################################################
        private void ocultarComboBox(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                comboBox1.Show();
                comboBox2.Show();
                label46.Show();
                button13.Show();
                textBox34.Show();
            }
            else
            {
                comboBox1.Hide();
                comboBox2.Hide();
                label46.Hide();
                button13.Hide();
                textBox34.Hide();
            }
        }

        //######################################################
        //             OCULTAR COMBO BOX
        //######################################################

        private void dataGridView6_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Text = "Restablecer datos";
            merc.llenarListas();
            foreach (DataGridViewRow row in dataGridView6.Rows)
            {
                if (e.RowIndex == row.Index)
                {
                    //CREAMOS LISTA PARA GUARDAR LOS PRODUCTOS
                    List<Producto> p = new List<Producto>();
                    p = merc.nProductos;
                    // LIMPIAMOS LA LISTA DE PRODUCTOS
                    merc.nProductos = new List<Producto>();

                    foreach (Producto pro in p)
                    {
                        if (pro.nCategoria.nNombre == row.Cells[0].Value.ToString())
                        {
                            //AGREGAMOS LOS PRODUCTOS QUE CUMPLAN CON LA QUERY
                            merc.AgregarProducto(pro.nNombre, pro.nPrecio, pro.nCantidad, pro.nCategoria.nID);
                        }
                    }
                    refreshData(merc);
                }
            }
        }


















            //######################################################
            //                  BOTON AGREGAR
            //######################################################

            private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Productos")
            {
                tabControl2.SelectedTab = AgregarProducto;
            }
            else if (tabControl1.SelectedTab.Text == "Categorias")
            {
                tabControl3.SelectedTab = AgregarCategoria;
            }
            else if (tabControl1.SelectedTab.Text == "Usuarios")
            {
                tabControl3.SelectedTab = AgregarUsuario;
            }
        }

       
        //######################################################
        //             BOTON ACTUALIZAR DATOS
        //######################################################
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "Actualizar Datos";
            textBox34.Text = "";
            merc.llenarListas();
            refreshData(merc); //RECARGA LAS LISTAS
        }
        //######################################################
        //             BOTON GUARDAR
        //######################################################
        private void button2_Click(object sender, EventArgs e)
        {
            merc.guardarTodo();//SI LO HACEMOS BOOLEANO PARA COMPROBAR SI FUE EXITOSO O NO
            MessageBox.Show("archivos guardados");// MENSAJE CORROBORANDO QUE SE GUARDO EXITOSAMENTE
        }



        
        //######################################################
        //             BOTON BUSCAR PRODUCTO
        //######################################################
        private void button13_Click(object sender, EventArgs e)
        {
            if  (int.TryParse(textBox34.Text, out int result))
            {
                merc.llenarListas();
                merc.BuscarProductoPorPrecio(textBox34.Text);
                refreshData(merc);
                button1.Text = "Restablecer Datos";
                tabControl1.SelectedTab = tabPage1;
                tabControl2.SelectedTab = ListaProductos;
            }
            else
            {
                merc.llenarListas();
                merc.BuscarProducto(textBox34.Text);
                refreshData(merc);
                button1.Text = "Restablecer Datos";
                tabControl1.SelectedTab = tabPage1;
                tabControl2.SelectedTab = ListaProductos;
            }
            

        }
        //######################################################
        //                COMBO BOX DE ORDEN
        //   COMBO BOX 1 -> ORDEN POR NOMBRE, CATEGORIA O PRECIO
        //   COMBO BOX 2 -> ORDEN ASCENDENTE O DESCENDENTE
        //######################################################
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Nombre")
            {
                merc.nProductos.Sort();
                refreshData(merc);
            }
            else if (comboBox1.Text == "Precio")
            {
                merc.MostrarTodosProductosPorPrecio();
                refreshData(merc);
            }
            else if (comboBox1.Text == "Categoria")
            {
                merc.MostrarTodosProductosPorCategoria();
                refreshData(merc);
            }
            // SI ESTA SELECCIONADO EL ORDEN DESCENDENTE
            if (comboBox2.SelectedIndex == 1)
            {
                merc.nProductos.Reverse();
                refreshData(merc);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                //EN CASO DE ESTAR PREVIAMENTE SELECCIONADO EL ORDEN DESCENDENTE, SE VUELVE A ORDENAR
                if (comboBox1.Text == "Nombre")
                {
                    merc.nProductos.Sort();
                    refreshData(merc);
                }
                else if (comboBox1.Text == "Precio")
                {
                    merc.MostrarTodosProductosPorPrecio();
                    refreshData(merc);
                }
                else if (comboBox1.Text == "Categoria")
                {
                    merc.MostrarTodosProductosPorCategoria();
                    refreshData(merc);
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                merc.nProductos.Reverse();
                refreshData(merc);
            }
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

        }

    }
}
