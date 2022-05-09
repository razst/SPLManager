using SPL_Manager.Library.PacketModel.Converters;
using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;

namespace SPL_Manager.Library.PacketModel
{
    public class PacketObject
    {
        private static readonly List<string> Groups = ProgramProps.groups;

        public int Id { get; set; }

        public string SateliteGroup { get; set; } = Groups[0];

        public int Type { get; set; }

        public int Subtype { get; set; }

        public int Length { get; set; }

        public Dictionary<string, object> DataCatalog { get; set; }

        public PacketProtocol JsonObject { get; set; }

        public string RawPacket { get; set; }


        public PacketObject(PacketProtocol json, string packetString, int gpDex = -1)
        {
            DataCatalog = new Dictionary<string, object>();
            RawPacket = packetString;
            JsonObject = json;
            try
            {
                this.FromString(gpDex);
            }
            catch (Exception e)
            {
                DataCatalog.Add("reason", e.Message);
                Type = -1;
            }
        }
        public PacketObject(string packetString)
        {
            PacketObject packet = new PacketObject();
            foreach (var RxOption in ProgramProps.PacketJsonFiles)
            {
                int currentGroupDex = -1;
                if (RxOption.Key.EndsWith("Tau")) currentGroupDex = 9;
                packet = new PacketObject(RxOption.Value, packetString, currentGroupDex);
                if (packet.Type != -1) break;
            }


            SateliteGroup = packet.SateliteGroup ?? (packet.Type == -1 ? Groups[0] : Groups[9]);
            Id = packet.Id;
            Type = packet.Type;
            Subtype = packet.Subtype;
            Length = packet.Length;
            DataCatalog = packet.DataCatalog;
            JsonObject = packet.JsonObject;
            RawPacket = packetString;
        }
        public PacketObject()
        {
            DataCatalog = new Dictionary<string, object>();
            RawPacket = "";
            Type = -1;
        }
        public PacketObject(PacketObject packet)
        {
            Id = packet.Id;
            Type = packet.Type;
            Subtype = packet.Subtype;
            Length = packet.Length;
            DataCatalog = packet.DataCatalog;
            JsonObject = packet.JsonObject;
            RawPacket = packet.RawPacket;
        }

        public override string ToString()
        {
            if (Type == -1) return "Errored Packet";
            return $"ID: {Id}, Type: {GetTypeName()}, SubType: {GetSubTypeName()}";
        }



        public string GetDescriptionString()
        {
            var newline = Environment.NewLine;
            string output = "";
            if (Type == -1)
            {
                output += $"manager was not able to translate this packet:{newline}";
                output += $"{RawPacket}{newline}";
                return output;
            }

            output += $"Satlite: {SateliteGroup}{newline}";
            output += $"ID: {Id}{newline}";
            output += $"type: {GetTypeName()}{newline}";
            output += $"subtype: {GetSubTypeName()}{newline}";
            output += $"length: {Length}{newline}";

            if (DataCatalog.Count == 0) return output;

            output += $"*******************************{newline}";
            foreach (var field in DataCatalog)
            {
                output += $"{field.Key}: {field.Value}{newline}";
            }
            return output;
        }
        public string GetDescriptionStringInline()
        {
            if (Type == -1) return "manager was not able to translate this packet";

            string dataStr = "";
            foreach (var line in DataCatalog)
            {
                dataStr += $"{line.Key}: {line.Value}. ";
            }

            return dataStr;
        }

        public string ToHeaderString(DateTime time)
        {
            if (Type == -1)
                return $"[{time}]   ERROR";
            return $"[{time.ToString("dd/MM/yyyy HH:mm:ss")}]   {GetTypeName()} - {GetSubTypeName()}  ||| ID:{Id}";
        }



        public int GetTypeIndex() => JsonObject?.Types?.FindIndex(item => item.Id == Type) ?? -1;
        public int GetSubtypeIndex() => JsonObject?.Types?[GetTypeIndex()].SubTypes?.FindIndex(item => item.Id == Subtype) ?? -1;
        public string GetTypeName() => Type == -1 ? "Error" : JsonObject?.Types?[GetTypeIndex()]?.Name ?? "Error";
        public string GetSubTypeName() => Type == -1 ? "logs" : JsonObject?.Types?[GetTypeIndex()]?.SubTypes[GetSubtypeIndex()]?.Name ?? "nill";
        public int GetSatDex() => Type == -1 ? 0 : Groups.FindIndex(x => x == SateliteGroup);
    }
}