using System;
using System.Collections.Generic;
using System.Text;
using SPL_Manager.Models.PacketFilesModels;
using SPL_Manager.Models.SatPacketModels;
using FluentAssertions;
using Xunit;

namespace SPL_Manager.Tests
{
    public class ExcelFilesTests: IDisposable
    {
        public ExcelFilesTests()
        {
            ProgramProps.Init();
            service = new PacketFilesService();
        }
        public void Dispose()
        {
            ProgramProps.Dispose();
        }

        private PacketFilesService service;
        [Fact]
        public void SaveFile_ShouldSaveOneExcelFile()
        {

            DateTime dt = DateTime.Now;

            PacketObject po = new PacketObject("HH HH HH");
            PacketObject po2 = new PacketObject("14 00 00 02 00 11 04 00 3C 00 00 00");

            po.SateliteGroup.Should().Be(ProgramProps.groups[0]);
            po.RawPacket.Should().Be("HH HH HH");
            po2.JsonObject.Should().Be(ProgramProps.PacketJsonFiles["Tx"]);
            Console.WriteLine(po.DataCatalog["reason"]);

            PacketFileData PFD = new PacketFileData()
            {
                Dates = new List<DateTime> { dt, dt,dt },
                Packets = new List<PacketObject> { po, po, po2 },
                FileLocation = "C:/Users/User/Desktop/Grabage/Eg",
                IsOneTypeFile = false
            };

            service.CreateNewPacketFile(PFD);

            Console.WriteLine("Done!");
            Assert.True(true);
        }


        [Fact]
        public void SaveFile_ShouldSaveMuteFile()
        {
            var dt = DateTime.Now;
            PacketObject po2 = new PacketObject("14 00 00 02 00 11 04 00 3C 00 00 00");
            PacketObject po3 = new PacketObject("14 00 00 02 00 11 04 00 FF 00 00 00");

            PacketFileData PFD = new PacketFileData()
            {
                Dates = new List<DateTime> { dt, dt, dt },
                Packets = new List<PacketObject> { po2, po2, po3 },
                FileLocation = "C:/Users/User/Desktop/Grabage/MuteEg",
                IsOneTypeFile = true
            };

            service.CreateNewPacketFile(PFD);

            Console.WriteLine("Done!");
            Assert.True(true);
        }
    }
}
