﻿namespace packet_maker
{
    partial class PacketListFrm
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
            this.storedPacketsDGV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addBtn = new System.Windows.Forms.Button();
            this.pacStringTxb = new System.Windows.Forms.TextBox();
            this.descTxb = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.storedPacketsDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // storedPacketsDGV
            // 
            this.storedPacketsDGV.AllowUserToAddRows = false;
            this.storedPacketsDGV.AllowUserToDeleteRows = false;
            this.storedPacketsDGV.AllowUserToResizeColumns = false;
            this.storedPacketsDGV.AllowUserToResizeRows = false;
            this.storedPacketsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.storedPacketsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.storedPacketsDGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.storedPacketsDGV.Location = new System.Drawing.Point(12, 12);
            this.storedPacketsDGV.Name = "storedPacketsDGV";
            this.storedPacketsDGV.Size = new System.Drawing.Size(350, 422);
            this.storedPacketsDGV.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(368, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "packet:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(368, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "description:";
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(368, 130);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 3;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // pacStringTxb
            // 
            this.pacStringTxb.Location = new System.Drawing.Point(461, 22);
            this.pacStringTxb.Name = "pacStringTxb";
            this.pacStringTxb.Size = new System.Drawing.Size(100, 20);
            this.pacStringTxb.TabIndex = 4;
            // 
            // descTxb
            // 
            this.descTxb.Location = new System.Drawing.Point(461, 60);
            this.descTxb.Multiline = true;
            this.descTxb.Name = "descTxb";
            this.descTxb.Size = new System.Drawing.Size(113, 49);
            this.descTxb.TabIndex = 5;
            // 
            // PacketListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 461);
            this.Controls.Add(this.descTxb);
            this.Controls.Add(this.pacStringTxb);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.storedPacketsDGV);
            this.Name = "PacketListFrm";
            this.Text = "PacketListFrm";
            this.Load += new System.EventHandler(this.PacketListFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.storedPacketsDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView storedPacketsDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox pacStringTxb;
        private System.Windows.Forms.TextBox descTxb;
    }
}