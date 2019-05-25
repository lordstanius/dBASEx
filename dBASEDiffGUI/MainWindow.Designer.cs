namespace dBASEDiffGUI
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.lbTracked = new System.Windows.Forms.ListBox();
            this.listBoxContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.dlgBrowseFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.tabWatch = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnStartTracking = new System.Windows.Forms.Button();
            this.listBoxContext.SuspendLayout();
            this.tabWatch.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTracked
            // 
            this.lbTracked.FormattingEnabled = true;
            this.lbTracked.Location = new System.Drawing.Point(18, 41);
            this.lbTracked.Name = "lbTracked";
            this.lbTracked.Size = new System.Drawing.Size(418, 134);
            this.lbTracked.TabIndex = 3;
            this.lbTracked.TabStop = false;
            this.lbTracked.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LbTracked_MouseDown);
            // 
            // listBoxContext
            // 
            this.listBoxContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeFolderToolStripMenuItem});
            this.listBoxContext.Name = "listBoxContext";
            this.listBoxContext.Size = new System.Drawing.Size(152, 26);
            // 
            // removeFolderToolStripMenuItem
            // 
            this.removeFolderToolStripMenuItem.Name = "removeFolderToolStripMenuItem";
            this.removeFolderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.removeFolderToolStripMenuItem.Text = "Remove folder";
            this.removeFolderToolStripMenuItem.Click += new System.EventHandler(this.RemoveFolderToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Folders to track:";
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.FlatAppearance.BorderSize = 0;
            this.btnAddFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFolder.Image = global::dBASEDiffGUI.Properties.Resources.OpenFolder_16x;
            this.btnAddFolder.Location = new System.Drawing.Point(305, 9);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(131, 26);
            this.btnAddFolder.TabIndex = 0;
            this.btnAddFolder.Text = "Add folder...";
            this.btnAddFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.BtnAddFolder_Click);
            // 
            // tabWatch
            // 
            this.tabWatch.Controls.Add(this.tabPage1);
            this.tabWatch.Controls.Add(this.tabPage2);
            this.tabWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWatch.Location = new System.Drawing.Point(4, 4);
            this.tabWatch.Name = "tabWatch";
            this.tabWatch.SelectedIndex = 0;
            this.tabWatch.Size = new System.Drawing.Size(449, 238);
            this.tabWatch.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lbTracked);
            this.tabPage1.Controls.Add(this.btnAddFolder);
            this.tabPage1.Controls.Add(this.btnStartTracking);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(441, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Track changes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(450, 220);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Apply changes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnStartTracking
            // 
            this.btnStartTracking.Enabled = false;
            this.btnStartTracking.Image = global::dBASEDiffGUI.Properties.Resources.Run_16x;
            this.btnStartTracking.Location = new System.Drawing.Point(305, 181);
            this.btnStartTracking.Name = "btnStartTracking";
            this.btnStartTracking.Size = new System.Drawing.Size(131, 26);
            this.btnStartTracking.TabIndex = 2;
            this.btnStartTracking.Text = "Start tracking";
            this.btnStartTracking.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnStartTracking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnStartTracking.UseVisualStyleBackColor = true;
            this.btnStartTracking.Click += new System.EventHandler(this.BtnStartTracking_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 246);
            this.Controls.Add(this.tabWatch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "dBASE Diff";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.listBoxContext.ResumeLayout(false);
            this.tabWatch.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbTracked;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStartTracking;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgBrowseFolder;
        private System.Windows.Forms.ContextMenuStrip listBoxContext;
        private System.Windows.Forms.ToolStripMenuItem removeFolderToolStripMenuItem;
        private System.Windows.Forms.TabControl tabWatch;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

