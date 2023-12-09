namespace LoginInterface
{
    partial class AccountCreationForm
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
            this.EmailBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.PassLabel = new System.Windows.Forms.Label();
            this.BackButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EmailBox
            // 
            this.EmailBox.Location = new System.Drawing.Point(305, 85);
            this.EmailBox.Name = "EmailBox";
            this.EmailBox.Size = new System.Drawing.Size(182, 22);
            this.EmailBox.TabIndex = 0;
            this.EmailBox.TextChanged += new System.EventHandler(this.EmailBox_TextChanged);
            // 
            // PasswordBox
            // 
            this.PasswordBox.Location = new System.Drawing.Point(305, 145);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.Size = new System.Drawing.Size(182, 22);
            this.PasswordBox.TabIndex = 1;
            this.PasswordBox.TextChanged += new System.EventHandler(this.PasswordBox_TextChanged);
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(55, 91);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(200, 16);
            this.EmailLabel.TabIndex = 2;
            this.EmailLabel.Text = "Please enter your desired email:";
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Location = new System.Drawing.Point(55, 151);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(226, 16);
            this.PassLabel.TabIndex = 3;
            this.PassLabel.Text = "Please enter your desired password:";
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(69, 354);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(137, 23);
            this.BackButton.TabIndex = 4;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(370, 205);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(116, 23);
            this.CreateButton.TabIndex = 5;
            this.CreateButton.Text = "Create Account";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // AccountCreation
            // 
            this.AcceptButton = this.CreateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CreateButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.PassLabel);
            this.Controls.Add(this.EmailLabel);
            this.Controls.Add(this.PasswordBox);
            this.Controls.Add(this.EmailBox);
            this.Name = "AccountCreation";
            this.Text = "AccountCreation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountCreation_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EmailBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.Label PassLabel;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button CreateButton;
    }
}