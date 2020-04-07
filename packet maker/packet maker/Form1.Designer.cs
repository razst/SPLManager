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
            this.make = new System.Windows.Forms.GroupBox();
            this.dataTypesDGV = new System.Windows.Forms.DataGridView();
            this.descSubType = new System.Windows.Forms.Label();
            this.descType = new System.Windows.Forms.Label();
            this.subtypeCB = new System.Windows.Forms.ComboBox();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.getRB = new System.Windows.Forms.RadioButton();
            this.createRB = new System.Windows.Forms.RadioButton();
            this.translate = new System.Windows.Forms.GroupBox();
            this.trasBtn = new System.Windows.Forms.Button();
            this.transOut = new System.Windows.Forms.TextBox();
            this.transIn = new System.Windows.Forms.TextBox();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.make.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.translate.SuspendLayout();
            this.SuspendLayout();
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(9, 16);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(21, 13);
            this.ID.TabIndex = 2;
            this.ID.Text = "ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "type:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "subtype:";
            // 
            // IDTxb
            // 
            this.IDTxb.Location = new System.Drawing.Point(63, 16);
            this.IDTxb.Name = "IDTxb";
            this.IDTxb.Size = new System.Drawing.Size(121, 20);
            this.IDTxb.TabIndex = 9;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(75, 139);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 10;
            this.OkBtn.Text = "generate";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // makeOut
            // 
            this.makeOut.Location = new System.Drawing.Point(205, 262);
            this.makeOut.Multiline = true;
            this.makeOut.Name = "makeOut";
            this.makeOut.Size = new System.Drawing.Size(332, 86);
            this.makeOut.TabIndex = 13;
            this.makeOut.Text = "output";
            // 
            // make
            // 
            this.make.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.make.Controls.Add(this.dataTypesDGV);
            this.make.Controls.Add(this.descSubType);
            this.make.Controls.Add(this.descType);
            this.make.Controls.Add(this.subtypeCB);
            this.make.Controls.Add(this.typeCB);
            this.make.Controls.Add(this.ID);
            this.make.Controls.Add(this.makeOut);
            this.make.Controls.Add(this.label1);
            this.make.Controls.Add(this.OkBtn);
            this.make.Controls.Add(this.label2);
            this.make.Controls.Add(this.IDTxb);
            this.make.Location = new System.Drawing.Point(12, 71);
            this.make.Name = "make";
            this.make.Size = new System.Drawing.Size(874, 354);
            this.make.TabIndex = 14;
            this.make.TabStop = false;
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
            this.dataTypesDGV.Location = new System.Drawing.Point(536, 16);
            this.dataTypesDGV.Name = "dataTypesDGV";
            this.dataTypesDGV.Size = new System.Drawing.Size(332, 240);
            this.dataTypesDGV.TabIndex = 19;
            this.dataTypesDGV.Visible = false;
            // 
            // descSubType
            // 
            this.descSubType.AutoSize = true;
            this.descSubType.Location = new System.Drawing.Point(202, 88);
            this.descSubType.Name = "descSubType";
            this.descSubType.Size = new System.Drawing.Size(0, 13);
            this.descSubType.TabIndex = 18;
            // 
            // descType
            // 
            this.descType.AutoSize = true;
            this.descType.Location = new System.Drawing.Point(202, 53);
            this.descType.Name = "descType";
            this.descType.Size = new System.Drawing.Size(0, 13);
            this.descType.TabIndex = 17;
            // 
            // subtypeCB
            // 
            this.subtypeCB.FormattingEnabled = true;
            this.subtypeCB.Location = new System.Drawing.Point(62, 85);
            this.subtypeCB.Name = "subtypeCB";
            this.subtypeCB.Size = new System.Drawing.Size(121, 21);
            this.subtypeCB.TabIndex = 15;
            this.subtypeCB.SelectedIndexChanged += new System.EventHandler(this.subtypeCB_SelectedIndexChanged);
            // 
            // typeCB
            // 
            this.typeCB.AllowDrop = true;
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Location = new System.Drawing.Point(63, 50);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(121, 21);
            this.typeCB.TabIndex = 14;
            this.typeCB.SelectedIndexChanged += new System.EventHandler(this.typeCB_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.getRB);
            this.groupBox1.Controls.Add(this.createRB);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 46);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "mode:";
            // 
            // getRB
            // 
            this.getRB.AutoSize = true;
            this.getRB.Location = new System.Drawing.Point(68, 20);
            this.getRB.Name = "getRB";
            this.getRB.Size = new System.Drawing.Size(65, 17);
            this.getRB.TabIndex = 1;
            this.getRB.Text = "translate";
            this.getRB.UseVisualStyleBackColor = true;
            this.getRB.CheckedChanged += new System.EventHandler(this.getRB_CheckedChanged);
            // 
            // createRB
            // 
            this.createRB.AutoSize = true;
            this.createRB.Checked = true;
            this.createRB.Location = new System.Drawing.Point(7, 20);
            this.createRB.Name = "createRB";
            this.createRB.Size = new System.Drawing.Size(55, 17);
            this.createRB.TabIndex = 0;
            this.createRB.TabStop = true;
            this.createRB.Text = "create";
            this.createRB.UseVisualStyleBackColor = true;
            this.createRB.CheckedChanged += new System.EventHandler(this.createRB_CheckedChanged);
            // 
            // translate
            // 
            this.translate.Controls.Add(this.trasBtn);
            this.translate.Controls.Add(this.transOut);
            this.translate.Controls.Add(this.transIn);
            this.translate.Location = new System.Drawing.Point(892, 102);
            this.translate.Name = "translate";
            this.translate.Size = new System.Drawing.Size(505, 374);
            this.translate.TabIndex = 16;
            this.translate.TabStop = false;
            this.translate.Visible = false;
            // 
            // trasBtn
            // 
            this.trasBtn.Location = new System.Drawing.Point(210, 147);
            this.trasBtn.Name = "trasBtn";
            this.trasBtn.Size = new System.Drawing.Size(75, 23);
            this.trasBtn.TabIndex = 2;
            this.trasBtn.Text = "translate";
            this.trasBtn.UseVisualStyleBackColor = true;
            this.trasBtn.Click += new System.EventHandler(this.trasBtn_Click);
            // 
            // transOut
            // 
            this.transOut.Location = new System.Drawing.Point(136, 181);
            this.transOut.Multiline = true;
            this.transOut.Name = "transOut";
            this.transOut.Size = new System.Drawing.Size(259, 187);
            this.transOut.TabIndex = 1;
            this.transOut.Text = "output";
            // 
            // transIn
            // 
            this.transIn.Location = new System.Drawing.Point(136, 45);
            this.transIn.Multiline = true;
            this.transIn.Name = "transIn";
            this.transIn.Size = new System.Drawing.Size(259, 64);
            this.transIn.TabIndex = 0;
            this.transIn.Text = "input";
            // 
            // desc
            // 
            this.desc.HeaderText = "Description:";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.Width = 88;
            // 
            // value
            // 
            this.value.HeaderText = "value:";
            this.value.Name = "value";
            this.value.Width = 61;
            // 
            // dataTypes
            // 
            this.dataTypes.HeaderText = "Item:";
            this.dataTypes.Name = "dataTypes";
            this.dataTypes.ReadOnly = true;
            this.dataTypes.Width = 55;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 450);
            this.Controls.Add(this.translate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.make);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.make.ResumeLayout(false);
            this.make.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataTypesDGV)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.translate.ResumeLayout(false);
            this.translate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox IDTxb;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.TextBox makeOut;
        private System.Windows.Forms.GroupBox make;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton getRB;
        private System.Windows.Forms.RadioButton createRB;
        private System.Windows.Forms.GroupBox translate;
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
    }
}

