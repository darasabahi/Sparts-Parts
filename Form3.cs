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

    public partial class Form3 : Form
    {
        Form1 f1;
        Boolean fill = false;
        Boolean combo1select = false;
        Boolean combo2select = false;
        Boolean combo3select = false;
        Boolean combo4select = false;
        
        Boolean picopen;
        Boolean refresh = true;
        int price;
       public int counter;
        int code;

        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        public Form3(Form1 f2)
        {
            InitializeComponent();
            f1 = f2;
        }
        public void Refreshcombo()
        {


            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Remove("عکس");
            conn.Close();

            int c = comboBox1.Items.Count;

            for (int d = 0; d < c; ++d)
            {
                comboBox1.Items.RemoveAt(0);
            }
            c = comboBox2.Items.Count;

            for (int d = 0; d < c; ++d)
            {
                comboBox2.Items.RemoveAt(0);
            }
            c = comboBox3.Items.Count;

            for (int d = 0; d < c; ++d)
            {
                comboBox3.Items.RemoveAt(0);
            }
            for (int d = 0; d < c; ++d)
            {
                comboBox4.Items.RemoveAt(0);
            }

            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"].ToString());
                comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"].ToString());
                comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"].ToString());
                comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"].ToString());


            }
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            numericUpDown1.Value = 1;
            richTextBox1.Text = "";

        }


        public void Refresh()
        {

            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Remove("عکس");
            conn.Close();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            conn.Open();


            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from faktor";
            cmd.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da = new OleDbDataAdapter(cmd);
           
            da.Fill(dt1);
            if (dt1.Rows.Count == 0)
            {
                code = 1;
            }
            else
            {

                code = Convert.ToInt32(dt1.Rows[dt1.Rows.Count - 1]["کد_فاکتور"]) + 1;
            }
            conn.Close();
            counterclass();
           
            
        }
        public string counterclass()
        {
            conn.Open();
           OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from cart where کد_سبد=" + code;
            cmd.ExecuteNonQuery();
           OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt = new DataTable();
            conn.Close();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                counter = 1;
            }
            else
            {

                counter = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["counter"]) + 1;
            }
            f1.label1.Text = (counter-1).ToString();
            return f1.label1.Text;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            refresh = false;
            openFileDialog1.Filter = "Images  files (*.jpg *.JPEG)|*.jpg;*.JPEG|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                picopen = true;
            }

           
        }
        private void ثبتسفارشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insert_to_cart_btn.Visible = true;
            update_btn.Visible = false;
            clear_btn.Visible = true;
            insertbtn.Visible = false;
            upload_btn.Visible = false;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            combo1select = false;
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";

            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            numericUpDown1.Value = 1;
            richTextBox1.Text = "";


            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();

            int c = comboBox1.Items.Count;
            for (int d = 0; d < c; ++d)
            {
                comboBox1.Items.RemoveAt(0);
            }
            for (int d = 0; d < c; ++d)
            {
                comboBox2.Items.RemoveAt(0);
            }
            for (int d = 0; d < c; ++d)
            {
                comboBox3.Items.RemoveAt(0);
            }
            for (int d = 0; d < c; ++d)
            {
                comboBox4.Items.RemoveAt(0);
            }

            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"]);
                comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"]);
                comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"]);


            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Remove("عکس");
        }

        private void اضافهکردنقطعهجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insert_to_cart_btn.Visible = false;
            update_btn.Visible = false;
            clear_btn.Visible = true;
            insertbtn.Visible = true;
            upload_btn.Visible = true;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            textBox1.Visible = true;
            textBox1.Enabled = false;
            textBox1.Focus();
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products";
            cmd.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable dt3 = new DataTable();
            da.Fill(dt3);
            conn.Close();
            if (dt3.Rows.Count == 0)
            {
                textBox1.Text = "1";
            }
            else
            {
                textBox1.Text = (Convert.ToInt32(dt3.Rows[dt3.Rows.Count - 1]["کد_قطعه"])+1).ToString();
            }
         
           

        }

        private void ویرایشمشخصاتقطعهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insert_to_cart_btn.Visible = false;
            clear_btn.Visible = true;
            insertbtn.Visible = false;
            upload_btn.Visible = true;
            update_btn.Visible = true;
            delete_btn.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            int num;
            int.TryParse(textBox6.Text, out num);
            numericUpDown1.Value = num;
            
       


        }
       

        private void insertbtn_Click(object sender, EventArgs e)
        {
            
             if (textBox2.Text == "")
            {
              
                label13.Visible = true;
                label14.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }
            else if (textBox5.Text == "")
            {
              
                label13.Visible = true;
                label14.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }
            else
            {
                if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\produstspic\\" + textBox1.Text + ".jpg"); }
                string pic = Application.StartupPath + "\\produstspic\\" + textBox1.Text + ".jpg";
               
                label13.Visible = false;
                label14.Visible = false;
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                //  int num;
                //int.TryParse(textBox5.Text, out num);
                cmd.CommandText = "insert into Products(نام_قطعه,شماره_فنی,نوع_خودرو,شرکت_تولید_کننده,قیمت,تعداد,توضیحات,عکس) values('" + textBox2.Text + "','" + textBox7.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + price + "','" + numericUpDown1.Value + "','" + richTextBox1.Text + "','" + pic + "'  )";
                cmd.ExecuteNonQuery();
                conn.Close();
                textBox1.Text = (Convert.ToInt32(textBox1.Text) + 1).ToString();
                textBox2.Text = "";
                textBox3.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                richTextBox1.Text = "";
                numericUpDown1.Value = 1;
                pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
                picopen = false;
                refresh = true;
                MessageBox.Show("قطعه با موفقیت ذخیره شد");
            }
           
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
            if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\produstspic\\" + comboBox1.Text + ".jpg"); }
            string pic = Application.StartupPath + "\\produstspic\\" + comboBox1.Text + ".jpg";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
                                                                                                                                                                                                                                                                                                                             
         //  int num;
         //  int.TryParse(textBox5.Text, out num);
            cmd.CommandText = "update Products set نام_قطعه='" + comboBox2.Text + "', شماره_فنی='" + textBox7.Text + "', نوع_خودرو='" + comboBox4.Text + "', شرکت_تولید_کننده='" + comboBox3.Text + "', قیمت='" + price + "', تعداد='" + numericUpDown1.Value + "', توضیحات='" + richTextBox1.Text + "', عکس='" + pic + "' where کد_قطعه=" + Convert.ToInt32(comboBox1.Text); 
            cmd.ExecuteNonQuery();
            conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            Refresh();
            richTextBox1.Text = "";
            numericUpDown1.Value = 1;
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
            picopen = false;
            comboBox1.Enabled = true;
            refresh = true;
            upload_btn.Visible = false;
            update_btn.Visible = false;
            delete_btn.Visible = false;
            insert_to_cart_btn.Visible = true;

            MessageBox.Show("قطعه با موفقیت ویرایش شد");
        }

        private void insert_to_cart_btn_Click(object sender, EventArgs e)
        {
            if (combo1select)
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from cart where کد_قطعه='" + comboBox1.Text + "' and کد_سبد="+ code;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                conn.Close();

                if (dt.Rows.Count == 0) 
                {

                    int temp1 = Convert.ToInt32(textBox6.Text);
                    int temp6 = Convert.ToInt32(numericUpDown1.Value);
                    int temp7 = temp1 - temp6;
                    if (temp7 >= 0)
                    {
                        label11.Visible = false;
                        conn.Open();
                        cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                      
                        //string cartcod = "0";

                        int temp2 = Convert.ToInt32(numericUpDown1.Value);
                        int temp3 = price * temp2;
                        f1.label1.Text = counter.ToString();
                        cmd.CommandText = "INSERT INTO `Cart` (`counter`, `کد_سبد`, `کد_قطعه`, `نام_قطعه`, `تعداد`, `قیمت`, `قیمت_کل`) VALUES( '" + counter + "' ,'" + code + "' ,'" + comboBox1.Text + "','" + comboBox2.Text + "','" + numericUpDown1.Value + "','" + price + "','" + temp3 + "'  )";
                        cmd.ExecuteNonQuery();
                        counter += 1;

                        cmd = conn.CreateCommand();
                        cmd.CommandType = CommandType.Text;

                        cmd.CommandText = "update Products set تعداد='" + temp7 + "' where کد_قطعه=" + Convert.ToInt32(comboBox1.Text); 
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        Refresh();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        comboBox1.Text = "";
                        comboBox2.Text = "";
                        comboBox3.Text = "";
                        comboBox4.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        richTextBox1.Text = "";
                        numericUpDown1.Value = 1;
                        pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
                        combo1select = false;

                     }
                    else
                    {
                        label11.Visible = true;
                    }


                }
                else 
                {
                    MessageBox.Show("این قطعه در لیست خرید موجود است");
                }
               
            }
            else
            {
                MessageBox.Show("لطفا یک قطعه انتخاب کنید");
            }
                 
           
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Products where کد_قطعه=" + Convert.ToInt32(comboBox1.Text); 
            cmd.ExecuteNonQuery();
            conn.Close();

            comboBox1.Items.Remove(comboBox1.Text);
            comboBox2.Items.Remove(comboBox2.Text);
            comboBox3.Items.Remove(comboBox3.Text);
            comboBox4.Items.Remove(comboBox4.Text);



            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox3.Text = "";
            textBox5.Text = "";
            numericUpDown1.Value = 1;
            richTextBox1.Text = "";
            refresh = true;
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\product.jpg";
            delete_btn.Visible = false;
            update_btn.Visible = false;
            upload_btn.Visible = false;
            insert_to_cart_btn.Visible = true;
            MessageBox.Show("قطعه با موفیت حذف شد");



        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            numericUpDown1.Value = 1;
            picopen = false;
            refresh = true;
            richTextBox1.Text = "";
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
            combo1select = true;
            conn.Close();
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Products where کد_قطعه=" + Convert.ToInt32(comboBox1.Text); 

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            comboBox2.Text = dt.Rows[0]["نام_قطعه"].ToString();
            comboBox3.Text = dt.Rows[0]["نوع_خودرو"].ToString();
            comboBox4.Text = dt.Rows[0]["شرکت_تولید_کننده"].ToString();
            string gh= dt.Rows[0]["قیمت"].ToString();
            price = Convert.ToInt32(gh);
            textBox5.Text = price.ToString("0,0");
            richTextBox1.Text = dt.Rows[0]["توضیحات"].ToString();
            textBox6.Text = (dt.Rows[0]["تعداد"]).ToString();
            textBox7.Text = (dt.Rows[0]["شماره_فنی"]).ToString();
            pictureBox1.ImageLocation = dt.Rows[0]["عکس"].ToString();
            conn.Close();
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select)
            {

                OleDbCommand cmd = conn.CreateCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                int c;
                combo2select = true;
                if (combo3select & !combo4select)
                {

                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where نوع_خودرو='" + comboBox3.Text + "'and نام_قطعه='" + comboBox2.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox4.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox4.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"]);
                    }

                }
                else if (!combo3select & !combo4select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Products where نام_قطعه='" + comboBox2.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"]);
                    }

                    c = comboBox4.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox4.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"]);
                    }
                }
                else if (!combo3select & combo4select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نام_قطعه='" + comboBox2.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"]);
                    }
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نام_قطعه='" + comboBox2.Text + "'and نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                }


            }
        }
        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select)
            {

                OleDbCommand cmd = conn.CreateCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                int c;
                combo3select = true;
                if (combo2select & !combo4select)
                {

                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where نوع_خودرو='" + comboBox3.Text + "' and نام_قطعه='" + comboBox2.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox4.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox4.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"]);
                    }

                }
                else if (!combo2select & !combo4select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Products where نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox2.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox2.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"]);
                    }

                    c = comboBox4.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox4.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox4.Items.Add(dt.Rows[d]["شرکت_تولید_کننده"]);
                    }
                }
                else if (!combo2select & combo4select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox2.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox2.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"]);
                    }
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نام_قطعه='" + comboBox2.Text + "'and نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                }


            }
        }
        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select)
            {

                OleDbCommand cmd = conn.CreateCommand();
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                int c;
                combo4select = true;
                if (combo2select & !combo3select)
                {

                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نام_قطعه='" + comboBox2.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"]);
                    }

                }
                else if (!combo2select & !combo3select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox2.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox2.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"]);
                    }

                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نوع_خودرو"]);
                    }
                }
                else if (!combo2select & combo3select)
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                    c = comboBox2.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox2.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox2.Items.Add(dt.Rows[d]["نام_قطعه"]);
                    }
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "select * from Products where شرکت_تولید_کننده='" + comboBox4.Text + "'and نام_قطعه='" + comboBox2.Text + "'and نوع_خودرو='" + comboBox3.Text + "' ";
                    cmd.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns.Remove("عکس");
                    conn.Close();
                    c = comboBox1.Items.Count;

                    for (int d = 0; d < c; ++d)
                    {
                        comboBox1.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox1.Items.Add(dt.Rows[d]["کد_قطعه"]);
                    }

                }


            }
        }

        private void دریافتازاکسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importtoexcel f = new importtoexcel();
            f.Show();
        }



        private void Form3_Activated(object sender, EventArgs e)
        {
            if (refresh) {
                Refreshcombo();
            
            }
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (price == 0)
            {
                textBox5.Text = "";
            }
            else
            {
                textBox5.Text = (price).ToString();
            }
           
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            price = Convert.ToInt32(textBox5.Text);
            textBox5.Text = price.ToString("0,0");
        }

        private void مشاهدهلیستقطعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Product_list(this).Show();
        }

        private void دریافتخروجیاکسلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = true;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Sheet1";

                for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                {

                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;

                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                }

            }
            catch (Exception D)
            {
                //  MessageBox.Show(D.ToString());
            }
          
        }

    

       


       
    }
}
