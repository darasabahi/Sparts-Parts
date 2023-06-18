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

    public partial class chek_form2 : Form
    {
        Int64 price;
        Boolean picopen;
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        string date;
        public chek_form2()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.Filter = "Images  files (*.jpg *.JPEG)|*.jpg;*.JPEG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                picopen = true;

            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            Int64.TryParse(textBox2.Text, out price);
            textBox2.Text = price.ToString("0,0");
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

        private void save_btn_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" & dateTimeSelector1.Value != null)
            {
                MessageBox.Show("*بانک صادر کننده چک را انتخاب کنید");
            }
            else if (textBox3.Text == "" & dateTimeSelector1.Value == null)
            {
                MessageBox.Show("*تاریخ چک را انتخاب کنید" + "\n" + "*بانک صادر کننده چک را انتخاب کنید");
            }
            else if (textBox3.Text != "" & dateTimeSelector1.Value == null)
            {
                MessageBox.Show("*تاریخ چک را انتخاب کنید");
            }
            else
            {
                try
                {
                    string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                    if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\chekpic\\" + textBox1.Text + " " + textBox1.Text + ".jpg"); }
                    string pic = Application.StartupPath + "\\chekpic\\" + textBox1.Text + ".jpg";
                    conn.Open();
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO `chek` (`user`, `time`, `date`, `کد_خریدار`, `نام_خریدار`, `تاریخ_چک`, `مبلغ`, `بانک`, `وضعیت`, `عکس`, `شماره_فاکتور`) VALUES ('" + Login.User + "','" + time + "','" + date + "','" + comboBox1.Text + "','" + textBox1.Text + "','" + dateTimeSelector1.Text + "','" + price + "','" + textBox3.Text + "','" + "وصول نشده" + "','" + pic + "','" + textBox4.Text + "'  )";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("مشخصات چک ذخیره شد");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex);
                }

            }

        }

        private void chek_form2_Load(object sender, EventArgs e)
        {
           
           
           
            Int64.TryParse(textBox2.Text, out price);
            textBox2.Text = price.ToString("0,0");


            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\chek.jpg";
            dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector1.Value = DateTime.Now;
            date = dateTimeSelector1.Text;
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                conn.Open();


                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from customer_info where  کد_خریدار='" + comboBox1.Text + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt1);
                textBox1.Text = dt1.Rows[0]["نام"].ToString() + " " + dt1.Rows[0]["نام_خانوادگی"].ToString();
                conn.Close();
            }
            catch
            {
                MessageBox.Show("");
                conn.Close();

            }
        }

       
   


    }
}
