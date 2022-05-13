using SPL_Manager.Library.Shared;
using SPL_Manager.UI.Views.MainTabViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SPL_Manager.UI.Views
{
    public static class CustomViewImplementation
    {
        public static void AlertUser(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void NotifyUser(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
