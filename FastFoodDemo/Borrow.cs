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
    public partial class Borrow : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public Borrow()
        {
            InitializeComponent();
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
                        string sql_insert = "INSERT INTO tblBorrow (StudentID,LibrarianID,BookID,QtyBorrow,BorrowDate,ReturnDate,isReturn) VALUES(@StudentID,@LibrarianID,@BookID,@QtyBorrow,@BorrowDate,@ReturnDate,0); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        cmd.Parameters.AddWithValue("@StudentID", comboStudentID.SelectedValue);
                        cmd.Parameters.AddWithValue("@LibrarianID", comboLibrarianID.SelectedValue);
                        cmd.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                        cmd.Parameters.AddWithValue("@QtyBorrow", txtQtyBorrow.Text);
                        cmd.Parameters.AddWithValue("@BorrowDate", BorrowDate.Value);
                        cmd.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("One record has been add.", "Record Added",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnAdd.Text = "  Add new";
                        btnDelete.Text = "  Delete";
                        btnUpdate.Enabled = true;
                        ShowBorrow();
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
                }
                ClearText();
            }
            else
            {
                btnAdd.Text = "  Add new";
                btnUpdate.Text = "  Update";
                btnDelete.Text = "  Delete";
                MessageBox.Show("Please Click Add New Again!", "Required Add New",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Focus();

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void Borrow_Load(object sender, EventArgs e)
        {
            ShowBorrow();
            ShowBook();
            ShowLibrarian();
            ShowStudent();
        }
        private void ClearText()
        {
            txtBorrowID.Clear();
            txtQtyBorrow.Clear();
        }
        private void ShowBook()
        {
            string sql_select = "SELECT BookID,BookTitle FROM tblBook";
            //Create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            //Create dataSet
            DataSet ds = new DataSet();
            //Fill data into dataset obj:ds
            adapter.Fill(ds, "Book");
            comboBookID.DataSource = ds.Tables["Book"];
            comboBookID.DisplayMember = "BookTitle";
            comboBookID.ValueMember = "BookID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void ShowBorrow()
        {
            SqlCommand cmd = new SqlCommand("ShowBorrow", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //create dataSet object
            DataSet ds = new DataSet();
            adapter.Fill(ds, "ShowBorrow");
            dataGridView1.DataSource = ds.Tables["ShowBorrow"];


            /*SqlCommand cmd = new SqlCommand("ShowBorrow", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //set value to parameter of StoredProcedure
            //cmd.Parameters.AddWithValue("@isReturn", 1);
            //create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //create dataSet object
            DataSet ds = new DataSet();
            adapter.Fill(ds, "ShowBorrow");
            dataGridView1.DataSource = ds.Tables["ShowBorrow"];
            dataGridView1.Columns[7].Visible = false;*/
        }
        private void ShowLibrarian()
        {
            string sql_select = "SELECT librarianID,LibrarianName FROM tblLibraian";
            //Create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            //Create dataSet
            DataSet ds = new DataSet();
            //Fill data into dataset obj:ds
            adapter.Fill(ds, "Librarian");
            comboLibrarianID.DataSource = ds.Tables["Librarian"];
            comboLibrarianID.DisplayMember = "LibrarianName";
            comboLibrarianID.ValueMember = "librarianID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void ShowStudent()
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
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBookID.Text))
            {
                MessageBox.Show("Please Select BookAuthor for Update!", "BookAuthor Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBookID.Focus();
            }
            else
            {
                try
                {
                    string sql_update = "UPDATE  tblBorrow SET StudentID=@StudentID,LibrarianID=@LibrarianID,BookID=@BookID,QtyBorrow=@QtyBorrow,BorrowDate=@BorrowDate,ReturnDate=@ReturnDate WHERE BorrowID=@BorrowID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@StudentID", comboStudentID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@LibrarianID", comboLibrarianID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@QtyBorrow",txtQtyBorrow.Text);
                    cmd_update.Parameters.AddWithValue("@BorrowDate", BorrowDate.Value);
                    cmd_update.Parameters.AddWithValue("@ReturnDate", ReturnDate.Value);
                    cmd_update.Parameters.AddWithValue("@BorrowID", txtBorrowID.Text);

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowBorrow();
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
            ClearText();
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
                    MessageBox.Show("Please Select a Borrow to Delete.", "No, Borrow Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Borrow'" + comboStudentID.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "UPDATE tblBorrow SET isReturn=1 WHERE BorrowID=@BorrowID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        cmd.Parameters.AddWithValue("@BorrowID", txtBorrowID.Text);
                        cmd.ExecuteNonQuery();
                        ShowBorrow();
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
                    ShowBorrow();
                }
                ClearText();
            }


            /*if (string.IsNullOrEmpty(txtBorrowID.Text))
            {
                MessageBox.Show("Please Select a Borrow to Delete.", "No, Borrow Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Are you sure ? You want to Delete a Borrow'" + comboStudentID.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UpdateBorrowStatus", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Bid", Convert.ToInt32(txtBorrowID.Text));
                    cmd.Parameters.AddWithValue("@isReturn", 1);
                    cmd.ExecuteNonQuery();

                    ShowBorrow();
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
                //ClearText(this);
            }*/
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM vBorrow WHERE StudentName LIKE '%" + txtSearch.Text + "%' OR LibrarianName LIKE '%" + txtSearch.Text + "%' ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            dt.Dispose();
            adapter.Dispose();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtBorrowID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboStudentID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboLibrarianID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBookID.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtQtyBorrow.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            BorrowDate.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            ReturnDate.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
