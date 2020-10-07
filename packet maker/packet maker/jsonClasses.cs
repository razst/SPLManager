using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace packet_maker
{

    #region settings

    public class BasicSettings
    {
        [JsonProperty("endNodePath")]
        public string endNodePath { get; set; }


        [JsonProperty("dataBaseEnabled")]
        public bool dataBaseEnabled { get; set; }

        public int pacCurId { get; set; }

        [JsonProperty("enableImage")]
        public bool enableImage { get; set; }

        [JsonProperty("tcpPortNumber")]
        public int tcpPortNumber { get; set; }

        [JsonProperty("defultSatGroup")]
        public int defultSatGroup { get; set; }

        [JsonProperty("enableExcel")]
        public bool enableExcel { get; set; }

    }

    #endregion


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
        public int calibration { get; set; }
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

        [JsonProperty("calibrations")]
        public List<Calibration> calibrations { get; set; }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var type in typenum)
                yield return type;
        }

    }

    public class Calibration
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("a")]
        public float muliplayer { get; set; }

        [JsonProperty("b")]
        public float constent { get; set; }
    }

    #endregion
}


