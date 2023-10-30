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
            this.SuspendLayout();
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(148, 119);
            this.SearchButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(205, 29);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Search stuff";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // EditDbButton
            // 
            this.EditDbButton.Location = new System.Drawing.Point(132, 208);
            this.EditDbButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.EditDbButton.Name = "EditDbButton";
            this.EditDbButton.Size = new System.Drawing.Size(249, 29);
            this.EditDbButton.TabIndex = 1;
            this.EditDbButton.Text = "Edit DB stuff";
            this.EditDbButton.UseVisualStyleBackColor = true;
            this.EditDbButton.Click += new System.EventHandler(this.EditDbButton_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.EditDbButton);
            this.Controls.Add(this.SearchButton);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button EditDbButton;
    }
}