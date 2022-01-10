using SPL_Manager.Library.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SPL_Manager.Library.Models.PacketFilesModels
{
    public interface IPacketFilesService
    {
        public void CreateNewPacketFile(PacketFileData fileData);
    }
    public struct PacketFileData
    {
        public string FileLocation;
        public List<PacketObject> Packets;
        public List<DateTime> Dates;
        public bool IsOneTypeFile;
    }
}