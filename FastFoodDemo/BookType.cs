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
using System.IO;

namespace FastFoodDemo
{
    public partial class BookType : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public BookType()
        {
            InitializeComponent();
        }

        private void BookType_Load(object sender, EventArgs e)
        {
            ShowBookType();
        }
        private void ClearText()
        {
            txtBookTypeID.Clear();
            txtDescription.Clear();
            txtTypeName.Clear();
        }
        private void ShowBookType()
        {
            try
            {
                //conn.Open();
                string sql_select = "SELECT * FROM tblBookType";
                //create dataa adapter obeject
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql_select, conn);
                //create data set object
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Booktype");
                dataGridView1.DataSource = ds.Tables["Booktype"];
                ds.Dispose();
                dataAdapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error; " + ex.Message, "Errors....", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "  Add New")
            {
                btnAdd.Text = "  Save";
                btnUpdate.Enabled = false;
                btnDelete.Text = "  Cancel";
                //Clear textbox
                //ClearText(this);
            }
            else if (btnAdd.Text == "  Save")
            {
                if (string.IsNullOrEmpty(txtTypeName.Text))
                {
                    MessageBox.Show("Please enter BookType Name!", "Required BookType Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTypeName.Focus();
                }
                else
                {
                    try
                    {
                        string sql_insert = "INSERT INTO tblBookType (TypeName,Description) VALUES( @TypeName, @Description); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        //cmd.Parameters.AddWithValue("@EmID", txtEmployeeID.Text);
                        cmd.Parameters.AddWithValue("@TypeName", txtTypeName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("One record has been add.", "Record Added",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnAdd.Text = "Add new";
                        btnDelete.Text = "Delete";
                        btnUpdate.Enabled = true;
                        ShowBookType();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error." + ex.Message, "Add Record Failed...?",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();//close the connection
                    }
                    ClearText();
                }
            }
            /*else
            {
                btnAdd.Text = "  Add new";
                btnUpdate.Text = "  Update";
                btnDelete.Text = "  Delete";
                MessageBox.Show("Please Click Add New Again!", "Required Add New",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Focus();

            }*/
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTypeName.Text))
            {
                MessageBox.Show("Please Select Book Type for Update!", "Book Type Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTypeName.Focus();

            }
            else
            {
                try
                {

                    string sql_update = "UPDATE  tblBookType SET TypeName =@TypeName,Description=@Description WHERE BookTypeID=@BookTypeID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@TypeName", txtTypeName.Text);
                    cmd_update.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd_update.Parameters.AddWithValue("@BookTypeID", txtBookTypeID.Text);

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowBookType();
                    MessageBox.Show("One Record has been updated", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Record Updated Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close(); // close the connection
                }
                ClearText();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "  Cancel")
            {
                btnAdd.Text = "  Add New";
                btnDelete.Text = "  Delete";
                btnUpdate.Enabled = true;
               // ClearText(this);
            }
            else if (btnDelete.Text == "  Delete")
            {
                if (string.IsNullOrEmpty(txtTypeName.Text))
                {
                    MessageBox.Show("Please Select a Book Type to Delete.", "No, Book Type Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Librarian'" + txtTypeName.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblBookType WHERE BookTypeID=@BookTypeID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        cmd.Parameters.AddWithValue("@BookTypeID", txtBookTypeID.Text);
                        cmd.ExecuteNonQuery();
                        ShowBookType();
                        //ClearText();
                        MessageBox.Show("One Record has been Daleted.", "Record Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error " + ex.Message, "Delete Failed...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                    }
                    ShowBookType();
                }
                ClearText();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtBookTypeID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtTypeName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDescription.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM tblBookType WHERE TypeName LIKE '%" + txtSearch.Text + "%'; ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dt.Dispose();
            adapter.Dispose();
        }
    }
}
//Done
