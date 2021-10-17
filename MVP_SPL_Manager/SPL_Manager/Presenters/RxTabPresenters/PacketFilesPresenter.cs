using System;
using System.Collections.Generic;
using System.Text;
using SPL_Manager.Views.RxTabViews;
using SPL_Manager.Models.PacketFilesModels;
using System.Windows.Forms;
using System.IO;
using SPL_Manager.Models.SatPacketModels;

namespace SPL_Manager.Presenters.RxTabPresenters
{
    public class PacketFilesPresenter
    {
        private readonly IPacketDisplayView _view;
        private readonly IPacketFilesService _fileService;

        public PacketFilesPresenter(IPacketDisplayView view, IPacketFilesService service = null)
        {
            _view = view;
            if (service == null) _fileService = new PacketFilesService();
            else _fileService = service;
        }


        private string AskUserForFileName()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "|",
                Title = "Export history to a File",
                FileName = $"Packets_Log_{DateTime.Now.ToString("dd-MM-yyyy")}"
            };


            if (saveFileDialog.ShowDialog() != DialogResult.OK) return "null";
            return saveFileDialog.FileName;
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
            catch(Exception e)
            {
                MessageBox.Show($"failed to save packet \n-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            foreach(var type in SortedPackets)
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
                    MessageBox.Show($"failed to save packet \n-{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

        }
    }
}
