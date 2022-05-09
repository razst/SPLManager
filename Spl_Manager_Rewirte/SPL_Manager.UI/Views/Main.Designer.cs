using System;

namespace SPL_Manager.UI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportToAFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportToAFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabQuery = new System.Windows.Forms.TabPage();
            this.QueryTabControl = new SPL_Manager.UI.Views.QueryTabViews.QueryTab();
            this.RxTab = new System.Windows.Forms.TabPage();
            this.RxTabControl = new SPL_Manager.UI.Views.RxTabViews.RxTab();
            this.TxTab = new System.Windows.Forms.TabPage();
            this.TxTabControl = new SPL_Manager.UI.Views.TxTabViews.TxTab();
            this.MainTab = new System.Windows.Forms.TabPage();
            this.MainTabControl = new SPL_Manager.UI.Views.MainTabViews.MainTab();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabQuery.SuspendLayout();
            this.RxTab.SuspendLayout();
            this.TxTab.SuspendLayout();
            this.MainTab.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1271, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // modifyToolStripMenuItem
            // 
            this.modifyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tXToolStripMenuItem,
            this.rXToolStripMenuItem});
            this.modifyToolStripMenuItem.Name = "modifyToolStripMenuItem";
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.modifyToolStripMenuItem.Text = "modify";
            // 
            // tXToolStripMenuItem
            // 
            this.tXToolStripMenuItem.Name = "tXToolStripMenuItem";
            this.tXToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.tXToolStripMenuItem.Text = "TX";
            // 
            // rXToolStripMenuItem
            // 
            this.rXToolStripMenuItem.Name = "rXToolStripMenuItem";
            this.rXToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.rXToolStripMenuItem.Text = "RX";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExportToAFileMenuItem,
            this.ExportToAFolderMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(53, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // ExportToAFileMenuItem
            // 
            this.ExportToAFileMenuItem.Name = "ExportToAFileMenuItem";
            this.ExportToAFileMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExportToAFileMenuItem.Text = "to a file";
            this.ExportToAFileMenuItem.Click += new System.EventHandler(this.ExportToAFileMenuItem_Click);
            // 
            // ExportToAFolderMenuItem
            // 
            this.ExportToAFolderMenuItem.Name = "ExportToAFolderMenuItem";
            this.ExportToAFolderMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExportToAFolderMenuItem.Text = "to a folder";
            this.ExportToAFolderMenuItem.Click += new System.EventHandler(this.ExportToAFolderMenuItem_Click);
            // 
            // tabQuery
            // 
            this.tabQuery.Controls.Add(this.QueryTabControl);
            this.tabQuery.Location = new System.Drawing.Point(4, 24);
            this.tabQuery.Name = "tabQuery";
            this.tabQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuery.Size = new System.Drawing.Size(1263, 599);
            this.tabQuery.TabIndex = 3;
            this.tabQuery.Text = "Query";
            this.tabQuery.UseVisualStyleBackColor = true;
            // 
            // QueryTabControl
            // 
            this.QueryTabControl.Location = new System.Drawing.Point(3, 6);
            this.QueryTabControl.Name = "QueryTabControl";
            this.QueryTabControl.Size = new System.Drawing.Size(1300, 550);
            this.QueryTabControl.TabIndex = 0;
            // 
            // RxTab
            // 
            this.RxTab.Controls.Add(this.RxTabControl);
            this.RxTab.Location = new System.Drawing.Point(4, 24);
            this.RxTab.Name = "RxTab";
            this.RxTab.Padding = new System.Windows.Forms.Padding(3);
            this.RxTab.Size = new System.Drawing.Size(1263, 599);
            this.RxTab.TabIndex = 1;
            this.RxTab.Text = "RX";
            this.RxTab.UseVisualStyleBackColor = true;
            // 
            // RxTabControl
            // 
            this.RxTabControl.Location = new System.Drawing.Point(0, 6);
            this.RxTabControl.Name = "RxTabControl";
            this.RxTabControl.RxPacketHex = "";
            this.RxTabControl.RxItemsIndex = -1;
            this.RxTabControl.TxItemsIndex = -1;
            this.RxTabControl.Size = new System.Drawing.Size(1250, 558);
            this.RxTabControl.TabIndex = 0;
            // 
            // TxTab
            // 
            this.TxTab.Controls.Add(this.TxTabControl);
            this.TxTab.Location = new System.Drawing.Point(4, 24);
            this.TxTab.Name = "TxTab";
            this.TxTab.Padding = new System.Windows.Forms.Padding(3);
            this.TxTab.Size = new System.Drawing.Size(1263, 599);
            this.TxTab.TabIndex = 0;
            this.TxTab.Text = "TX";
            this.TxTab.UseVisualStyleBackColor = true;
            // 
            // TxTabControl
            // 
            this.TxTabControl.CmdSleepValue = "";
            this.TxTabControl.Location = new System.Drawing.Point(6, 6);
            this.TxTabControl.Name = "TxTabControl";
            this.TxTabControl.PlayListItems = ((System.Collections.Generic.List<string>)(resources.GetObject("TxTabControl.PlayListItems")));
            this.TxTabControl.PlaylistsNames = ((System.Collections.Generic.List<string>)(resources.GetObject("TxTabControl.PlaylistsNames")));
            this.TxTabControl.SelectedPlaylist = -1;
            this.TxTabControl.SelectedPlaylistItem = -1;
            this.TxTabControl.Size = new System.Drawing.Size(1260, 600);
            this.TxTabControl.TabIndex = 0;
            this.TxTabControl.TxCurrentSubtypeIndex = -1;
            this.TxTabControl.TxCurrentTypeIndex = -1;
            this.TxTabControl.TxPacketHexStr = "";
            // 
            // MainTab
            // 
            this.MainTab.BackColor = System.Drawing.Color.Black;
            this.MainTab.Controls.Add(this.MainTabControl);
            this.MainTab.Location = new System.Drawing.Point(4, 24);
            this.MainTab.Name = "MainTab";
            this.MainTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainTab.Size = new System.Drawing.Size(1263, 599);
            this.MainTab.TabIndex = 4;
            this.MainTab.Text = "Main";
            // 
            // MainTabControl
            // 
            this.MainTabControl.BackColor = System.Drawing.SystemColors.Desktop;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.Size = new System.Drawing.Size(1283, 613);
            this.MainTabControl.TabIndex = 0;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.MainTab);
            this.TabControl.Controls.Add(this.TxTab);
            this.TabControl.Controls.Add(this.RxTab);
            this.TabControl.Controls.Add(this.tabQuery);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(1271, 627);
            this.TabControl.TabIndex = 17;
            // 
            // Main
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1271, 651);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPL Manager";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabQuery.ResumeLayout(false);
            this.RxTab.ResumeLayout(false);
            this.TxTab.ResumeLayout(false);
            this.MainTab.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportToAFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExportToAFolderMenuItem;
        private System.Windows.Forms.TabPage tabQuery;
        public UI.Views.QueryTabViews.QueryTab QueryTabControl;
        private System.Windows.Forms.TabPage RxTab;
        public UI.Views.RxTabViews.RxTab RxTabControl;
        private System.Windows.Forms.TabPage TxTab;
        public UI.Views.TxTabViews.TxTab TxTabControl;
        private System.Windows.Forms.TabPage MainTab;
        public UI.Views.MainTabViews.MainTab MainTabControl;
        private System.Windows.Forms.TabControl TabControl;
    }
}

