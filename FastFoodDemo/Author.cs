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
    public partial class Author : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public Author()
        {
            InitializeComponent();
        }

        private void Author_Load(object sender, EventArgs e)
        {
            ShowAuthor();
        }
        private void Cleartext()
        {
            txtAddress.Clear();
            txtAuthorID.Clear();
            txtAuthorName.Clear();
            txtEmail.Clear();
            txtGender.Clear();
            txtPhone.Clear();
           
        }
        private void ShowAuthor()
        {
            try
            {
                //conn.Open();
                string sql_select = "SELECT * FROM tblAuthor";
                //create dataa adapter obeject
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql_select, conn);
                //create data set object
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Student");
                dataGridView1.DataSource = ds.Tables["Student"];
                dataGridView1.RowTemplate.Height = 75;
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn)dataGridView1.Columns[6];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
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

            //Fix Row Heighgt
            DataGridViewImageColumn imgcol = (DataGridViewImageColumn)dataGridView1.Columns[6];
            imgcol.ImageLayout = DataGridViewImageCellLayout.Zoom;

        }
        private void ClearText(Form frm)
        {
            foreach (Control ctrl in frm.Controls)
            {
                if (ctrl is TextBox || ctrl is ComboBox)
                {
                    ctrl.Text = String.Empty;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "images | *.png; *.jpg; *.jpeg; *.bmp;";
            openFileDialog1.FilterIndex = 4;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
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
                if (string.IsNullOrEmpty(txtAuthorName.Text))
                {
                    MessageBox.Show("Please enter Author Name!", "Required Author Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAuthorName.Focus();
                }
                else
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        string sql_insert = "INSERT INTO tblAuthor (AuthorName,Gender,Address,Phone,Email,Image) VALUES( @AuthorName, @Gender, @Address, @Phone, @Email, @Image); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        cmd.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                        cmd.Parameters.AddWithValue("@Gender", txtGender.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Image", ms.ToArray());
                        //open the connection
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("One record has been add.", "Record Added",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Change text buttons
                        btnAdd.Text = "  Add new";
                        btnDelete.Text = "  Delete";
                        //enble button Update
                        btnUpdate.Enabled = true;
                        ShowAuthor();
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
                Cleartext();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAuthorName.Text))
            {
                MessageBox.Show("Please Select Student for Update!", "Student Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAuthorName.Focus();

            }
            else
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    string sql_update = "UPDATE  tblAuthor SET AuthorName =@AuthorName,Gender=@Gender,Address=@Address,Phone=@Phone,Email=@Email,Image=@Image WHERE AuthorID=@AuthorID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@AuthorID", txtAuthorID.Text);
                    cmd_update.Parameters.AddWithValue("@AuthorName", txtAuthorName.Text);
                    cmd_update.Parameters.AddWithValue("@Gender", txtGender.Text);
                    cmd_update.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd_update.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd_update.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd_update.Parameters.AddWithValue("@Image", ms.ToArray());

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method
                   // ClearText(this);
                    ShowAuthor();
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
                Cleartext();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "  Cancel")
            {
                btnAdd.Text = "  Add New";
                btnDelete.Text = "  Delete";
                btnUpdate.Enabled = true;
                //ClearText(this);
            }
            else if (btnDelete.Text == "  Delete")
            {
                if (string.IsNullOrEmpty(txtAuthorName.Text))
                {
                    MessageBox.Show("Please Select a Author to Delete.", "No, Author Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Librarian'" + txtAuthorName.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblAuthor WHERE AuthorID=@AuthorID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        //SqlCommand cmd = new SqlCommand("UpdateEmployeeStatus", conn);
                        // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AuthorID", txtAuthorID.Text);
                        // cmd.Parameters.AddWithValue("@isDeleted", 1);
                        cmd.ExecuteNonQuery();

                        ShowAuthor();
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
                    ShowAuthor();
                }
                Cleartext();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtAuthorID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAuthorName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtGender.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtPhone.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            byte[] img = (byte[])dataGridView1.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM tblAuthor WHERE AuthorName LIKE '%" + txtSearch.Text + "%'; ";
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
