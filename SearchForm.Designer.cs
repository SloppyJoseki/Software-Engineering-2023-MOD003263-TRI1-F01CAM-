namespace SearchFeature
{
    partial class SearchForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelSV = new System.Windows.Forms.Label();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxCloud = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(877, 338);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // labelSV
            // 
            this.labelSV.AutoSize = true;
            this.labelSV.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSV.Location = new System.Drawing.Point(24, 13);
            this.labelSV.Name = "labelSV";
            this.labelSV.Size = new System.Drawing.Size(185, 29);
            this.labelSV.TabIndex = 1;
            this.labelSV.Text = "Search Vendors";
            // 
            // searchBar
            // 
            this.searchBar.Location = new System.Drawing.Point(29, 71);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(291, 22);
            this.searchBar.TabIndex = 2;
            this.searchBar.TextChanged += new System.EventHandler(this.searchBar_TextChanged);
            // 
            // comboBoxType
            // 
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Items.AddRange(new object[] {
            "Wealth Management",
            "Ent Architecture",
            "BP Management"});
            this.comboBoxType.Location = new System.Drawing.Point(346, 71);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxType.TabIndex = 3;
            this.comboBoxType.Text = "Software Type";
            this.comboBoxType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(824, 71);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(80, 24);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxCloud
            // 
            this.comboBoxCloud.FormattingEnabled = true;
            this.comboBoxCloud.Items.AddRange(new object[] {
            "Based ",
            "Enabled"});
            this.comboBoxCloud.Location = new System.Drawing.Point(488, 71);
            this.comboBoxCloud.Name = "comboBoxCloud";
            this.comboBoxCloud.Size = new System.Drawing.Size(121, 24);
            this.comboBoxCloud.TabIndex = 5;
            this.comboBoxCloud.Text = "Cloud Type";
            this.comboBoxCloud.SelectedIndexChanged += new System.EventHandler(this.comboBoxCloud_SelectedIndexChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 541);
            this.Controls.Add(this.comboBoxCloud);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.labelSV);
            this.Controls.Add(this.dataGridView1);
            this.Name = "SearchForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label labelSV;
        private System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.ComboBox comboBoxCloud;
    }
}

