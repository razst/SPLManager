using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.PacketServer
{
    public interface IPacketServer
    {
        public bool IsRunning { get; }
        public void Start();

        public Task Send(string msg);

        public event Action<string> OnPacketRecived;

        public void Stop();
    }
}