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
    public partial class ReportLibrarain : Form
    {
        public ReportLibrarain()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBConnection.connect())
            {
                SqlCommand cmd = new SqlCommand("Librarain", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet1 ds = new DataSet1();
                adapter.Fill(ds, "Librarain");
                CrystalReport2 rpt = new CrystalReport2();
                rpt.SetDataSource(ds.Tables["Librarain"]);
                crystalReportViewer1.ReportSource = rpt;

            }
        }
    }
}
