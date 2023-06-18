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
using Stimulsoft.Report;
using System.IO;

namespace spare_parts
{
    public partial class faktor : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        Boolean fill = false;
        Boolean combo1select = false;
        Boolean combo2select = false;
        Boolean combo3select = false;
          int code1;
        Form1 f1;
      
        int cartnum;
       public int kol = 0;
        string code;
        public faktor(Form1 f2)
        {
            InitializeComponent();
            f1 = f2;
        }
        
        private void faktor_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
         dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
       dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector1.Value = DateTime.Now;
      
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info";
            cmd.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
        
            cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from faktor";
            cmd.ExecuteNonQuery();
            da = new OleDbDataAdapter(cmd);
        
            da.Fill(dt3);
            if (dt3.Rows.Count == 0) {
                code = "1";
            } 
            else {
                
                code = (Convert.ToInt32(dt3.Rows[dt3.Rows.Count - 1]["کد_فاکتور"]) + 1).ToString();
            }
            textBox1.Text = code;
            cartnum = Convert.ToInt32(code);
            for (int d = 0; d < dt.Rows.Count; d++)
            {
                comboBox1.Items.Add(dt.Rows[d]["کد_خریدار"]);
                comboBox2.Items.Add(dt.Rows[d]["نام"]);
                comboBox3.Items.Add(dt.Rows[d]["نام_خانوادگی"]);
            }

        
            conn.Close();
           
