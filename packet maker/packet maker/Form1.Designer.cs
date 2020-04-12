namespace packet_maker
{
    partial class Form1
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
            this.label4 = new System.Windows.Forms.Label();
            this.groupsCB = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.label2.Location = new System.Drawing.Point(594, 27);
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
            this.OkBtn.Location = new System.Drawing.Point(662, 363);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 10;
            this.OkBtn.Text = "generate";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // makeOut
            // 
            this.makeOut.Location = new System.Drawing.Point(54, 418);
            this.makeOut.Name = "makeOut";
            this.makeOut.Size = new System.Drawing.Size(707, 20);
            this.makeOut.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 421);
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
            this.dataTypesDGV.Location = new System.Drawing.Point(30, 126);
            this.dataTypesDGV.Name = "dataTypesDGV";
            this.dataTypesDGV.Size = new System.Drawing.Size(606, 260);
            this.dataTypesDGV.TabIndex = 3;
            this.dataTypesDGV.Visible = false;
            // 
            // dataTypes
            // 
            this.dataTypes.HeaderText = "Item:";
            this.dataTypes.Name = "dataTypes";
            this.dataTypes.ReadOnly = true;
            this.dataTypes.Width = 55;
            // 
            // value
            // 
            this.value.HeaderText = "value:";
            this.value.Name = "value";
            this.value.Width = 61;
            // 
            // desc
            // 
            this.desc.HeaderText = "Description:";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.Width = 88;
            // 
            // descSubType
            // 
            this.descSubType.AutoSize = true;
            this.descSubType.Location = new System.Drawing.Point(594, 65);
            this.descSubType.Name = "descSubType";
            this.descSubType.Size = new System.Drawing.Size(63, 13);
            this.descSubType.TabIndex = 18;
            this.descSubType.Text = "Description:";
            // 
            // descType
            // 
            this.descType.AutoSize = true;
            this.descType.Location = new System.Drawing.Point(401, 65);
            this.descType.Name = "descType";
            this.descType.Size = new System.Drawing.Size(63, 13);
            this.descType.TabIndex = 17;
            this.descType.Text = "Description:";
            // 
            // subtypeCB
            // 
            this.subtypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subtypeCB.FormattingEnabled = true;
            this.subtypeCB.Location = new System.Drawing.Point(662, 19);
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
            this.trasBtn.Location = new System.Drawing.Point(360, 105);
            this.trasBtn.Name = "trasBtn";
            this.trasBtn.Size = new System.Drawing.Size(75, 23);
            this.trasBtn.TabIndex = 2;
            this.trasBtn.Text = "translate";
            this.trasBtn.UseVisualStyleBackColor = true;
            this.trasBtn.Click += new System.EventHandler(this.trasBtn_Click);
            // 
            // transOut
            // 
            this.transOut.Location = new System.Drawing.Point(291, 170);
            this.transOut.Multiline = true;
            this.transOut.Name = "transOut";
            this.transOut.Size = new System.Drawing.Size(259, 187);
            this.transOut.TabIndex = 1;
            this.transOut.Text = "output";
            // 
            // transIn
            // 
            this.transIn.Location = new System.Drawing.Point(143, 33);
            this.transIn.Multiline = true;
            this.transIn.Name = "transIn";
            this.transIn.Size = new System.Drawing.Size(566, 22);
            this.transIn.TabIndex = 0;
            this.transIn.Text = "input";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(956, 487);
            this.tabControl.TabIndex = 17;
            // 
            // tabPage1
            // 
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
            this.tabPage1.Size = new System.Drawing.Size(948, 461);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "create";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.groupsCB.Items.AddRange(new object[] {
            "Any",
            "Ofakim (T1)",
            "Yerucham (T2)",
            "Kiryat Ata (T3)",
            "Taybe (T4)",
            "Shaar HaNegev (T5)",
            "Nazareth (T6)",
            "Maale Adomin (T7)",
            "Guvat Shmuel (T8)"});
            this.groupsCB.Location = new System.Drawing.Point(70, 20);
            this.groupsCB.Name = "groupsCB";
            this.groupsCB.Size = new System.Drawing.Size(150, 21);
            this.groupsCB.TabIndex = 21;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.transOut);
            this.tabPage2.Controls.Add(this.trasBtn);
            this.tabPage2.Controls.Add(this.transIn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(948, 461);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "traslate";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 511);
            this.Controls.Add(this.tabControl);
            this.Name = "Form1";
            this.Text = "SPL Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.DataGridViewTextBoxColumn dataTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox groupsCB;
        private System.Windows.Forms.Label label4;
    }
}

