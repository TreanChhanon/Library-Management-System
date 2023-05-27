using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //note add text for SQL Connetionz
using System.Windows.Forms;  //note add text for using message box

namespace FastFoodDemo
{
    class DBConnection
    {
        //data member variable
        private static SqlConnection conn;
        //member method
        public static SqlConnection connect()
        {
            try
            {
                string strConn = "Server=Nun; Database=Library; Trusted_Connection=true";
                conn = new SqlConnection();
                conn.ConnectionString = strConn;
                conn.Open();
                //MessageBox.Show("Connection to Database is OK");
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ërror;" + ex.Message, "Error....", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
