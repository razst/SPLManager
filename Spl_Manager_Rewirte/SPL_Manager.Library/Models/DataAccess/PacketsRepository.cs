using Nest;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using SPL_Manager.Library.Models.SatPacketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.DataAccess
{
    public class PacketsRepository : IPacketsRepository
    {
        public PacketsRepository()
        {
            RedisEndpoint ep = new RedisEndpoint(
                "redis-10182.c17.us-east-1-4.ec2.cloud.redislabs.com",
                10182,
                "nOv4AXTkjIjweZ6DxPlX48qcettKpFDW");

            IdClient = new RedisClient(ep).As<int>();
        }
        public async Task<PacketObject> LoadLastBeacon(int groupIndex)
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

        public async Task<List<string>> LoadLastFrameDates(int groupIndex)
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


        private IRedisTypedClient<int> IdClient;
        public Task<int> GetSplId(string group)
        {
            if (!ProgramProps.IsOnline)
            {
                return Task.FromResult(20);
            }

            string SatIndex = $"SAT{ProgramProps.groups.FindIndex(g => g == group)}";
            int id = 0;

            id = IdClient.GetValue(SatIndex);
            IdClient.IncrementValue(SatIndex);


            return Task.FromResult(id);
        }
    }
}