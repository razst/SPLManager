
namespace SPL_Manager.UI.Views.QueryTabViews
{
    partial class QueryTab
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
            this.qrySettingsPnl = new System.Windows.Forms.Panel();
            this.QryLimitCB = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.FieldOptionsPanel = new System.Windows.Forms.Panel();
            this.QryFieldCB = new System.Windows.Forms.ComboBox();
            this.QryFiledConditionCB = new System.Windows.Forms.ComboBox();
            this.QryConditionValueDtp = new System.Windows.Forms.DateTimePicker();
            this.QryConditionValueTxb = new System.Windows.Forms.TextBox();
            this.QrySubtypeCB = new System.Windows.Forms.ComboBox();
            this.QryFieldChbx = new System.Windows.Forms.CheckBox();
            this.qryTxChbx = new System.Windows.Forms.CheckBox();
            this.qryRxChbx = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.QryGroupsCB = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.qryStartBtn = new System.Windows.Forms.Button();
            this.QryMinDateDtp = new System.Windows.Forms.DateTimePicker();
            this.label24 = new System.Windows.Forms.Label();
            this.QryIdTxb = new System.Windows.Forms.TextBox();
            this.QryIdChbx = new System.Windows.Forms.CheckBox();
            this.QryMaxDateDtp = new System.Windows.Forms.DateTimePicker();
            this.qryRx2TxBtn = new System.Windows.Forms.Button();
            this.qryTx2RxBtn = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.TxPacQryLibx = new System.Windows.Forms.ListBox();
            this.QryPacketDetailsTxb = new System.Windows.Forms.TextBox();
            this.qryClearBtn = new System.Windows.Forms.Button();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.RxPacQryLibx = new System.Windows.Forms.ListBox();
            this.qrySettingsPnl.SuspendLayout();
            this.FieldOptionsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // qrySettingsPnl
            // 
            this.qrySettingsPnl.Controls.Add(this.QryLimitCB);
            this.qrySettingsPnl.Controls.Add(this.label23);
            this.qrySettingsPnl.Controls.Add(this.FieldOptionsPanel);
            this.qrySettingsPnl.Controls.Add(this.QrySubtypeCB);
            this.qrySettingsPnl.Controls.Add(this.QryFieldChbx);
            this.qrySettingsPnl.Controls.Add(this.qryTxChbx);
            this.qrySettingsPnl.Controls.Add(this.qryRxChbx);
            this.qrySettingsPnl.Controls.Add(this.label25);
            this.qrySettingsPnl.Controls.Add(this.QryGroupsCB);
            this.qrySettingsPnl.Controls.Add(this.label22);
            this.qrySettingsPnl.Controls.Add(this.label31);
            this.qrySettingsPnl.Controls.Add(this.qryStartBtn);
            this.qrySettingsPnl.Controls.Add(this.QryMinDateDtp);
            this.qrySettingsPnl.Controls.Add(this.label24);
            this.qrySettingsPnl.Controls.Add(this.QryIdTxb);
            this.qrySettingsPnl.Controls.Add(this.QryIdChbx);
            this.qrySettingsPnl.Controls.Add(this.QryMaxDateDtp);
            this.qrySettingsPnl.Location = new System.Drawing.Point(9, 23);
            this.qrySettingsPnl.Name = "qrySettingsPnl";
            this.qrySettingsPnl.Size = new System.Drawing.Size(1261, 77);
            this.qrySettingsPnl.TabIndex = 62;
            // 
            // QryLimitCB
            // 
            this.QryLimitCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QryLimitCB.FormattingEnabled = true;
            this.QryLimitCB.Items.AddRange(new object[] {
            "100",
            "500",
            "1000",
            "No Limit"});
            this.QryLimitCB.Location = new System.Drawing.Point(993, 26);
            this.QryLimitCB.Name = "QryLimitCB";
            this.QryLimitCB.Size = new System.Drawing.Size(128, 23);
            this.QryLimitCB.TabIndex = 32;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(956, 29);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(37, 15);
            this.label23.TabIndex = 33;
            this.label23.Text = "Limit:";
            // 
            // FieldOptionsPanel
            // 
            this.FieldOptionsPanel.Controls.Add(this.QryFieldCB);
            this.FieldOptionsPanel.Controls.Add(this.QryFiledConditionCB);
            this.FieldOptionsPanel.Controls.Add(this.QryConditionValueDtp);
            this.FieldOptionsPanel.Controls.Add(this.QryConditionValueTxb);
            this.FieldOptionsPanel.Location = new System.Drawing.Point(244, 43);
            this.FieldOptionsPanel.Name = "FieldOptionsPanel";
            this.FieldOptionsPanel.Size = new System.Drawing.Size(343, 31);
            this.FieldOptionsPanel.TabIndex = 50;
            this.FieldOptionsPanel.Visible = false;
            // 
            // QryFieldCB
            // 
            this.QryFieldCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QryFieldCB.DropDownWidth = 200;
            this.QryFieldCB.FormattingEnabled = true;
            this.QryFieldCB.Location = new System.Drawing.Point(6, 4);
            this.QryFieldCB.Name = "QryFieldCB";
            this.QryFieldCB.Size = new System.Drawing.Size(128, 23);
            this.QryFieldCB.TabIndex = 44;
            this.QryFieldCB.SelectedIndexChanged += new System.EventHandler(this.QryFieldCB_SelectedIndexChanged);
            // 
            // QryFiledConditionCB
            // 
            this.QryFiledConditionCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QryFiledConditionCB.DropDownWidth = 200;
            this.QryFiledConditionCB.FormattingEnabled = true;
            this.QryFiledConditionCB.Items.AddRange(new object[] {
            ">",
            "<",
            "!=",
            "="});
            this.QryFiledConditionCB.Location = new System.Drawing.Point(140, 4);
            this.QryFiledConditionCB.Name = "QryFiledConditionCB";
            this.QryFiledConditionCB.Size = new System.Drawing.Size(55, 23);
            this.QryFiledConditionCB.TabIndex = 45;
            // 
            // QryConditionValueDtp
            // 
            this.QryConditionValueDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            this.QryConditionValueDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.QryConditionValueDtp.Location = new System.Drawing.Point(201, 4);
            this.QryConditionValueDtp.Name = "QryConditionValueDtp";
            this.QryConditionValueDtp.Size = new System.Drawing.Size(128, 23);
            this.QryConditionValueDtp.TabIndex = 47;
            this.QryConditionValueDtp.Visible = false;
            // 
            // QryConditionValueTxb
            // 
            this.QryConditionValueTxb.Location = new System.Drawing.Point(203, 4);
            this.QryConditionValueTxb.Name = "QryConditionValueTxb";
            this.QryConditionValueTxb.Size = new System.Drawing.Size(115, 23);
            this.QryConditionValueTxb.TabIndex = 46;
            // 
            // QrySubtypeCB
            // 
            this.QrySubtypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QrySubtypeCB.DropDownWidth = 200;
            this.QrySubtypeCB.FormattingEnabled = true;
            this.QrySubtypeCB.Items.AddRange(new object[] {
            "All"});
            this.QrySubtypeCB.Location = new System.Drawing.Point(593, 9);
            this.QrySubtypeCB.Name = "QrySubtypeCB";
            this.QrySubtypeCB.Size = new System.Drawing.Size(128, 23);
            this.QrySubtypeCB.TabIndex = 48;
            this.QrySubtypeCB.SelectedIndexChanged += new System.EventHandler(this.QrySubtypeCB_SelectedIndexChanged);
            // 
            // QryFieldChbx
            // 
            this.QryFieldChbx.AutoSize = true;
            this.QryFieldChbx.Location = new System.Drawing.Point(199, 49);
            this.QryFieldChbx.Name = "QryFieldChbx";
            this.QryFieldChbx.Size = new System.Drawing.Size(52, 19);
            this.QryFieldChbx.TabIndex = 49;
            this.QryFieldChbx.Text = "field:";
            this.QryFieldChbx.UseVisualStyleBackColor = true;
            this.QryFieldChbx.CheckedChanged += new System.EventHandler(this.QryFieldChbx_CheckedChanged);
            // 
            // qryTxChbx
            // 
            this.qryTxChbx.AutoSize = true;
            this.qryTxChbx.Location = new System.Drawing.Point(911, 9);
            this.qryTxChbx.Name = "qryTxChbx";
            this.qryTxChbx.Size = new System.Drawing.Size(39, 19);
            this.qryTxChbx.TabIndex = 37;
            this.qryTxChbx.Text = "TX";
            this.qryTxChbx.UseVisualStyleBackColor = true;
            // 
            // qryRxChbx
            // 
            this.qryRxChbx.AutoSize = true;
            this.qryRxChbx.Checked = true;
            this.qryRxChbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.qryRxChbx.Location = new System.Drawing.Point(910, 44);
            this.qryRxChbx.Name = "qryRxChbx";
            this.qryRxChbx.Size = new System.Drawing.Size(40, 19);
            this.qryRxChbx.TabIndex = 36;
            this.qryRxChbx.Text = "RX";
            this.qryRxChbx.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(16, 13);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 15);
            this.label25.TabIndex = 30;
            this.label25.Text = "from:";
            // 
            // QryGroupsCB
            // 
            this.QryGroupsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.QryGroupsCB.FormattingEnabled = true;
            this.QryGroupsCB.Location = new System.Drawing.Point(398, 9);
            this.QryGroupsCB.Name = "QryGroupsCB";
            this.QryGroupsCB.Size = new System.Drawing.Size(136, 23);
            this.QryGroupsCB.TabIndex = 40;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(540, 13);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 15);
            this.label22.TabIndex = 34;
            this.label22.Text = "subtype:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(355, 13);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(47, 15);
            this.label31.TabIndex = 41;
            this.label31.Text = "satelite:";
            // 
            // qryStartBtn
            // 
            this.qryStartBtn.Location = new System.Drawing.Point(1128, 24);
            this.qryStartBtn.Name = "qryStartBtn";
            this.qryStartBtn.Size = new System.Drawing.Size(101, 23);
            this.qryStartBtn.TabIndex = 27;
            this.qryStartBtn.Text = "Load from DB";
            this.qryStartBtn.UseVisualStyleBackColor = true;
            this.qryStartBtn.Click += new System.EventHandler(this.QryStartBtn_Click);
            // 
            // QryMinDateDtp
            // 
            this.QryMinDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            this.QryMinDateDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.QryMinDateDtp.Location = new System.Drawing.Point(52, 10);
            this.QryMinDateDtp.Name = "QryMinDateDtp";
            this.QryMinDateDtp.Size = new System.Drawing.Size(128, 23);
            this.QryMinDateDtp.TabIndex = 28;
            this.QryMinDateDtp.Value = new System.DateTime(2020, 1, 1, 0, 0, 0, 0);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(196, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(21, 15);
            this.label24.TabIndex = 31;
            this.label24.Text = "to:";
            // 
            // QryIdTxb
            // 
            this.QryIdTxb.Location = new System.Drawing.Point(65, 49);
            this.QryIdTxb.Name = "QryIdTxb";
            this.QryIdTxb.ReadOnly = true;
            this.QryIdTxb.Size = new System.Drawing.Size(115, 23);
            this.QryIdTxb.TabIndex = 38;
            // 
            // QryIdChbx
            // 
            this.QryIdChbx.AutoSize = true;
            this.QryIdChbx.Location = new System.Drawing.Point(19, 51);
            this.QryIdChbx.Name = "QryIdChbx";
            this.QryIdChbx.Size = new System.Drawing.Size(37, 19);
            this.QryIdChbx.TabIndex = 42;
            this.QryIdChbx.Text = "ID";
            this.QryIdChbx.UseVisualStyleBackColor = true;
            this.QryIdChbx.CheckedChanged += new System.EventHandler(this.QryIdChbx_CheckedChanged);
            // 
            // QryMaxDateDtp
            // 
            this.QryMaxDateDtp.CustomFormat = "dd/MM/yyyy HH:mm";
            this.QryMaxDateDtp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.QryMaxDateDtp.Location = new System.Drawing.Point(221, 10);
            this.QryMaxDateDtp.Name = "QryMaxDateDtp";
            this.QryMaxDateDtp.Size = new System.Drawing.Size(128, 23);
            this.QryMaxDateDtp.TabIndex = 29;
            // 
            // qryRx2TxBtn
            // 
            this.qryRx2TxBtn.Location = new System.Drawing.Point(393, 247);
            this.qryRx2TxBtn.Name = "qryRx2TxBtn";
            this.qryRx2TxBtn.Size = new System.Drawing.Size(60, 23);
            this.qryRx2TxBtn.TabIndex = 61;
            this.qryRx2TxBtn.Text = "<<<";
            this.qryRx2TxBtn.UseVisualStyleBackColor = true;
            // 
            // qryTx2RxBtn
            // 
            this.qryTx2RxBtn.Location = new System.Drawing.Point(393, 203);
            this.qryTx2RxBtn.Name = "qryTx2RxBtn";
            this.qryTx2RxBtn.Size = new System.Drawing.Size(60, 23);
            this.qryTx2RxBtn.TabIndex = 60;
            this.qryTx2RxBtn.Text = ">>>";
            this.qryTx2RxBtn.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(393, 74);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 15);
            this.label26.TabIndex = 59;
            this.label26.Text = "satelite:";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(442, 67);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(136, 23);
            this.comboBox3.TabIndex = 58;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(3, 103);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(23, 15);
            this.label27.TabIndex = 57;
            this.label27.Text = "TX:";
            // 
            // TxPacQryLibx
            // 
            this.TxPacQryLibx.FormattingEnabled = true;
            this.TxPacQryLibx.HorizontalScrollbar = true;
            this.TxPacQryLibx.ItemHeight = 15;
            this.TxPacQryLibx.Location = new System.Drawing.Point(28, 103);
            this.TxPacQryLibx.Name = "TxPacQryLibx";
            this.TxPacQryLibx.Size = new System.Drawing.Size(359, 289);
            this.TxPacQryLibx.TabIndex = 56;
            this.TxPacQryLibx.SelectedIndexChanged += new System.EventHandler(this.TxPacQryLibx_SelectedIndexChanged);
            // 
            // QryPacketDetailsTxb
            // 
            this.QryPacketDetailsTxb.Location = new System.Drawing.Point(757, 103);
            this.QryPacketDetailsTxb.Multiline = true;
            this.QryPacketDetailsTxb.Name = "QryPacketDetailsTxb";
            this.QryPacketDetailsTxb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.QryPacketDetailsTxb.Size = new System.Drawing.Size(420, 303);
            this.QryPacketDetailsTxb.TabIndex = 55;
            this.QryPacketDetailsTxb.WordWrap = false;
            // 
            // qryClearBtn
            // 
            this.qryClearBtn.Location = new System.Drawing.Point(378, 414);
            this.qryClearBtn.Name = "qryClearBtn";
            this.qryClearBtn.Size = new System.Drawing.Size(75, 23);
            this.qryClearBtn.TabIndex = 54;
            this.qryClearBtn.Text = "clear";
            this.qryClearBtn.UseVisualStyleBackColor = true;
            this.qryClearBtn.Click += new System.EventHandler(this.QryClearBtn_Click);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(714, 103);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(40, 15);
            this.label28.TabIndex = 53;
            this.label28.Text = "Detail:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(400, 103);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(24, 15);
            this.label29.TabIndex = 52;
            this.label29.Text = "RX:";
            // 
            // RxPacQryLibx
            // 
            this.RxPacQryLibx.FormattingEnabled = true;
            this.RxPacQryLibx.HorizontalScrollbar = true;
            this.RxPacQryLibx.ItemHeight = 15;
            this.RxPacQryLibx.Location = new System.Drawing.Point(456, 103);
            this.RxPacQryLibx.Name = "RxPacQryLibx";
            this.RxPacQryLibx.Size = new System.Drawing.Size(252, 289);
            this.RxPacQryLibx.TabIndex = 51;
            this.RxPacQryLibx.SelectedIndexChanged += new System.EventHandler(this.RxPacQryLibx_SelectedIndexChanged);
            // 
            // QueryTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.qrySettingsPnl);
            this.Controls.Add(this.qryRx2TxBtn);
            this.Controls.Add(this.qryTx2RxBtn);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.TxPacQryLibx);
            this.Controls.Add(this.QryPacketDetailsTxb);
            this.Controls.Add(this.qryClearBtn);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.RxPacQryLibx);
            this.Name = "QueryTab";
            this.Size = new System.Drawing.Size(1300, 550);
            this.qrySettingsPnl.ResumeLayout(false);
            this.qrySettingsPnl.PerformLayout();
            this.FieldOptionsPanel.ResumeLayout(false);
            this.FieldOptionsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel qrySettingsPnl;
        private System.Windows.Forms.ComboBox QryLimitCB;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Panel FieldOptionsPanel;
        private System.Windows.Forms.ComboBox QryFieldCB;
        private System.Windows.Forms.ComboBox QryFiledConditionCB;
        private System.Windows.Forms.DateTimePicker QryConditionValueDtp;
        private System.Windows.Forms.TextBox QryConditionValueTxb;
        private System.Windows.Forms.ComboBox QrySubtypeCB;
        private System.Windows.Forms.CheckBox QryFieldChbx;
        private System.Windows.Forms.CheckBox qryTxChbx;
        private System.Windows.Forms.CheckBox qryRxChbx;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox QryGroupsCB;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Button qryStartBtn;
        private System.Windows.Forms.DateTimePicker QryMinDateDtp;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox QryIdTxb;
        private System.Windows.Forms.CheckBox QryIdChbx;
        private System.Windows.Forms.DateTimePicker QryMaxDateDtp;
        private System.Windows.Forms.Button qryRx2TxBtn;
        private System.Windows.Forms.Button qryTx2RxBtn;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.ListBox TxPacQryLibx;
        private System.Windows.Forms.TextBox QryPacketDetailsTxb;
        private System.Windows.Forms.Button qryClearBtn;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ListBox RxPacQryLibx;
    }
}
