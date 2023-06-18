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
    public partial class Product_list : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        DataTable dt = new DataTable();
       public int en;
       Form3 f3;
       Form1 f1;
       BindingSource BindingSource1 = new BindingSource();
        public Product_list(Form3 f)
        {
            InitializeComponent();
            f3 = f;
        }
        public Product_list(Form1 f1e)
        {
            InitializeComponent();
            f1 = f1e;
            
        }

        private void Product_list_Load(object sender, EventArgs e)
        {
           
            //pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products";
            cmd.ExecuteNonQuery();
            conn.Close();
           
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            
            da.Fill(dt);
            BindingSource1.DataSource = dt;
            dataGridView1.DataSource = BindingSource1;
            dataGridView1.Columns.Remove("عکس");
            dataGridView1.Columns.Remove("شماره_فنی");
            dataGridView1.Columns.Remove("توضیحات");
            dataGridView1.Columns.Remove("قیمت");
            dataGridView1.Columns.Remove("شرکت_تولید_کننده");
            dataGridView1.Columns["کد_قطعه"].Width = 60;
            dataGridView1.Columns["تعداد"].Width = 50;
            dataGridView1.Columns["نام_قطعه"].Width = 150;
            dataGridView1.AllowUserToAddRows = false;
            
            dataGridView1.RowHeadersVisible = false;
            for (int d = 0; d < dt.Rows.Count; d++)
            {
              comboBox1.Items.Add(dt.Rows[d]["نام_قطعه"].ToString() + " - " + dt.Rows[d]["نوع_خودرو"].ToString());
                
              //  comboBox1.Items.Add(dt.Rows[d]["نام_قطعه"].ToString());
            }
         //   

            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
         //  btnColumn.HeaderText = "";
         //  btnColumn.Text = "مشاهده عکس";
         //  btnColumn.Name = "btn1";
         //  btnColumn.Width = 40;
         //  dataGridView1.Columns.Add(btnColumn);
         //  btnColumn.UseColumnTextForButtonValue = true;
         //
             btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "";
            btnColumn.Text = "اضافه به سبد خرید";
            btnColumn.Name = "btn2";
            btnColumn.Width = 40;
            dataGridView1.Columns.Add(btnColumn);
            btnColumn.UseColumnTextForButtonValue = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try {
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("لیست قطعات خالیه،انتظار داره چی ببینی؟؟!!!");
                }


                else if (dt.Rows[dataGridView1.CurrentRow.Index]["عکس"].ToString() != "")
                {
                    pictureBox1.ImageLocation = dt.Rows[dataGridView1.CurrentRow.Index]["عکس"].ToString();
                    pictureBox2.ImageLocation = dt.Rows[dataGridView1.CurrentRow.Index]["عکس"].ToString();
                }
                else
                {
                    pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
                    pictureBox2.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
                }
            }
            catch
            {

            }
       
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label2.Text = dt.Rows[dataGridView1.CurrentRow.Index]["تعداد"].ToString();
            label4.Text = dt.Rows[dataGridView1.CurrentRow.Index]["نام_قطعه"].ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void Product_list_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            en = e.RowIndex;
            if (e.ColumnIndex == 4)
            {
                new ADD_TO__CART_FORM(this).Show();
            }
        }

        private void Product_list_Activated(object sender, EventArgs e)
        {
            //f3.counterclass();
            label7.Text = f3.counterclass();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
           // BindingSource1.Filter = string.Format("CONVERT(کد_قطعه, System.String) LIKE '%{0}%' ", comboBox1.SelectedItem);
            BindingSource1.Filter = string.Format("نام_قطعه LIKE '%{0}%'", dt.Rows[comboBox1.SelectedIndex]["نام_قطعه"].ToString());

        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource1.Filter = string.Format("نام_قطعه LIKE '%{0}%' or نوع_خودرو LIKE '%{0}%'", comboBox1.Text);
           // BindingSource1.Filter = string.Format("نوع_خودرو LIKE '%{0}%'", comboBox1.Text);
        }


    }
}
