
namespace SPL_Manager.UI.Views.RxTabViews
{
    partial class RxTab
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
            this.RxResendTxBtn = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.RxLimitCB = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.RxQueryMaxDateDtp = new System.Windows.Forms.DateTimePicker();
            this.RxQueryMinDateDtp = new System.Windows.Forms.DateTimePicker();
            this.RxLoadDBBtn = new System.Windows.Forms.Button();
            this.RxRxToTxBtn = new System.Windows.Forms.Button();
            this.RxTxToRxBtn = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.RxGroupsCB = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.RxSentPacketsLibx = new System.Windows.Forms.ListBox();
            this.RxPacketDetailsTxb = new System.Windows.Forms.TextBox();
            this.RxPacketHexTxb = new System.Windows.Forms.TextBox();
            this.RxClearBtn = new System.Windows.Forms.Button();
            this.RxConnectBtn = new System.Windows.Forms.Button();
            this.RxPasteBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.RxRecivedPacketsLibx = new System.Windows.Forms.ListBox();
            this.RxTranslatePacketBtn = new System.Windows.Forms.Button();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // RxResendTxBtn
            // 
            this.RxResendTxBtn.Location = new System.Drawing.Point(183, 501);
            this.RxResendTxBtn.Name = "RxResendTxBtn";
            this.RxResendTxBtn.Size = new System.Drawing.Size(75, 23);
            this.RxResendTxBtn.TabIndex = 51;
            this.RxResendTxBtn.Text = "Resend";
            this.RxResendTxBtn.UseVisualStyleBackColor = true;
            this.RxResendTxBtn.Click += new System.EventHandler(this.RxResendTxBtn_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label20);
            this.panel4.Controls.Add(this.RxLimitCB);
            this.panel4.Controls.Add(this.label19);
            this.panel4.Controls.Add(this.label18);
            this.panel4.Controls.Add(this.RxQueryMaxDateDtp);
            this.panel4.Controls.Add(this.RxQueryMinDateDtp);
            this.panel4.Controls.Add(this.RxLoadDBBtn);
            this.panel4.Location = new System.Drawing.Point(832, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(357, 65);
            this.panel4.TabIndex = 50;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 39);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 15);
            this.label20.TabIndex = 33;
            this.label20.Text = "Limit:";
            // 
            // RxLimitCB
            // 
            this.RxLimitCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RxLimitCB.FormattingEnabled = true;
            this.RxLimitCB.Items.AddRange(new object[] {
            "100",
            "500",
            "1000",
            "No Limit"});
            this.RxLimitCB.Location = new System.Drawing.Point(49, 36);
            this.RxLimitCB.Name = "RxLimitCB";
            this.RxLimitCB.Size = new System.Drawing.Size(128, 23);
            this.RxLimitCB.TabIndex = 32;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(183, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(21, 15);
            this.label19.TabIndex = 31;
            this.label19.Text = "to:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(13, 10);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(36, 15);
            this.label18.TabIndex = 30;
            this.label18.Text = "from:";
            // 
            // RxQueryMaxDateDtp
            // 
            this.RxQueryMaxDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            this.RxQueryMaxDateDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RxQueryMaxDateDtp.Location = new System.Drawing.Point(208, 8);
            this.RxQueryMaxDateDtp.Name = "RxQueryMaxDateDtp";
            this.RxQueryMaxDateDtp.Size = new System.Drawing.Size(128, 23);
            this.RxQueryMaxDateDtp.TabIndex = 29;
            // 
            // RxQueryMinDateDtp
            // 
            this.RxQueryMinDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            this.RxQueryMinDateDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.RxQueryMinDateDtp.Location = new System.Drawing.Point(49, 8);
            this.RxQueryMinDateDtp.Name = "RxQueryMinDateDtp";
            this.RxQueryMinDateDtp.Size = new System.Drawing.Size(128, 23);
            this.RxQueryMinDateDtp.TabIndex = 28;
            this.RxQueryMinDateDtp.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // RxLoadDBBtn
            // 
            this.RxLoadDBBtn.Location = new System.Drawing.Point(218, 34);
            this.RxLoadDBBtn.Name = "RxLoadDBBtn";
            this.RxLoadDBBtn.Size = new System.Drawing.Size(101, 23);
            this.RxLoadDBBtn.TabIndex = 27;
            this.RxLoadDBBtn.Text = "Load from DB";
            this.RxLoadDBBtn.UseVisualStyleBackColor = true;
            this.RxLoadDBBtn.Click += new System.EventHandler(this.RxLoadDBBtn_Click);
            // 
            // RxRxToTxBtn
            // 
            this.RxRxToTxBtn.Location = new System.Drawing.Point(405, 230);
            this.RxRxToTxBtn.Name = "RxRxToTxBtn";
            this.RxRxToTxBtn.Size = new System.Drawing.Size(60, 23);
            this.RxRxToTxBtn.TabIndex = 49;
            this.RxRxToTxBtn.Text = "<<<";
            this.RxRxToTxBtn.UseVisualStyleBackColor = true;
            this.RxRxToTxBtn.Click += new System.EventHandler(this.RxRxToTxBtn_Click);
            // 
            // RxTxToRxBtn
            // 
            this.RxTxToRxBtn.Location = new System.Drawing.Point(405, 186);
            this.RxTxToRxBtn.Name = "RxTxToRxBtn";
            this.RxTxToRxBtn.Size = new System.Drawing.Size(60, 23);
            this.RxTxToRxBtn.TabIndex = 48;
            this.RxTxToRxBtn.Text = ">>>";
            this.RxTxToRxBtn.UseVisualStyleBackColor = true;
            this.RxTxToRxBtn.Click += new System.EventHandler(this.RxTxToRxBtn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(405, 57);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 15);
            this.label17.TabIndex = 47;
            this.label17.Text = "satelite:";
            // 
            // RxGroupsCB
            // 
            this.RxGroupsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RxGroupsCB.FormattingEnabled = true;
            this.RxGroupsCB.Location = new System.Drawing.Point(454, 50);
            this.RxGroupsCB.Name = "RxGroupsCB";
            this.RxGroupsCB.Size = new System.Drawing.Size(136, 23);
            this.RxGroupsCB.TabIndex = 46;
            this.RxGroupsCB.SelectedIndexChanged += new System.EventHandler(this.RxGroupsCB_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 86);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 15);
            this.label16.TabIndex = 45;
            this.label16.Text = "Sent:";
            // 
            // RxSentPacketsLibx
            // 
            this.RxSentPacketsLibx.FormattingEnabled = true;
            this.RxSentPacketsLibx.HorizontalScrollbar = true;
            this.RxSentPacketsLibx.ItemHeight = 15;
            this.RxSentPacketsLibx.Location = new System.Drawing.Point(63, 86);
            this.RxSentPacketsLibx.Name = "RxSentPacketsLibx";
            this.RxSentPacketsLibx.Size = new System.Drawing.Size(336, 394);
            this.RxSentPacketsLibx.TabIndex = 44;
            this.RxSentPacketsLibx.SelectedIndexChanged += new System.EventHandler(this.RxSentPacketsLibx_SelectedIndexChanged);
            // 
            // RxPacketDetailsTxb
            // 
            this.RxPacketDetailsTxb.Location = new System.Drawing.Point(769, 86);
            this.RxPacketDetailsTxb.Multiline = true;
            this.RxPacketDetailsTxb.Name = "RxPacketDetailsTxb";
            this.RxPacketDetailsTxb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.RxPacketDetailsTxb.Size = new System.Drawing.Size(459, 394);
            this.RxPacketDetailsTxb.TabIndex = 43;
            this.RxPacketDetailsTxb.WordWrap = false;
            // 
            // RxPacketHexTxb
            // 
            this.RxPacketHexTxb.Location = new System.Drawing.Point(63, 16);
            this.RxPacketHexTxb.Name = "RxPacketHexTxb";
            this.RxPacketHexTxb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.RxPacketHexTxb.Size = new System.Drawing.Size(550, 23);
            this.RxPacketHexTxb.TabIndex = 34;
            this.RxPacketHexTxb.WordWrap = false;
            // 
            // RxClearBtn
            // 
            this.RxClearBtn.Location = new System.Drawing.Point(548, 503);
            this.RxClearBtn.Name = "RxClearBtn";
            this.RxClearBtn.Size = new System.Drawing.Size(75, 23);
            this.RxClearBtn.TabIndex = 42;
            this.RxClearBtn.Text = "Clear";
            this.RxClearBtn.UseVisualStyleBackColor = true;
            this.RxClearBtn.Click += new System.EventHandler(this.RxClearBtn_Click);
            // 
            // RxConnectBtn
            // 
            this.RxConnectBtn.Location = new System.Drawing.Point(720, 13);
            this.RxConnectBtn.Name = "RxConnectBtn";
            this.RxConnectBtn.Size = new System.Drawing.Size(75, 23);
            this.RxConnectBtn.TabIndex = 41;
            this.RxConnectBtn.Text = "Connect";
            this.RxConnectBtn.UseVisualStyleBackColor = true;
            this.RxConnectBtn.Click += new System.EventHandler(this.RxConnectBtn_Click);
            // 
            // RxPasteBtn
            // 
            this.RxPasteBtn.Location = new System.Drawing.Point(619, 14);
            this.RxPasteBtn.Name = "RxPasteBtn";
            this.RxPasteBtn.Size = new System.Drawing.Size(75, 23);
            this.RxPasteBtn.TabIndex = 40;
            this.RxPasteBtn.Text = "Paste";
            this.RxPasteBtn.UseVisualStyleBackColor = true;
            this.RxPasteBtn.Click += new System.EventHandler(this.RxPasteBtn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(726, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 15);
            this.label7.TabIndex = 39;
            this.label7.Text = "Detail:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(412, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 15);
            this.label6.TabIndex = 38;
            this.label6.Text = "Recived:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 37;
            this.label5.Text = "Input:";
            // 
            // RxRecivedPacketsLibx
            // 
            this.RxRecivedPacketsLibx.FormattingEnabled = true;
            this.RxRecivedPacketsLibx.HorizontalScrollbar = true;
            this.RxRecivedPacketsLibx.ItemHeight = 15;
            this.RxRecivedPacketsLibx.Location = new System.Drawing.Point(468, 86);
            this.RxRecivedPacketsLibx.Name = "RxRecivedPacketsLibx";
            this.RxRecivedPacketsLibx.Size = new System.Drawing.Size(252, 394);
            this.RxRecivedPacketsLibx.TabIndex = 36;
            this.RxRecivedPacketsLibx.SelectedIndexChanged += new System.EventHandler(this.RxRecivedPacketsLibx_SelectedIndexChanged);
            // 
            // RxTranslatePacketBtn
            // 
            this.RxTranslatePacketBtn.Location = new System.Drawing.Point(285, 50);
            this.RxTranslatePacketBtn.Name = "RxTranslatePacketBtn";
            this.RxTranslatePacketBtn.Size = new System.Drawing.Size(75, 23);
            this.RxTranslatePacketBtn.TabIndex = 35;
            this.RxTranslatePacketBtn.Text = "Translate";
            this.RxTranslatePacketBtn.UseVisualStyleBackColor = true;
            this.RxTranslatePacketBtn.Click += new System.EventHandler(this.RxTranslatePacketBtn_Click);
            // 
            // RxTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RxResendTxBtn);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.RxRxToTxBtn);
            this.Controls.Add(this.RxTxToRxBtn);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.RxGroupsCB);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.RxSentPacketsLibx);
            this.Controls.Add(this.RxPacketDetailsTxb);
            this.Controls.Add(this.RxPacketHexTxb);
            this.Controls.Add(this.RxClearBtn);
            this.Controls.Add(this.RxConnectBtn);
            this.Controls.Add(this.RxPasteBtn);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RxRecivedPacketsLibx);
            this.Controls.Add(this.RxTranslatePacketBtn);
            this.Name = "RxTab";
            this.Size = new System.Drawing.Size(1250, 600);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RxResendTxBtn;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox RxLimitCB;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DateTimePicker RxQueryMaxDateDtp;
        private System.Windows.Forms.DateTimePicker RxQueryMinDateDtp;
        private System.Windows.Forms.Button RxLoadDBBtn;
        private System.Windows.Forms.Button RxRxToTxBtn;
        private System.Windows.Forms.Button RxTxToRxBtn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox RxGroupsCB;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListBox RxSentPacketsLibx;
        private System.Windows.Forms.TextBox RxPacketDetailsTxb;
        private System.Windows.Forms.TextBox RxPacketHexTxb;
        private System.Windows.Forms.Button RxClearBtn;
        public System.Windows.Forms.Button RxConnectBtn;
        private System.Windows.Forms.Button RxPasteBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox RxRecivedPacketsLibx;
        private System.Windows.Forms.Button RxTranslatePacketBtn;
    }
}
