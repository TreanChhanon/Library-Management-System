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
    public partial class BookAuthor : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public BookAuthor()
        {
            InitializeComponent();
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
        private void ShowAuthor()
        {
            string sql_select = "SELECT AuthorID,AuthorName FROM tblAuthor";
            //Create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            //Create dataSet
            DataSet ds = new DataSet();
            //Fill data into dataset obj:ds
            adapter.Fill(ds, "Author");
            comboAuthorID.DataSource = ds.Tables["Author"];
            comboAuthorID.DisplayMember = "AuthorName";
            comboAuthorID.ValueMember = "AuthorID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void ShowBookAuthor()
        {
            SqlCommand cmd = new SqlCommand("ShowBookAuthor", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            //create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //create dataSet object
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Author");
            dataGridView1.DataSource = ds.Tables["Author"];

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
                    MessageBox.Show("Please enter Book Name!", "Required Book Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBookID.Focus();
                }
                else
                {
                    try
                    {
                        string sql_insert = "INSERT INTO tblBookAuthor (BookID,AuthorID,AuthorDate) VALUES( @BookID, @AuthorID,@AuthorDate); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        cmd.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                        cmd.Parameters.AddWithValue("@AuthorID", comboAuthorID.SelectedValue);
                        cmd.Parameters.AddWithValue("@AuthorDate", AuthorDate.Value);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("One record has been add.", "Record Added",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnAdd.Text = "  Add new";
                        btnDelete.Text = "  Delete";
                        btnUpdate.Enabled = true;
                        ShowBookAuthor();
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
                }ClearText();
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

        private void BookAuthor_Load(object sender, EventArgs e)
        {
            ShowBook();
            ShowBookAuthor();
            ShowAuthor();
        }
        private void ClearText()
        {
            txtBookAuthorID.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
                    string sql_update = "UPDATE  tblBookAuthor SET BookID =@BookID,AuthorID=@AuthorID,AuthorDate=@AuthorDate WHERE BookAuthorID=@BookAuthorID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@BookAuthorID", txtBookAuthorID.Text);
                    cmd_update.Parameters.AddWithValue("@BookID", comboBookID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@AuthorID", comboAuthorID.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@AuthorDate", AuthorDate.Value);

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowBookAuthor();
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
                    MessageBox.Show("Please Select a Book Type to Delete.", "No, Book Type Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a BookAuthor'" + comboBookID.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblBookAuthor WHERE BookAuthorID=@BookAuthorID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        cmd.Parameters.AddWithValue("@BookAuthorID", comboBookID.SelectedValue);
                        cmd.ExecuteNonQuery();
                        ShowBookAuthor();
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
                    ShowBookAuthor();
                }
                ClearText();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtBookAuthorID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBookID.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboAuthorID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            AuthorDate.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
//Done
