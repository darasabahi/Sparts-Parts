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
    public partial class Faktor_form : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        Financial_from f;
        int kol;
        DataTable dt2 = new DataTable();
        int code;
        int en;
        int bedhkar;
        public Faktor_form(Financial_from fin)
        {
            InitializeComponent();
            f = fin;
        }
      public void DataGrid()
      {
          int faknum = Convert.ToInt32(f.dt1.Rows[f.en]["کد_سبد"]);

          try
          {
              dataGridView1.Columns.Remove("btn1");
              dataGridView1.Columns.Remove("btn2");
          }
          catch(Exception e)
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
              dt2 = new DataTable();
              OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
              da1.Fill(dt2);
              kol = 0;
              for (int d = 0; d < dt2.Rows.Count; d++)
              {
                  int temp = Convert.ToInt32(dt2.Rows[d]["قیمت_کل"]);
                  kol = kol + temp;
              }
              textBox5.Text = kol.ToString("0,0");
              conn.Close();
              dataGridView1.DataSource = dt2;
             


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

            //  DataGridViewCheckBoxColumn ch = new DataGridViewCheckBoxColumn();
            //  dataGridView1.Columns.Add(ch);
            //
            //  DataGridViewTextBoxColumn tex = new DataGridViewTextBoxColumn();
            //  tex.Width = 40;
            //  tex.Name = "tex1";
            //  tex.Dispose();
            //  dataGridView1.Columns.Add(tex);

              DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
              btnColumn.HeaderText = "";
              btnColumn.Text =  "تغییر";
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
          catch(Exception e)
          {
              MessageBox.Show(e.GetType().ToString());
          }
      }
        private void Faktor_form_Load(object sender, EventArgs e)
        {
            textBox1.Text= f.dt1.Rows[f.en]["کد_فاکتور"].ToString();
            textBox2.Text = f.dt1.Rows[f.en]["کد_خریدار"].ToString();
            textBox3.Text = f.dt1.Rows[f.en]["نام_خریدار"].ToString();
            dateTimeSelector1.Text = f.dt1.Rows[f.en]["تاریخ"].ToString();
            DataGrid();
            dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
            dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
         
          
          //  textBox1.Text = f.dt1.Rows[f.en]["تلفن"].ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                groupBox2.Visible = true;
                textBox6.Text = dt2.Rows[e.RowIndex]["نام_قطعه"].ToString();
                textBox4.Text = dt2.Rows[e.RowIndex]["قیمت"].ToString();
                numericUpDown1.Value = Convert.ToInt32(dt2.Rows[e.RowIndex]["تعداد"]);
                code = Convert.ToInt32(e.RowIndex);

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

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            conn.Open();
            int price = Convert.ToInt32(textBox4.Text);
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Cart set قیمت=" +price + ",تعداد="+numericUpDown1.Value+",قیمت_کل="+price*numericUpDown1.Value+" where کد_قطعه='" + dt2.Rows[code]["کد_قطعه"].ToString() +"' and کد_سبد="+Convert.ToInt32(dt2.Rows[code]["کد_سبد"]);
            cmd.ExecuteNonQuery();
            conn.Close();
            DataGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
            string datefak = dateTimeSelector1.Text;
            dateTimeSelector1.Value = DateTime.Now;
            string date = dateTimeSelector1.Text;
            conn.Open();

            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from payment where  کد_خریدار='" + textBox2.Text + "'and ساعت='" + f.dt1.Rows[f.en]["ساعت"].ToString() + "' ";
            cmd.ExecuteNonQuery();
            DataTable dtmande = new DataTable();
            OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
            da1.Fill(dtmande);
            //MessageBox.Show(dt2.Rows.Count.ToString());
            int mande = 0;
            if (dtmande.Rows.Count != 0)
            {
                mande = Convert.ToInt32(dtmande.Rows[dtmande.Rows.Count - 1]["مانده"]);
                 bedhkar = Convert.ToInt32(dtmande.Rows[dtmande.Rows.Count - 1]["بدهکار"]);
            }
            bedhkar = bedhkar - kol;
            mande = mande + bedhkar;
           cmd = conn.CreateCommand();
           cmd.CommandType = CommandType.Text;
           cmd.CommandText = "update `payment` set مانده=" + mande + ",بدهکار=" + kol + ",توضیحات='" + "اصلاح شده" + "',ساعت='" + time + "',کاربر='" + Login.User + "' where  کد_خریدار='" + textBox2.Text + "'and ساعت='" + f.dt1.Rows[f.en]["ساعت"].ToString() + "' ";
           cmd.ExecuteNonQuery();
           conn.Close();
           
           // this.Close();
        }
    }
}
