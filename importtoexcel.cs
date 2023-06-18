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
    public partial class importtoexcel : Form
    {
        public importtoexcel()
        {
            InitializeComponent();
        }
        OleDbConnection conn1 = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");

        private void import_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) 
            { 
                   label4.Visible = true;
                   openFileDialog1.Filter = "Execl files (*.xlsx *.xls)|*.xlsx;*.xls";
                   openFileDialog1.FilterIndex = 1;
                   openFileDialog1.RestoreDirectory = true;
          
               if (openFileDialog1.ShowDialog() == DialogResult.OK)
               {
                try
                {
                    var fileName = openFileDialog1.FileName;
                    var connectionString = (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                    var conn = new OleDbConnection(connectionString);
                    conn.Open();
                    
                    var cmd = conn.CreateCommand();
                    string sheet = textBox1.Text;
                    cmd.CommandText = "SELECT * FROM [Sheet" + sheet + "$]";
                    var adapter = new OleDbDataAdapter(cmd);
                    var ds = new DataSet();
                    DataTable dt = new DataTable();
                    adapter.Fill(ds);
                    adapter.Fill(dt);

                    conn.Close();
              
                    conn1.Open();
                    int r1 = Convert.ToInt32(textBox2.Text);
                    int r2;
                    int.TryParse(textBox3.Text, out r2);
                    if(textBox3.Text=="تا آخر")
                    {
                        r2=dt.Rows.Count;
                    }
                    if (dt.Columns[0].ColumnName.ToString() == "کد قطعه")
                    {
                        for (int d = r1; d < r2; d++)
                        {
                            OleDbCommand cmd1 = conn1.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "insert into Products(نام_قطعه,شماره_فنی,نوع_خودرو,شرکت_تولید_کننده,قیمت,تعداد,توضیحات) values('" + dt.Rows[d]["نام قطعه"].ToString() + "','" + dt.Rows[d]["شماره فنی"].ToString() + "','" + dt.Rows[d]["نوع خودرو"].ToString() + "','" + dt.Rows[d]["شرکت تولید کننده"].ToString() + "','" + Convert.ToInt32(dt.Rows[d]["قیمت"]) + "','" + Convert.ToInt32(dt.Rows[d]["تعداد"]) + "','" + dt.Rows[d]["توضیحات"].ToString() + "'  )";
                            cmd1.ExecuteNonQuery();
                        }
                        conn1.Close();
                        label4.Visible = false;
                        (this).Close();
                    }
                    else if (dt.Columns[0].ColumnName.ToString() == "کد_قطعه")
                    {
                        for (int d = r1; d < r2; d++)
                        {
                            OleDbCommand cmd1 = conn1.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "insert into Products(نام_قطعه,شماره_فنی,نوع_خودرو,شرکت_تولید_کننده,قیمت,تعداد,توضیحات) values('" + dt.Rows[d]["نام_قطعه"].ToString() + "','" + dt.Rows[d]["شماره_فنی"].ToString() + "','" + dt.Rows[d]["نوع_خودرو"].ToString() + "','" + dt.Rows[d]["شرکت_تولید_کننده"].ToString() + "','" + Convert.ToInt32(dt.Rows[d]["قیمت"]) + "','" + Convert.ToInt32(dt.Rows[d]["تعداد"]) + "','" + dt.Rows[d]["توضیحات"].ToString() + "'  )";
                            cmd1.ExecuteNonQuery();
                        }
                        conn1.Close();
                        label4.Visible = false;
                        (this).Close();
                        
                    }
                    else
                    {
                        conn1.Close();

                        MessageBox.Show("                   ساختار این فایل درست نیست،لطفا یک فایل اکسل دیگری انتخاب کنید" );
                        label4.Visible = false;

                    }
                  
                   
                
                }
                catch(Exception ex)
                {
                    conn1.Close();
               
                    MessageBox.Show("                   لطفا یک فایل اکسل دیگری انتخاب کنید"+"\n \n \n"+ex);
                    label4.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("لطفا یک فایل اکسل انتخاب کنید");
                label4.Visible = false;
            }
            
          
          }
        }

        private void importtoexcel_Load(object sender, EventArgs e)
        {

        }

        private void importtoexcel_FormClosed(object sender, FormClosedEventArgs e)
        {
            
      
        }

        private void importtoexcel_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

       

     
    }
}
