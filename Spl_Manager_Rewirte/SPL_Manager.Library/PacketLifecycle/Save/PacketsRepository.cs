using Nest;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPL_Manager.Library.PacketLifecycle.Save
{
    public interface IPacketsRepository
    {
        public Task<int> GetSplId(string group);

        public Task UploadPacket(PacketObject packet, string collection);
    }

    public class PacketsRepository : GenericSingleton<PacketsRepository>, IPacketsRepository
    {
        public PacketsRepository()
        {
            RedisEndpoint ep = new RedisEndpoint(
                "redis-10182.c17.us-east-1-4.ec2.cloud.redislabs.com",
                10182,
                "nOv4AXTkjIjweZ6DxPlX48qcettKpFDW");

            IdClient = new RedisClient(ep).As<int>();
        }

        public async Task UploadPacket(PacketObject packet, string collection)
        {
            if (packet.GetSatDex() == 9) return;

            Dictionary<string, object> DBPacket = new Dictionary<string, object>
            {
                {"packetString",packet.RawPacket },
                {"time",DateTime.UtcNow }
            };
            if (packet.Type == -1)
            {
                DBPacket.Add("type", "Error");
            }
            else
            {
                DBPacket.Add("splID", packet.Id);
                DBPacket.Add("type", packet.GetTypeName());
                DBPacket.Add("subtype", packet.GetSubTypeName());
                DBPacket.Add("lenght", packet.Length);
                DBPacket.Add("satId", packet.GetSatDex());
                DBPacket.Add("satName", packet.SateliteGroup);
                foreach (var item in packet.DataCatalog) DBPacket.Add(item.Key, item.Value);
            }

            var result = await ProgramProps.Database.IndexAsync(
                    new IndexRequest<Dictionary<string, object>>
                        (DBPacket, collection));

            Console.WriteLine(result);
        }



        private IRedisTypedClient<int> IdClient;
        public Task<int> GetSplId(string group)
        {
            if (!ProgramProps.GetIfOnline())
            {
                return Task.FromResult(20);
            }

            string SatIndex = $"SAT{ProgramProps.groups.FindIndex(g => g == group)}";
            int id = 20;
            try
            {
                id = IdClient.GetValue(SatIndex);
                IdClient.IncrementValue(SatIndex);
            }
            finally
            {
            }
            return Task.FromResult(id);
        }
    }
}