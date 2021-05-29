namespace SlimeServer
{
    partial class SelectControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectControl));
            this.TSL = new System.Windows.Forms.Panel();
            this.TButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TSL
            // 
            this.TSL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(170)))), ((int)(((byte)(255)))));
            this.TSL.Location = new System.Drawing.Point(0, 4);
            this.TSL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TSL.Name = "TSL";
            this.TSL.Size = new System.Drawing.Size(2, 40);
            this.TSL.TabIndex = 6;
            // 
            // TButton
            // 
            this.TButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.TButton.FlatAppearance.BorderSize = 0;
            this.TButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TButton.Image = ((System.Drawing.Image)(resources.GetObject("TButton.Image")));
            this.TButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.TButton.Location = new System.Drawing.Point(3, 4);
            this.TButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TButton.Name = "TButton";
            this.TButton.Size = new System.Drawing.Size(192, 40);
            this.TButton.TabIndex = 7;
            this.TButton.Text = "Text    ";
            this.TButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TButton.UseVisualStyleBackColor = false;
            this.TButton.Click += new System.EventHandler(this.TButton_Click);
            // 
            // SelectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.Controls.Add(this.TSL);
            this.Controls.Add(this.TButton);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(200, 48);
            this.MinimumSize = new System.Drawing.Size(200, 48);
            this.Name = "SelectControl";
            this.Size = new System.Drawing.Size(200, 48);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TSL;
        private System.Windows.Forms.Button TButton;
    }
}
