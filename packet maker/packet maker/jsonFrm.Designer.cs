namespace packet_maker
{
    partial class jsonFrm
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
            this.typeLibx = new System.Windows.Forms.ListBox();
            this.subtypeLibx = new System.Windows.Forms.ListBox();
            this.paramsLisbx = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // typeLibx
            // 
            this.typeLibx.FormattingEnabled = true;
            this.typeLibx.HorizontalScrollbar = true;
            this.typeLibx.Location = new System.Drawing.Point(165, 47);
            this.typeLibx.Name = "typeLibx";
            this.typeLibx.Size = new System.Drawing.Size(120, 264);
            this.typeLibx.TabIndex = 0;
            this.typeLibx.SelectedIndexChanged += new System.EventHandler(this.typeLibx_SelectedIndexChanged);
            this.typeLibx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.typeLibx_MouseDoubleClick);
            // 
            // subtypeLibx
            // 
            this.subtypeLibx.FormattingEnabled = true;
            this.subtypeLibx.HorizontalScrollbar = true;
            this.subtypeLibx.Location = new System.Drawing.Point(291, 47);
            this.subtypeLibx.Name = "subtypeLibx";
            this.subtypeLibx.Size = new System.Drawing.Size(142, 264);
            this.subtypeLibx.TabIndex = 1;
            this.subtypeLibx.SelectedIndexChanged += new System.EventHandler(this.subtypeLibx_SelectedIndexChanged);
            this.subtypeLibx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.subtypeLibx_MouseDoubleClick);
            // 
            // paramsLisbx
            // 
            this.paramsLisbx.FormattingEnabled = true;
            this.paramsLisbx.HorizontalScrollbar = true;
            this.paramsLisbx.Location = new System.Drawing.Point(440, 47);
            this.paramsLisbx.Name = "paramsLisbx";
            this.paramsLisbx.Size = new System.Drawing.Size(120, 264);
            this.paramsLisbx.TabIndex = 2;
            this.paramsLisbx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.paramsLisbx_MouseDoubleClick);
            // 
            // jsonFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.paramsLisbx);
            this.Controls.Add(this.subtypeLibx);
            this.Controls.Add(this.typeLibx);
            this.Name = "jsonFrm";
            this.Text = "jsonFrm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.jsonFrm_FormClosing);
            this.Load += new System.EventHandler(this.jsonFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox typeLibx;
        private System.Windows.Forms.ListBox subtypeLibx;
        private System.Windows.Forms.ListBox paramsLisbx;
    }
}