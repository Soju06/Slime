namespace SlimeServer
{
    partial class NodeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeView));
            this.ViewNode = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.FileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FileNameToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FileUploadToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FileRenameToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FileMoveToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FilePropertyToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.FileRemoveToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DirectoryNameToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryMoveToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.DirectoryRemoveToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.LoadingPanel.SuspendLayout();
            this.FileContextMenu.SuspendLayout();
            this.DirectoryContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewNode
            // 
            this.ViewNode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewNode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.ViewNode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ViewNode.ForeColor = System.Drawing.Color.White;
            this.ViewNode.ImageIndex = 0;
            this.ViewNode.ImageList = this.imageList;
            this.ViewNode.Location = new System.Drawing.Point(12, 12);
            this.ViewNode.Name = "ViewNode";
            this.ViewNode.SelectedImageIndex = 2;
            this.ViewNode.Size = new System.Drawing.Size(557, 747);
            this.ViewNode.TabIndex = 0;
            this.ViewNode.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ViewNodeClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.ico");
            this.imageList.Images.SetKeyName(1, "file.ico");
            this.imageList.Images.SetKeyName(2, "select.ico");
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 24);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(466, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "0%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadingPanel.Controls.Add(this.label1);
            this.LoadingPanel.Controls.Add(this.progressBar1);
            this.LoadingPanel.Location = new System.Drawing.Point(44, 344);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(480, 59);
            this.LoadingPanel.TabIndex = 5;
            // 
            // FileContextMenu
            // 
            this.FileContextMenu.BackColor = System.Drawing.SystemColors.Control;
            this.FileContextMenu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNameToolStrip,
            this.FileUploadToolStrip,
            this.FileRenameToolStrip,
            this.FileMoveToolStrip,
            this.FilePropertyToolStrip,
            this.FileRemoveToolStrip});
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.FileContextMenu.Size = new System.Drawing.Size(129, 136);
            // 
            // FileNameToolStrip
            // 
            this.FileNameToolStrip.Name = "FileNameToolStrip";
            this.FileNameToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FileNameToolStrip.Text = "FileName";
            this.FileNameToolStrip.Click += new System.EventHandler(this.PathClick);
            // 
            // FileUploadToolStrip
            // 
            this.FileUploadToolStrip.Name = "FileUploadToolStrip";
            this.FileUploadToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FileUploadToolStrip.Text = "Upload";
            // 
            // FileRenameToolStrip
            // 
            this.FileRenameToolStrip.Name = "FileRenameToolStrip";
            this.FileRenameToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FileRenameToolStrip.Text = "Rename";
            // 
            // FileMoveToolStrip
            // 
            this.FileMoveToolStrip.Name = "FileMoveToolStrip";
            this.FileMoveToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FileMoveToolStrip.Text = "Move";
            // 
            // FilePropertyToolStrip
            // 
            this.FilePropertyToolStrip.Name = "FilePropertyToolStrip";
            this.FilePropertyToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FilePropertyToolStrip.Text = "Property";
            // 
            // FileRemoveToolStrip
            // 
            this.FileRemoveToolStrip.Name = "FileRemoveToolStrip";
            this.FileRemoveToolStrip.Size = new System.Drawing.Size(128, 22);
            this.FileRemoveToolStrip.Text = "Remove";
            // 
            // DirectoryContextMenu
            // 
            this.DirectoryContextMenu.BackColor = System.Drawing.SystemColors.Control;
            this.DirectoryContextMenu.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DirectoryNameToolStrip,
            this.DirectoryMoveToolStrip,
            this.DirectoryRemoveToolStrip});
            this.DirectoryContextMenu.Name = "FileContextMenu";
            this.DirectoryContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.DirectoryContextMenu.Size = new System.Drawing.Size(181, 92);
            // 
            // DirectoryNameToolStrip
            // 
            this.DirectoryNameToolStrip.Name = "DirectoryNameToolStrip";
            this.DirectoryNameToolStrip.Size = new System.Drawing.Size(180, 22);
            this.DirectoryNameToolStrip.Text = "DirectoryName";
            this.DirectoryNameToolStrip.Click += new System.EventHandler(this.PathClick);
            // 
            // DirectoryMoveToolStrip
            // 
            this.DirectoryMoveToolStrip.Name = "DirectoryMoveToolStrip";
            this.DirectoryMoveToolStrip.Size = new System.Drawing.Size(180, 22);
            this.DirectoryMoveToolStrip.Text = "Move";
            // 
            // DirectoryRemoveToolStrip
            // 
            this.DirectoryRemoveToolStrip.Name = "DirectoryRemoveToolStrip";
            this.DirectoryRemoveToolStrip.Size = new System.Drawing.Size(180, 22);
            this.DirectoryRemoveToolStrip.Text = "Remove";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(400, 651);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 108);
            this.label2.TabIndex = 8;
            this.label2.Text = "PCID:\r\n000000\r\nStartTime:\r\n0000-00-00 00:00:00\r\nEndTime:\r\n0000-00-00 00:00:00\r\n";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(581, 771);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoadingPanel);
            this.Controls.Add(this.ViewNode);
            this.Font = new System.Drawing.Font("Arial", 11.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NodeView";
            this.Text = "NodeView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NodeView_FormClosing);
            this.LoadingPanel.ResumeLayout(false);
            this.FileContextMenu.ResumeLayout(false);
            this.DirectoryContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.ContextMenuStrip FileContextMenu;
        private System.Windows.Forms.ContextMenuStrip DirectoryContextMenu;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ToolStripMenuItem FileUploadToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileNameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FilePropertyToolStrip;
        public System.Windows.Forms.TreeView ViewNode;
        public System.Windows.Forms.ToolStripMenuItem FileRenameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileMoveToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileRemoveToolStrip;
        public System.Windows.Forms.ToolStripMenuItem DirectoryMoveToolStrip;
        public System.Windows.Forms.ToolStripMenuItem DirectoryNameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem DirectoryRemoveToolStrip;
    }
}