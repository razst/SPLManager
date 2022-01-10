using System;
using System.Collections.Generic;
using System.Text;
using SPL_Manager.Library.Views.RxTabViews;
using SPL_Manager.Library.Models.PacketFilesModels;
using System.IO;
using SPL_Manager.Library.Models.SatPacketModels;

namespace SPL_Manager.Library.Presenters.RxTabPresenters
{
    public class PacketFilesPresenter
    {
        private IPacketDisplayView _view;
        private readonly IPacketFilesService _fileService;

        public PacketFilesPresenter(IPacketFilesService service)
        {
            _fileService = service;
        }
        public void SetView(IPacketDisplayView view)
        {
            _view = view;
        }

        private string AskUserForFileName()
        {
            /*
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "|",
                Title = "Export history to a File",
                FileName = $"Packets_Log_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };


            if (saveFileDialog.ShowDialog() != DialogResult.OK) return "null";
            return saveFileDialog.FileName;
            */
            return "";
        }

        public void SaveAllPacketsInOneFile()
        {
            var PacLst = _view.RxTabPresenter.GetAllRxPackets();
            var DateLst = _view.RxTabPresenter.GetAllRxDates();


            var name = AskUserForFileName();
            if (name == "null") return;

            PacketFileData fileData = new PacketFileData()
            {
                Packets = PacLst,
                Dates = DateLst,
                IsOneTypeFile = false,
                FileLocation = name,
            };
            try
            {
                _fileService.CreateNewPacketFile(fileData);
            }
            catch (Exception e)
            {
                //MessageBox.Show($"failed to save packet \n-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        public void SaveByPacketTypesInSeparateFiles()
        {
            var PacLst = _view.RxTabPresenter.GetAllRxPackets();
            var DateLst = _view.RxTabPresenter.GetAllRxDates();

            var name = AskUserForFileName();
            if (name == "null") return;
            System.IO.Directory.CreateDirectory(name);

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
                    FileLocation = $"{name}\\{type.Key}",
                };
                try
                {
                    _fileService.CreateNewPacketFile(fileData);
                }
                catch (Exception e)
                {
                    //MessageBox.Show($"failed to save packet \n-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
    }
}