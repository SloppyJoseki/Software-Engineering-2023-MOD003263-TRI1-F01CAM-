namespace LoginInterface
{
    partial class Dashboard
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
            this.SearchButton = new System.Windows.Forms.Button();
            this.EditDbButton = new System.Windows.Forms.Button();
            this.viewFilesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(132, 95);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(182, 23);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Search stuff";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // EditDbButton
            // 
            this.EditDbButton.Location = new System.Drawing.Point(117, 166);
            this.EditDbButton.Name = "EditDbButton";
            this.EditDbButton.Size = new System.Drawing.Size(221, 23);
            this.EditDbButton.TabIndex = 1;
            this.EditDbButton.Text = "Edit DB stuff";
            this.EditDbButton.UseVisualStyleBackColor = true;
            this.EditDbButton.Click += new System.EventHandler(this.EditDbButton_Click);
            // 
            // viewFilesButton
            // 
            this.viewFilesButton.Location = new System.Drawing.Point(117, 226);
            this.viewFilesButton.Name = "viewFilesButton";
            this.viewFilesButton.Size = new System.Drawing.Size(221, 23);
            this.viewFilesButton.TabIndex = 2;
            this.viewFilesButton.Text = "View Files";
            this.viewFilesButton.UseVisualStyleBackColor = true;
            this.viewFilesButton.Click += new System.EventHandler(this.ViewFilesButton_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.viewFilesButton);
            this.Controls.Add(this.EditDbButton);
            this.Controls.Add(this.SearchButton);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Dashboard_FormClosing);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button EditDbButton;
        private System.Windows.Forms.Button viewFilesButton;
    }
}