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
    }
    public class SubType
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public IList<Params> parmas { get; set; }
    }
    public class Type
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public IList<SubType> subTypes { get; set; }
    }
    public class TypeList
    {
        [JsonProperty("types")]
        public IList<Type> typenum { get; set; }

        public IEnumerator<Type> GetEnumerator()
        {
            foreach (var type in typenum)
                yield return type;
        }

    }


}


