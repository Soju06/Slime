namespace SlimeServer
{
    partial class ServerLogControl
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "MM/dd/yyyy H:mm:ss",
            "TestCode",
            "Wa! Nokduro!"}, -1);
            this.logView = new System.Windows.Forms.ListView();
            this.Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MessageType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.logView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Time,
            this.MessageType,
            this.Value,
            this.IP});
            this.logView.ForeColor = System.Drawing.Color.White;
            this.logView.FullRowSelect = true;
            this.logView.HideSelection = false;
            this.logView.ImeMode = System.Windows.Forms.ImeMode.On;
            this.logView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.logView.Location = new System.Drawing.Point(10, 10);
            this.logView.Margin = new System.Windows.Forms.Padding(15);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(1298, 592);
            this.logView.TabIndex = 0;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.Details;
            // 
            // Time
            // 
            this.Time.Text = "Time";
            this.Time.Width = 168;
            // 
            // MessageType
            // 
            this.MessageType.Text = "MessageType";
            this.MessageType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MessageType.Width = 153;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 819;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IP.Width = 156;
            // 
            // ServerLogControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.logView);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ServerLogControl";
            this.Size = new System.Drawing.Size(1318, 612);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColumnHeader Time;
        private System.Windows.Forms.ColumnHeader MessageType;
        private System.Windows.Forms.ColumnHeader Value;
        public System.Windows.Forms.ListView logView;
        private System.Windows.Forms.ColumnHeader IP;
    }
}
