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

namespace spare_parts
{
    public partial class Financial_from : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        DataTable dt = new DataTable();
        public DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtcombo = new DataTable();
        DataTable dtname = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dtmande = new DataTable();
        BindingSource BindingSource1 = new BindingSource();
        BindingSource BindingSource2 = new BindingSource();
        BindingSource BindingSource3 = new BindingSource();
        BindingSource BindingSource4 = new BindingSource();
        public int en;
        public string fakdate;
        public string fakname;
        public string fakphone;
        int mande;
        Faktor_form f;
        string costumername;
        Int64 price;
        public Financial_from( )
        {
            InitializeComponent();
          
        }
        public Financial_from(Faktor_form fak)
        {
            InitializeComponent();
            f = fak;
        }
        public void DataGrid1()
        {
            try
            {
                dataGridView1.Columns.Remove("btn1");
               // dataGridView1.Columns.Remove("btn2");
            }
            catch
            {

            }
           conn.Open();
           OleDbCommand cmd = conn.CreateCommand();
           cmd.CommandType = CommandType.Text;
           cmd.CommandText = "select * from chek";
           cmd.ExecuteNonQuery();
           dt = new DataTable();
           OleDbDataAdapter da = new OleDbDataAdapter(cmd);
           da.Fill(dt);
           conn.Close();
           //**************************************************************
           BindingSource1.DataSource = dt;
           dataGridView1.DataSource = BindingSource1;
           DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
           btnColumn.HeaderText = "عملیات";
           btnColumn.Text = "وصول شد";
           btnColumn.Name = "btn1";
           dataGridView1.Columns.Add(btnColumn);
           btnColumn.UseColumnTextForButtonValue = true;
          // dataGridView1.Columns.Remove("ID");
           dataGridView1.RowHeadersVisible = false;

         //**************************************************************
           dataGridView1.ClearSelection();
           int nRowIndex = dataGridView1.Rows.Count - 2;
           try
           {
               dataGridView1.Rows[nRowIndex].Selected = true;
               dataGridView1.Rows[nRowIndex].Cells[0].Selected = true;
           }
           catch
           {

           }


        }
        public void addbtnDataGrid2() 
        {
            try
            {
                dataGridView2.Columns.Remove("btn1");
                dataGridView2.Columns.Remove("btn2");
                dataGridView2.Columns.Remove("btn3");
            }
            catch
            {

            }
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "تغییر وضعیت";
            btnColumn.Text = "پرداخت شد";
            btnColumn.Name = "btn1";
            dataGridView2.Columns.Add(btnColumn);
            btnColumn.UseColumnTextForButtonValue = true;

            btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "عملیات";
            btnColumn.Text = "مشاهده فاکتور";
            btnColumn.Name = "btn2";
            dataGridView2.Columns.Add(btnColumn);
            btnColumn.UseColumnTextForButtonValue = true;

            btnColumn = new DataGridViewButtonColumn();
            btnColumn.HeaderText = "فاکتور مرجوعی";
            btnColumn.Text = "مرجوع فاکتور";
            btnColumn.Name = "btn3";
            dataGridView2.Columns.Add(btnColumn);
            btnColumn.UseColumnTextForButtonValue = true;
           
        }


        public void DataGrid2()
        {
            dataGridView2.DataSource = "";
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from faktor";
            cmd.ExecuteNonQuery();
            dt1 = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt1);
            conn.Close();
            //**************************************************************
            BindingSource2.DataSource = dt1;
            dataGridView2.DataSource = BindingSource2;
            dataGridView2.RowHeadersVisible = false;

