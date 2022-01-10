using Autofac;
using SPL_Manager.Library.Models.RadioPassesModels;
using SPL_Manager.Library.Presenters.MainTabPresenters;
using SPL_Manager.Library.Views.MainTabViews;
using SPL_Manager.Tests.Mucks.Models;
using SPL_Manager.Tests.Mucks.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Tests
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

        public static void ConfigRadioTests()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<RadioPassesPresenter>()
                .SingleInstance();

            builder.RegisterType<RadioPassServiceMuck>()
                .As<IRadioPassesService>()
                .SingleInstance();

            Container = builder.Build();
        }
    }
}
