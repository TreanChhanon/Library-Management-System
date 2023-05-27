using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFoodDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SidePanel.Height = btnHome.Height;
            SidePanel.Top = btnHome.Top;
            firstCustomControl2.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Do you want to Exit ?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void btnLibrarian_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnLibrarian.Height;
            SidePanel.Top = btnLibrarian.Top;
            librarian1.BringToFront();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnHome.Height;
            SidePanel.Top = btnHome.Top;
            firstCustomControl2.BringToFront();
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnStudent.Height;
            SidePanel.Top = btnStudent.Top;
            student1.BringToFront();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnBook.Height;
            SidePanel.Top = btnBook.Top;
            book1.BringToFront();
        }

        private void btnBookType_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnBookType.Height;
            SidePanel.Top = btnBookType.Top;
            bookType1.BringToFront();
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnAuthor.Height;
            SidePanel.Top = btnAuthor.Top;
            author1.BringToFront();
        }

        private void btnBookAuthor_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnBookAuthor.Height;
            SidePanel.Top = btnBookAuthor.Top;
            bookAuthor1.BringToFront();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnBorrow.Height;
            SidePanel.Top = btnBorrow.Top;
            borrow1.BringToFront();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            SidePanel.Height = btnReturn.Height;
            SidePanel.Top = btnReturn.Top;
            return1.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Do you want to Logout Account ?", "Exit", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            this.Hide();
            var myForm = new Login();
            myForm.Closed += (s, args) => this.Close();
            myForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UserName.Text = UserLogin.getUserName();
            Datatime.Text = DateTime.Now.ToString("D");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenForm(new Report(), "Librarian Management Form");
        }
        private void OpenForm(Form frm, string title)
        {
            bool isOpen = false;
            foreach (Form form in Application.OpenForms)
            {
                if (form.Text == title)
                {
                    isOpen = true;
                    form.Focus();
                }
            }
            if (isOpen == false)
            {
                //frm.MdiParent = this;
                frm.Show();
            }
        }

        private void librarainReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new ReportLibrarain(), "Librarian Management Form");
        }

        private void bookReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new Report(), "Book Management Form");
        }

        private void studentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new ReportStudent(), "Student Management Form");
        }

        private void borrowReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FormReportBorrow(), "Borrow Management Form");
        }

        private void returnReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new FormReturnReport(), "Return Management Form");
        }

        private void UserName_Click(object sender, EventArgs e)
        {

        }
    }
}