            //**************************************************************
            dataGridView2.ClearSelection();
            int nRowIndex = dataGridView2.Rows.Count - 2;
            try
            {
                dataGridView2.Rows[nRowIndex].Selected = true;
                dataGridView2.Rows[nRowIndex].Cells[0].Selected = true;
            }
            catch
            {

            }

        }
        public void DataGrid3()
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from payment";// Order By تاریخ DESC";
           
            cmd.ExecuteNonQuery();
            dt2 = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt2);
            conn.Close();
            //**************************************************************
            BindingSource3.DataSource = dt2;
           
            dataGridView3.DataSource = BindingSource3;
            dataGridView3.Columns.Remove("کد_خریدار");
            dataGridView3.Columns["نحوه پرداخت"].HeaderText = "شرح";
            dataGridView3.Columns["نحوه پرداخت"].Width = 300;
            dataGridView3.Columns["شماره سند"].Width = 70;
            dataGridView3.RowHeadersVisible=false;
            dataGridView3.ClearSelection();
            int nRowIndex = dataGridView3.Rows.Count - 2;
            try
            {
                dataGridView3.Rows[nRowIndex].Selected = true;
                dataGridView3.Rows[nRowIndex].Cells[0].Selected = true;
            }
            catch
            {

            }
            if (comboBox6.Text != "")
            {
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from payment where  کد_خریدار='" + comboBox6.Text + "' ";
                cmd.ExecuteNonQuery();
                DataTable dtmande = new DataTable();
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                da1.Fill(dtmande);
                //MessageBox.Show(dt2.Rows.Count.ToString());
                 mande = 0;
                if (dtmande.Rows.Count != 0)
                {
                    mande = Convert.ToInt32(dtmande.Rows[dtmande.Rows.Count - 1]["مانده"]);
                }
                textBox3.Text = mande.ToString("0,0");
                if (mande > 0)
                {
                    label9.Text = "بدهکار";
                }
                else if (mande == 0)
                {
                    label9.Text = "تسویه حساب";
                }
                else
                {
                    label9.Text = "بستانکار";
                }
                conn.Close();

            }
           
        }
        public void combobox()
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info";
            cmd.ExecuteNonQuery();
            dtcombo= new DataTable();
            dtname = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dtcombo);
            da.Fill(dtname);
            conn.Close();
            BindingSource4.DataSource = dtname;
          

        }
        private void Financial_from_Load(object sender, EventArgs e)
        {
            paymentpanel.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            dateTimeSelector1.Visible = false;
            groupBox2.Visible = false;
            dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector2.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector2.CustomFormat = "yyyy/MM/dd";
            DataGrid2();
            addbtnDataGrid2();
            DataGrid1();
            DataGrid3();
            combobox();
            for (int d = 0; d < dtcombo.Rows.Count; d++)
            {
                comboBox1.Items.Add(dtcombo.Rows[d]["کد_خریدار"].ToString());
            }
            for (int d = 0; d < dtcombo.Rows.Count; d++)
            {
                comboBox6.Items.Add(dtcombo.Rows[d]["کد_خریدار"].ToString());
            }
       
           
        }
        private void مدریتچکهاToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            paymentpanel.Visible = false;
            groupBox2.Text = "جستجوی چک";
            label1.Visible = true;
            label2.Visible = true;
            label4.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            label3.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            dateTimeSelector2.Visible = false;
            dateTimeSelector1.Visible = true;
            groupBox2.Visible = true;
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
        //    DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
        //    cmb.HeaderText = "Select Data";
        //    cmb.Name = "cmb";
        //    cmb.MaxDropDownItems = 4;
        //    cmb.Items.Add("True");
        //    cmb.Items.Add("False");
        //    dataGridView1.Columns.Add(cmb);
        }

        private void مدریتفاکتورToolStripMenuItem_Click(object sender, EventArgs e)
        {
            paymentpanel.Visible = false;
            panel1.Visible = true;
            groupBox2.Text = "جستجوی فاکتور";
            label3.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
            comboBox5.Visible = true;
            dateTimeSelector2.Visible= true;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            dateTimeSelector1.Visible = false;
            groupBox2.Visible = true;
            dataGridView2.Visible = true;
            dataGridView1.Visible = false;
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            BindingSource1.Filter = string.Format("کد_خریدار LIKE '%{0}%'", comboBox1.Text);
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            BindingSource1.Filter = string.Format("کد_خریدار LIKE '%{0}%'", comboBox1.Text);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingSource1.Filter = string.Format("وضعیت LIKE '%{0}%'", comboBox2.Text);
           
        }

        private void dateTimeSelector1_ValueChanged(object sender, EventArgs e)
        {
            string date = dateTimeSelector1.Text;
            BindingSource1.Filter = string.Format("تاریخ_چک LIKE '%{0}%'", date);
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            int code;
            int.TryParse(comboBox5.Text, out code);
            DataView dv = new DataView(dt1);
            dv.RowFilter = "کد_فاکتور='" + code + "'";
            dataGridView2.DataSource = dv;
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            int code;
            int.TryParse(comboBox5.Text, out code);
            DataView dv = new DataView(dt1);
            dv.RowFilter = "کد_فاکتور='"+code+"'";
            dataGridView2.DataSource = dv;
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            BindingSource2.Filter = string.Format("کد_خریدار LIKE '%{0}%'", comboBox4.Text);
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            BindingSource2.Filter = string.Format("کد_خریدار LIKE '%{0}%'", comboBox4.Text);
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            BindingSource2.Filter = string.Format("وضعیت_فاکتور LIKE '%{0}%'", comboBox3.Text);
        }

        private void dateTimeSelector2_ValueChanged(object sender, EventArgs e)
        {
            string date = dateTimeSelector2.Text;
            BindingSource2.Filter = string.Format("تاریخ LIKE '%{0}%'", date);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindingSource2.Filter = string.Format("وضعیت_فاکتور LIKE '%{0}%'", comboBox3.Text);
        }

        private void ثبتچکجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chek_form2 f = new chek_form2();
            f.Show();
        }
        private void مدیریتحسابافرادToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGrid3();
            dataGridView3.Visible = true;
            panel1.Visible = false;
            paymentpanel.Visible = true;
        }

        private void comboBox6_SelectedValueChanged(object sender, EventArgs e)
        {
            DataGrid3();
            dataGridView3.Visible = true;
            BindingSource3.Filter = string.Format("کد_خریدار LIKE '%{0}%'", comboBox6.Text);
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
           
            string s = "صادرشده";
            try
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from faktor where  کد_خریدار='" + comboBox6.Text + "' ";
                cmd.ExecuteNonQuery();
                
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt4);

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from payment where  کد_خریدار='" + comboBox6.Text +"' ";
                cmd.ExecuteNonQuery();
                dtmande.Clear();
                OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                da1.Fill(dtmande);
                //MessageBox.Show(dt2.Rows.Count.ToString());
                int mande = 0;
                if (dtmande.Rows.Count != 0)
                {
                    mande = Convert.ToInt32(dtmande.Rows[dtmande.Rows.Count - 1]["مانده"]);
                }
                textBox3.Text = mande.ToString("0,0");
                if (mande > 0)
                {
                    label9.Text = "بدهکار";
                }
                else if (mande == 0)
                {
                    label9.Text = "تسویه حساب";
                }
                else
                {
                    label9.Text = "بستانکار";
                }

                dt6 = new DataTable();
                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from customer_info where  کد_خریدار='" + comboBox6.Text + "' ";
                cmd.ExecuteNonQuery();

                da = new OleDbDataAdapter(cmd);
                da.Fill(dt6);

                conn.Close();
            }
            catch(Exception ex)
            {
                conn.Close();
                MessageBox.Show(ex.ToString());
                

            }
             textBox4.Text = dt6.Rows[0]["نام"].ToString() + " " + dt6.Rows[0]["نام_خانوادگی"].ToString();
            int kol = 0;
            for (int d = 0; d < dt4.Rows.Count; d++)
            {
                
                int temp = Convert.ToInt32(dt4.Rows[d]["قیمت_کل"]);
                kol  += temp;
            }
            textBox1.Text = kol.ToString("0,0");
            Int64 paykol = 0;
            for (int d = 0; d < dt5.Rows.Count; d++)
            {
                Int64 temp = Convert.ToInt64(dt5.Rows[d]["بستانکار"]);
                paykol += temp;
            }
            textBox2.Text = paykol.ToString("0,0");

          
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
           // Int64.TryParse(textBox2.Text, out price);
           // textBox2.Text = price.ToString("0,0");
        }

        private void پرداختToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payment f = new payment();
            f.Show();
        }

        private void Financial_from_Activated(object sender, EventArgs e)
        {
            DataGrid1();
            DataGrid2();
            addbtnDataGrid2();
            DataGrid3();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimeSelector3.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector3.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector3.Value = DateTime.Now;
           string date = dateTimeSelector3.Text;
          // MessageBox.Show(e.ColumnIndex.ToString());
            if (e.ColumnIndex == 10) {
                conn.Open();
                string s = "پرداخت شده";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update faktor set وضعیت_فاکتور='" + s + "' where کد_فاکتور=" + Convert.ToInt32(dt1.Rows[e.RowIndex]["کد_فاکتور"]);
                cmd.ExecuteNonQuery();
                conn.Close();
                //MessageBox.Show("فاکتور شماره"+(e.RowIndex+1).ToString()+"پرداخت شد");
                DataGrid2();
                addbtnDataGrid2();
                try
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from payment where کد_خریدار='" + dt1.Rows[e.RowIndex]["کد_خریدار"].ToString() + "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dt2);
                    //MessageBox.Show(dt2.Rows.Count.ToString());
                    int mande = 0;
                    if (dt2.Rows.Count != 0)
                    {
                        mande = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["مانده"]);
                    }

                    mande = mande - Convert.ToInt32(dt1.Rows[e.RowIndex]["قیمت_کل"]);

                    string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
                 
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO `payment` (`کد_خریدار`, `تاریخ`, `ساعت`,`بدهکار`,  `بستانکار`, `مانده`, `نحوه پرداخت`, `توضیحات`, `کاربر`) VALUES ('" + dt1.Rows[e.RowIndex]["کد_خریدار"].ToString() + "','" + date + "','" + time + "','" + "0" + "','" + dt1.Rows[e.RowIndex]["قیمت_کل"].ToString() + "','" + mande + "','" + "پرداخت فاکتورشماره" + dt1.Rows[e.RowIndex]["کد_فاکتور"] + "','" + "" + "','" + Login.User + "'  )";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                   
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex);
                }
             //MessageBox.Show((e.RowIndex).ToString());
             }
            if (e.ColumnIndex == 11)
            {

                en = e.RowIndex;
                new Faktor_form(this).Show();
                
            }
            if (e.ColumnIndex == 12)
            {

                en = e.RowIndex;
                new marjoa(this).Show();

            }
         
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dateTimeSelector3.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector3.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector3.Value = DateTime.Now;
            string date = dateTimeSelector3.Text;
            // MessageBox.Show(e.ColumnIndex.ToString());
            if (e.ColumnIndex == 12)
            {
                conn.Open();
                string s = "وصول شده";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update chek set وضعیت='" + s + "' WHERE (`شماره سند چک` = "+Convert.ToInt32(dt.Rows[e.RowIndex]["شماره سند چک"])+")";
                cmd.ExecuteNonQuery();
                conn.Close();
                //MessageBox.Show("فاکتور شماره"+(e.RowIndex+1).ToString()+"پرداخت شد");
                DataGrid1();
                
                try
                {
                    conn.Open();
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from payment where کد_خریدار='" + dt1.Rows[e.RowIndex]["کد_خریدار"].ToString() + "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt2 = new DataTable();
                    OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
                    da1.Fill(dt2);
                    //MessageBox.Show(dt2.Rows.Count.ToString());
                 
                    int mande = 0;
                    if (dt2.Rows.Count != 0)
                    {
                        mande = Convert.ToInt32(dt2.Rows[dt2.Rows.Count - 1]["مانده"]);
                    }

                    mande = mande - Convert.ToInt32(dt.Rows[e.RowIndex]["مبلغ"]);

                    string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO `payment` (`کد_خریدار`, `تاریخ`, `ساعت`,`بدهکار`,  `بستانکار`, `مانده`, `نحوه پرداخت`, `توضیحات`, `کاربر`) VALUES ('" + dt1.Rows[e.RowIndex]["کد_خریدار"].ToString() + "','" + date + "','" + time + "','" + "0" + "','" + dt.Rows[e.RowIndex]["مبلغ"].ToString() + "','" + mande + "','" + " وصول چک به شماره" + dt.Rows[e.RowIndex]["شماره سند چک"] +"مورخ"+ dt.Rows[e.RowIndex]["تاریخ_چک"] + "به مبلغ" + dt.Rows[e.RowIndex]["مبلغ"] + "','" + "" + "','" + Login.User + "'  )";
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
                catch (Exception ex)
                {
                 
                    MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex.GetType().ToString());
                }
                //MessageBox.Show((e.RowIndex).ToString());
            }
            if (e.ColumnIndex == 13)
            {
                MessageBox.Show("بزودی این قسمت فعال خواهد شد");
            }
         
        }

        private void dataGridView3_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
        }

        private void چاپکارتحسابToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dateTimeSelector3.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector3.CustomFormat = "yyyy/MM/dd";
            dateTimeSelector3.Value = DateTime.Now;
            string date = dateTimeSelector3.Text;

            StiReport report = new StiReport();
            report.Load(Application.StartupPath + "\\kartehesab.mrt");
            report.Dictionary.Variables["kol"].Value = mande.ToString("0,0");
            report.Dictionary.Variables["faktordate"].Value = date;
            report.Dictionary.Variables["vazeyat"].Value = label9.Text;
            report.RegBusinessObject("customer", dt6);
            report.RegBusinessObject("peyment", dtmande);
            report.Render();
            report.ExportDocument(StiExportFormat.Pdf, "Report-hesab.pdf");
            report.Show();
        }

   

  



     

     

      
    }
}
