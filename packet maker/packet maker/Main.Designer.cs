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
            this.OkBtn = new System.Windows.Forms.Button();
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
            this.transOut = new System.Windows.Forms.TextBox();
            this.transIn = new System.Windows.Forms.TextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.copyBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupsCB = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pasteBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.privHex = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(695, 338);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 10;
            this.OkBtn.Text = "generate";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // makeOut
            // 
            this.makeOut.Location = new System.Drawing.Point(70, 389);
            this.makeOut.Name = "makeOut";
            this.makeOut.Size = new System.Drawing.Size(604, 20);
            this.makeOut.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 392);
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
            this.dataTypesDGV.Size = new System.Drawing.Size(604, 224);
            this.dataTypesDGV.TabIndex = 3;
            this.dataTypesDGV.Visible = false;
            this.dataTypesDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataTypesDGV_CellClick);
            // 
            // dataTypes
            // 
            this.dataTypes.HeaderText = "Item:";
            this.dataTypes.Name = "dataTypes";
            this.dataTypes.ReadOnly = true;
            this.dataTypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataTypes.Width = 187;
            // 
            // value
            // 
            this.value.HeaderText = "value:";
            this.value.Name = "value";
            this.value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.value.Width = 187;
            // 
            // desc
            // 
            this.desc.HeaderText = "Description:";
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
            // transOut
            // 
            this.transOut.Location = new System.Drawing.Point(48, 96);
            this.transOut.Multiline = true;
            this.transOut.Name = "transOut";
            this.transOut.Size = new System.Drawing.Size(224, 306);
            this.transOut.TabIndex = 1;
            // 
            // transIn
            // 
            this.transIn.Location = new System.Drawing.Point(48, 21);
            this.transIn.Name = "transIn";
            this.transIn.Size = new System.Drawing.Size(550, 20);
            this.transIn.TabIndex = 0;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 24);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(799, 455);
            this.tabControl.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.copyBTN);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.groupsCB);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.ID);
            this.tabPage1.Controls.Add(this.dataTypesDGV);
            this.tabPage1.Controls.Add(this.IDTxb);
            this.tabPage1.Controls.Add(this.descSubType);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.descType);
            this.tabPage1.Controls.Add(this.OkBtn);
            this.tabPage1.Controls.Add(this.subtypeCB);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.typeCB);
            this.tabPage1.Controls.Add(this.makeOut);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(791, 429);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "TX";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // copyBTN
            // 
            this.copyBTN.Location = new System.Drawing.Point(695, 386);
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
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pasteBTN);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.privHex);
            this.tabPage2.Controls.Add(this.transOut);
            this.tabPage2.Controls.Add(this.trasBtn);
            this.tabPage2.Controls.Add(this.transIn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(791, 429);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RX";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.label7.Location = new System.Drawing.Point(303, 99);
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
            this.privHex.Location = new System.Drawing.Point(347, 99);
            this.privHex.Name = "privHex";
            this.privHex.Size = new System.Drawing.Size(401, 303);
            this.privHex.TabIndex = 3;
            this.privHex.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(799, 24);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
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
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "SPL Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox makeOut;
        private System.Windows.Forms.Button trasBtn;
        private System.Windows.Forms.TextBox transOut;
        private System.Windows.Forms.TextBox transIn;
        private System.Windows.Forms.ComboBox subtypeCB;
        private System.Windows.Forms.ComboBox typeCB;
        private System.Windows.Forms.Label descSubType;
        private System.Windows.Forms.Label descType;
        private System.Windows.Forms.DataGridView dataTypesDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
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
    }
}

