using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SPL_Manager.UI.Views.MainTabViews;

using SPL_Manager.Library.Views.RxTabViews;
using SPL_Manager.Library.Views.MainTabViews;
using SPL_Manager.Library.Views.TxTabViews;
using SPL_Manager.Library.Presenters;
using SPL_Manager.Library.Models.PacketServer;
using SPL_Manager.Library.Models.PacketFilesModels;
using SPL_Manager.Library.Presenters.RxTabPresenters;
using SPL_Manager.Library.Presenters.MainTabPresenters;
using SPL_Manager.Library.Models.RadioPassesModels;
using SPL_Manager.Library.Models.DataAccess;
using SPL_Manager.Library.Presenters.QueryTabPresenters;
using SPL_Manager.Library.Presenters.TxTabPresenters;

namespace SPL_Manager.UI
{
    public static class ContainerConfig
    {
        static IContainer? Container;

        public static T Resolve<T>() where T : notnull
        {
            if (Container == null) throw new Exception("Container is null");
            using var scope = Container.BeginLifetimeScope();
            return scope.Resolve<T>();
        }
        public static void ConfigMain()
        {
            var builder = new ContainerBuilder();

            // Presenters
            builder.RegisterType<PacketServerPresenter>()
                .SingleInstance();

            builder.RegisterType<PacketFilesPresenter>()
                .SingleInstance();

            builder.RegisterType<RxTabPresenter>()
                .SingleInstance();

            builder.RegisterType<RxQueryPresenter>()
                .SingleInstance();

            builder.RegisterType<RadioPassesPresenter>()
                .SingleInstance();

            builder.RegisterType<SatDataPresenter>()
                .SingleInstance();

            builder.RegisterType<QuerySelctionPresenter>()
                .SingleInstance();

            builder.RegisterType<QueryPacketDisplayPresenter>()
                .SingleInstance();

            builder.RegisterType<PlaylistsPresenter>()
                .SingleInstance();

            builder.RegisterType<CreatePacketPresenter>()
                .SingleInstance();

            //Models

            // TODO: rework this registery: TCP server or UDP server
            builder.RegisterType<PacketServer>()
                .As<IPacketServer>()
                .SingleInstance();

            builder.RegisterType<PacketFilesService>()
                .As<IPacketFilesService>()
                .SingleInstance();

            builder.RegisterType<RadioPassesService>()
                .As<IRadioPassesService>()
                .SingleInstance();

            builder.RegisterType<PacketsRepository>()
                .As<IPacketsRepository>()
                .SingleInstance();

            builder.RegisterType<PlaylistRepository>()
                .As<IPlaylistRepository>()
                .SingleInstance();

            Container = builder.Build();
        }
    }
}