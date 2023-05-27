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
    public partial class Login : Form
    {
        SqlConnection conn = DBConnection.connect();
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //User Login
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("User Name is Required...!", "Messing User Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
            }
            else if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Password is Required...!", "Messing Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql_select = "SELECT * FROM tblLibraian WHERE UserName ='" + txtUserName.Text.Trim() + "' AND Password ='" + txtPassword.Text.Trim() + "'";
                    //Create Data Table Object
                    SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        UserLogin.setUserID(dt.Rows[0]["LibrarianID"].ToString());
                        UserLogin.setUserName(dt.Rows[0]["UserName"].ToString());
                        UserLogin.setUserPassword(dt.Rows[0]["Password"].ToString());
                        //UserLogin.setUserType(dt.Rows[0]["UserType"].ToString());
                        Form1 frm = new Form1();
                        this.Hide();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Login The User...", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
            //Student Login

            /*if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("User Name is Required...!", "Messing User Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
            }
            else
            {
                try
                {
                    conn.Open();
                    string sql_select = "SELECT * FROM tblSudent WHERE StudentName ='" + txtUserName.Text.Trim();
                    //Create Data Table Object
                    SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
                    DataTable dt = new DataTable();

                    adapter.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        UserLogin.setUserID(dt.Rows[0]["StudentID"].ToString());
                        UserLogin.setUserName(dt.Rows[0]["StudentName"].ToString());
                        //UserLogin.setUserPassword(dt.Rows[0]["Password"].ToString());
                        //UserLogin.setUserType(dt.Rows[0]["UserType"].ToString());
                        Form1 frm = new Form1();

                        
                        this.Hide();
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Failed to Login The User...", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }*/
        }
    }
}
