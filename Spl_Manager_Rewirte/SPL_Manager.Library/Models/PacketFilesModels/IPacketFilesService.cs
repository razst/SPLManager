using SPL_Manager.Library.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SPL_Manager.Library.Models.PacketFilesModels
{
    public interface IPacketFilesService
    {
        public void CreateNewTableFromData(PacketFileData fileData);
        public void SaveAsExcelFileAt(string fileLoc);
        public void SaveAsCsvFileAt(string fileLoc);
    }
    public struct PacketFileData
    {
        public string FileLocation;
        public List<PacketObject> Packets;
        public List<DateTime> Dates;
        public bool IsOneTypeFile;
    }
}