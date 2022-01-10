using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SPL_Manager.UI.Views.AboutView
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }
        private Main _main;
        public void Init(Main main)
        {
            _main = main;
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            CenterToScreen();
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            VersionLabel.Text = $"Version: {version}";
        }

        private void AboutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _main.Show();
        }
    }
}
