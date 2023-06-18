using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace spare_parts
{   
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
           
         

        }
       
        private void button3_Click(object sender, EventArgs e)
        {
                   
            new Form3(this).Show();
            
           
           
            
           
        }
        public void timer1_Tick(object sender, EventArgs e)
        {
        //   conn.Open();
        //   OleDbCommand cmd = conn.CreateCommand();
        //   cmd.CommandType = CommandType.Text;
        //   cmd.CommandText = "select * from cart";
        //   cmd.ExecuteNonQuery();
        //   DataTable dt = new DataTable();
        //   OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //   da.Fill(dt);
        //   conn.Close();
        //   label1.Text = dt.Rows.Count.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            new faktor(this).Show();
        }
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    e.Graphics.DrawImage(Properties.Resources.Spare_parts_main_690_432, new Rectangle(Point.Empty, this.ClientSize));
        //}
        private void Form1_Load(object sender, EventArgs e)
        {
         
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Financial_from f = new Financial_from();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sellers_form f = new Sellers_form();
            f.Show();
         
        }

      
    }
}
