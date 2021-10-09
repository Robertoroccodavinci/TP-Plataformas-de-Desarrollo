﻿using System;
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
        private Mercado merc;
        private int ID;
        public delegate void TransfDelegado2();
        public TransfDelegado2 TrasfEvento;



        public Form3(int ID, string nombre, Object m)
        {

            InitializeComponent();
            this.ID = ID;
            label2.Text = nombre;
            merc = (Mercado)m;
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
            dataGridView5.Rows.Clear(); //LIMPIAMOS TABLA MI CARRO

            //CREAMOS EVENTOS EN LAS TABLAS PARA DAR MAS ACCIONES
            dataGridView1.CellClick += dataGridView1_CellClick; //EVENTO TABLA PRODUCTOS
            dataGridView5.CellClick += dataGridView5_CellClick; //EVENTO TABLA MI CARRO

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
                                        kvp.Value.ToString()};
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
            int indice = int.Parse(dataGridView1[0, e.RowIndex].Value.ToString());
            foreach (Producto p in merc.nProductos) 
            {
                if (p.nIDProd == indice) 
                {
                    label20.Text = p.nIDProd.ToString();
                    label7.Text = p.nNombre;
                    label8.Text = p.nPrecio.ToString();
                    label9.Text = p.nCantidad.ToString();
                    label10.Text = p.nCategoria.nID.ToString();
                    numericUpDown1.Maximum = p.nCantidad;
                    tabControl2.SelectedTab = MostrarProducto;
                }
            }
        }
       
        //######################################################
        //             AGREGAR PRODUCTO AL CARRO
        //######################################################
        private void button5_Click(object sender, EventArgs e)
        {
            if (merc.AgregarAlCarro(int.Parse(label20.Text), int.Parse(numericUpDown1.Value.ToString()), ID))
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
            merc.llenarListas();

            //CREAMOS LISTA PARA GUARDAR LOS PRODUCTOS
            List<Producto> p = new List<Producto>();
            p = merc.nProductos;
            // LIMPIAMOS LA LISTA DE PRODUCTOS
            merc.nProductos = new List<Producto>();

            foreach (Producto pro in p)
            {
                if (pro.nCategoria.nNombre == dataGridView6[0, e.RowIndex].Value.ToString())
                {
                    //AGREGAMOS LOS PRODUCTOS QUE CUMPLAN CON LA QUERY
                    merc.AgregarProducto(pro.nNombre, pro.nPrecio, pro.nCantidad, pro.nCategoria.nID);
                }
            }
            refreshData(merc);
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
        //             BOTON BUSCAR PRODUCTO
        //######################################################
        private void button13_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox34.Text, out int result))
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
            /*if (merc.QuitarAlCarro(int.Parse(textBox31.Text), )) 
            {
                tabControl6.SelectedTab = ListaCarro;
            }*/
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
