namespace LoginInterface
{
    partial class RegistrationCodeForm
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
            this.RegCodeBox = new System.Windows.Forms.TextBox();
            this.RegCodeLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RegCodeBox
            // 
            this.RegCodeBox.Location = new System.Drawing.Point(9, 96);
            this.RegCodeBox.Name = "RegCodeBox";
            this.RegCodeBox.Size = new System.Drawing.Size(311, 22);
            this.RegCodeBox.TabIndex = 0;
            this.RegCodeBox.TextChanged += new System.EventHandler(this.RegCodeBox_TextChanged);
            // 
            // RegCodeLabel
            // 
            this.RegCodeLabel.AutoSize = true;
            this.RegCodeLabel.Location = new System.Drawing.Point(6, 51);
            this.RegCodeLabel.Name = "RegCodeLabel";
            this.RegCodeLabel.Size = new System.Drawing.Size(317, 16);
            this.RegCodeLabel.TabIndex = 1;
            this.RegCodeLabel.Text = "Please enter the registration code sent to your email:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(9, 153);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 2;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Location = new System.Drawing.Point(244, 153);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 3;
            this.ConfirmButton.Text = "Confirm";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // RegistrationCodeForm
            // 
            this.AcceptButton = this.ConfirmButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 318);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.RegCodeLabel);
            this.Controls.Add(this.RegCodeBox);
            this.Name = "RegistrationCodeForm";
            this.Text = "RegistrationCodeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistrationCodeForm_FormClosing);
            this.Load += new System.EventHandler(this.RegistrationCodeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox RegCodeBox;
        private System.Windows.Forms.Label RegCodeLabel;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConfirmButton;
    }
}