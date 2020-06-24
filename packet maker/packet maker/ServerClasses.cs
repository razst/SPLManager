using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace packet_maker
{
    class Msg
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("Content")]
        public string Content { get; set; }
    }

    class cmd
    {
        public string Type { get; set; }
        public byte[] Content { get; set; }
    }

    public class Message
    {
        public string Type { get; }
        public object Content { get; }
        public Message(string type, object content)
        {
            Type = type;
            Content = content ?? new object();
        }
    }
}
