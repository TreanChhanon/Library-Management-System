using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace FastFoodDemo
{
    public partial class FormReturnReport : Form
    {
        public FormReturnReport()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBConnection.connect())
            {
                SqlCommand cmd = new SqlCommand("ShowReturnn", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet5 ds = new DataSet5();
                adapter.Fill(ds, "ShowReturnn");
                CrystalReport5 rpt = new CrystalReport5();
                rpt.SetDataSource(ds.Tables["ShowReturnn"]);
                crystalReportViewer1.ReportSource = rpt;

            }
        }
    }
}
