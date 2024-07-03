using System;
using System.Collections.Generic;

namespace packet_maker
{
    public class PLInfo
    {
        public string name { get; set; }

        public List<string> commands { get; set; }

        public int sleepBetweenCommands { get; set; }
    }
}
