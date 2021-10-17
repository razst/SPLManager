using SPL_Manager.Models.PacketServer;
using SPL_Manager.Models.SatPacketModels;
using SPL_Manager.Views;
using SPL_Manager.Views.RxTabViews;
using SPL_Manager.Views.TxTabViews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPL_Manager.Presenters
{
    public class PacketServerPresenter
    {
        private readonly ITxTabView _txView;
        private readonly IPacketDisplayView _rxView;
        private readonly IPacketServer _packetServer;
        public PacketServerPresenter(IPacketDisplayView rxView, ITxTabView txView, IPacketServer packetServer = null)
        {
            _rxView = rxView;
            _txView = txView;
            if (packetServer == null) 
            {
                _packetServer = new PacketServer();
                _packetServer.OnPacketRecived += OnPacketRecived;
            } 
            else _packetServer = packetServer;
        }

        public async Task SendCurrentPacket(int count = 1)
        {
            if (!PacketServer.isOnline)
            {
                if(count == 1)
                    MessageBox.Show("server is not online", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PacketObject po = 
                new PacketObject(ProgramProps.PacketJsonFiles["Tx"], _txView.TxPacketHexStr.Trim());
            if (po.Type == -1) return;
            await _packetServer.Send(_txView.TxPacketHexStr);

            _rxView.RxTabPresenter.AddSentPacket(po, DateTime.Now);

            if (ProgramProps.DataBaseEnabled)
                await po.Upload("parsed-tx");
        }




        public void StartServer()
        {
            _packetServer.Strat();
        }


        private async void OnPacketRecived(string PacketHex)
        {
            string mess = PacketHex.Trim().Replace('-', ' ');
            PacketObject po = new PacketObject();

            //loop to find type of parsing
            foreach (var RxOption in ProgramProps.PacketJsonFiles)
            {
                int currentGroupDex = -1;
                if (RxOption.Key.EndsWith("Tau")) currentGroupDex = 9;
                Task.Delay(1).GetAwaiter().GetResult();
                po = new PacketObject(RxOption.Value, mess, currentGroupDex);
                if (po.Type != -1)
                {
                    if (RxOption.Key.StartsWith("Tx")) return;
                    break;
                }

            }

            _rxView.RxTabPresenter.AddRecivedPacket(po, DateTime.Now);

            if(ProgramProps.DataBaseEnabled)
                await po.Upload("parsed-rx");
        }



        public async Task ResendCurrentTxItem()
        {
            if (_rxView.TxItemsIndex == -1) return;
            PacketObject po = _rxView.RxTabPresenter.GetCurrentTxItem();
            await _packetServer.Send(po.RawPacket);
            _rxView.RxTabPresenter.AddSentPacket(po, DateTime.Now);

            if (ProgramProps.DataBaseEnabled)
                await po.Upload("parsed-tx");
        }


        public void OpenEndNode()
        {
            if (PacketServer.ServerMode != "TCP") return; //TODO: popup to user - "no need to init connection"
            try
            {
                System.Diagnostics.Process.Start((string)ProgramProps.settings.endNodePath);
            }
            catch
            {

            }
        }

    }
}
