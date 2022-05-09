using Xunit;
using SPL_Manager.Tests.Mucks.View;
using System.Threading.Tasks;
using FluentAssertions;
using SPL_Manager.Library.SatRadioPass;

namespace SPL_Manager.Tests
{
    public class RadioPassesTests
    {
        private RadioPassViewMuck _radioViewMuck;
        private RadioPassesPresenter _radioPassesPresenter;
        public RadioPassesTests()
        {
            _radioViewMuck = new RadioPassViewMuck();
            _radioPassesPresenter = new RadioPassesPresenter();
            _radioPassesPresenter.SetView(_radioViewMuck);
        }

        [Fact /*(Skip = "Only works on Debug for some reason")*/]
        public async void BasicUtcTest()
        {
            await Task.Delay(1000);

            string time1 = _radioViewMuck.UtcDate;
            time1.Should().NotBeNullOrEmpty();
            Task.Delay(1000).GetAwaiter().GetResult();
            var time2 = _radioViewMuck.UtcDate;
            time2.Should()
                .NotBeNullOrEmpty()
                .And
                .NotBe(time1);
        }
    }
}
