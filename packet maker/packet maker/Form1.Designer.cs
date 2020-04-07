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
            this.label3 = new System.Windows.Forms.Label();
            this.dataTxb = new System.Windows.Forms.TextBox();
            this.subTypeTxb = new System.Windows.Forms.TextBox();
            this.typeTxb = new System.Windows.Forms.TextBox();
            this.IDTxb = new System.Windows.Forms.TextBox();
            this.OkBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lenTxb = new System.Windows.Forms.TextBox();
            this.makeOut = new System.Windows.Forms.TextBox();
            this.make = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.getRB = new System.Windows.Forms.RadioButton();
            this.createRB = new System.Windows.Forms.RadioButton();
            this.translate = new System.Windows.Forms.GroupBox();
            this.trasBtn = new System.Windows.Forms.Button();
            this.transOut = new System.Windows.Forms.TextBox();
            this.transIn = new System.Windows.Forms.TextBox();
            this.typeCB = new System.Windows.Forms.ComboBox();
            this.subtypeCB = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.make.SuspendLayout();
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "data:";
            // 
            // dataTxb
            // 
            this.dataTxb.Location = new System.Drawing.Point(63, 149);
            this.dataTxb.Name = "dataTxb";
            this.dataTxb.Size = new System.Drawing.Size(100, 20);
            this.dataTxb.TabIndex = 6;
            // 
            // subTypeTxb
            // 
            this.subTypeTxb.Location = new System.Drawing.Point(63, 80);
            this.subTypeTxb.Name = "subTypeTxb";
            this.subTypeTxb.Size = new System.Drawing.Size(100, 20);
            this.subTypeTxb.TabIndex = 7;
            // 
            // typeTxb
            // 
            this.typeTxb.Location = new System.Drawing.Point(63, 45);
            this.typeTxb.Name = "typeTxb";
            this.typeTxb.Size = new System.Drawing.Size(100, 20);
            this.typeTxb.TabIndex = 8;
            // 
            // IDTxb
            // 
            this.IDTxb.Location = new System.Drawing.Point(63, 16);
            this.IDTxb.Name = "IDTxb";
            this.IDTxb.Size = new System.Drawing.Size(100, 20);
            this.IDTxb.TabIndex = 9;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(63, 210);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 10;
            this.OkBtn.Text = "generate";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "length:";
            // 
            // lenTxb
            // 
            this.lenTxb.Location = new System.Drawing.Point(63, 115);
            this.lenTxb.Name = "lenTxb";
            this.lenTxb.Size = new System.Drawing.Size(100, 20);
            this.lenTxb.TabIndex = 12;
            // 
            // makeOut
            // 
            this.makeOut.Location = new System.Drawing.Point(421, 181);
            this.makeOut.Multiline = true;
            this.makeOut.Name = "makeOut";
            this.makeOut.Size = new System.Drawing.Size(332, 167);
            this.makeOut.TabIndex = 13;
            this.makeOut.Text = "output";
            // 
            // make
            // 
            this.make.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.make.Controls.Add(this.groupBox2);
            this.make.Controls.Add(this.subtypeCB);
            this.make.Controls.Add(this.typeCB);
            this.make.Controls.Add(this.ID);
            this.make.Controls.Add(this.makeOut);
            this.make.Controls.Add(this.label1);
            this.make.Controls.Add(this.OkBtn);
            this.make.Controls.Add(this.lenTxb);
            this.make.Controls.Add(this.label2);
            this.make.Controls.Add(this.label4);
            this.make.Controls.Add(this.label3);
            this.make.Controls.Add(this.dataTxb);
            this.make.Controls.Add(this.IDTxb);
            this.make.Controls.Add(this.subTypeTxb);
            this.make.Controls.Add(this.typeTxb);
            this.make.Location = new System.Drawing.Point(12, 71);
            this.make.Name = "make";
            this.make.Size = new System.Drawing.Size(759, 354);
            this.make.TabIndex = 14;
            this.make.TabStop = false;
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
            this.translate.Location = new System.Drawing.Point(777, 105);
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
            // typeCB
            // 
            this.typeCB.FormattingEnabled = true;
            this.typeCB.Location = new System.Drawing.Point(194, 45);
            this.typeCB.Name = "typeCB";
            this.typeCB.Size = new System.Drawing.Size(121, 21);
            this.typeCB.TabIndex = 14;
            // 
            // subtypeCB
            // 
            this.subtypeCB.FormattingEnabled = true;
            this.subtypeCB.Location = new System.Drawing.Point(194, 80);
            this.subtypeCB.Name = "subtypeCB";
            this.subtypeCB.Size = new System.Drawing.Size(121, 21);
            this.subtypeCB.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(194, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 39);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.translate);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.make);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.make.ResumeLayout(false);
            this.make.PerformLayout();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox dataTxb;
        private System.Windows.Forms.TextBox subTypeTxb;
        private System.Windows.Forms.TextBox typeTxb;
        private System.Windows.Forms.TextBox IDTxb;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lenTxb;
        private System.Windows.Forms.TextBox makeOut;
        private System.Windows.Forms.GroupBox make;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton getRB;
        private System.Windows.Forms.RadioButton createRB;
        private System.Windows.Forms.GroupBox translate;
        private System.Windows.Forms.Button trasBtn;
        private System.Windows.Forms.TextBox transOut;
        private System.Windows.Forms.TextBox transIn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox subtypeCB;
        private System.Windows.Forms.ComboBox typeCB;
    }
}