            for (int d = 0; d < dt1.Rows.Count; d++) {
                int temp = Convert.ToInt32(dt1.Rows[d]["قیمت_کل"]);
                 kol = kol + temp;
            }
            DataGrid();
           
        }
        public void DataGrid()
        {
            int faknum = cartnum;

            try
            {
                dataGridView1.Columns.Remove("btn1");
                dataGridView1.Columns.Remove("btn2");
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.GetType().ToString());
            }
            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Cart where کد_سبد=" + faknum;
                cmd.ExecuteNonQuery();
                dt1 = new DataTable();
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                da1.Fill(dt1);
                kol = 0;
                for (int d = 0; d < dt1.Rows.Count; d++)
                {
                    int temp = Convert.ToInt32(dt1.Rows[d]["قیمت_کل"]);
                    kol = kol + temp;
                }
                textBox5.Text = kol.ToString("0,0");
                conn.Close();
                dataGridView1.DataSource = dt1;



            }
            catch (Exception ex)
            {

                MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex.GetType().ToString());
            }
            try
            {

                dataGridView1.Columns["counter"].HeaderText = "ردیف";
                dataGridView1.Columns["نام_قطعه"].HeaderText = "نام قطعه";
                dataGridView1.Columns["کد_قطعه"].HeaderText = "کد قطعه";
                dataGridView1.Columns["قیمت_کل"].HeaderText = "قیمت کل";
                dataGridView1.Columns["counter"].Width = 40;
                dataGridView1.Columns["تعداد"].Width = 50;
                dataGridView1.Columns["کد_قطعه"].Width = 70;
                dataGridView1.Columns["قیمت"].Width = 80;
                dataGridView1.Columns["نام_قطعه"].Width = 120;
                dataGridView1.Columns.Remove("کد_سبد");
                dataGridView1.RowHeadersVisible = false;



                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "";
                btnColumn.Text = "تغییر";
                btnColumn.Name = "btn1";
                btnColumn.Width = 40;
                dataGridView1.Columns.Add(btnColumn);
                btnColumn.UseColumnTextForButtonValue = true;

                btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "";
                btnColumn.Text = "حذف";
                btnColumn.Name = "btn2";
                btnColumn.Width = 40;
                dataGridView1.Columns.Add(btnColumn);
                btnColumn.UseColumnTextForButtonValue = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.GetType().ToString());
            }
        }
        private void dateTimeSelector1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("سبد کالا خالی است");
             }
             else if(!combo1select)
             {
                 MessageBox.Show("لطفا خریدار را انتخاب کنید");
             }
            else if(comboBox4.SelectedIndex==-1){
                MessageBox.Show("نحوه پرداخت را انتخاب کنید");
            }
            else{
              conn.Open();
              OleDbCommand cmd = conn.CreateCommand();
              cmd.CommandType = CommandType.Text;
              string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
              cmd.CommandText = "insert into faktor(تاریخ,کد_خریدار,نام_خریدار,نحوه_پرداخت,ساعت,کد_فاکتور,کد_سبد,قیمت_کل,وضعیت_فاکتور,کاربر)  values( '" + dateTimeSelector1.Text + "' ,'" + comboBox1.Text + "','" + comboBox2.Text + " " + comboBox3.Text + "','" + comboBox4.Text + "','" + time + "','" + textBox1.Text + "','" + cartnum + "','" + kol.ToString() + "','" + "صادرشده" + "','" + Login.User + "'  )";
              cmd.ExecuteNonQuery();
              f1.label1.Text = "0";

              cmd = conn.CreateCommand();
              cmd.CommandType = CommandType.Text;
              cmd.CommandText = "select * from payment where کد_خریدار='" + comboBox1.Text + "' ";
              cmd.ExecuteNonQuery();
              DataTable dt3 = new DataTable();
              OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
              da1.Fill(dt3);
              int mande = 0;
              if (dt3.Rows.Count != 0)
              {
                  mande = Convert.ToInt32(dt3.Rows[dt3.Rows.Count-1]["مانده"]);
              }

              mande += kol;

              cmd = conn.CreateCommand();
              cmd.CommandType = CommandType.Text;
              cmd.CommandText = "INSERT INTO `payment` (`کد_خریدار`, `تاریخ`, `ساعت`,`بدهکار`,  `بستانکار`, `مانده`, `نحوه پرداخت`, `توضیحات`, `کاربر`) VALUES ('" + comboBox1.Text + "','" + dateTimeSelector1.Text + "','" + time + "','" + kol.ToString() + "','" + "0" + "','" + mande + "','" + " صدور فاکتور شماره "+textBox1.Text + "','" + "" + "','" + Login.User + "'  )";
              cmd.ExecuteNonQuery();             
              conn.Close();
              dataGridView1.DataSource = "";
        //    comboBox1.SelectedIndex = -1;
        //    comboBox2.SelectedIndex = -1;
        //    comboBox3.SelectedIndex = -1;
        //    comboBox4.SelectedIndex = -1;
           
            StiReport report = new StiReport();
            report.Load(Application.StartupPath + "\\faktor.mrt");
            report.Dictionary.Variables["faktornum"].Value = code;
            report.Dictionary.Variables["kol"].Value = kol.ToString("0,0");
            report.Dictionary.Variables["faktordate"].Value = dateTimeSelector1.Text;
            report.RegBusinessObject("customer", dt2);
            report.RegBusinessObject("products", dt1);
             report.Render();
             report.ExportDocument(StiExportFormat.Pdf, "Report.pdf");
             report.Show();
         
           }

        }

        private void button4_Click(object sender, EventArgs e)
        {
          
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            combo1select = true;
           
            conn.Close();
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info where کد_خریدار='" + comboBox1.Text + "' ";

            cmd.ExecuteNonQuery();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt2 = new DataTable();
            da.Fill(dt2);
            comboBox2.Text = dt2.Rows[0]["نام"].ToString();
            comboBox3.Text = dt2.Rows[0]["نام_خانوادگی"].ToString();
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
                if (combo3select)
                {

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
                else
                {
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
            }

        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!combo1select)
            {
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                DataTable dt = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                int c;
                combo3select = true;
                if (combo2select)
                {
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

                    if (!combo2select)
                    {
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
                else
                {
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

                    if (!combo2select)
                    {
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

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "چک" ) {
                button2.Enabled = true;
            }
            else if (comboBox4.Text == "ترکیبی")
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("سبد کالا خالی است");
            }
            else if (!combo1select)
            {
                MessageBox.Show("لطفا خریدار را انتخاب کنید");
            }
            else
            {
               new chek_form(this).Show();
               
                
            }
           
        
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                groupBox2.Visible = true;
                textBox6.Text = dt1.Rows[e.RowIndex]["نام_قطعه"].ToString();
                textBox4.Text = dt1.Rows[e.RowIndex]["قیمت"].ToString();
                numericUpDown1.Value = Convert.ToInt32(dt1.Rows[e.RowIndex]["تعداد"]);
                code1 = Convert.ToInt32(e.RowIndex);

            }
            if (e.ColumnIndex == 7)
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Cart where کد_قطعه='" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'and کد_سبد=" + Convert.ToInt32(textBox1.Text);
                cmd.ExecuteNonQuery();

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Products where کد_قطعه=" + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                cmd.ExecuteNonQuery();
                DataTable dtcount = new DataTable();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dtcount);

                int count1 = Convert.ToInt32(dtcount.Rows[0]["تعداد"]);
                int count = count1 + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[3].Value);

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Products set تعداد=" + count + " where کد_قطعه=" + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                cmd.ExecuteNonQuery();
                conn.Close();
                DataGrid();
                f1.label1.Text = (Convert.ToInt32(f1.label1.Text) - 1).ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            conn.Open();
            int price = Convert.ToInt32(textBox4.Text);
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Cart set قیمت=" + price + ",تعداد=" + numericUpDown1.Value + ",قیمت_کل=" + price * numericUpDown1.Value + " where کد_قطعه='" + dt1.Rows[code1]["کد_قطعه"].ToString() + "' and کد_سبد=" + Convert.ToInt32(dt1.Rows[code1]["کد_سبد"]);
            cmd.ExecuteNonQuery();
            conn.Close();
            DataGrid();
        }




    }
}

