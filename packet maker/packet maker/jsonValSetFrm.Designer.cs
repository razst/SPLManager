namespace packet_maker
{
    partial class jsonValSetFrm
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
            this.IdTxb = new System.Windows.Forms.TextBox();
            this.descTxb = new System.Windows.Forms.TextBox();
            this.nameTxb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.OkBtn = new System.Windows.Forms.Button();
            this.defult = new System.Windows.Forms.Panel();
            this.defult.SuspendLayout();
            this.SuspendLayout();
            // 
            // IdTxb
            // 
            this.IdTxb.Location = new System.Drawing.Point(100, 43);
            this.IdTxb.Name = "IdTxb";
            this.IdTxb.Size = new System.Drawing.Size(100, 20);
            this.IdTxb.TabIndex = 0;
            // 
            // descTxb
            // 
            this.descTxb.Location = new System.Drawing.Point(66, 52);
            this.descTxb.Name = "descTxb";
            this.descTxb.Size = new System.Drawing.Size(100, 20);
            this.descTxb.TabIndex = 1;
            // 
            // nameTxb
            // 
            this.nameTxb.Location = new System.Drawing.Point(66, 4);
            this.nameTxb.Name = "nameTxb";
            this.nameTxb.Size = new System.Drawing.Size(100, 20);
            this.nameTxb.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-3, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description:";
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(100, 259);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(75, 23);
            this.OkBtn.TabIndex = 6;
            this.OkBtn.Text = "Set";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // defult
            // 
            this.defult.Controls.Add(this.descTxb);
            this.defult.Controls.Add(this.label3);
            this.defult.Controls.Add(this.nameTxb);
            this.defult.Controls.Add(this.label2);
            this.defult.Location = new System.Drawing.Point(34, 92);
            this.defult.Name = "defult";
            this.defult.Size = new System.Drawing.Size(200, 100);
            this.defult.TabIndex = 8;
            // 
            // jsonValSetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 323);
            this.Controls.Add(this.defult);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IdTxb);
            this.Name = "jsonValSetFrm";
            this.Text = "jsonValSetFrm";
            this.defult.ResumeLayout(false);
            this.defult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IdTxb;
        private System.Windows.Forms.TextBox descTxb;
        private System.Windows.Forms.TextBox nameTxb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Panel defult;
    }
}