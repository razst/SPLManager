using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Views.TxTabViews
{
    public interface IPlaylistsView : IView
    {
        public List<string> PlaylistsNames { get; set; }
        public int SelectedPlaylist { get; set; }
        public List<string> PlayListItems { get; set; }
        public int SelectedPlaylistItem { get; set; }
        public string CmdSleepValue { get; set; }

        public Presenters.TxTabPresenters.PlaylistsPresenter PlaylistsPresenter { get; }
    }

    public static class IPlaylistsViewExtentions
    {
        public static void Clear(this IPlaylistsView view)
        {
            var t = view.PlaylistsNames;
            if (view.SelectedPlaylist != -1)
                t.RemoveAt(view.SelectedPlaylist);
            view.PlaylistsNames = t;
            view.SelectedPlaylist = -1;

            view.PlayListItems = new List<string>();
            view.CmdSleepValue = "";

        }
    }
}