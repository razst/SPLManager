namespace packet_maker
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
            this.ID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IDTxb = new System.Windows.Forms.TextBox();
            this.makeOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataTypesDGV = new System.Windows.Forms.DataGridView();
            this.dataTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descSubType = new System.Windows.Forms.Label();
            this.descType = new System.Windows.Forms.Label();
            this.subtypeCB = new System.Windows.Forms.ComboBox();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.trasBtn = new System.Windows.Forms.Button();
            this.transIn = new System.Windows.Forms.TextBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TxTab = new System.Windows.Forms.TabPage();
            this.sendPacketBtn = new System.Windows.Forms.Button();
            this.copyBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupsCB = new System.Windows.Forms.ComboBox();
            this.RxTab = new System.Windows.Forms.TabPage();
            this.connectBtn = new System.Windows.Forms.Button();
            this.transOut = new System.Windows.Forms.ListBox();
            this.pasteBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.privHex = new System.Windows.Forms.ListBox();
            this.ImageTab = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sendImgReqBtn = new System.Windows.Forms.Button();
            this.imgTypeCB = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewPacketListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).BeginInit();
            this.TabControl.SuspendLayout();
            this.TxTab.SuspendLayout();
            this.RxTab.SuspendLayout();
            this.ImageTab.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(226, 28);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(21, 13);
            this.ID.TabIndex = 2;
            this.ID.Text = "ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(559, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subtype:";
            // 
            // IDTxb
            // 
            this.IDTxb.Location = new System.Drawing.Point(253, 21);
            this.IDTxb.Name = "IDTxb";
            this.IDTxb.Size = new System.Drawing.Size(84, 20);
            this.IDTxb.TabIndex = 0;
            this.IDTxb.Text = "100";
            // 
            // makeOut
            // 
            this.makeOut.Location = new System.Drawing.Point(70, 367);
            this.makeOut.Name = "makeOut";
            this.makeOut.Size = new System.Drawing.Size(604, 20);
            this.makeOut.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 374);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Output:";
            // 
            // dataTypesDGV
            // 
            this.dataTypesDGV.AllowUserToAddRows = false;
            this.dataTypesDGV.AllowUserToDeleteRows = false;
            this.dataTypesDGV.AllowUserToResizeColumns = false;
            this.dataTypesDGV.AllowUserToResizeRows = false;
            this.dataTypesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTypesDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataTypes,
            this.value,
            this.desc});
            this.dataTypesDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataTypesDGV.Location = new System.Drawing.Point(70, 137);
            this.dataTypesDGV.Name = "dataTypesDGV";
            this.dataTypesDGV.RowHeadersWidth = 62;
            this.dataTypesDGV.Size = new System.Drawing.Size(604, 224);
            this.dataTypesDGV.TabIndex = 3;
            this.dataTypesDGV.Visible = false;
            this.dataTypesDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTypesDGV_CellClick);
            // 
            // dataTypes
            // 
            this.dataTypes.HeaderText = "Item:";
            this.dataTypes.MinimumWidth = 8;
            this.dataTypes.Name = "dataTypes";
            this.dataTypes.ReadOnly = true;
            this.dataTypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataTypes.Width = 187;
            // 
            // value
            // 
            this.value.HeaderText = "value:";
            this.value.MinimumWidth = 8;
            this.value.Name = "value";
            this.value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.value.Width = 187;
            // 
            // desc
            // 
            this.desc.HeaderText = "Description:";
            this.desc.MinimumWidth = 8;
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.desc.Width = 187;
            // 
            // descSubType
            // 
            this.descSubType.Location = new System.Drawing.Point(545, 66);
            this.descSubType.Name = "descSubType";
            this.descSubType.Size = new System.Drawing.Size(225, 68);
            this.descSubType.TabIndex = 18;
            this.descSubType.Text = "Description:";
            // 
            // descType
            // 
            this.descType.Location = new System.Drawing.Point(335, 65);
            this.descType.Name = "descType";
            this.descType.Size = new System.Drawing.Size(190, 69);
            this.descType.TabIndex = 17;
            this.descType.Text = "Description:";
            // 
            // subtypeCB
            // 
            this.subtypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subtypeCB.FormattingEnabled = true;
            this.subtypeCB.Location = new System.Drawing.Point(627, 20);
            this.subtypeCB.Name = "subtypeCB";
            this.subtypeCB.Size = new System.Drawing.Size(143, 21);
            this.subtypeCB.TabIndex = 2;
            this.subtypeCB.SelectedIndexChanged += new System.EventHandler(this.subtypeCB_SelectedIndexChanged);
            // 
            // typeCB
            // 
            this.typeCB.AllowDrop = true;
            this.typeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Location = new System.Drawing.Point(404, 21);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(121, 21);
            this.typeCB.TabIndex = 1;
            this.typeCB.SelectedIndexChanged += new System.EventHandler(this.typeCB_SelectedIndexChanged);
            // 
            // trasBtn
            // 
            this.trasBtn.Location = new System.Drawing.Point(270, 55);
            this.trasBtn.Name = "trasBtn";
            this.trasBtn.Size = new System.Drawing.Size(75, 23);
            this.trasBtn.TabIndex = 2;
            this.trasBtn.Text = "translate";
            this.trasBtn.UseVisualStyleBackColor = true;
            this.trasBtn.Click += new System.EventHandler(this.trasBtn_Click);
            // 
            // transIn
            // 
            this.transIn.Location = new System.Drawing.Point(48, 21);
            this.transIn.Name = "transIn";
            this.transIn.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.transIn.Size = new System.Drawing.Size(550, 20);
            this.transIn.TabIndex = 0;
            this.transIn.WordWrap = false;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TxTab);
            this.TabControl.Controls.Add(this.RxTab);
            this.TabControl.Controls.Add(this.ImageTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 24);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(799, 455);
            this.TabControl.TabIndex = 17;
            // 
            // TxTab
            // 
            this.TxTab.Controls.Add(this.sendPacketBtn);
            this.TxTab.Controls.Add(this.copyBTN);
            this.TxTab.Controls.Add(this.label4);
            this.TxTab.Controls.Add(this.groupsCB);
            this.TxTab.Controls.Add(this.label3);
            this.TxTab.Controls.Add(this.ID);
            this.TxTab.Controls.Add(this.dataTypesDGV);
            this.TxTab.Controls.Add(this.IDTxb);
            this.TxTab.Controls.Add(this.descSubType);
            this.TxTab.Controls.Add(this.label2);
            this.TxTab.Controls.Add(this.descType);
            this.TxTab.Controls.Add(this.subtypeCB);
            this.TxTab.Controls.Add(this.label1);
            this.TxTab.Controls.Add(this.typeCB);
            this.TxTab.Controls.Add(this.makeOut);
            this.TxTab.Location = new System.Drawing.Point(4, 22);
            this.TxTab.Name = "TxTab";
            this.TxTab.Padding = new System.Windows.Forms.Padding(3);
            this.TxTab.Size = new System.Drawing.Size(791, 429);
            this.TxTab.TabIndex = 0;
            this.TxTab.Text = "TX";
            this.TxTab.UseVisualStyleBackColor = true;
            // 
            // sendPacketBtn
            // 
            this.sendPacketBtn.Location = new System.Drawing.Point(367, 393);
            this.sendPacketBtn.Name = "sendPacketBtn";
            this.sendPacketBtn.Size = new System.Drawing.Size(51, 23);
            this.sendPacketBtn.TabIndex = 24;
            this.sendPacketBtn.Text = "send";
            this.sendPacketBtn.UseVisualStyleBackColor = true;
            this.sendPacketBtn.Click += new System.EventHandler(this.sendPacketBtn_Click);
            // 
            // copyBTN
            // 
            this.copyBTN.Location = new System.Drawing.Point(294, 393);
            this.copyBTN.Name = "copyBTN";
            this.copyBTN.Size = new System.Drawing.Size(54, 23);
            this.copyBTN.TabIndex = 23;
            this.copyBTN.Text = "copy";
            this.copyBTN.UseVisualStyleBackColor = true;
            this.copyBTN.Click += new System.EventHandler(this.copyBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "satelite:";
            // 
            // groupsCB
            // 
            this.groupsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.groupsCB.FormattingEnabled = true;
            this.groupsCB.Location = new System.Drawing.Point(70, 20);
            this.groupsCB.Name = "groupsCB";
            this.groupsCB.Size = new System.Drawing.Size(136, 21);
            this.groupsCB.TabIndex = 21;
            // 
            // RxTab
            // 
            this.RxTab.Controls.Add(this.connectBtn);
            this.RxTab.Controls.Add(this.transOut);
            this.RxTab.Controls.Add(this.pasteBTN);
            this.RxTab.Controls.Add(this.label7);
            this.RxTab.Controls.Add(this.label6);
            this.RxTab.Controls.Add(this.label5);
            this.RxTab.Controls.Add(this.privHex);
            this.RxTab.Controls.Add(this.trasBtn);
            this.RxTab.Controls.Add(this.transIn);
            this.RxTab.Location = new System.Drawing.Point(4, 22);
            this.RxTab.Name = "RxTab";
            this.RxTab.Padding = new System.Windows.Forms.Padding(3);
            this.RxTab.Size = new System.Drawing.Size(791, 429);
            this.RxTab.TabIndex = 1;
            this.RxTab.Text = "RX";
            this.RxTab.UseVisualStyleBackColor = true;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(705, 18);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 9;
            this.connectBtn.Text = "connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // transOut
            // 
            this.transOut.FormattingEnabled = true;
            this.transOut.HorizontalScrollbar = true;
            this.transOut.Location = new System.Drawing.Point(54, 99);
            this.transOut.Name = "transOut";
            this.transOut.Size = new System.Drawing.Size(248, 303);
            this.transOut.TabIndex = 8;
            // 
            // pasteBTN
            // 
            this.pasteBTN.Location = new System.Drawing.Point(604, 19);
            this.pasteBTN.Name = "pasteBTN";
            this.pasteBTN.Size = new System.Drawing.Size(75, 23);
            this.pasteBTN.TabIndex = 7;
            this.pasteBTN.Text = "paste";
            this.pasteBTN.UseVisualStyleBackColor = true;
            this.pasteBTN.Click += new System.EventHandler(this.pasteBTN_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "History:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Output:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Input:";
            // 
            // privHex
            // 
            this.privHex.FormattingEnabled = true;
            this.privHex.HorizontalScrollbar = true;
            this.privHex.Location = new System.Drawing.Point(368, 98);
            this.privHex.Name = "privHex";
            this.privHex.Size = new System.Drawing.Size(401, 303);
            this.privHex.TabIndex = 3;
            this.privHex.SelectedIndexChanged += new System.EventHandler(this.privHex_SelectedIndexChanged);
            // 
            // ImageTab
            // 
            this.ImageTab.Controls.Add(this.panel1);
            this.ImageTab.Location = new System.Drawing.Point(4, 22);
            this.ImageTab.Name = "ImageTab";
            this.ImageTab.Size = new System.Drawing.Size(791, 429);
            this.ImageTab.TabIndex = 2;
            this.ImageTab.Text = "images";
            this.ImageTab.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sendImgReqBtn);
            this.panel1.Controls.Add(this.imgTypeCB);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(8, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 42);
            this.panel1.TabIndex = 1;
            // 
            // sendImgReqBtn
            // 
            this.sendImgReqBtn.Location = new System.Drawing.Point(489, 8);
            this.sendImgReqBtn.Name = "sendImgReqBtn";
            this.sendImgReqBtn.Size = new System.Drawing.Size(75, 23);
            this.sendImgReqBtn.TabIndex = 6;
            this.sendImgReqBtn.Text = "send";
            this.sendImgReqBtn.UseVisualStyleBackColor = true;
            // 
            // imgTypeCB
            // 
            this.imgTypeCB.AllowDrop = true;
            this.imgTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imgTypeCB.FormattingEnabled = true;
            this.imgTypeCB.Items.AddRange(new object[] {
            "XL",
            "LL",
            "SS",
            "HIT MAP"});
            this.imgTypeCB.Location = new System.Drawing.Point(332, 10);
            this.imgTypeCB.Name = "imgTypeCB";
            this.imgTypeCB.Size = new System.Drawing.Size(121, 21);
            this.imgTypeCB.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(161, 11);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(267, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Image type";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(106, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Image ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Image info request:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modifyToolStripMenuItem,
            this.viewPacketListToolStripMenuItem});
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
            this.modifyToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.modifyToolStripMenuItem.Text = "modify";
            // 
            // tXToolStripMenuItem
            // 
            this.tXToolStripMenuItem.Name = "tXToolStripMenuItem";
            this.tXToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.tXToolStripMenuItem.Text = "TX";
            this.tXToolStripMenuItem.Click += new System.EventHandler(this.tXToolStripMenuItem_Click);
            // 
            // rXToolStripMenuItem
            // 
            this.rXToolStripMenuItem.Name = "rXToolStripMenuItem";
            this.rXToolStripMenuItem.Size = new System.Drawing.Size(88, 22);
            this.rXToolStripMenuItem.Text = "RX";
            this.rXToolStripMenuItem.Click += new System.EventHandler(this.rXToolStripMenuItem_Click);
            // 
            // viewPacketListToolStripMenuItem
            // 
            this.viewPacketListToolStripMenuItem.Name = "viewPacketListToolStripMenuItem";
            this.viewPacketListToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.viewPacketListToolStripMenuItem.Text = "view packet list";
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
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 479);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "SPL Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.TxTab.ResumeLayout(false);
            this.TxTab.PerformLayout();
            this.RxTab.ResumeLayout(false);
            this.RxTab.PerformLayout();
            this.ImageTab.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IDTxb;
        private System.Windows.Forms.TextBox makeOut;
        private System.Windows.Forms.Button trasBtn;
        private System.Windows.Forms.TextBox transIn;
        private System.Windows.Forms.ComboBox subtypeCB;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label descSubType;
        private System.Windows.Forms.Label descType;
        private System.Windows.Forms.DataGridView dataTypesDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage TxTab;
        private System.Windows.Forms.TabPage RxTab;
        private System.Windows.Forms.ComboBox groupsCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox privHex;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button copyBTN;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button pasteBTN;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.ListBox transOut;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rXToolStripMenuItem;
        public System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.ToolStripMenuItem viewPacketListToolStripMenuItem;
        private System.Windows.Forms.Button sendPacketBtn;
        private System.Windows.Forms.TabPage ImageTab;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button sendImgReqBtn;
        private System.Windows.Forms.ComboBox imgTypeCB;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}

