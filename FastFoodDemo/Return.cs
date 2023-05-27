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
    public partial class Return : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public Return()
        {
            InitializeComponent();
        }

        private void Return_Load(object sender, EventArgs e)
        {
            ShowReturn();
            ShowBook();
            ShowLibrarian();
            showreturnstudents();
        }
        private void ShowBook()
        {
            SqlCommand cmd = new SqlCommand("showreturnBook", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Book");
            comboBookID.DataSource = ds.Tables["Book"];
            comboBookID.DisplayMember = "BookTitle";
            comboBookID.ValueMember = "BookID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void ShowReturn()
        {
            SqlCommand cmd = new SqlCommand("ShowReturnn", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //create dataSet object
            DataSet ds = new DataSet();
            adapter.Fill(ds, "ShowReturn");
            dataGridView1.DataSource = ds.Tables["ShowReturn"];

        }
        private void ShowLibrarian()
        {
            SqlCommand cmd = new SqlCommand("showreturnlibrary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "libarian");
            comboLibrarianID.DataSource = ds.Tables["libarian"];
            comboLibrarianID.DisplayMember = "LibrarianName";
            comboLibrarianID.ValueMember = "LibrarianID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void showreturnstudents()
        {
            SqlCommand cmd = new SqlCommand("showreturnstudents", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "showreturnstudents");
            comboStudentID.DataSource = ds.Tables["showreturnstudents"];
            comboStudentID.DisplayMember = "StudentName";
            comboStudentID.ValueMember = "StudentID";
            ds.Dispose();
            adapter.Dispose();

        }
        /*private void ShowStudent()
        {
            string sql_select = "SELECT StudentID,StudentName FROM tblStudent";
                        //Create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            //Create dataSet
            DataSet ds = new DataSet();
            //Fill data into dataset obj:ds
            adapter.Fill(ds, "Student");
            comboStudentID.DataSource = ds.Tables["Student"];
            comboStudentID.DisplayMember = "StudentName";
            comboStudentID.ValueMember = "StudentID";
            ds.Dispose();
            adapter.Dispose();
        }*/

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                if (string.IsNullOrEmpty(comboBookID.Text))
                {
                    MessageBox.Show("Please enter Student Name!", "Required Student Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboStudentID.Focus();
                }
                else
                {
                    try
                    {
                        string sql_insert = "INSERT INTO tblReturn (StudentID,LibrarianID,BookID,ReturnDate,Qty) VALUES(@StudentID,@LibrarianID,@BookID,@ReturnDate,@Qty); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        cmd.Parameters.AddWithValue("@StudentID", comboStudentID.SelectedValue);
                        cmd.Parameters.AddWithValue("@LibrarianID", comboLibrarianID.SelectedValue);
                        cmd.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
                        cmd.Parameters.AddWithValue("@Qty",txtQty.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("One record has been add.", "Record Added",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnAdd.Text = "Add new";
                        btnDelete.Text = "Delete";
                        btnUpdate.Enabled = true;
                        ShowReturn();
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
                    ShowReturn();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBookID.Text))
            {
                MessageBox.Show("Please Select ReturnBook for Update!", "ReturnBook Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBookID.Focus();
            }
            else
            {
                try
                {
                    string sql_update = "UPDATE  tblReturn SET StudentID=@StudentID,LibrarianID=@LibrarianID,BookID=@BookID,ReturnDate=@ReturnDate,Qty=@Qty WHERE ReturnID=@ReturnID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@StudentID", comboStudentID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@LibrarianID", comboLibrarianID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
                    cmd_update.Parameters.AddWithValue("@Qty", txtQty.Text);
                    cmd_update.Parameters.AddWithValue("@ReturnID", txtReturnID.Text);

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowReturn();
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
            }
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
                if (string.IsNullOrEmpty(comboBookID.Text))
                {
                    MessageBox.Show("Please Select a Return to Delete.", "No, Return Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Borrow'" + comboStudentID.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblReturn WHERE ReturnID=@ReturnID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        cmd.Parameters.AddWithValue("@ReturnID", txtReturnID.Text);
                        cmd.ExecuteNonQuery();
                        ShowReturn();
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
                    ShowReturn();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM vReturn WHERE StudentName LIKE '%" + txtSearch.Text + "%' OR LibrarianName LIKE '%" + txtSearch.Text + "%' ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dt.Dispose();
            adapter.Dispose();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtReturnID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboStudentID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboLibrarianID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBookID.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            ReturnDate.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtQty.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
