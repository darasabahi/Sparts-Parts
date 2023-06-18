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
using System.Configuration;
using System.IO;

namespace spare_parts
{
    public partial class Form2 : Form
    {
        Boolean fill = false;
        Boolean combo1select = false;
        Boolean combo2select = false;
        Boolean combo3select = false;
        Boolean picopen = false;
      
        string combotext;

        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        public Form2()
        {
            InitializeComponent();
        }
        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        } 

        private void Form2_Load(object sender, EventArgs e)
        {
            ویرایشخریدارToolStripMenuItem.Enabled = false;
            حذفخریدارToolStripMenuItem.Enabled = false;
            combotext = comboBox2.Text;
            label3.Visible = false;
            label7.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") {

                label3.Visible = true;
                label7.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }
            else if (textBox2.Text == "")
            {
                label3.Visible = true;
                label7.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }
            else if (textBox3.Text == "")
            {
                label3.Visible = true;
                label7.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }
            else if (textBox4.Text == "")
            {
                label3.Visible = true;
                label7.Visible = true;
                label10.Visible = true;
                label11.Visible = true;

                MessageBox.Show("موارد مهم را وارد کنید");
            }

            else {
                     if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\customerpic\\" + textBox1.Text + ".jpg"); }
                     string pic = Application.StartupPath + "\\customerpic\\" + textBox1.Text + ".jpg";
                     conn.Open();
                     OleDbCommand cmd = conn.CreateCommand();
                     cmd.CommandType = CommandType.Text;
                     cmd.CommandText = "insert into customer_info values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + pic + "'  )";
                     cmd.ExecuteNonQuery();
                     conn.Close();
                    
                   //  MessageBox.Show(openFileDialog1.FileName);
                     textBox1.Text = "";
                     textBox2.Text = "";
                     textBox3.Text = "";
                     comboBox1.Text = "";
                     comboBox2.Text = "";
                     comboBox3.Text = "";
                     textBox4.Text = "";
                     textBox5.Text = "";
                     richTextBox1.Text = "";
                     richTextBox2.Text = "";
                     pictureBox1.ImageLocation = Application.StartupPath+"\\pic\\nopic.jpg";
                     picopen = false;
                     label3.Visible = false;
                     label7.Visible = false;
                     label10.Visible = false;
                     label11.Visible = false;
                     MessageBox.Show("خریدار با موفقیت ذخیره شد");
                     fill = true;
              }
        }
        private void button2_Click(object sender, EventArgs e)
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
        private void update_Click(object sender, EventArgs e)
        {
            if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\customerpic\\" + comboBox1.Text + ".jpg"); }
            string pic = Application.StartupPath + "\\customerpic\\" + comboBox1.Text + ".jpg";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update customer_info set نام='" + comboBox2.Text + "', نام_خانوادگی='" + comboBox3.Text + "', تلفن='" + textBox4.Text + "', آدرس='" + richTextBox1.Text + "', توضیحات='" + richTextBox2.Text + "', عکس='" + pic + "' where کد_خریدار='" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            conn.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\nopic.jpg";
            picopen = false;
            comboBox1.Enabled = true;
            button2.Visible = false;
            button4.Visible = false;
            update.Visible = false;
            dataGridView1.DataSource = "";
            MessageBox.Show("خریدار با موفقیت ویرایش شد");
        }
        private void حذفخریدارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from customer_info where کد_خریدار='" + comboBox1.Text + "'";
            cmd.ExecuteNonQuery();
            conn.Close();

            comboBox1.Items.Remove(comboBox1.Text);
            comboBox2.Items.Remove(comboBox2.Text);
            comboBox3.Items.Remove(comboBox3.Text);

            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;

            button2.Visible = false;

            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\nopic.jpg";
            MessageBox.Show("خریدار با موفیت حذف شد");
            dataGridView1.DataSource = "";



        }
        private void اضافهکردنخریدارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            update.Visible = false;
            panel1.Visible = true;
            panel2.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button4.Visible = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;
            ویرایشخریدارToolStripMenuItem.Enabled = false;
            حذفخریدارToolStripMenuItem.Enabled = false;

            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            dataGridView1.DataSource = "";
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\nopic.jpg";
            
        }

        private void جستوجوToolStripMenuItem_Click(object sender, EventArgs e)
        {
            combo3select = false;
            combo2select = false;
            combo1select = false;
            panel1.Visible = true;
            panel2.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button4.Visible = false;
            update.Visible = false;


            ویرایشخریدارToolStripMenuItem.Enabled = true;
            حذفخریدارToolStripMenuItem.Enabled = true;

            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;

            //comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\nopic.jpg";
            dataGridView1.DataSource = "";
      
            //***********************************************************************************************
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info";
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
          for(int d=0 ;d<dt.Rows.Count;d++)
           {
            comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
            comboBox2.Items.Add(dt.Rows[d]["نام"]);
            comboBox3.Items.Add(dt.Rows[d]["نام_خانوادگی"]);
          
             
          }

            }

        private void ویرایشخریدارToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button4.Visible = true;
            button2.Visible = true;
            update.Visible = true;

            comboBox1.Enabled = false;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox5.Enabled = true;
            richTextBox1.Enabled = true;
            richTextBox2.Enabled = true;

            


        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            pictureBox1.ImageLocation = Application.StartupPath + "\\pic\\nopic.jpg";
            if (picopen) { pictureBox1.Image.Save(Application.StartupPath + "\\customerpic\\" + textBox1.Text + ".jpg"); }
            dataGridView1.DataSource = "";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            combo1select = true;
            conn.Close();
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info where کد_خریدار='" + comboBox1.Text + "' ";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
              
            da.Fill(dt);
            comboBox2.Text = dt.Rows[0]["نام"].ToString();
            comboBox2.Enabled = false;
            comboBox3.Text = dt.Rows[0]["نام_خانوادگی"].ToString();
            comboBox3.Enabled = false;
            textBox4.Text = dt.Rows[0]["تلفن"].ToString();
            textBox4.Enabled = false;
            textBox5.Text = dt.Rows[0]["نام_فروشگاه"].ToString();
            textBox5.Enabled = false;
            richTextBox1.Text = dt.Rows[0]["آدرس"].ToString();
            richTextBox1.Enabled = false;
            richTextBox2.Text = dt.Rows[0]["توضیحات"].ToString();
            richTextBox2.Enabled = false;
            pictureBox1.ImageLocation = dt.Rows[0]["عکس"].ToString();
            conn.Close();

            conn.Open();
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Faktor where کد_خریدار='" + comboBox1.Text + "' "; 
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
     //      DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
     //      btnColumn.HeaderText = "عملیات";
     //      btnColumn.Text = "حذف";
     //      dataGridView1.Columns.Add(btnColumn);
     //      btnColumn.UseColumnTextForButtonValue = true;
            
     }
        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select) { 
            
            OleDbCommand cmd = conn.CreateCommand();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            int c;
            combo2select = true;
            if (combo3select) {
               
                conn.Close();
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
               
                cmd.CommandText = "select * from customer_info where نام_خانوادگی='" + comboBox3.Text + "'and نام='" + comboBox2.Text + "' ";
                cmd.ExecuteNonQuery();
                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
              
                c = comboBox1.Items.Count;

                for (int d = 0; d < c; ++d)
                {
                    comboBox1.Items.RemoveAt(0);
                }
                for (int d = 0; d < dt.Rows.Count; d++)
                {
                    comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
                }


                if (combo3select == false)
                {

                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نام_خانوادگی"]);
                    }


                    conn.Close();

                }
            }
            else { 
                conn.Close();
                conn.Open();
                 cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from customer_info where نام='" + comboBox2.Text + "' ";
                cmd.ExecuteNonQuery();
                 dt = new DataTable();
                 da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                 c = comboBox1.Items.Count;
                
                for (int d = 0; d < c; ++d)
                {
                    comboBox1.Items.RemoveAt(0);
                }
                for (int d = 0; d < dt.Rows.Count; d++)
                {
                    comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
                }


                if (combo3select==false)
                {
                    
                    c = comboBox3.Items.Count;
                    for (int d = 0; d < c; ++d)
                    {
                        comboBox3.Items.RemoveAt(0);
                    }
                    for (int d = 0; d < dt.Rows.Count; d++)
                    {
                        comboBox3.Items.Add(dt.Rows[d]["نام_خانوادگی"]);
                    }
                

                    conn.Close();
                     }

                }

}
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select) { 
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            int c;
            combo3select = true;
            if(combo2select){
                 conn.Close();
            conn.Open();
           
            cmd.CommandText = "select * from customer_info where نام_خانوادگی='" + comboBox3.Text + "'and نام='" + comboBox2.Text + "' ";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
           
             c = comboBox1.Items.Count;

            for (int d = 0; d < c; ++d)
            {

                comboBox1.Items.RemoveAt(0);
            }
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
            }

            if (!combo2select) {
            c = comboBox2.Items.Count;
            
            for (int d = 0; d < c; ++d)
            {
                comboBox2.Items.RemoveAt(0);
            }
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox2.Items.Add(dt.Rows[d]["نام"]);
            }
            }

            conn.Close();

            }
            else { 
            conn.Close();
            conn.Open();
            
            cmd.CommandText = "select * from customer_info where نام_خانوادگی='" + comboBox3.Text + "' ";
            cmd.ExecuteNonQuery();
            dt = new DataTable();
            da = new OleDbDataAdapter(cmd);
            da.Fill(dt);

             c = comboBox1.Items.Count;

            for (int d = 0; d < c; ++d)
            {

                comboBox1.Items.RemoveAt(0);
            }
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
            }

            if (!combo2select) {
            c = comboBox2.Items.Count;
            
            for (int d = 0; d < c; ++d)
            {
                comboBox2.Items.RemoveAt(0);
            }
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox2.Items.Add(dt.Rows[d]["نام"]);
            }
            }

            conn.Close();
            }
            }
        }

        private void مشاهدخلیستخریدارانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            costumerlist_from f = new costumerlist_from();
            f.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          //  MessageBox.Show((e.ColumnIndex).ToString());
          //  MessageBox.Show((e.RowIndex).ToString());
            //MessageBox.Show((dataGridView1.CurrentRow.Index).ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

 

 
        

      
    
      
    }
}
