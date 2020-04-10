using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace packet_maker
{
    public class Params
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string type { get; set; }
        [JsonProperty("values")]
        public List<string> values { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var type in values)
                yield return type;
        }

    }
    public class SubType
    {
        public int id { get; set; }
        public string name { get; set; }
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
        public int id { get; set; }
        public string name { get; set; }
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


}


