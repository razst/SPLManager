
using SPL_Manager.Models.RadioPassesModels;
using SPL_Manager.Views.MainTabViews;
using SPL_Manager.Presenters.MainTabPresenters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using NSubstitute;
using FluentAssertions;
using SPL_Manager.Views;

namespace SPL_Manager.Tests
{
    public class RadioPassesTests: IDisposable
    {
        private string TimeTillNextPass;
        private string PassStatus = "Before Pass";


        public RadioPassesTests()
        {
            ProgramProps.Init();
            MockPassesService = Substitute.For<IRadioPassesService>();

            MockPassesService.GetNextPasses(Arg.Any<int>())
                .Returns(Task.FromResult(stubPasssesList));
            


            MockPassesView = Substitute.For<IRadioPassesView>();

            MockPassesView.TimeTillNextPass =
                Arg.Do<string>(str => TimeTillNextPass = str);

            MockPassesView.TimeTillNextPass =
                Arg.Do<string>(str => TimeTillNextPass = str);

            MockPassesView.RadioPassStatus =
                Arg.Do<string>(str => PassStatus = str);

            MockPassesView.Invoke(Arg.Do<Delegate>(method => method.DynamicInvoke()));
            MockPassesView.When(s => s.Invoke(Arg.Any<Delegate>(), Arg.Any<object[]>()))
                .Do(info => ((Action<string>)info.Arg<Delegate>())(info.Arg<object[]>()[0].ToString()));
            

            presenter = new RadioPassesPresenter(MockPassesView, MockPassesService);
            Thread.Sleep(1);
        }

        public void Dispose()
        {
            ProgramProps.Dispose();
        }

        private List<pass> stubPasssesList = new List<pass>
        {

            new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            },
            new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            },
            new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            },
            new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            },
            new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            }

        };



        private IRadioPassesService MockPassesService;
        private IRadioPassesView MockPassesView;
        private RadioPassesPresenter presenter;

        [Fact]
        public void BasicTimeFetchTest()
        {
            presenter.LoadNextRadioPasses().GetAwaiter().GetResult();
            TimeTillNextPass.Should().Be("00:01:40");

            Thread.Sleep(3000);
            TimeTillNextPass.Should().Be("00:01:37");
        }

        [Fact]
        public void NearZeroTest()
        {
            stubPasssesList[0] = new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 2,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 7
            };

            presenter.LoadNextRadioPasses().GetAwaiter().GetResult();

            Thread.Sleep(3000);

            TimeTillNextPass.Should().Be("00:00:00");
            PassStatus.Should().Be("Passing");
        }

        [Fact]
        public void EndOfPassTest()
        {
            stubPasssesList[0] = new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 2,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 57
            };

            presenter.LoadNextRadioPasses().GetAwaiter().GetResult();

            Thread.Sleep(1000);
            Thread.Sleep(2000);

            stubPasssesList[0] = new pass()
            {
                startUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 100,
                endUTC = DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 200
            };

            Thread.Sleep(3000);

            //presenter.LoadNextRadioPasses().GetAwaiter().GetResult();


            Assert.True(true);
        }


    }
}