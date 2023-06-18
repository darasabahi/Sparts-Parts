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
    public partial class costumerlist_from : Form
    {
        public costumerlist_from()
        {
            InitializeComponent();
        }
        OleDbConnection conn = new OleDbConnection(@"provider=Microsoft.ACE.OLEDB.12.0;Data source=" + Application.StartupPath + "\\Database.mdb");
        private void costumerlist_from_Load(object sender, EventArgs e)
        {
            conn.Open();
            OleDbCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from customer_info";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            da.Fill(dt);
            conn.Close();
            dataGridView1.DataSource = dt;
            dataGridView1.Columns.Remove("عکس");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                app.Visible = true;
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Exported from gridview";

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
              MessageBox.Show(D.ToString());
            }
          
            // workbook.SaveAs("C:\\Users\\daras\\Desktop\\utput.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //  app.Quit();
                          ///////////////////////////////////////////////////////////////////////////////////////////////////////////
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Excel Documents (*.xls)|*.xls";
            //sfd.FileName = "export.xls";
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
            //    //ToCsV(dataGridView1, @"c:\export.xls");
            //    ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
            //}
        }
    }
}
