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
    public partial class merjoatoDB : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        marjoa f;
        int count;
        int count1;
        int code;
        int temp;
        public merjoatoDB(marjoa m)
        {
            InitializeComponent();
            f = m;
        }

        private void merjoatoDB_Load(object sender, EventArgs e)
        {
            textBox1.Text = f.dataGridView1.Rows[f.en].Cells[4].Value.ToString();
            numericUpDown1.Value = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[3].Value);
            numericUpDown1.Maximum = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[3].Value);
            textBox6.Text = f.dataGridView1.Rows[f.en].Cells[2].Value.ToString();
             code = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[1].Value);

            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products where کد_قطعه=" + code;
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            textBox4.Text=dt.Rows[0]["قیمت"].ToString();
            count1 = Convert.ToInt32(dt.Rows[0]["تعداد"]);
           
            

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            temp = Convert.ToInt32(f.dataGridView1.Rows[f.en].Cells[3].Value) - Convert.ToInt32(numericUpDown1.Value);
            f.dataGridView1.Rows[f.en].Cells[3].Value = temp;
            count = count1 + Convert.ToInt32(numericUpDown1.Value);
            int price = Convert.ToInt32(textBox1.Text);
            int temp2 = Convert.ToInt32(numericUpDown1.Value);
            int temp3 = price * temp2;
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Products set تعداد=" + count  +" where کد_قطعه=" + code;
            cmd.ExecuteNonQuery();


          
            cmd.CommandText = "INSERT INTO `Cart` (`counter`, `کد_سبد`, `کد_قطعه`, `نام_قطعه`, `تعداد`, `قیمت`, `قیمت_کل`) VALUES( '" + f.counter + "' ,'" + f.codefaktor + "' ,'" + code + "','" + textBox6.Text + "','" + numericUpDown1.Value + "','" + price + "','" + temp3 + "'  )";
            cmd.ExecuteNonQuery();
            f.counter += 1;
           


          

            conn.Close();
            this.Close();
        }
    }
}
