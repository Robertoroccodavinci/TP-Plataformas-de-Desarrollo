using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_2_PlataformasDeDesarrollo
{
    public partial class Form3 : Form
    {
        public string[] argumentos;
        List<List<string>> datos;
        public Form3(string[] args)
        {
            InitializeComponent();
            argumentos = args;
            label2.Text = args[0];
            datos = new List<List<string>>();
            List<string> producto1 = new List<string>(new string[] { "TV", "50000", "200" });
            List<string> producto2 = new List<string>(new string[] { "PC", "75000", "150" });
            datos.Add(producto1);
            datos.Add(producto2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshData(datos);
        }

        private void refreshData(List<List<string>> data)
        {
            //borro los datos
            dataGridView1.Rows.Clear();
            //agrego lo nuevo
            foreach (List<string> producto in data)
                dataGridView1.Rows.Add(producto.ToArray());
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
