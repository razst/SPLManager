using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace SPL_Manager.Library.PacketModel
{
    public class vars
    {
        [JsonProperty("id")]
        public int VarId { get; set; }
        [JsonProperty("name")]
        public string VarName { get; set; } = "";
    }
    public class Params
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("desc")]
        public string Desc { get; set; } = "";

        [JsonProperty("type")]
        public string ParamType { get; set; } = "";

        [JsonProperty("values")]
        public List<vars> Values { get; set; } = new List<vars>();

        [JsonProperty("subParams")]
        public List<string> SubParams { get; set; } = new List<string>();

        [JsonProperty("calibration")]
        public string Calibration { get; set; } = "";
    }
    public class PacketSubtype
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("desc")]
        public string Desc { get; set; } = "";

        [JsonProperty("inParams")]
        public List<Params> Parmas { get; set; } = new List<Params>();

        public override string ToString()
        {
            return Name;
        }
        public IEnumerator<Params> GetEnumerator()
        {
            foreach (var type in Parmas)
                yield return type;
        }
    }
    public class PacketType
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("desc")]
        public string Desc { get; set; } = "";
        [JsonProperty("subtypes")]
        public List<PacketSubtype> SubTypes { get; set; } = new List<PacketSubtype>();

        public override string ToString()
        {
            return Name;
        }

        public IEnumerator<PacketSubtype> GetEnumerator()
        {
            foreach (var type in SubTypes)
                yield return type;
        }
    }
    public class PacketProtocol
    {
        [JsonProperty("types")]
        public List<PacketType> Types { get; set; }

        [JsonProperty("spl_header")]

        public dynamic header;

        [JsonProperty("ReverseBytes")]
        public bool ReverseBytes { get; set; }

        [JsonProperty("calibrations")]
        public List<Calibration> Calibrations { get; set; } = new List<Calibration>();

        public IEnumerator<PacketType> GetEnumerator()
        {
            if (Types != null)
                foreach (var type in Types)
                    yield return type;
        }
        public PacketProtocol(string jsonPath)
        {
            PacketProtocol t = JsonConvert.DeserializeObject<PacketProtocol>(File.ReadAllText(jsonPath));

            Types = t?.Types ?? new List<PacketType>();
            header = t?.header;
            Calibrations = t?.Calibrations ?? new List<Calibration>();
            ReverseBytes = t?.ReverseBytes ?? false;
        }
        public PacketProtocol()
        {
            Types = new List<PacketType>();
        }
    }

    public class Calibration
    {
        [JsonProperty("ID")]
        public string ID { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("a")]
        public float Muliplayer { get; set; }

        [JsonProperty("b")]
        public float Constent { get; set; }

        public float Apply(float input)
        {
            return input * Muliplayer + Constent;
        }

        public override string ToString()
        {
            return $"{Name} ({ID})";
        }
    }
}