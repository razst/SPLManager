using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Shared
{
    /// <summary>
    /// Extend this interface to add behaviors to all views
    /// </summary>
    public interface ICustomView
    {
        void AlertUser(string title, string message);
        void NotifyUser(string title, string message);
    }
}
