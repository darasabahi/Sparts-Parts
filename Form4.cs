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
    public partial class chek_form : Form
    {
        faktor f;
        Int64 price;
        Boolean picopen;
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        public chek_form(faktor fa)
        {
            InitializeComponent();
            f = fa;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox4.Text = f.textBox1.Text;
            textBox1.Text = f.comboBox2.Text + " " + f.comboBox3.Text;
            dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector1.CustomFormat = "yyyy/MM/dd";

            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\chek.jpg";
            textBox2.Text = f.kol.ToString();
            Int64.TryParse(textBox2.Text, out price);
            textBox2.Text = price.ToString("0,0");
          
         
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
            if (textBox3.Text =="" & dateTimeSelector1.Value != null)
            {
                MessageBox.Show("*بانک صادر کننده چک را انتخاب کنید");
            }
            else if (textBox3.Text == "" & dateTimeSelector1.Value == null)
            {                
                MessageBox.Show("*تاریخ چک را انتخاب کنید"+"\n"+"*بانک صادر کننده چک را انتخاب کنید");
            }
            else if (textBox3.Text != "" & dateTimeSelector1.Value == null)
            {
                MessageBox.Show("*تاریخ چک را انتخاب کنید" );
            }
            else
            {
                try
                {
                    string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                    if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\chekpic\\" + textBox1.Text + " " + f.textBox1.Text + ".jpg"); }
                    string pic = Application.StartupPath + "\\chekpic\\" + textBox1.Text + ".jpg";
                    conn.Open();
                    OleDbCommand cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "INSERT INTO `chek` (`user`, `time`, `date`, `کد_خریدار`, `نام_خریدار`, `تاریخ_چک`, `مبلغ`, `بانک`, `وضعیت`, `عکس`, `شماره_فاکتور`) VALUES ('" + Login.User + "','" + time + "','" + f.dateTimeSelector1.Text + "','" + f.comboBox1.Text + "','" + textBox1.Text + "','" + dateTimeSelector1.Text + "','" + price + "','" + textBox3.Text + "','" + "وصول نشده" + "','" + pic + "','" + f.textBox1.Text + "'  )";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("مشخصات چک ذخیره شد");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex);
                }
               
            }
           
        }

    

     
    }
}
