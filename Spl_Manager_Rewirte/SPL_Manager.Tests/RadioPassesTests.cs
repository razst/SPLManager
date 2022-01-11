using System;
using Xunit;
using Autofac;
using SPL_Manager.Tests.Mucks.View;
using SPL_Manager.Library.Presenters.MainTabPresenters;
using System.Threading.Tasks;
using FluentAssertions;
using System.Threading;

namespace SPL_Manager.Tests
{
    public class RadioPassesTests
    {
        private RadioPassViewMuck _radioViewMuck;
        private RadioPassesPresenter _radioPassesPresenter;
        public RadioPassesTests()
        {
            ContainerConfig.ConfigRadioTests();
            _radioViewMuck = new RadioPassViewMuck();
            _radioPassesPresenter = ContainerConfig.Resolve<RadioPassesPresenter>();
            _radioPassesPresenter.SetView(_radioViewMuck);
        }

        [Fact (Skip = "Only works on Debug for some reason")]
        public void BasicUtcTest()
        {
            string time1 = _radioViewMuck.UtcDate;
            time1.Should().NotBeNullOrEmpty();

            Task.Delay(1000).GetAwaiter().GetResult();

            _radioViewMuck.UtcDate.Should().NotBe(time1);
        }
    }
}
