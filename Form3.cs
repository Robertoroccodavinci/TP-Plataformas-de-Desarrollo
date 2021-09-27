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
        Mercado merc = new Mercado();
        string[] lines;
        public Form3 ()
        {
            InitializeComponent();
            //merc = m;
            //label2.Text = u.nNombre;
            
            lines = System.IO.File.ReadAllLines(merc.Dest(0));

            datos = new List<List<string>>();
            List<string> producto1 = new List<string>(new string[] { "TV", "50000", "200" });
            List<string> producto2 = new List<string>(new string[] { "PC", "75000", "150" });
            datos.Add(producto1);
            datos.Add(producto2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refreshData(lines);
        }

        private void refreshData(string[] prod)
        {
            //borro los datos
            dataGridView1.Rows.Clear();
            //agrego lo nuevo
            foreach (string p in prod) {
                dataGridView1.Rows.Add(p.ToArray());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage4_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
