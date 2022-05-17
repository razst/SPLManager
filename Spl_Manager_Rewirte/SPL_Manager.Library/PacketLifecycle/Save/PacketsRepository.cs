using Nest;
using SPL_Manager.Library.PacketModel;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPL_Manager.Library.PacketLifecycle.Save
{
    public interface IPacketsRepository
    {
        public Task<long> GetSplId(string group);

        public Task UploadPacket(PacketObject packet, string collection);
    }

    public class PacketsRepository : GenericSingleton<PacketsRepository>, IPacketsRepository
    {
        public PacketsRepository()
        {
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



        public async Task<long> GetSplId(string group)
        {
            if (!ProgramProps.GetIfOnline())
            {
                return 20;
            }

            string SatIndex = $"SAT{ProgramProps.groups.FindIndex(g => g == group)}";

            var searchTask =  ProgramProps.Database.SearchAsync<Dictionary<string, object>>(s => s
                                    .Query(q => q.Ids(c => c.Values(SatIndex)))
                                    .Index("local-data"));

            var searchResponse = await searchTask;
            long currenId = (long)searchResponse.Documents.First().First().Value;


            currenId += 1;


            try
            {
                var dict = new Dictionary<string, object> { { "CommandId", currenId } };
                var incrementIdTask = ProgramProps.Database.IndexAsync(new IndexRequest<Dictionary<string, object>>(dict, "local-data", SatIndex));
                await incrementIdTask;
            }
            finally
            {
                //TODO? if something went worng, idk why
            }
            return currenId;
        }
    }
}