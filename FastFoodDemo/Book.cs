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
    public partial class Book : UserControl
    {
        SqlConnection conn = DBConnection.connect();
        public Book()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void ClearText()
        {
            txtBookID.Clear();
            txtBookTitle.Clear();
            txtNumCopy.Clear();
            txtNumCopy.Clear();
            txtNumPage.Clear();
            txtPublisher.Clear();
            txtQtyBook.Clear();
            //pictureBox1.Enabled = false;
            
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
                if (string.IsNullOrEmpty(txtBookTitle.Text))
                {
                    MessageBox.Show("Please enter Student Name!", "Required Student Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBookTitle.Focus();
                }
                else
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        string sql_insert = "INSERT INTO tblBook (BookTitle ,PublishDate,NumOfPages,NumCopies,Publisher,BookTypeID,QtyBook,Image) VALUES( @BookTitle, @PublishDate, @NumOfPages, @NumCopies, @Publisher,@BookTypeID,@QtyBook,@Image); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        //cmd.Parameters.AddWithValue("@EmID", txtEmployeeID.Text);
                        cmd.Parameters.AddWithValue("@BookTitle", txtBookTitle.Text);
                        cmd.Parameters.AddWithValue("@PublishDate", PublishDate.Value);
                        cmd.Parameters.AddWithValue("@NumOfPages", txtNumPage.Text);
                        cmd.Parameters.AddWithValue("@NumCopies", txtNumCopy.Text);
                        cmd.Parameters.AddWithValue("@Publisher", txtPublisher.Text);
                        cmd.Parameters.AddWithValue("@BookTypeID", comboBookType.SelectedValue);
                        cmd.Parameters.AddWithValue("@QtyBook", txtQtyBook.Text);
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
                        ShowBook();
                        ClearText();
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
        private void ShowBookType()
        {
            string sql_select = "SELECT BookTypeID,TypeName FROM tblBookType";
            //Create data adapter
            SqlDataAdapter adapter = new SqlDataAdapter(sql_select, conn);
            //Create dataSet
            DataSet ds = new DataSet();
            //Fill data into dataset obj:ds
            adapter.Fill(ds, "Category");
            comboBookType.DataSource = ds.Tables["Category"];
            comboBookType.DisplayMember = "TypeName";
            comboBookType.ValueMember = "BookTypeID";
            ds.Dispose();
            adapter.Dispose();
        }
        private void ShowBook()
        {
            SqlCommand cmd = new SqlCommand("ShowBookType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            //create data adapter
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "Student");
            dataGridView1.DataSource = ds.Tables["Student"];
            dataGridView1.RowTemplate.Height = 75;
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            ds.Dispose();
            dataAdapter.Dispose();

        }

        private void Book_Load(object sender, EventArgs e)
        {
            ShowBookType();
            ShowBook();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBookTitle.Text))
            {
                MessageBox.Show("Please Select Book for Update!", "Book Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBookTitle.Focus();

            }
            else
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    string sql_update = "UPDATE  tblBook SET BookTitle=@BookTitle ,PublishDate=@PublishDate,NumOfPages=@NumOfPages,NumCopies=@NumCopies,Publisher=@Publisher,BookTypeID=@BookTypeID,QtyBook=@QtyBook,Image=@Image WHERE BookID=@BookID";

                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@BookID", txtBookID.Text);
                    cmd_update.Parameters.AddWithValue("@BookTitle", txtBookTitle.Text);
                    cmd_update.Parameters.AddWithValue("@PublishDate", PublishDate.Value);
                    cmd_update.Parameters.AddWithValue("@NumOfPages", txtNumPage.Text);
                    cmd_update.Parameters.AddWithValue("@NumCopies", txtNumCopy.Text);
                    cmd_update.Parameters.AddWithValue("@Publisher", txtPublisher.Text);                   
                    cmd_update.Parameters.AddWithValue("@BookTypeID", comboBookType.SelectedValue);
                    cmd_update.Parameters.AddWithValue("@QtyBook", txtQtyBook.Text);
                    cmd_update.Parameters.AddWithValue("@Image", ms.ToArray());

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowBook();
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
                //ClearText(this);
            }
            else if (btnDelete.Text == "  Delete")
            {
                if (string.IsNullOrEmpty(txtBookTitle.Text))
                {
                    MessageBox.Show("Please Select a Book to Delete.", "No, Book Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Book'" + txtBookTitle.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblBook WHERE BookID=@BookID";
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        //SqlCommand cmd = new SqlCommand("UpdateEmployeeStatus", conn);
                        // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookID", txtBookID.Text);
                        // cmd.Parameters.AddWithValue("@isDeleted", 1);
                        cmd.ExecuteNonQuery();

                        ShowBook();
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
                    ShowBook();
                }
                ClearText();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtBookID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtBookTitle.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            PublishDate.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtNumPage.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtNumCopy.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtPublisher.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBookType.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtQtyBook.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            byte[] img = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string sql_select = "SELECT * FROM tblBook WHERE BookTitle LIKE '%" + txtSearch.Text + "%' OR BookID LIKE '%" + txtSearch.Text + "%' ";
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