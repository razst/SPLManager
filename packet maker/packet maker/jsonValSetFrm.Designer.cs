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
            this.descTxb.Location = new System.Drawing.Point(100, 143);
            this.descTxb.Name = "descTxb";
            this.descTxb.Size = new System.Drawing.Size(100, 20);
            this.descTxb.TabIndex = 1;
            // 
            // nameTxb
            // 
            this.nameTxb.Location = new System.Drawing.Point(100, 91);
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
            this.label2.Location = new System.Drawing.Point(31, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Description:";
            // 
            // jsonValSetFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 323);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameTxb);
            this.Controls.Add(this.descTxb);
            this.Controls.Add(this.IdTxb);
            this.Name = "jsonValSetFrm";
            this.Text = "jsonValSetFrm";
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
    }
}