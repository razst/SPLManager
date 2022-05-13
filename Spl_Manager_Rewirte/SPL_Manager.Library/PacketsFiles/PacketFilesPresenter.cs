using System;
using System.Collections.Generic;
using System.IO;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.PacketLifecycle.History;
using SPL_Manager.Library.Shared;

namespace SPL_Manager.Library.PacketsFiles
{
    public class PacketFilesPresenter
    {
        private IPacketHistoryView _view;
        private readonly IPacketFilesService _fileService;

        public PacketFilesPresenter(IPacketFilesService service)
        {
            _fileService = service;
        }
        public PacketFilesPresenter() : this(new PacketFilesService()) { }

        public void SetView(IPacketHistoryView view)
        {
            _view = view;
        }

        public void SaveAllPacketsInOneFile()
        {
            var PacLst = _view.RxTabPresenter.GetAllRxPackets();
            var DateLst = _view.RxTabPresenter.GetAllRxDates();

            var fileName = _view.AskUserForFileName(
                "Export history to one file", 
                $"Packets_Log_{DateTime.Now.ToString("dd-MM-yyyy")}");

            if (fileName == null) return;

            PacketFileData fileData = new PacketFileData()
            {
                Packets = PacLst,
                Dates = DateLst,
                IsOneTypeFile = false,
                FileLocation = fileName,
            };
            try
            {
                _fileService.CreateNewTableFromData(fileData);

                if ((bool)ProgramProps.settings.enableExcel)
                {
                    _fileService.SaveAsExcelFileAt(fileData.FileLocation);
                }
                else
                {
                    _fileService.SaveAsCsvFileAt(fileData.FileLocation);
                }
            }
            catch (Exception e)
            {
                _view.AlertUser("Error", $"failed to save packets \n-{e.Message}");
                return;
            }

        }

        public void SaveByPacketTypesInFolder()
        {
            var PacLst = _view.RxTabPresenter.GetAllRxPackets();
            var DateLst = _view.RxTabPresenter.GetAllRxDates();

            var fileName = _view.AskUserForFileName(
                "Export history to a folder",
                $"Packets_Log_{DateTime.Now.ToString("dd-MM-yyyy")}");
            
            if (fileName == null) return;
            Directory.CreateDirectory(fileName);

            Dictionary<string, List<PacketObject>> SortedPackets = new Dictionary<string, List<PacketObject>>();
            Dictionary<string, List<DateTime>> SortedPacketDates = new Dictionary<string, List<DateTime>>();


            // seperate packets by types
            int i = -1;
            foreach (var packet in PacLst)
            {
                i++;
                string keyName = $"{packet.GetTypeName()}_{packet.GetSubTypeName()}";
                if (!SortedPackets.ContainsKey(keyName))
                {
                    SortedPackets.Add(keyName, new List<PacketObject>());
                    SortedPacketDates.Add(keyName, new List<DateTime>());

                }
                SortedPackets[keyName].Add(packet);
                SortedPacketDates[keyName].Add(DateLst[i]);
            }


            // save each type to file
            foreach (var type in SortedPackets)
            {
                PacketFileData fileData = new PacketFileData()
                {
                    Packets = type.Value,
                    Dates = SortedPacketDates[type.Key],
                    IsOneTypeFile = true,
                    FileLocation = $"{fileName}\\{type.Key}",
                };
                try
                {
                    _fileService.CreateNewTableFromData(fileData);

                    if ((bool)ProgramProps.settings.enableExcel)
                    {
                        _fileService.SaveAsExcelFileAt(fileData.FileLocation);
                    }
                    else
                    {
                        _fileService.SaveAsCsvFileAt(fileData.FileLocation);
                    }
                }
                catch (Exception e)
                {
                    _view.AlertUser("Error", $"failed to save packets \n-{e.Message}");
                    return;
                }
            }

        }
    }
}