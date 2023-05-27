namespace FastFoodDemo
{
    partial class FormReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.librarianReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.librarianReportToolStripMenuItem,
            this.studentReportToolStripMenuItem,
            this.bookReportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 40);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // librarianReportToolStripMenuItem
            // 
            this.librarianReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.librarianReportToolStripMenuItem.Name = "librarianReportToolStripMenuItem";
            this.librarianReportToolStripMenuItem.Size = new System.Drawing.Size(207, 36);
            this.librarianReportToolStripMenuItem.Text = "Librarian Report";
            // 
            // studentReportToolStripMenuItem
            // 
            this.studentReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.studentReportToolStripMenuItem.Name = "studentReportToolStripMenuItem";
            this.studentReportToolStripMenuItem.Size = new System.Drawing.Size(196, 36);
            this.studentReportToolStripMenuItem.Text = "Student Report";
            // 
            // bookReportToolStripMenuItem
            // 
            this.bookReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.bookReportToolStripMenuItem.Name = "bookReportToolStripMenuItem";
            this.bookReportToolStripMenuItem.Size = new System.Drawing.Size(167, 36);
            this.bookReportToolStripMenuItem.Text = "Book Report";
            // 
            // FormReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 614);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormReport";
            this.Text = "FormReport";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem librarianReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem studentReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookReportToolStripMenuItem;
    }
}