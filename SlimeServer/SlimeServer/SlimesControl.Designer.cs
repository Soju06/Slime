namespace SlimeServer
{
    partial class SlimesControl
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
            this.FlowLayoutSlimePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // FlowLayoutSlimePanel
            // 
            this.FlowLayoutSlimePanel.AutoSize = true;
            this.FlowLayoutSlimePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutSlimePanel.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutSlimePanel.Margin = new System.Windows.Forms.Padding(0);
            this.FlowLayoutSlimePanel.Name = "FlowLayoutSlimePanel";
            this.FlowLayoutSlimePanel.Size = new System.Drawing.Size(1318, 612);
            this.FlowLayoutSlimePanel.TabIndex = 0;
            // 
            // SlimesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.FlowLayoutSlimePanel);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SlimesControl";
            this.Size = new System.Drawing.Size(1318, 612);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel FlowLayoutSlimePanel;
    }
}
