using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.PacketsFiles
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


    public class PacketFilesService : IPacketFilesService
    {
        private DataTable _packetFileTable;
        public PacketFilesService()
        {

        }
        public void CreateNewTableFromData(PacketFileData fileData)
        {
            DataTable XlTable = new DataTable();
            List<string> lst = new List<string>
            {
                "Date",
                "Id",
                "SatGroup",
                "Type",
                "Subtype",
                "Lenght",
                "Raw packet",
                "Raw data"
            };

            if (fileData.IsOneTypeFile)
            {
                lst.AddRange
                    (fileData.Packets[0].DataCatalog.Keys.ToList());
            }
            lst.ForEach(col => XlTable.Columns.Add(col));


            int i = -1;
            foreach (var packet in fileData.Packets)
            {
                i++;


                var PacLstTemp = packet.Type != -1 ?

                    new List<string>
                    {
                        fileData.Dates[i].ToString("G"),
                        packet.Id.ToString(),
                        packet.SateliteGroup,
                        packet.GetTypeName(),
                        packet.GetSubTypeName(),
                        packet.Length.ToString(),
                        packet.RawPacket,
                        packet.GetDescriptionStringInline()
                    }
                    :
                    new List<string>
                    {
                        fileData.Dates[i].ToString("G"),
                        "-1",
                        "-",
                        "ERROR",
                        "-",
                        "-",
                        packet.RawPacket,
                        packet.GetDescriptionStringInline()
                    }
                ;

                if (fileData.IsOneTypeFile)
                    packet.DataCatalog.Values.ToList()
                        .ForEach(val => PacLstTemp.Add(val.ToString()));

                XlTable.Rows.Add(PacLstTemp.ToArray());
            }




            _packetFileTable = XlTable;

        }


        public void SaveAsExcelFileAt(string fileLoc)
        {
            using XLWorkbook wk = new XLWorkbook();

            var sheet = wk.Worksheets.Add(_packetFileTable, "Packets");
            sheet.Table(0).Theme = XLTableTheme.None;
            sheet.Table(0).SetShowAutoFilter(false);
            sheet.Columns().AdjustToContents();
            sheet.RightToLeft = false;
            wk.SaveAs(fileLoc);
        }
        public void SaveAsCsvFileAt(string fileLoc)
        {
            using StreamWriter writer = new StreamWriter(new FileStream(
                    fileLoc,
                    FileMode.Create,
                    FileAccess.Write));

            var f = string.Join(", ",
                _packetFileTable.Columns.Cast<DataColumn>().ToList()
                .ConvertAll(col => col.ColumnName));
            writer.WriteLine(f);

            foreach (var line in _packetFileTable.Rows.Cast<DataRow>())
            {
                writer.WriteLine(string.Join(", ", line.ItemArray));
            }
        }
    }
}