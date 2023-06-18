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
    public partial class payment : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        Int64 price;
        public payment()
        {
            InitializeComponent();
        }

        private void payment_Load(object sender, EventArgs e)
        {
            dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector1.Value = DateTime.Now;

            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (price == 0)
            {
                textBox2.Text = "";
            }
            else
            {
                textBox2.Text = (price).ToString();
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Int64.TryParse(textBox2.Text, out price);
            textBox2.Text = price.ToString("0,0");
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || dateTimeSelector1.Value == null ||comboBox1.Text=="" || comboBox2.Text=="")
            {
                MessageBox.Show("*موارد مهم را وارد کنید");
            }
          
            else
            {
                try
                {
                    conn.Open();
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from payment where کد_خریدار='" + comboBox1.Text+ "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dt2);
                    //MessageBox.Show(dt2.Rows.Count.ToString());
                    Int64 mande = 0;
                    if (dt2.Rows.Count != 0)
                    {
                        mande = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["مانده"]);
                    }

                    mande = mande - price;

                    string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
               
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO `payment` (`کد_خریدار`, `تاریخ`, `ساعت`, `بدهکار`,`بستانکار`,`مانده`, `نحوه پرداخت`, `توضیحات`, `کاربر`) VALUES ('" + comboBox1.Text + "','" + dateTimeSelector1.Text + "','" + time + "','" + "0" + "','" + price + "','" + mande + "','" + "پرداخت به صورت " + comboBox2.Text + "','" + textBox3.Text + "','" + Login.User + "'  )";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("مشخصات پرداخت ذخیره شد");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex);
                }

            }
        }
    }
}
