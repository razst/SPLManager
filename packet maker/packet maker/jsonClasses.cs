﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace packet_maker
{

    class Msg
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
    }


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


        public IEnumerator<vars> GetEnumerator()
        {
            foreach (var type in values)
                yield return type;
        }
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



        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var type in typenum)
                yield return type;
        }

    }

    #endregion
}

