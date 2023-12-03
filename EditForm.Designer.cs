namespace LoginInterface
{
    partial class EditForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ViewSoftwareButton = new System.Windows.Forms.Button();
            this.ViewCompanyButton = new System.Windows.Forms.Button();
            this.home_btn = new System.Windows.Forms.Button();
            this.companyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dBExcludingUsersDataSet = new LoginInterface.DBExcludingUsersDataSet();
            this.companyTableAdapter = new LoginInterface.DBExcludingUsersDataSetTableAdapters.CompanyTableAdapter();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBExcludingUsersDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datadase editing tool";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.ViewSoftwareButton);
            this.panel1.Controls.Add(this.ViewCompanyButton);
            this.panel1.Location = new System.Drawing.Point(103, 26);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 463);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DGV);
            this.panel2.Location = new System.Drawing.Point(19, 76);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 351);
            this.panel2.TabIndex = 8;
            // 
            // DGV
            // 
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Edit});
            this.DGV.Location = new System.Drawing.Point(4, 4);
            this.DGV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV.Name = "DGV";
            this.DGV.RowHeadersWidth = 62;
            this.DGV.Size = new System.Drawing.Size(891, 347);
            this.DGV.TabIndex = 0;
            this.DGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Edit
            // 
            this.Edit.DataPropertyName = "Company_ID";
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.UseColumnTextForButtonValue = true;
            this.Edit.Width = 150;
            // 
            // ViewSoftwareButton
            // 
            this.ViewSoftwareButton.Location = new System.Drawing.Point(138, 47);
            this.ViewSoftwareButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ViewSoftwareButton.Name = "ViewSoftwareButton";
            this.ViewSoftwareButton.Size = new System.Drawing.Size(101, 27);
            this.ViewSoftwareButton.TabIndex = 3;
            this.ViewSoftwareButton.Text = "View Software";
            this.ViewSoftwareButton.UseVisualStyleBackColor = true;
            // 
            // ViewCompanyButton
            // 
            this.ViewCompanyButton.Location = new System.Drawing.Point(19, 46);
            this.ViewCompanyButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ViewCompanyButton.Name = "ViewCompanyButton";
            this.ViewCompanyButton.Size = new System.Drawing.Size(113, 27);
            this.ViewCompanyButton.TabIndex = 2;
            this.ViewCompanyButton.Text = "View Company";
            this.ViewCompanyButton.UseVisualStyleBackColor = true;
            this.ViewCompanyButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // home_btn
            // 
            this.home_btn.Location = new System.Drawing.Point(11, 102);
            this.home_btn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.home_btn.Name = "home_btn";
            this.home_btn.Size = new System.Drawing.Size(81, 80);
            this.home_btn.TabIndex = 3;
            this.home_btn.Text = "Home";
            this.home_btn.UseVisualStyleBackColor = true;
            this.home_btn.Click += new System.EventHandler(this.Button7_Click);
            // 
            // companyBindingSource
            // 
            this.companyBindingSource.DataMember = "Company";
            this.companyBindingSource.DataSource = this.dBExcludingUsersDataSet;
            // 
            // dBExcludingUsersDataSet
            // 
            this.dBExcludingUsersDataSet.DataSetName = "DBExcludingUsersDataSet";
            this.dBExcludingUsersDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // companyTableAdapter
            // 
            this.companyTableAdapter.ClearBeforeFill = true;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 635);
            this.Controls.Add(this.home_btn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EditForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.Load += new System.EventHandler(this.EditForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.companyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dBExcludingUsersDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ViewSoftwareButton;
        private System.Windows.Forms.Button ViewCompanyButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button home_btn;
        private System.Windows.Forms.DataGridView DGV;
        private DBExcludingUsersDataSet dBExcludingUsersDataSet;
        private System.Windows.Forms.BindingSource companyBindingSource;
        private DBExcludingUsersDataSetTableAdapters.CompanyTableAdapter companyTableAdapter;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
    }
}