using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Views
{
    public interface IView
    {
        object Invoke(Delegate method, params object[] args);
        object Invoke(Delegate method);
    }

    public static class IViewExtentions
    {
        public static void SafelyRun(this IView view, Action<string> SetText, string text)
        {
            view.Invoke(SetText, new object[] { text });
        }
        public static void SafelyRun(this IView view, Func<Task> AsyncAction)
        {
            view.Invoke(AsyncAction);
        }

    }
}