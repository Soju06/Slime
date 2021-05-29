namespace SlimeServer
{
    partial class FileInfoForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LocationLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SizeLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.LastWriteTime = new System.Windows.Forms.Label();
            this.Exists = new System.Windows.Forms.Label();
            this.CreationTime = new System.Windows.Forms.Label();
            this.ErrorTextBox = new System.Windows.Forms.TextBox();
            this.LastAccessTime = new System.Windows.Forms.Label();
            this.IsReadOnlyLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(296, 501);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.NameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NameTextBox.ForeColor = System.Drawing.Color.White;
            this.NameTextBox.Location = new System.Drawing.Point(173, 28);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(214, 26);
            this.NameTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Location:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LocationLabel
            // 
            this.LocationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LocationLabel.Location = new System.Drawing.Point(171, 77);
            this.LocationLabel.Name = "LocationLabel";
            this.LocationLabel.Size = new System.Drawing.Size(216, 26);
            this.LocationLabel.TabIndex = 2;
            this.LocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Size:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SizeLabel
            // 
            this.SizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SizeLabel.Location = new System.Drawing.Point(171, 128);
            this.SizeLabel.Name = "SizeLabel";
            this.SizeLabel.Size = new System.Drawing.Size(216, 26);
            this.SizeLabel.TabIndex = 2;
            this.SizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 179);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 26);
            this.label4.TabIndex = 2;
            this.label4.Text = "Creation Time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(152, 26);
            this.label5.TabIndex = 2;
            this.label5.Text = "LastWrite Time:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 281);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 26);
            this.label6.TabIndex = 2;
            this.label6.Text = "LastAccess Time:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LastWriteTime
            // 
            this.LastWriteTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LastWriteTime.Location = new System.Drawing.Point(170, 230);
            this.LastWriteTime.Name = "LastWriteTime";
            this.LastWriteTime.Size = new System.Drawing.Size(216, 26);
            this.LastWriteTime.TabIndex = 2;
            this.LastWriteTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Exists
            // 
            this.Exists.Location = new System.Drawing.Point(12, 501);
            this.Exists.Name = "Exists";
            this.Exists.Size = new System.Drawing.Size(109, 26);
            this.Exists.TabIndex = 2;
            this.Exists.Text = "Exists: ";
            this.Exists.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CreationTime
            // 
            this.CreationTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.CreationTime.Location = new System.Drawing.Point(170, 179);
            this.CreationTime.Name = "CreationTime";
            this.CreationTime.Size = new System.Drawing.Size(216, 26);
            this.CreationTime.TabIndex = 2;
            this.CreationTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ErrorTextBox
            // 
            this.ErrorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.ErrorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorTextBox.ForeColor = System.Drawing.Color.White;
            this.ErrorTextBox.Location = new System.Drawing.Point(12, 332);
            this.ErrorTextBox.Multiline = true;
            this.ErrorTextBox.Name = "ErrorTextBox";
            this.ErrorTextBox.ReadOnly = true;
            this.ErrorTextBox.Size = new System.Drawing.Size(374, 157);
            this.ErrorTextBox.TabIndex = 3;
            // 
            // LastAccessTime
            // 
            this.LastAccessTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.LastAccessTime.Location = new System.Drawing.Point(170, 281);
            this.LastAccessTime.Name = "LastAccessTime";
            this.LastAccessTime.Size = new System.Drawing.Size(216, 26);
            this.LastAccessTime.TabIndex = 2;
            this.LastAccessTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IsReadOnlyLabel
            // 
            this.IsReadOnlyLabel.Location = new System.Drawing.Point(127, 501);
            this.IsReadOnlyLabel.Name = "IsReadOnlyLabel";
            this.IsReadOnlyLabel.Size = new System.Drawing.Size(163, 26);
            this.IsReadOnlyLabel.TabIndex = 2;
            this.IsReadOnlyLabel.Text = "IsReadOnly: ";
            this.IsReadOnlyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FileInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(398, 541);
            this.Controls.Add(this.ErrorTextBox);
            this.Controls.Add(this.LastWriteTime);
            this.Controls.Add(this.LastAccessTime);
            this.Controls.Add(this.IsReadOnlyLabel);
            this.Controls.Add(this.Exists);
            this.Controls.Add(this.SizeLabel);
            this.Controls.Add(this.CreationTime);
            this.Controls.Add(this.LocationLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(41400, 580);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(414, 580);
            this.Name = "FileInfoForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FileInfoForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LocationLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label SizeLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LastWriteTime;
        private System.Windows.Forms.Label Exists;
        private System.Windows.Forms.Label CreationTime;
        private System.Windows.Forms.TextBox ErrorTextBox;
        private System.Windows.Forms.Label LastAccessTime;
        private System.Windows.Forms.Label IsReadOnlyLabel;
    }
}