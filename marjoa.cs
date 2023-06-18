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
    public partial class marjoa : Form
    {
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        Financial_from f;
        int kol;
        int kolm;
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        public int en;
        int code;
        public int codefaktor = 1;
       public int counter;
       DataTable dt;
        public marjoa(Financial_from fin)
        {
            InitializeComponent();
            f = fin;
        }
         public void DataGrid1()
      {
          int faknum = Convert.ToInt32(f.dt1.Rows[f.en]["کد_سبد"]);

          try
          {
              dataGridView1.Columns.Remove("btn1");
              //dataGridView1.Columns.Remove("btn2");
          }
          catch(Exception e)
          {
            
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
              dataGridView1.Columns["نام_قطعه"].Width = 160;
              dataGridView1.Columns.Remove("کد_سبد");
              dataGridView1.RowHeadersVisible = false;

            

              DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
              btnColumn.HeaderText = "";
              btnColumn.Text =  "مرجوع";
              btnColumn.Name = "btn1";
              btnColumn.Width = 40;
              dataGridView1.Columns.Add(btnColumn);
              btnColumn.UseColumnTextForButtonValue = true;

            //  btnColumn = new DataGridViewButtonColumn();
            //  btnColumn.HeaderText = "";
            //  btnColumn.Text = "حذف";
            //  btnColumn.Name = "btn2";
            //  btnColumn.Width = 40;
            //  dataGridView1.Columns.Add(btnColumn);
            //  btnColumn.UseColumnTextForButtonValue = true;
          }
          catch(Exception e)
          {
              MessageBox.Show(e.GetType().ToString());
          }
      }
         public void DataGrid2()
         {
             int faknum = codefaktor;

             try
             {
                 dataGridView2.Columns.Remove("btn1");
                 //dataGridView1.Columns.Remove("btn2");
             }
             catch (Exception e)
             {

             }
             try
             {
                 conn.Open();
                 OleDbCommand cmd = conn.CreateCommand();
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = "select * from Cart where کد_سبد=" + faknum;
                 cmd.ExecuteNonQuery();
                 dt3 = new DataTable();
                 OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
                 da2.Fill(dt3);
                 kolm = 0;
                 for (int d = 0; d < dt3.Rows.Count; d++)
                 {
                     int temp = Convert.ToInt32(dt3.Rows[d]["قیمت_کل"]);
                     kolm = kolm + temp;
                 }
                 textBox4.Text = kolm.ToString("0,0");
                 conn.Close();
                 dataGridView2.DataSource = dt3;



             }
             catch (Exception ex)
             {

                 MessageBox.Show("                                    اوخ! به دشواری برخوردیم" + "\n" + "                                 لطفاُ با پشتیبانی تماس بگیرید" + "\n" + "\n" + "\n" + ex.GetType().ToString());
             }
             try
             {

                 dataGridView2.Columns["counter"].HeaderText = "ردیف";
                 dataGridView2.Columns["نام_قطعه"].HeaderText = "نام قطعه";
                 dataGridView2.Columns["کد_قطعه"].HeaderText = "کد قطعه";
                 dataGridView2.Columns["قیمت_کل"].HeaderText = "قیمت کل";
                 dataGridView2.Columns["counter"].Width = 40;
                 dataGridView2.Columns["تعداد"].Width = 50;
                 dataGridView2.Columns["کد_قطعه"].Width = 70;
                 dataGridView2.Columns["قیمت"].Width = 80;
                 dataGridView2.Columns["نام_قطعه"].Width = 160;
                 dataGridView2.Columns.Remove("کد_سبد");
                 dataGridView2.RowHeadersVisible = false;
                        


                 DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                 btnColumn.HeaderText = "";
                 btnColumn.Text = "حذف";
                 btnColumn.Name = "btn1";
                 btnColumn.Width = 40;
                 dataGridView2.Columns.Add(btnColumn);
                 btnColumn.UseColumnTextForButtonValue = true;

                 //  btnColumn = new DataGridViewButtonColumn();
                 //  btnColumn.HeaderText = "";
                 //  btnColumn.Text = "حذف";
                 //  btnColumn.Name = "btn2";
                 //  btnColumn.Width = 40;
                 //  dataGridView1.Columns.Add(btnColumn);
                 //  btnColumn.UseColumnTextForButtonValue = true;
             }
             catch (Exception e)
             {
                 MessageBox.Show(e.GetType().ToString());
             }
         }

         private void marjoa_Load(object sender, EventArgs e)
         {
             dateTimeSelector1.Format = Atf.UI.DateTimeSelectorFormat.Custom;
             dateTimeSelector1.CustomFormat = "yyyy/MM/dd";
             dateTimeSelector2.Format = Atf.UI.DateTimeSelectorFormat.Custom;
             dateTimeSelector2.CustomFormat = "yyyy/MM/dd";
             dateTimeSelector2.Value = DateTime.Now;
             textBox1.Text = f.dt1.Rows[f.en]["کد_فاکتور"].ToString();
             textBox2.Text = f.dt1.Rows[f.en]["کد_خریدار"].ToString();
             textBox3.Text = f.dt1.Rows[f.en]["نام_خریدار"].ToString();
             dateTimeSelector1.Text = f.dt1.Rows[f.en]["تاریخ"].ToString();
             DataGrid1();

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
                 codefaktor = 1;
             }
             else
             {

                 codefaktor = Convert.ToInt32(dt1.Rows[dt1.Rows.Count - 1]["کد_فاکتور"]) + 1;
             }

             cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = "select * from cart where کد_سبد=" + codefaktor;
             cmd.ExecuteNonQuery();
             da = new OleDbDataAdapter(cmd);
             dt = new DataTable();
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

         
            // dataGridView2.DataSource = dt;
          
             conn.Close();
             DataGrid2();
             textBox6.Text = codefaktor.ToString();
         //   this.dataGridView2.Columns.Add("ردیف", "ردیف");
         //   this.dataGridView2.Columns.Add("کد قطعه", "کد قطعه");
         //   this.dataGridView2.Columns.Add("نام قطعه", "نام قطعه");
         //   this.dataGridView2.Columns.Add("تعداد", "تعداد");
         //   this.dataGridView2.Columns.Add("قیمت", "قیمت");
         //   this.dataGridView2.Columns.Add("قیمت کل", "قیمت کل");
            // this.dataGridView2.Columns.Add("قیمت کل", "قیمت کل");



         }

         private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 6)
             {
                 en = e.RowIndex;
                 new merjoatoDB(this).Show();
                // dataGridView2.Rows.Add(dataGridView1.Rows[e.RowIndex]);
                 //dataGridView2.Rows.Add((from cell in dataGridView1.CurrentRow.Cells.Cast<DataGridViewCell>()  select cell.Value).ToArray());
             }
         }

         private void marjoa_Activated(object sender, EventArgs e)
         {
             DataGrid2();
          
         }

         private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
         {
             if (e.ColumnIndex == 6)
             {
                 conn.Open();
                 OleDbCommand cmd = conn.CreateCommand();
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = "delete from Cart where کد_قطعه='" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'and کد_سبد=" + codefaktor;
                 cmd.ExecuteNonQuery();

                 cmd = conn.CreateCommand();
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = "select * from Products where کد_قطعه=" + Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value);
                 cmd.ExecuteNonQuery();
                 DataTable dtcount = new DataTable();
                 OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                 da.Fill(dtcount);
          
                 int count1 = Convert.ToInt32(dtcount.Rows[0]["تعداد"]);
                 int count = count1 + Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[3].Value);
                 dataGridView1.Rows[en].Cells[3].Value = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[3].Value)+Convert.ToInt32( dataGridView1.Rows[en].Cells[3].Value);
                 cmd = conn.CreateCommand();
                 cmd.CommandType = CommandType.Text;
                 cmd.CommandText = "update Products set تعداد=" + count + " where کد_قطعه=" + Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value)  ;
                 cmd.ExecuteNonQuery();
                 conn.Close();
                 DataGrid2();
             }
         }

         private void button1_Click(object sender, EventArgs e)
         {
             conn.Open();
             OleDbCommand cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.Text;
             string time = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString();
             cmd.CommandText = "insert into faktor(تاریخ,کد_خریدار,نام_خریدار,نحوه_پرداخت,ساعت,کد_فاکتور,کد_سبد,قیمت_کل,وضعیت_فاکتور,کاربر)  values( '" + dateTimeSelector2.Text + "' ,'" + textBox2.Text + "','" + textBox3.Text +"','" + "" + "','" + time + "','" + textBox6.Text + "','" + codefaktor + "','" + kolm.ToString() + "','" + "فاکتور مرجوعی" + "','" + Login.User + "'  )";
             cmd.ExecuteNonQuery();

             cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = "select * from customer_info where کد_خریدار='" + textBox2.Text + "' ";
             cmd.ExecuteNonQuery();
             DataTable dt4 = new DataTable();
             OleDbDataAdapter da2 = new OleDbDataAdapter(cmd);
             da2.Fill(dt4);

             cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = "select * from payment where کد_خریدار='" + textBox2.Text + "' ";
             cmd.ExecuteNonQuery();
             DataTable dtp = new DataTable();
             OleDbDataAdapter da1 = new OleDbDataAdapter(cmd);
             da1.Fill(dtp);
             int mande = 0;
             if (dtp.Rows.Count != 0)
             {
                 mande = Convert.ToInt32(dtp.Rows[dtp.Rows.Count - 1]["مانده"]);
             }

             mande =mande- kolm;

             cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = "INSERT INTO `payment` (`کد_خریدار`, `تاریخ`, `ساعت`,`بدهکار`,  `بستانکار`, `مانده`, `نحوه پرداخت`, `توضیحات`, `کاربر`) VALUES ('" + textBox2.Text + "','" + dateTimeSelector2.Text + "','" + time + "','" + "0" + "','" + kolm.ToString() + "','" + mande + "','" + "  صدور فاکتور به شماره " + textBox6.Text + "','" + "مرجوعی فاکتور بشماره" + textBox1.Text + "','" + Login.User + "'  )";
             cmd.ExecuteNonQuery();
             conn.Close();

             StiReport report = new StiReport();
             report.Load(Application.StartupPath + "\\faktor_m.mrt");
             report.Dictionary.Variables["faktornum"].Value = codefaktor.ToString();
             report.Dictionary.Variables["kol"].Value = kolm.ToString("0,0");
             report.Dictionary.Variables["faktordate"].Value = dateTimeSelector2.Text;
             report.RegBusinessObject("customer", dt4);
             report.RegBusinessObject("products", dt3);
             report.Render();
             report.ExportDocument(StiExportFormat.Pdf, "Report.pdf");
             report.Show();
             


         }
    }
}
