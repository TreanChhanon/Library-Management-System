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
    public partial class Librarian : UserControl
    {

        SqlConnection conn = DBConnection.connect();
        public Librarian()
        {
            InitializeComponent();
        }
        private void ShowLibrarain()
        {
            try
            {
                //conn.Open();
                string sql_select = "SELECT * FROM tblLibraian";
                //create dataa adapter obeject
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sql_select, conn);
                //create data set object
                DataSet ds = new DataSet();
                dataAdapter.Fill(ds, "Librarain");
                dataGridView1.DataSource = ds.Tables["Librarain"];                
                dataGridView1.RowTemplate.Height = 75;
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn)dataGridView1.Columns[8];
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
            DataGridViewImageColumn imgcol = (DataGridViewImageColumn)dataGridView1.Columns[8];
            imgcol.ImageLayout = DataGridViewImageCellLayout.Zoom;

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
                if (string.IsNullOrEmpty(txtLibrarianName.Text))
                {
                    MessageBox.Show("Please enter Librarain Name!", "Required Librarain Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtLibrarianName.Focus();
                }
                else
                {
                    try
                    {
                        MemoryStream ms = new MemoryStream();
                        pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                        string sql_insert = "INSERT INTO tblLibraian(LibrarianName,UserName,Password,Email,Phone,Address,Country,Image) VALUES( @LibraianName, @UserName, @Password, @Email, @Phone, @Address, @Country, @Image); ";
                        SqlCommand cmd = new SqlCommand(sql_insert, conn);
                        //set value for parameters
                        //cmd.Parameters.AddWithValue("@EmID", txtEmployeeID.Text);
                        cmd.Parameters.AddWithValue("@LibraianName", txtLibrarianName.Text);
                        cmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
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
                        ShowLibrarain();
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
                //btnUpdate.Text = "  Update";
                btnDelete.Text = "  Delete";
                MessageBox.Show("Please Click Add New Again!", "Required Add New",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAdd.Focus();

            }*/
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

        private void Librarian_Load(object sender, EventArgs e)
        {
            ShowLibrarain();
        }
        private void ClearText()
        {
            txtAddress.Clear();
            txtCountry.Clear();
            txtEmail.Clear();
            txtLibrarianID.Clear();
            txtLibrarianName.Clear();
            txtPassword .Clear();
            txtPhone.Clear();   
            txtUserName.Clear();
            //pictureBox1.Image=null;

           // txtAddress.Clear();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLibrarianName.Text))
            {
                MessageBox.Show("Please Select Librarain for Update!", "Librarian Null", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLibrarianName.Focus();

            }
            else
            {
                try
                {
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    //string sql_update = "UPDATE tblEmployees SET EmployeeName=@EmName, Description= @desc, UpdateDate=@updatedate, UpdateBy=@by WHERE CategoryID=@id";
                    string sql_update = "UPDATE  tblLibraian SET LibrarianName=@LibrarianName,UserName=@UserName,Password=@Password,Email=@Email,Phone=@Phone,Address=@Address,Country=@Country,Image=@Image WHERE LibrarianID=@LibrarianID";
                    // " VALUES(@EmName, @UserName, @Pass, @UserType, @Role, @Job, @Email, @Phone, @HPhone, @Address, @City, @State, @Zip, @Country, @image); ";
                    // create command object
                    SqlCommand cmd_update = new SqlCommand(sql_update, conn);
                    cmd_update.Parameters.AddWithValue("@LibrarianID", txtLibrarianID.Text);
                    cmd_update.Parameters.AddWithValue("@LibrarianName", txtLibrarianName.Text);
                    cmd_update.Parameters.AddWithValue("@UserName", txtUserName.Text);
                    cmd_update.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd_update.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd_update.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd_update.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd_update.Parameters.AddWithValue("@Country", txtCountry.Text);
                    cmd_update.Parameters.AddWithValue("@Image", ms.ToArray());

                    //open the connection
                    conn.Open();
                    cmd_update.ExecuteNonQuery();

                    // Calling the method

                    ShowLibrarain();
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text == "  Cancel")
            {
                btnAdd.Text = "  Add New";
                btnDelete.Text = "  Delete";
                btnUpdate.Enabled = true;
                //ClearText();
            }
            else if (btnDelete.Text == "  Delete")
            {
                if (string.IsNullOrEmpty(txtLibrarianName.Text))
                {
                    MessageBox.Show("Please Select a Librarian to Delete.", "No, Librarain Selected.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show("Are you sure ? You want to Delete a Librarian'" + txtLibrarianName.Text + "'?", "Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        conn.Open();
                        string spl_delete = "DELETE FROM  tblLibraian WHERE LibrarianID=@LibrarianID";//(EmployeeID,EmployeeName,UserName,Password,UserType,Role,JobTile,Email,Phone,HomePhone,Address,City,StateProvince,ZipPostalCode,CountryRegion,Photo) VALUES(@EmID, @EmName, @UserName, @Pass, @UserType, @Role, @Job, @Email, @Phone, @HPhone, @Address, @City, @State, @Zip, @Country, @image); ";//parameters used @
                        SqlCommand cmd = new SqlCommand(spl_delete, conn);
                        //SqlCommand cmd = new SqlCommand("UpdateEmployeeStatus", conn);
                        // cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@LibrarianID", txtLibrarianID.Text);
                        // cmd.Parameters.AddWithValue("@isDeleted", 1);
                        cmd.ExecuteNonQuery();

                        ShowLibrarain();
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
                    ShowLibrarain();
                    ClearText();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtLibrarianID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtLibrarianName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtUserName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtPhone.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtAddress.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtCountry.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();

            byte[] img = (byte[])dataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox1.Image = Image.FromStream(ms);
        }
    }
}
//Done
