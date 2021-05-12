using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace packet_maker
{

    #region rx/tx
    public class vars
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
    }
    public class Params
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("desc")]
        public string desc { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }

        [JsonProperty("values")]
        public List<vars> values { get; set; }

        [JsonProperty("subParams")]
        public List<string> subParams { get; set; }

        [JsonProperty("calibration")]
        public string calibration { get; set; }
    }
    public class SubType
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("desc")]
        public string desc { get; set; }

        [JsonProperty("inParams")]
        public List<Params> parmas { get; set; }

        public override string ToString()
        {
            return name;
        }
        public IEnumerator<Params> GetEnumerator()
        {
            foreach (var type in parmas)
                yield return type;
        }
    }
    public class Type
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("desc")]
        public string desc { get; set; }
        [JsonProperty("subtypes")]
        public List<SubType> subTypes { get; set; }

        public override string ToString()
        {
            return name;
        }

        public IEnumerator<SubType> GetEnumerator()
        {
            foreach (var type in subTypes)
                yield return type;
        }
    }
    public class TypeList
    {
        [JsonProperty("types")]
        public List<Type> typenum { get; set; }

        [JsonProperty("spl_header")]

        public dynamic header; 

        [JsonProperty("calibrations")]
        public List<Calibration> calibrations { get; set; }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var type in typenum)
                yield return type;
        }
        public TypeList(string jsonPath)
        {
            JsonSerializer Serializer = new JsonSerializer();
            StreamReader sr = new StreamReader(jsonPath);
            var t = (TypeList)Serializer.Deserialize(sr, typeof(TypeList));
            typenum = t.typenum ?? null;
            header = t.header ?? null;
            calibrations = t.calibrations ?? null;
        }
        public TypeList() { }
    }

    public class Calibration
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("a")]
        public float muliplayer { get; set; }

        [JsonProperty("b")]
        public float constent { get; set; }

        public override string ToString()
        {
            return $"{name} ({ID})";
        }
    }

    #endregion
}


