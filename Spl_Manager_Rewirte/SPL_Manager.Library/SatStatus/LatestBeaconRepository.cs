using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPL_Manager.Library.SatStatus
{
    public class LatestBeaconRepository
    {
        public async Task<PacketObject> GetLastBeacon(int groupIndex)
        {
            if (groupIndex == -1) return new PacketObject();
            if (groupIndex == 9) return new PacketObject();
            if (groupIndex == 0) return new PacketObject();

            RepositoryWorker worker = new RepositoryWorker();
            worker.AddMatchFilter("satId", groupIndex.ToString());
            worker.AddTermFilter("subtype", "Beacon");
            worker.AddQuerySize(3);
            worker.AddSortDescending();

            var Docs = await worker.StartWork("parsed-rx");
            if (Docs.Count == 0) return new PacketObject();

            PacketObject po = new PacketObject(ProgramProps.PacketJsonFiles["Rx"], Docs[0]?["packetString"]?.ToString() ?? "nill");

            return po;

        }

        public async Task<List<string>> GetLatestPacketsDates(int groupIndex)
        {
            List<string> output = new List<string>();

            if (groupIndex == -1) return output;
            if (groupIndex == 9) return output;

            RepositoryWorker worker = new RepositoryWorker();
            if (groupIndex != 0) worker.AddMatchFilter("satId", groupIndex.ToString());
            worker.AddQuerySize(3);
            worker.AddSortDescending();

            //Rx
            var Docs = await worker.StartWork("parsed-rx");
            if (Docs.Count > 0)
                output.Add((DateTime.Parse((string)Docs[0]["time"])).ToLocalTime().ToString("G"));

            Docs = await worker.StartWork("parsed-tx");
            if (Docs.Count > 0)
                output.Add((DateTime.Parse((string)Docs[0]["time"])).ToLocalTime().ToString("G"));

            worker.AddTermFilter("subtype", "Beacon");
            Docs = await worker.StartWork("parsed-rx");
            if (Docs.Count > 0)
                output.Add((DateTime.Parse((string)Docs[0]["time"])).ToLocalTime().ToString("G"));

            return output;
        }
    }
}
