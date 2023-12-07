namespace LoginInterface
{
    partial class Form2
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
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.textBoxAI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvRV = new System.Windows.Forms.DataGridView();
            this.RecomendedLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRV)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxSN
            // 
            this.textBoxSN.Location = new System.Drawing.Point(159, 104);
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(167, 22);
            this.textBoxSN.TabIndex = 0;
            // 
            // textBoxD
            // 
            this.textBoxD.Location = new System.Drawing.Point(159, 148);
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.Size = new System.Drawing.Size(167, 22);
            this.textBoxD.TabIndex = 1;
            // 
            // textBoxAI
            // 
            this.textBoxAI.Location = new System.Drawing.Point(159, 199);
            this.textBoxAI.Name = "textBoxAI";
            this.textBoxAI.Size = new System.Drawing.Size(167, 22);
            this.textBoxAI.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Software Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Software Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Software Type:";
            // 
            // dgvRV
            // 
            this.dgvRV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRV.Location = new System.Drawing.Point(368, 98);
            this.dgvRV.Name = "dgvRV";
            this.dgvRV.RowHeadersWidth = 51;
            this.dgvRV.RowTemplate.Height = 24;
            this.dgvRV.Size = new System.Drawing.Size(403, 242);
            this.dgvRV.TabIndex = 6;
            // 
            // RecomendedLabel
            // 
            this.RecomendedLabel.AutoSize = true;
            this.RecomendedLabel.Location = new System.Drawing.Point(365, 68);
            this.RecomendedLabel.Name = "RecomendedLabel";
            this.RecomendedLabel.Size = new System.Drawing.Size(147, 16);
            this.RecomendedLabel.TabIndex = 7;
            this.RecomendedLabel.Text = "Recomended Vendors:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RecomendedLabel);
            this.Controls.Add(this.dgvRV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxAI);
            this.Controls.Add(this.textBoxD);
            this.Controls.Add(this.textBoxSN);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.TextBox textBoxD;
        private System.Windows.Forms.TextBox textBoxAI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvRV;
        private System.Windows.Forms.Label RecomendedLabel;
    }
}