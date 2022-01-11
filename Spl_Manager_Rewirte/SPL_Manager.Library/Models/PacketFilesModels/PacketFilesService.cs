using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using ClosedXML.Excel;

namespace SPL_Manager.Library.Models.PacketFilesModels
{
    public class PacketFilesService : IPacketFilesService
    {
        public PacketFilesService()
        {

        }
        public void CreateNewPacketFile(PacketFileData fileData) //TODO: return XLTable and let the presenter choose how to save
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




            if ((bool)ProgramProps.settings.enableExcel)
            {
                SaveAsExcelFileAt($"{fileData.FileLocation}.xlsx", XlTable);
            }
            else
            {
                SaveAsCsvFileAt($"{fileData.FileLocation}.csv", XlTable);
            }

        }


        private void SaveAsExcelFileAt(string fileLoc, DataTable XLTable)
        {
            using XLWorkbook wk = new XLWorkbook();

            var sheet = wk.Worksheets.Add(XLTable, "Packets");
            sheet.Table(0).Theme = XLTableTheme.None;
            sheet.Table(0).SetShowAutoFilter(false);
            sheet.Columns().AdjustToContents();
            sheet.RightToLeft = false;
            wk.SaveAs(fileLoc);
        }
        private void SaveAsCsvFileAt(string fileLoc, DataTable XLTable)
        {
            using StreamWriter writer = new StreamWriter(new FileStream(
                    fileLoc,
                    FileMode.Create,
                    FileAccess.Write));

            var f = String.Join(", ",
                XLTable.Columns.Cast<DataColumn>().ToList()
                .ConvertAll(col => col.ColumnName));
            writer.WriteLine(f);

            foreach (var line in XLTable.Rows.Cast<DataRow>())
            {
                writer.WriteLine(String.Join(", ", line.ItemArray));
            }
        }
    }
}