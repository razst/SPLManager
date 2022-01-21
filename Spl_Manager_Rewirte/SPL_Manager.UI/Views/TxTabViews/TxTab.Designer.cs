
namespace SPL_Manager.UI.Views.TxTabViews
{
    partial class TxTab
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxDescSubtypeLbl = new System.Windows.Forms.Label();
            this.TxDescTypeLbl = new System.Windows.Forms.Label();
            this.TxSubtypesCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxTypesCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxSatGroupsCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PlDeleteListBtn = new System.Windows.Forms.Button();
            this.PlSaveListBtn = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.PlCmdSleepTxb = new System.Windows.Forms.TextBox();
            this.PlDeleteItemBtn = new System.Windows.Forms.Button();
            this.PlNewPlaylistBtn = new System.Windows.Forms.Button();
            this.PlMonedownBtn = new System.Windows.Forms.Button();
            this.PlMoveupBtn = new System.Windows.Forms.Button();
            this.PlSendListBtn = new System.Windows.Forms.Button();
            this.PlPlaylistItemsLibx = new System.Windows.Forms.ListBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.PlPlaylistsCB = new System.Windows.Forms.ComboBox();
            this.TxAddToPlaylistBtn = new System.Windows.Forms.Button();
            this.TxSendPacketBtn = new System.Windows.Forms.Button();
            this.TxCopyPacketBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.TxPacketParamsDGV = new System.Windows.Forms.DataGridView();
            this.dataTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxHexOutputTxb = new System.Windows.Forms.TextBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxPacketParamsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // TxDescSubtypeLbl
            // 
            this.TxDescSubtypeLbl.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxDescSubtypeLbl.Location = new System.Drawing.Point(414, 65);
            this.TxDescSubtypeLbl.Name = "TxDescSubtypeLbl";
            this.TxDescSubtypeLbl.Size = new System.Drawing.Size(144, 28);
            this.TxDescSubtypeLbl.TabIndex = 40;
            this.TxDescSubtypeLbl.Text = "Description:";
            // 
            // TxDescTypeLbl
            // 
            this.TxDescTypeLbl.Location = new System.Drawing.Point(230, 66);
            this.TxDescTypeLbl.Name = "TxDescTypeLbl";
            this.TxDescTypeLbl.Size = new System.Drawing.Size(161, 48);
            this.TxDescTypeLbl.TabIndex = 39;
            this.TxDescTypeLbl.Text = "Description:";
            // 
            // TxSubtypesCB
            // 
            this.TxSubtypesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxSubtypesCB.FormattingEnabled = true;
            this.TxSubtypesCB.Location = new System.Drawing.Point(469, 34);
            this.TxSubtypesCB.Name = "TxSubtypesCB";
            this.TxSubtypesCB.Size = new System.Drawing.Size(143, 23);
            this.TxSubtypesCB.TabIndex = 34;
            this.TxSubtypesCB.SelectedIndexChanged += new System.EventHandler(this.TxSubtypesCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(414, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "Subtype:";
            // 
            // TxTypesCB
            // 
            this.TxTypesCB.AllowDrop = true;
            this.TxTypesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxTypesCB.FormattingEnabled = true;
            this.TxTypesCB.Location = new System.Drawing.Point(270, 34);
            this.TxTypesCB.Name = "TxTypesCB";
            this.TxTypesCB.Size = new System.Drawing.Size(121, 23);
            this.TxTypesCB.TabIndex = 33;
            this.TxTypesCB.SelectedIndexChanged += new System.EventHandler(this.TxTypesCB_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 15);
            this.label1.TabIndex = 35;
            this.label1.Text = "Type:";
            // 
            // TxSatGroupsCB
            // 
            this.TxSatGroupsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxSatGroupsCB.FormattingEnabled = true;
            this.TxSatGroupsCB.Location = new System.Drawing.Point(73, 34);
            this.TxSatGroupsCB.Name = "TxSatGroupsCB";
            this.TxSatGroupsCB.Size = new System.Drawing.Size(136, 23);
            this.TxSatGroupsCB.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 43;
            this.label4.Text = "satelite:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(992, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 17);
            this.label14.TabIndex = 47;
            this.label14.Text = "Play Lists";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.PlDeleteListBtn);
            this.panel3.Controls.Add(this.PlSaveListBtn);
            this.panel3.Controls.Add(this.label15);
            this.panel3.Controls.Add(this.PlCmdSleepTxb);
            this.panel3.Controls.Add(this.PlDeleteItemBtn);
            this.panel3.Controls.Add(this.PlNewPlaylistBtn);
            this.panel3.Controls.Add(this.PlMonedownBtn);
            this.panel3.Controls.Add(this.PlMoveupBtn);
            this.panel3.Controls.Add(this.PlSendListBtn);
            this.panel3.Controls.Add(this.PlPlaylistItemsLibx);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.PlPlaylistsCB);
            this.panel3.Location = new System.Drawing.Point(860, 126);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(329, 295);
            this.panel3.TabIndex = 48;
            // 
            // PlDeleteListBtn
            // 
            this.PlDeleteListBtn.Location = new System.Drawing.Point(218, 260);
            this.PlDeleteListBtn.Name = "PlDeleteListBtn";
            this.PlDeleteListBtn.Size = new System.Drawing.Size(75, 23);
            this.PlDeleteListBtn.TabIndex = 39;
            this.PlDeleteListBtn.Text = "Delete";
            this.PlDeleteListBtn.UseVisualStyleBackColor = true;
            this.PlDeleteListBtn.Click += new System.EventHandler(this.PlDeleteListBtn_Click);
            // 
            // PlSaveListBtn
            // 
            this.PlSaveListBtn.Location = new System.Drawing.Point(128, 260);
            this.PlSaveListBtn.Name = "PlSaveListBtn";
            this.PlSaveListBtn.Size = new System.Drawing.Size(75, 23);
            this.PlSaveListBtn.TabIndex = 38;
            this.PlSaveListBtn.Text = "Save";
            this.PlSaveListBtn.UseVisualStyleBackColor = true;
            this.PlSaveListBtn.Click += new System.EventHandler(this.PlSaveListBtn_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(11, 229);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(173, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "Sleep between commands (milis)";
            // 
            // PlCmdSleepTxb
            // 
            this.PlCmdSleepTxb.Location = new System.Drawing.Point(190, 225);
            this.PlCmdSleepTxb.Name = "PlCmdSleepTxb";
            this.PlCmdSleepTxb.Size = new System.Drawing.Size(91, 23);
            this.PlCmdSleepTxb.TabIndex = 36;
            this.PlCmdSleepTxb.TextChanged += new System.EventHandler(this.PlCmdSleepTxb_TextChanged);
            // 
            // PlDeleteItemBtn
            // 
            this.PlDeleteItemBtn.Location = new System.Drawing.Point(268, 162);
            this.PlDeleteItemBtn.Name = "PlDeleteItemBtn";
            this.PlDeleteItemBtn.Size = new System.Drawing.Size(47, 23);
            this.PlDeleteItemBtn.TabIndex = 35;
            this.PlDeleteItemBtn.Text = "del";
            this.PlDeleteItemBtn.UseVisualStyleBackColor = true;
            this.PlDeleteItemBtn.Click += new System.EventHandler(this.PlDeleteItemBtn_Click);
            // 
            // PlNewPlaylistBtn
            // 
            this.PlNewPlaylistBtn.Location = new System.Drawing.Point(269, 16);
            this.PlNewPlaylistBtn.Name = "PlNewPlaylistBtn";
            this.PlNewPlaylistBtn.Size = new System.Drawing.Size(47, 23);
            this.PlNewPlaylistBtn.TabIndex = 34;
            this.PlNewPlaylistBtn.Text = "New";
            this.PlNewPlaylistBtn.UseVisualStyleBackColor = true;
            this.PlNewPlaylistBtn.Click += new System.EventHandler(this.PlNewPlaylistBtn_Click);
            // 
            // PlMonedownBtn
            // 
            this.PlMonedownBtn.Location = new System.Drawing.Point(269, 133);
            this.PlMonedownBtn.Name = "PlMonedownBtn";
            this.PlMonedownBtn.Size = new System.Drawing.Size(47, 23);
            this.PlMonedownBtn.TabIndex = 33;
            this.PlMonedownBtn.Text = "down";
            this.PlMonedownBtn.UseVisualStyleBackColor = true;
            this.PlMonedownBtn.Click += new System.EventHandler(this.PlMonedownBtn_Click);
            // 
            // PlMoveupBtn
            // 
            this.PlMoveupBtn.Location = new System.Drawing.Point(269, 104);
            this.PlMoveupBtn.Name = "PlMoveupBtn";
            this.PlMoveupBtn.Size = new System.Drawing.Size(47, 23);
            this.PlMoveupBtn.TabIndex = 32;
            this.PlMoveupBtn.Text = "up";
            this.PlMoveupBtn.UseVisualStyleBackColor = true;
            this.PlMoveupBtn.Click += new System.EventHandler(this.PlMoveupBtn_Click);
            // 
            // PlSendListBtn
            // 
            this.PlSendListBtn.Location = new System.Drawing.Point(35, 260);
            this.PlSendListBtn.Name = "PlSendListBtn";
            this.PlSendListBtn.Size = new System.Drawing.Size(75, 23);
            this.PlSendListBtn.TabIndex = 31;
            this.PlSendListBtn.Text = "Send auto";
            this.PlSendListBtn.UseVisualStyleBackColor = true;
            this.PlSendListBtn.Click += new System.EventHandler(this.PlSendListBtn_Click);
            // 
            // PlPlaylistItemsLibx
            // 
            this.PlPlaylistItemsLibx.FormattingEnabled = true;
            this.PlPlaylistItemsLibx.ItemHeight = 15;
            this.PlPlaylistItemsLibx.Location = new System.Drawing.Point(70, 59);
            this.PlPlaylistItemsLibx.Name = "PlPlaylistItemsLibx";
            this.PlPlaylistItemsLibx.Size = new System.Drawing.Size(192, 154);
            this.PlPlaylistItemsLibx.TabIndex = 30;
            this.PlPlaylistItemsLibx.SelectedIndexChanged += new System.EventHandler(this.PlPlaylistItemsLibx_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(21, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(44, 15);
            this.label13.TabIndex = 29;
            this.label13.Text = "PL Info";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 15);
            this.label12.TabIndex = 28;
            this.label12.Text = "PL Name";
            // 
            // PlPlaylistsCB
            // 
            this.PlPlaylistsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PlPlaylistsCB.FormattingEnabled = true;
            this.PlPlaylistsCB.Location = new System.Drawing.Point(70, 18);
            this.PlPlaylistsCB.Name = "PlPlaylistsCB";
            this.PlPlaylistsCB.Size = new System.Drawing.Size(192, 23);
            this.PlPlaylistsCB.TabIndex = 26;
            this.PlPlaylistsCB.SelectedIndexChanged += new System.EventHandler(this.PlPlaylistsCB_SelectedIndexChanged);
            this.PlPlaylistsCB.Click += new System.EventHandler(this.PlPlaylistsCB_Click);
            // 
            // TxAddToPlaylistBtn
            // 
            this.TxAddToPlaylistBtn.Location = new System.Drawing.Point(423, 520);
            this.TxAddToPlaylistBtn.Name = "TxAddToPlaylistBtn";
            this.TxAddToPlaylistBtn.Size = new System.Drawing.Size(75, 23);
            this.TxAddToPlaylistBtn.TabIndex = 46;
            this.TxAddToPlaylistBtn.Text = "Add To PL";
            this.TxAddToPlaylistBtn.UseVisualStyleBackColor = true;
            this.TxAddToPlaylistBtn.Click += new System.EventHandler(this.TxAddToPlaylistBtn_Click);
            // 
            // TxSendPacketBtn
            // 
            this.TxSendPacketBtn.Location = new System.Drawing.Point(354, 520);
            this.TxSendPacketBtn.Name = "TxSendPacketBtn";
            this.TxSendPacketBtn.Size = new System.Drawing.Size(51, 23);
            this.TxSendPacketBtn.TabIndex = 45;
            this.TxSendPacketBtn.Text = "Send";
            this.TxSendPacketBtn.UseVisualStyleBackColor = true;
            this.TxSendPacketBtn.Click += new System.EventHandler(this.TxSendPacketBtn_Click);
            // 
            // TxCopyPacketBtn
            // 
            this.TxCopyPacketBtn.Location = new System.Drawing.Point(281, 520);
            this.TxCopyPacketBtn.Name = "TxCopyPacketBtn";
            this.TxCopyPacketBtn.Size = new System.Drawing.Size(54, 23);
            this.TxCopyPacketBtn.TabIndex = 44;
            this.TxCopyPacketBtn.Text = "Copy";
            this.TxCopyPacketBtn.UseVisualStyleBackColor = true;
            this.TxCopyPacketBtn.Click += new System.EventHandler(this.TxCopyPacketBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 501);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 41;
            this.label3.Text = "Output:";
            // 
            // TxPacketParamsDGV
            // 
            this.TxPacketParamsDGV.AllowUserToAddRows = false;
            this.TxPacketParamsDGV.AllowUserToDeleteRows = false;
            this.TxPacketParamsDGV.AllowUserToResizeColumns = false;
            this.TxPacketParamsDGV.AllowUserToResizeRows = false;
            this.TxPacketParamsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TxPacketParamsDGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataTypes,
            this.value,
            this.desc});
            this.TxPacketParamsDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.TxPacketParamsDGV.Location = new System.Drawing.Point(57, 126);
            this.TxPacketParamsDGV.Name = "TxPacketParamsDGV";
            this.TxPacketParamsDGV.RowHeadersWidth = 62;
            this.TxPacketParamsDGV.Size = new System.Drawing.Size(700, 338);
            this.TxPacketParamsDGV.TabIndex = 36;
            this.TxPacketParamsDGV.Visible = false;
            this.TxPacketParamsDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TxPacketParamsDGV_CellClick);
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
            // TxHexOutputTxb
            // 
            this.TxHexOutputTxb.Location = new System.Drawing.Point(57, 494);
            this.TxHexOutputTxb.Name = "TxHexOutputTxb";
            this.TxHexOutputTxb.Size = new System.Drawing.Size(700, 23);
            this.TxHexOutputTxb.TabIndex = 38;
            // 
            // TxTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TxDescSubtypeLbl);
            this.Controls.Add(this.TxDescTypeLbl);
            this.Controls.Add(this.TxSubtypesCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxTypesCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxSatGroupsCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.TxAddToPlaylistBtn);
            this.Controls.Add(this.TxSendPacketBtn);
            this.Controls.Add(this.TxCopyPacketBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxPacketParamsDGV);
            this.Controls.Add(this.TxHexOutputTxb);
            this.Name = "TxTab";
            this.Size = new System.Drawing.Size(1260, 600);
            this.Load += new System.EventHandler(this.TxTab_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TxPacketParamsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TxDescSubtypeLbl;
        private System.Windows.Forms.Label TxDescTypeLbl;
        private System.Windows.Forms.ComboBox TxSubtypesCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox TxTypesCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox TxSatGroupsCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button PlDeleteListBtn;
        private System.Windows.Forms.Button PlSaveListBtn;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox PlCmdSleepTxb;
        private System.Windows.Forms.Button PlDeleteItemBtn;
        private System.Windows.Forms.Button PlNewPlaylistBtn;
        private System.Windows.Forms.Button PlMonedownBtn;
        private System.Windows.Forms.Button PlMoveupBtn;
        private System.Windows.Forms.Button PlSendListBtn;
        private System.Windows.Forms.ListBox PlPlaylistItemsLibx;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox PlPlaylistsCB;
        private System.Windows.Forms.Button TxAddToPlaylistBtn;
        private System.Windows.Forms.Button TxSendPacketBtn;
        private System.Windows.Forms.Button TxCopyPacketBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView TxPacketParamsDGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.TextBox TxHexOutputTxb;
    }
}
