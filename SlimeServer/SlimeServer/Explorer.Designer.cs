namespace SlimeServer
{
    partial class Explorer
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("TestDirectory", 0);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("TestFile", 1);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Explorer));
            this.FileView = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.ItemMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.ClearTempFileCheck = new System.Windows.Forms.CheckBox();
            this.ProcessStartToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.LoadingPanel.SuspendLayout();
            this.FileContextMenu.SuspendLayout();
            this.DirectoryContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileView
            // 
            this.FileView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.FileView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FileView.ForeColor = System.Drawing.Color.White;
            this.FileView.HideSelection = false;
            this.FileView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.FileView.LargeImageList = this.imageList;
            this.FileView.Location = new System.Drawing.Point(12, 41);
            this.FileView.MultiSelect = false;
            this.FileView.Name = "FileView";
            this.FileView.Size = new System.Drawing.Size(608, 679);
            this.FileView.SmallImageList = this.imageList;
            this.FileView.TabIndex = 0;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.View = System.Windows.Forms.View.Tile;
            this.FileView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ItemMouseClick);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "folder.ico");
            this.imageList.Images.SetKeyName(1, "file.ico");
            this.imageList.Images.SetKeyName(2, "select.ico");
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LoadingPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadingPanel.Controls.Add(this.label1);
            this.LoadingPanel.Controls.Add(this.progressBar1);
            this.LoadingPanel.Location = new System.Drawing.Point(76, 337);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(480, 59);
            this.LoadingPanel.TabIndex = 6;
            this.LoadingPanel.Visible = false;
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
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 24);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(466, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // PathTextBox
            // 
            this.PathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PathTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.PathTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PathTextBox.ForeColor = System.Drawing.Color.White;
            this.PathTextBox.Location = new System.Drawing.Point(12, 10);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(538, 25);
            this.PathTextBox.TabIndex = 7;
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button.ForeColor = System.Drawing.Color.White;
            this.button.Location = new System.Drawing.Point(556, 10);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(64, 25);
            this.button.TabIndex = 8;
            this.button.Text = "Enter";
            this.button.UseVisualStyleBackColor = true;
            // 
            // ItemMenuStrip
            // 
            this.ItemMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.ItemMenuStrip.Name = "ItemMenuStrip";
            this.ItemMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ItemMenuStrip.Size = new System.Drawing.Size(61, 4);
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
            this.FileRemoveToolStrip,
            this.ProcessStartToolStrip});
            this.FileContextMenu.Name = "FileContextMenu";
            this.FileContextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.FileContextMenu.Size = new System.Drawing.Size(149, 158);
            // 
            // FileNameToolStrip
            // 
            this.FileNameToolStrip.Name = "FileNameToolStrip";
            this.FileNameToolStrip.Size = new System.Drawing.Size(148, 22);
            this.FileNameToolStrip.Text = "FileName";
            // 
            // FileUploadToolStrip
            // 
            this.FileUploadToolStrip.Name = "FileUploadToolStrip";
            this.FileUploadToolStrip.Size = new System.Drawing.Size(148, 22);
            this.FileUploadToolStrip.Text = "Upload";
            // 
            // FileRenameToolStrip
            // 
            this.FileRenameToolStrip.Name = "FileRenameToolStrip";
            this.FileRenameToolStrip.Size = new System.Drawing.Size(148, 22);
            this.FileRenameToolStrip.Text = "Rename";
            // 
            // FileMoveToolStrip
            // 
            this.FileMoveToolStrip.Name = "FileMoveToolStrip";
            this.FileMoveToolStrip.Size = new System.Drawing.Size(148, 22);
            this.FileMoveToolStrip.Text = "Move";
            // 
            // FilePropertyToolStrip
            // 
            this.FilePropertyToolStrip.Name = "FilePropertyToolStrip";
            this.FilePropertyToolStrip.Size = new System.Drawing.Size(148, 22);
            this.FilePropertyToolStrip.Text = "Property";
            // 
            // FileRemoveToolStrip
            // 
            this.FileRemoveToolStrip.Name = "FileRemoveToolStrip";
            this.FileRemoveToolStrip.Size = new System.Drawing.Size(148, 22);
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
            this.DirectoryContextMenu.Size = new System.Drawing.Size(157, 70);
            // 
            // DirectoryNameToolStrip
            // 
            this.DirectoryNameToolStrip.Name = "DirectoryNameToolStrip";
            this.DirectoryNameToolStrip.Size = new System.Drawing.Size(156, 22);
            this.DirectoryNameToolStrip.Text = "DirectoryName";
            // 
            // DirectoryMoveToolStrip
            // 
            this.DirectoryMoveToolStrip.Name = "DirectoryMoveToolStrip";
            this.DirectoryMoveToolStrip.Size = new System.Drawing.Size(156, 22);
            this.DirectoryMoveToolStrip.Text = "Move";
            // 
            // DirectoryRemoveToolStrip
            // 
            this.DirectoryRemoveToolStrip.Name = "DirectoryRemoveToolStrip";
            this.DirectoryRemoveToolStrip.Size = new System.Drawing.Size(156, 22);
            this.DirectoryRemoveToolStrip.Text = "Remove";
            // 
            // ClearTempFileCheck
            // 
            this.ClearTempFileCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearTempFileCheck.AutoSize = true;
            this.ClearTempFileCheck.Checked = true;
            this.ClearTempFileCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ClearTempFileCheck.Location = new System.Drawing.Point(499, 699);
            this.ClearTempFileCheck.Name = "ClearTempFileCheck";
            this.ClearTempFileCheck.Size = new System.Drawing.Size(121, 21);
            this.ClearTempFileCheck.TabIndex = 12;
            this.ClearTempFileCheck.Text = "ClearTempFile";
            this.ClearTempFileCheck.UseVisualStyleBackColor = true;
            // 
            // ProcessStartToolStrip
            // 
            this.ProcessStartToolStrip.Name = "ProcessStartToolStrip";
            this.ProcessStartToolStrip.Size = new System.Drawing.Size(148, 22);
            this.ProcessStartToolStrip.Text = "Process Start";
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(632, 732);
            this.Controls.Add(this.ClearTempFileCheck);
            this.Controls.Add(this.button);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.LoadingPanel);
            this.Controls.Add(this.FileView);
            this.Font = new System.Drawing.Font("Arial", 11.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Explorer";
            this.Text = "Explorer";
            this.LoadingPanel.ResumeLayout(false);
            this.FileContextMenu.ResumeLayout(false);
            this.DirectoryContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button button;
        public System.Windows.Forms.ListView FileView;
        public System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.ContextMenuStrip ItemMenuStrip;
        private System.Windows.Forms.ContextMenuStrip FileContextMenu;
        public System.Windows.Forms.ToolStripMenuItem FileNameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileUploadToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileRenameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileMoveToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FilePropertyToolStrip;
        public System.Windows.Forms.ToolStripMenuItem FileRemoveToolStrip;
        private System.Windows.Forms.ContextMenuStrip DirectoryContextMenu;
        public System.Windows.Forms.ToolStripMenuItem DirectoryNameToolStrip;
        public System.Windows.Forms.ToolStripMenuItem DirectoryMoveToolStrip;
        public System.Windows.Forms.ToolStripMenuItem DirectoryRemoveToolStrip;
        private System.Windows.Forms.CheckBox ClearTempFileCheck;
        public System.Windows.Forms.ToolStripMenuItem ProcessStartToolStrip;
    }
}