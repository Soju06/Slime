namespace SlimeServer
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LeftControlPanel = new System.Windows.Forms.Panel();
            this.DrawingPanel = new System.Windows.Forms.Panel();
            this.TablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.ExitButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ServerLogControl = new SlimeServer.SelectControl();
            this.SlimesControl = new SlimeServer.SelectControl();
            this.sCreateControl = new SlimeServer.SelectControl();
            this.LeftControlPanel.SuspendLayout();
            this.TablePanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftControlPanel
            // 
            this.LeftControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.LeftControlPanel.Controls.Add(this.panel1);
            this.LeftControlPanel.Controls.Add(this.ServerLogControl);
            this.LeftControlPanel.Controls.Add(this.SlimesControl);
            this.LeftControlPanel.Controls.Add(this.sCreateControl);
            this.LeftControlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftControlPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LeftControlPanel.Name = "LeftControlPanel";
            this.LeftControlPanel.Size = new System.Drawing.Size(189, 773);
            this.LeftControlPanel.TabIndex = 0;
            // 
            // DrawingPanel
            // 
            this.DrawingPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.DrawingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingPanel.ForeColor = System.Drawing.Color.White;
            this.DrawingPanel.Location = new System.Drawing.Point(189, 0);
            this.DrawingPanel.Margin = new System.Windows.Forms.Padding(0);
            this.DrawingPanel.Name = "DrawingPanel";
            this.DrawingPanel.Size = new System.Drawing.Size(1437, 773);
            this.DrawingPanel.TabIndex = 1;
            // 
            // TablePanel
            // 
            this.TablePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablePanel.ColumnCount = 2;
            this.TablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 189F));
            this.TablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TablePanel.Controls.Add(this.DrawingPanel, 1, 0);
            this.TablePanel.Controls.Add(this.LeftControlPanel, 0, 0);
            this.TablePanel.Location = new System.Drawing.Point(3, 35);
            this.TablePanel.Margin = new System.Windows.Forms.Padding(0, 35, 0, 0);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.RowCount = 1;
            this.TablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TablePanel.Size = new System.Drawing.Size(1626, 773);
            this.TablePanel.TabIndex = 2;
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(149)))), ((int)(((byte)(38)))));
            this.TopPanel.Controls.Add(this.ExitButton);
            this.TopPanel.Controls.Add(this.TextLabel);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.ForeColor = System.Drawing.Color.White;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(1632, 35);
            this.TopPanel.TabIndex = 3;
            this.TopPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMouseDown);
            this.TopPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMouseMove);
            // 
            // ExitButton
            // 
            this.ExitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExitButton.FlatAppearance.BorderSize = 0;
            this.ExitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(219)))), ((int)(((byte)(182)))));
            this.ExitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(184)))), ((int)(((byte)(110)))));
            this.ExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Image = ((System.Drawing.Image)(resources.GetObject("ExitButton.Image")));
            this.ExitButton.Location = new System.Drawing.Point(1585, 0);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(47, 35);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.CloseClick);
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLabel.Location = new System.Drawing.Point(10, 8);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(45, 19);
            this.TextLabel.TabIndex = 0;
            this.TextLabel.Text = "Main";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(186, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(3, 773);
            this.panel1.TabIndex = 1;
            // 
            // ServerLogControl
            // 
            this.ServerLogControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.ServerLogControl.Checked = false;
            this.ServerLogControl.ControlTag = "ServerLogControl";
            this.ServerLogControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerLogControl.ForeColor = System.Drawing.Color.White;
            this.ServerLogControl.ForeText = "ServerLog            ";
            this.ServerLogControl.Image = ((System.Drawing.Image)(resources.GetObject("ServerLogControl.Image")));
            this.ServerLogControl.Location = new System.Drawing.Point(5, 123);
            this.ServerLogControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ServerLogControl.MaximumSize = new System.Drawing.Size(200, 48);
            this.ServerLogControl.MinimumSize = new System.Drawing.Size(200, 48);
            this.ServerLogControl.Name = "ServerLogControl";
            this.ServerLogControl.Size = new System.Drawing.Size(200, 48);
            this.ServerLogControl.TabIndex = 0;
            this.ServerLogControl.sClick += new SlimeServer.SelectControl.SClick(this.MenuClick);
            // 
            // SlimesControl
            // 
            this.SlimesControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.SlimesControl.Checked = true;
            this.SlimesControl.ControlTag = "SlimesControl";
            this.SlimesControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SlimesControl.ForeColor = System.Drawing.Color.White;
            this.SlimesControl.ForeText = "Slimes                 ";
            this.SlimesControl.Image = ((System.Drawing.Image)(resources.GetObject("SlimesControl.Image")));
            this.SlimesControl.Location = new System.Drawing.Point(5, 67);
            this.SlimesControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SlimesControl.MaximumSize = new System.Drawing.Size(200, 48);
            this.SlimesControl.MinimumSize = new System.Drawing.Size(200, 48);
            this.SlimesControl.Name = "SlimesControl";
            this.SlimesControl.Size = new System.Drawing.Size(200, 48);
            this.SlimesControl.TabIndex = 0;
            this.SlimesControl.sClick += new SlimeServer.SelectControl.SClick(this.MenuClick);
            // 
            // sCreateControl
            // 
            this.sCreateControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(22)))), ((int)(((byte)(22)))));
            this.sCreateControl.Checked = false;
            this.sCreateControl.ControlTag = "CreateControl";
            this.sCreateControl.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sCreateControl.ForeColor = System.Drawing.Color.White;
            this.sCreateControl.ForeText = "Create                 ";
            this.sCreateControl.Image = ((System.Drawing.Image)(resources.GetObject("sCreateControl.Image")));
            this.sCreateControl.Location = new System.Drawing.Point(5, 11);
            this.sCreateControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sCreateControl.MaximumSize = new System.Drawing.Size(200, 48);
            this.sCreateControl.MinimumSize = new System.Drawing.Size(200, 48);
            this.sCreateControl.Name = "sCreateControl";
            this.sCreateControl.Size = new System.Drawing.Size(200, 48);
            this.sCreateControl.TabIndex = 0;
            this.sCreateControl.sClick += new SlimeServer.SelectControl.SClick(this.MenuClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1632, 811);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.TablePanel);
            this.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThisClosing);
            this.LeftControlPanel.ResumeLayout(false);
            this.TablePanel.ResumeLayout(false);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SelectControl sCreateControl;
        private SelectControl SlimesControl;
        private SelectControl ServerLogControl;
        private System.Windows.Forms.Panel LeftControlPanel;
        private System.Windows.Forms.Panel DrawingPanel;
        private System.Windows.Forms.TableLayoutPanel TablePanel;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Panel panel1;
    }
}

