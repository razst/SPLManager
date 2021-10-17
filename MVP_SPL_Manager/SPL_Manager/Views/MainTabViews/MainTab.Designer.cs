
namespace SPL_Manager.Views.MainTabViews
{
    partial class MainTab
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
            this.BatTempGauge = new AquaControls.AquaGauge();
            this.FreeSpaceGauge = new AquaControls.AquaGauge();
            this.OBCTempGauge = new AquaControls.AquaGauge();
            this.VBatGauge = new AquaControls.AquaGauge();
            this.panel19 = new System.Windows.Forms.Panel();
            this.label44 = new System.Windows.Forms.Label();
            this.MainCorruptBytesLbl = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label49 = new System.Windows.Forms.Label();
            this.MainCmdRestesLbl = new System.Windows.Forms.Label();
            this.panel16 = new System.Windows.Forms.Panel();
            this.label45 = new System.Windows.Forms.Label();
            this.MainSatResetsLbl = new System.Windows.Forms.Label();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label47 = new System.Windows.Forms.Label();
            this.MainSatUptimeLbl = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.label43 = new System.Windows.Forms.Label();
            this.MainSatTimeLbl = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label41 = new System.Windows.Forms.Label();
            this.MainLastPassLbl = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label39 = new System.Windows.Forms.Label();
            this.MainPassStatusLbl = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label32 = new System.Windows.Forms.Label();
            this.MainLastBeaconLbl = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label38 = new System.Windows.Forms.Label();
            this.MainLastTxLbl = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label36 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.MainLastRxLbl = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.utcTitle = new System.Windows.Forms.Label();
            this.MainUtcTimeLbl = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.MainNextPassLbl1 = new System.Windows.Forms.Label();
            this.MainNextPassLbl2 = new System.Windows.Forms.Label();
            this.MainNextPassLbl3 = new System.Windows.Forms.Label();
            this.MainNextPassLbl4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label30 = new System.Windows.Forms.Label();
            this.MainTimeTillPassLbl = new System.Windows.Forms.Label();
            this.MainGroupsCB = new System.Windows.Forms.ComboBox();
            this.panel19.SuspendLayout();
            this.panel18.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel17.SuspendLayout();
            this.panel15.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // BatTempGauge
            // 
            this.BatTempGauge.BackColor = System.Drawing.Color.Transparent;
            this.BatTempGauge.DialColor = System.Drawing.Color.Lavender;
            this.BatTempGauge.DialText = "Bat Temp";
            this.BatTempGauge.Glossiness = 11.36364F;
            this.BatTempGauge.Location = new System.Drawing.Point(430, 415);
            this.BatTempGauge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BatTempGauge.MaxValue = 80F;
            this.BatTempGauge.MinValue = -40F;
            this.BatTempGauge.Name = "BatTempGauge";
            this.BatTempGauge.NoOfDivisions = 12;
            this.BatTempGauge.NoOfSubDivisions = 1;
            this.BatTempGauge.RecommendedValue = 0F;
            this.BatTempGauge.Size = new System.Drawing.Size(175, 173);
            this.BatTempGauge.TabIndex = 63;
            this.BatTempGauge.ThresholdPercent = 0F;
            this.BatTempGauge.Value = 0F;
            // 
            // FreeSpaceGauge
            // 
            this.FreeSpaceGauge.BackColor = System.Drawing.Color.Transparent;
            this.FreeSpaceGauge.DialColor = System.Drawing.Color.Lavender;
            this.FreeSpaceGauge.DialText = "Free Space (MB)";
            this.FreeSpaceGauge.Glossiness = 11.36364F;
            this.FreeSpaceGauge.Location = new System.Drawing.Point(638, 415);
            this.FreeSpaceGauge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.FreeSpaceGauge.MaxValue = 2200F;
            this.FreeSpaceGauge.MinValue = 0F;
            this.FreeSpaceGauge.Name = "FreeSpaceGauge";
            this.FreeSpaceGauge.NoOfDivisions = 11;
            this.FreeSpaceGauge.NoOfSubDivisions = 1;
            this.FreeSpaceGauge.RecommendedValue = 0F;
            this.FreeSpaceGauge.Size = new System.Drawing.Size(175, 173);
            this.FreeSpaceGauge.TabIndex = 62;
            this.FreeSpaceGauge.ThresholdPercent = 0F;
            this.FreeSpaceGauge.Value = 0F;
            // 
            // OBCTempGauge
            // 
            this.OBCTempGauge.BackColor = System.Drawing.Color.Transparent;
            this.OBCTempGauge.DialColor = System.Drawing.Color.Lavender;
            this.OBCTempGauge.DialText = "OBC Temp";
            this.OBCTempGauge.Glossiness = 11.36364F;
            this.OBCTempGauge.Location = new System.Drawing.Point(225, 415);
            this.OBCTempGauge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.OBCTempGauge.MaxValue = 80F;
            this.OBCTempGauge.MinValue = -40F;
            this.OBCTempGauge.Name = "OBCTempGauge";
            this.OBCTempGauge.NoOfDivisions = 12;
            this.OBCTempGauge.NoOfSubDivisions = 1;
            this.OBCTempGauge.RecommendedValue = 0F;
            this.OBCTempGauge.Size = new System.Drawing.Size(175, 173);
            this.OBCTempGauge.TabIndex = 61;
            this.OBCTempGauge.ThresholdPercent = 0F;
            this.OBCTempGauge.Value = 0F;
            // 
            // VBatGauge
            // 
            this.VBatGauge.BackColor = System.Drawing.Color.Transparent;
            this.VBatGauge.DialColor = System.Drawing.Color.Lavender;
            this.VBatGauge.DialText = "VBat";
            this.VBatGauge.Glossiness = 11.36364F;
            this.VBatGauge.Location = new System.Drawing.Point(17, 415);
            this.VBatGauge.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VBatGauge.MaxValue = 9F;
            this.VBatGauge.MinValue = 0F;
            this.VBatGauge.Name = "VBatGauge";
            this.VBatGauge.NoOfDivisions = 9;
            this.VBatGauge.RecommendedValue = 8F;
            this.VBatGauge.Size = new System.Drawing.Size(175, 173);
            this.VBatGauge.TabIndex = 60;
            this.VBatGauge.ThresholdPercent = 0F;
            this.VBatGauge.Value = 8F;
            // 
            // panel19
            // 
            this.panel19.BackColor = System.Drawing.Color.DimGray;
            this.panel19.Controls.Add(this.label44);
            this.panel19.Controls.Add(this.MainCorruptBytesLbl);
            this.panel19.Location = new System.Drawing.Point(1054, 290);
            this.panel19.Name = "panel19";
            this.panel19.Size = new System.Drawing.Size(200, 100);
            this.panel19.TabIndex = 59;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label44.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label44.Location = new System.Drawing.Point(39, 13);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(150, 26);
            this.label44.TabIndex = 1;
            this.label44.Text = "Corrupt Bytes";
            this.label44.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainCorruptBytesLbl
            // 
            this.MainCorruptBytesLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainCorruptBytesLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainCorruptBytesLbl.Location = new System.Drawing.Point(51, 45);
            this.MainCorruptBytesLbl.Name = "MainCorruptBytesLbl";
            this.MainCorruptBytesLbl.Size = new System.Drawing.Size(105, 40);
            this.MainCorruptBytesLbl.TabIndex = 1;
            this.MainCorruptBytesLbl.Text = "---";
            this.MainCorruptBytesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel18
            // 
            this.panel18.BackColor = System.Drawing.Color.DimGray;
            this.panel18.Controls.Add(this.label49);
            this.panel18.Controls.Add(this.MainCmdRestesLbl);
            this.panel18.Location = new System.Drawing.Point(1054, 184);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(200, 100);
            this.panel18.TabIndex = 51;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label49.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label49.Location = new System.Drawing.Point(39, 13);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(153, 26);
            this.label49.TabIndex = 1;
            this.label49.Text = "Planed Restes";
            this.label49.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainCmdRestesLbl
            // 
            this.MainCmdRestesLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainCmdRestesLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainCmdRestesLbl.Location = new System.Drawing.Point(51, 45);
            this.MainCmdRestesLbl.Name = "MainCmdRestesLbl";
            this.MainCmdRestesLbl.Size = new System.Drawing.Size(105, 40);
            this.MainCmdRestesLbl.TabIndex = 1;
            this.MainCmdRestesLbl.Text = "---";
            this.MainCmdRestesLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.DimGray;
            this.panel16.Controls.Add(this.label45);
            this.panel16.Controls.Add(this.MainSatResetsLbl);
            this.panel16.Location = new System.Drawing.Point(1054, 78);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(200, 100);
            this.panel16.TabIndex = 49;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label45.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label45.Location = new System.Drawing.Point(25, 13);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(179, 26);
            this.label45.TabIndex = 1;
            this.label45.Text = "Unplaned Restes";
            this.label45.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainSatResetsLbl
            // 
            this.MainSatResetsLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainSatResetsLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainSatResetsLbl.Location = new System.Drawing.Point(51, 44);
            this.MainSatResetsLbl.Name = "MainSatResetsLbl";
            this.MainSatResetsLbl.Size = new System.Drawing.Size(91, 40);
            this.MainSatResetsLbl.TabIndex = 1;
            this.MainSatResetsLbl.Text = "---";
            this.MainSatResetsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel17
            // 
            this.panel17.BackColor = System.Drawing.Color.DimGray;
            this.panel17.Controls.Add(this.label47);
            this.panel17.Controls.Add(this.MainSatUptimeLbl);
            this.panel17.Location = new System.Drawing.Point(844, 499);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(410, 100);
            this.panel17.TabIndex = 57;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label47.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label47.Location = new System.Drawing.Point(160, 15);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(122, 26);
            this.label47.TabIndex = 1;
            this.label47.Text = "Sat Uptime";
            this.label47.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainSatUptimeLbl
            // 
            this.MainSatUptimeLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainSatUptimeLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainSatUptimeLbl.Location = new System.Drawing.Point(3, 48);
            this.MainSatUptimeLbl.Name = "MainSatUptimeLbl";
            this.MainSatUptimeLbl.Size = new System.Drawing.Size(406, 56);
            this.MainSatUptimeLbl.TabIndex = 7;
            this.MainSatUptimeLbl.Text = "-- : -- : --";
            this.MainSatUptimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.DimGray;
            this.panel15.Controls.Add(this.label43);
            this.panel15.Controls.Add(this.MainSatTimeLbl);
            this.panel15.Location = new System.Drawing.Point(844, 396);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(410, 100);
            this.panel15.TabIndex = 56;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label43.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label43.Location = new System.Drawing.Point(160, 9);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(110, 26);
            this.label43.TabIndex = 1;
            this.label43.Text = "OBC Time";
            this.label43.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainSatTimeLbl
            // 
            this.MainSatTimeLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainSatTimeLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainSatTimeLbl.Location = new System.Drawing.Point(3, 44);
            this.MainSatTimeLbl.Name = "MainSatTimeLbl";
            this.MainSatTimeLbl.Size = new System.Drawing.Size(406, 56);
            this.MainSatTimeLbl.TabIndex = 7;
            this.MainSatTimeLbl.Text = "-- : -- : --";
            this.MainSatTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.DimGray;
            this.panel12.Controls.Add(this.label41);
            this.panel12.Controls.Add(this.MainLastPassLbl);
            this.panel12.Location = new System.Drawing.Point(222, 184);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(410, 100);
            this.panel12.TabIndex = 58;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label41.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label41.Location = new System.Drawing.Point(157, 13);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(108, 26);
            this.label41.TabIndex = 1;
            this.label41.Text = "Last Pass";
            this.label41.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainLastPassLbl
            // 
            this.MainLastPassLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLastPassLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainLastPassLbl.Location = new System.Drawing.Point(3, 40);
            this.MainLastPassLbl.Name = "MainLastPassLbl";
            this.MainLastPassLbl.Size = new System.Drawing.Size(406, 56);
            this.MainLastPassLbl.TabIndex = 24;
            this.MainLastPassLbl.Text = "-- : -- : --";
            this.MainLastPassLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.DimGray;
            this.panel11.Controls.Add(this.label39);
            this.panel11.Controls.Add(this.MainPassStatusLbl);
            this.panel11.Location = new System.Drawing.Point(222, 78);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(410, 100);
            this.panel11.TabIndex = 55;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label39.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label39.Location = new System.Drawing.Point(171, 13);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(76, 26);
            this.label39.TabIndex = 1;
            this.label39.Text = "Status";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainPassStatusLbl
            // 
            this.MainPassStatusLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainPassStatusLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainPassStatusLbl.Location = new System.Drawing.Point(35, 37);
            this.MainPassStatusLbl.Name = "MainPassStatusLbl";
            this.MainPassStatusLbl.Size = new System.Drawing.Size(319, 56);
            this.MainPassStatusLbl.TabIndex = 1;
            this.MainPassStatusLbl.Text = "Before Pass";
            this.MainPassStatusLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.DimGray;
            this.panel10.Controls.Add(this.label32);
            this.panel10.Controls.Add(this.MainLastBeaconLbl);
            this.panel10.Location = new System.Drawing.Point(638, 290);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(410, 100);
            this.panel10.TabIndex = 54;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label32.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label32.Location = new System.Drawing.Point(141, 13);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(135, 26);
            this.label32.TabIndex = 1;
            this.label32.Text = "Last Beacon";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainLastBeaconLbl
            // 
            this.MainLastBeaconLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLastBeaconLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainLastBeaconLbl.Location = new System.Drawing.Point(3, 44);
            this.MainLastBeaconLbl.Name = "MainLastBeaconLbl";
            this.MainLastBeaconLbl.Size = new System.Drawing.Size(406, 56);
            this.MainLastBeaconLbl.TabIndex = 24;
            this.MainLastBeaconLbl.Text = "-- : -- : --";
            this.MainLastBeaconLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.DimGray;
            this.panel9.Controls.Add(this.label38);
            this.panel9.Controls.Add(this.MainLastTxLbl);
            this.panel9.Location = new System.Drawing.Point(638, 184);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(410, 100);
            this.panel9.TabIndex = 53;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label38.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label38.Location = new System.Drawing.Point(123, 13);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(155, 26);
            this.label38.TabIndex = 1;
            this.label38.Text = "Last Frame TX";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainLastTxLbl
            // 
            this.MainLastTxLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLastTxLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainLastTxLbl.Location = new System.Drawing.Point(3, 40);
            this.MainLastTxLbl.Name = "MainLastTxLbl";
            this.MainLastTxLbl.Size = new System.Drawing.Size(406, 56);
            this.MainLastTxLbl.TabIndex = 24;
            this.MainLastTxLbl.Text = "-- : -- : --";
            this.MainLastTxLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.DimGray;
            this.panel8.Controls.Add(this.panel13);
            this.panel8.Controls.Add(this.panel14);
            this.panel8.Controls.Add(this.label37);
            this.panel8.Controls.Add(this.MainLastRxLbl);
            this.panel8.Location = new System.Drawing.Point(638, 78);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(410, 100);
            this.panel8.TabIndex = 52;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.DimGray;
            this.panel13.Controls.Add(this.label34);
            this.panel13.Controls.Add(this.label35);
            this.panel13.Location = new System.Drawing.Point(0, 212);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(410, 100);
            this.panel13.TabIndex = 34;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label34.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label34.Location = new System.Drawing.Point(141, 13);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(135, 26);
            this.label34.TabIndex = 1;
            this.label34.Text = "Last Beacon";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label35.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label35.Location = new System.Drawing.Point(3, 40);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(406, 56);
            this.label35.TabIndex = 24;
            this.label35.Text = "-- : -- : --";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.DimGray;
            this.panel14.Controls.Add(this.label36);
            this.panel14.Controls.Add(this.label42);
            this.panel14.Location = new System.Drawing.Point(0, 106);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(410, 100);
            this.panel14.TabIndex = 33;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label36.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label36.Location = new System.Drawing.Point(123, 13);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(155, 26);
            this.label36.TabIndex = 1;
            this.label36.Text = "Last Frame TX";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label42
            // 
            this.label42.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label42.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label42.Location = new System.Drawing.Point(3, 40);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(406, 56);
            this.label42.TabIndex = 24;
            this.label42.Text = "-- : -- : --";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label37.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label37.Location = new System.Drawing.Point(121, 13);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(156, 26);
            this.label37.TabIndex = 1;
            this.label37.Text = "Last Frame RX";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainLastRxLbl
            // 
            this.MainLastRxLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainLastRxLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainLastRxLbl.Location = new System.Drawing.Point(3, 44);
            this.MainLastRxLbl.Name = "MainLastRxLbl";
            this.MainLastRxLbl.Size = new System.Drawing.Size(406, 56);
            this.MainLastRxLbl.TabIndex = 7;
            this.MainLastRxLbl.Text = "-- : -- : --";
            this.MainLastRxLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.DimGray;
            this.panel7.Controls.Add(this.utcTitle);
            this.panel7.Controls.Add(this.MainUtcTimeLbl);
            this.panel7.Location = new System.Drawing.Point(222, 290);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(409, 100);
            this.panel7.TabIndex = 50;
            // 
            // utcTitle
            // 
            this.utcTitle.AutoSize = true;
            this.utcTitle.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.utcTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.utcTitle.Location = new System.Drawing.Point(180, 13);
            this.utcTitle.Name = "utcTitle";
            this.utcTitle.Size = new System.Drawing.Size(53, 26);
            this.utcTitle.TabIndex = 1;
            this.utcTitle.Text = "UTC";
            this.utcTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainUtcTimeLbl
            // 
            this.MainUtcTimeLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainUtcTimeLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainUtcTimeLbl.Location = new System.Drawing.Point(3, 40);
            this.MainUtcTimeLbl.Name = "MainUtcTimeLbl";
            this.MainUtcTimeLbl.Size = new System.Drawing.Size(406, 56);
            this.MainUtcTimeLbl.TabIndex = 1;
            this.MainUtcTimeLbl.Text = "-- : -- : --";
            this.MainUtcTimeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DimGray;
            this.panel6.Controls.Add(this.label33);
            this.panel6.Controls.Add(this.MainNextPassLbl1);
            this.panel6.Controls.Add(this.MainNextPassLbl2);
            this.panel6.Controls.Add(this.MainNextPassLbl3);
            this.panel6.Controls.Add(this.MainNextPassLbl4);
            this.panel6.Location = new System.Drawing.Point(16, 184);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(200, 206);
            this.panel6.TabIndex = 48;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.label33.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label33.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label33.Location = new System.Drawing.Point(21, 13);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(182, 26);
            this.label33.TabIndex = 1;
            this.label33.Text = "Following Passes";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainNextPassLbl1
            // 
            this.MainNextPassLbl1.AutoSize = true;
            this.MainNextPassLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainNextPassLbl1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainNextPassLbl1.Location = new System.Drawing.Point(25, 45);
            this.MainNextPassLbl1.Name = "MainNextPassLbl1";
            this.MainNextPassLbl1.Size = new System.Drawing.Size(40, 17);
            this.MainNextPassLbl1.TabIndex = 2;
            this.MainNextPassLbl1.Text = "____";
            // 
            // MainNextPassLbl2
            // 
            this.MainNextPassLbl2.AutoSize = true;
            this.MainNextPassLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainNextPassLbl2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainNextPassLbl2.Location = new System.Drawing.Point(25, 86);
            this.MainNextPassLbl2.Name = "MainNextPassLbl2";
            this.MainNextPassLbl2.Size = new System.Drawing.Size(40, 17);
            this.MainNextPassLbl2.TabIndex = 3;
            this.MainNextPassLbl2.Text = "____";
            // 
            // MainNextPassLbl3
            // 
            this.MainNextPassLbl3.AutoSize = true;
            this.MainNextPassLbl3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainNextPassLbl3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainNextPassLbl3.Location = new System.Drawing.Point(25, 127);
            this.MainNextPassLbl3.Name = "MainNextPassLbl3";
            this.MainNextPassLbl3.Size = new System.Drawing.Size(40, 17);
            this.MainNextPassLbl3.TabIndex = 4;
            this.MainNextPassLbl3.Text = "____";
            // 
            // MainNextPassLbl4
            // 
            this.MainNextPassLbl4.AutoSize = true;
            this.MainNextPassLbl4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainNextPassLbl4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainNextPassLbl4.Location = new System.Drawing.Point(25, 168);
            this.MainNextPassLbl4.Name = "MainNextPassLbl4";
            this.MainNextPassLbl4.Size = new System.Drawing.Size(40, 17);
            this.MainNextPassLbl4.TabIndex = 5;
            this.MainNextPassLbl4.Text = "____";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.DimGray;
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.MainTimeTillPassLbl);
            this.panel5.Location = new System.Drawing.Point(16, 78);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 100);
            this.panel5.TabIndex = 47;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Arial Black", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label30.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label30.Location = new System.Drawing.Point(51, 13);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(112, 26);
            this.label30.TabIndex = 1;
            this.label30.Text = "Next Pass";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainTimeTillPassLbl
            // 
            this.MainTimeTillPassLbl.AutoSize = true;
            this.MainTimeTillPassLbl.Font = new System.Drawing.Font("Arial Black", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.MainTimeTillPassLbl.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.MainTimeTillPassLbl.Location = new System.Drawing.Point(22, 44);
            this.MainTimeTillPassLbl.Name = "MainTimeTillPassLbl";
            this.MainTimeTillPassLbl.Size = new System.Drawing.Size(152, 48);
            this.MainTimeTillPassLbl.TabIndex = 1;
            this.MainTimeTillPassLbl.Text = "-- : -- : --";
            this.MainTimeTillPassLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainGroupsCB
            // 
            this.MainGroupsCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MainGroupsCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainGroupsCB.FormattingEnabled = true;
            this.MainGroupsCB.Location = new System.Drawing.Point(513, 26);
            this.MainGroupsCB.Name = "MainGroupsCB";
            this.MainGroupsCB.Size = new System.Drawing.Size(261, 33);
            this.MainGroupsCB.TabIndex = 46;
            this.MainGroupsCB.SelectedIndexChanged += new System.EventHandler(this.MainGroupsCB_SelectedIndexChanged);
            // 
            // MainTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.Controls.Add(this.BatTempGauge);
            this.Controls.Add(this.FreeSpaceGauge);
            this.Controls.Add(this.OBCTempGauge);
            this.Controls.Add(this.VBatGauge);
            this.Controls.Add(this.panel19);
            this.Controls.Add(this.panel18);
            this.Controls.Add(this.panel16);
            this.Controls.Add(this.panel17);
            this.Controls.Add(this.panel15);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.MainGroupsCB);
            this.Name = "MainTab";
            this.Size = new System.Drawing.Size(1287, 610);
            this.panel19.ResumeLayout(false);
            this.panel19.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AquaControls.AquaGauge BatTempGauge;
        private AquaControls.AquaGauge FreeSpaceGauge;
        private AquaControls.AquaGauge OBCTempGauge;
        private AquaControls.AquaGauge VBatGauge;
        private System.Windows.Forms.Panel panel19;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label MainCorruptBytesLbl;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label MainCmdRestesLbl;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label MainSatResetsLbl;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label MainSatUptimeLbl;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label MainSatTimeLbl;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label MainLastPassLbl;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label MainPassStatusLbl;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label MainLastBeaconLbl;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label MainLastTxLbl;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label MainLastRxLbl;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label utcTitle;
        private System.Windows.Forms.Label MainUtcTimeLbl;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label MainNextPassLbl1;
        private System.Windows.Forms.Label MainNextPassLbl2;
        private System.Windows.Forms.Label MainNextPassLbl3;
        private System.Windows.Forms.Label MainNextPassLbl4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label MainTimeTillPassLbl;
        private System.Windows.Forms.ComboBox MainGroupsCB;
    }
}
