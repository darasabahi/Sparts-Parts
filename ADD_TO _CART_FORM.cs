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
    public partial class ADD_TO__CART_FORM : Form
    {
        Product_list f;
        Form1 f1;
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        int count;
        int count1;
        int code;
        int temp;
        int counter;
        Int64 price;
        string name;
        int codefak;
        public ADD_TO__CART_FORM(Product_list fl)
        {
            InitializeComponent();
            f = fl;
        }
        public ADD_TO__CART_FORM(Form1 f1f)
        {
            InitializeComponent();
            f1 = f1f;
        }


        private void ADD_TO__CART_FORM_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            numericUpDown1.Maximum = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[3].Value);
            code = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[0].Value);
            name = f.dataGridView1.Rows[f.en].Cells[1].Value.ToString();

            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products where کد_قطعه=" + code;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            int p = Convert.ToInt32(dt.Rows[0]["قیمت"]);
            textBox4.Text = p.ToString("0,0");
           
            textBox1.Text = dt.Rows[0]["قیمت"].ToString();
         
            count1 = Convert.ToInt32(dt.Rows[0]["تعداد"]);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            price = Convert.ToInt64(textBox1.Text);
              conn.Open();


              OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from faktor";
                cmd.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
     

                da1.Fill(dt2);
                if (dt2.Rows.Count == 0)
                {
                    codefak = 1;
                }
                else
                {

                    codefak = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["کد_فاکتور"]) + 1;
                }

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from cart where کد_قطعه='" + code + "' and کد_سبد=" + codefak;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from cart where کد_سبد=" + codefak;
                cmd.ExecuteNonQuery();
                da = new OleDbDataAdapter(cmd);
                DataTable dt1 = new DataTable();
                conn.Close();
                da.Fill(dt1);

                if (dt1.Rows.Count == 0)
                {
                    counter = 1;
                }
                else
                {

                    counter = Convert.ToInt32(dt1.Rows[dt1.Rows.Count - 1]["counter"]) + 1;
                }
          
                conn.Close();

                if (dt.Rows.Count == 0) 
                {
                    int temp1 = count1;
                    int temp6 = Convert.ToInt32(numericUpDown1.Value);
                    int temp7 = temp1 - temp6;
                        conn.Open();
                        cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                      
                        //string cartcod = "0";

                        Int64 temp2 = Convert.ToInt64(numericUpDown1.Value);
                        Int64 temp3 = price * temp2;

                        cmd.CommandText = "INSERT INTO `Cart` (`counter`, `کد_سبد`, `کد_قطعه`, `نام_قطعه`, `تعداد`, `قیمت`, `قیمت_کل`) VALUES( '" + counter + "' ,'" + codefak + "' ,'" + code + "','" + name + "','" + numericUpDown1.Value + "','" + price + "','" + temp3 + "'  )";
                        cmd.ExecuteNonQuery();
                        counter += 1;

                        cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "update Products set تعداد='" + temp7 + "' where کد_قطعه=" + code; 
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        this.Close();
                }
                else 
                {
                    MessageBox.Show("این قطعه در لیست خرید موجود است");
                }
               
            }        
    }
}
