using Xunit;
using NSubstitute;
using FluentAssertions;

using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;
using System.Threading.Tasks;
using SPL_Manager.Library.SatRadioPass;

namespace SPL_Manager.Tests
{
    public class PacketModelTests
    {

        public PacketModelTests()
        {
            ProgramProps.Init();
        }

        [Theory]
        [InlineData(SamplePackets.Beacon, "TRXVU", "Beacon")]
        public void PacketShouldTranslateWithNoErrors(string rawPacket, string expectedType, string expectedSubType)
        {
            PacketObject po = new PacketObject(rawPacket);
            po.GetTypeName().Should().Be(expectedType);
            po.GetSubTypeName().Should().Be(expectedSubType);
        }

        [Theory]
        [InlineData("FF FF 00 00")]
        public void PacketTranlationShouldThrowErrors(string rawPacket)
        {
            PacketObject po = new PacketObject(rawPacket);
            po.GetTypeName().Should().Be("Error");
        }
    }
}
