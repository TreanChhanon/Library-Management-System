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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBConnection.connect())
            {
                SqlCommand cmd = new SqlCommand("book", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet1 ds = new DataSet1();
                adapter.Fill(ds, "book");
                CrystalReport1 rpt = new CrystalReport1();
                rpt.SetDataSource(ds.Tables["book"]);
                crystalReportViewer1.ReportSource = rpt;

            }
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }
    }
}
