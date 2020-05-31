using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;




namespace packet_maker
{
    [FirestoreData]
    internal class Packet
    {
        [FirestoreProperty]

        public string packetString { get; set; }

    }





    public class packetObject
    {
        private string[] groups =
        {
            "Any",
            "T1OFK (Ofakim)",
            "T2YRC (Yerucham)",
            "T3TYB (Taybe)",
            "T4ATA (Kiryat Ata)",
            "T5SNG (Shaar HaNegev)",
            "T6NZR (Nazareth)",
            "T7ADM (Maale Adomim)",
            "T8GBS (Guvat Shmuel)"
        };

        public int id { get; set; }

        public string sateliteGroup { get; set; }

        public int type { get; set; }

        public int subtype { get; set; }

        public int length { get; set; }

        public List<string> data = new List<string>();

        public TypeList jsonObject { get; set; }

        private int typeDex;
        private int subtypeDex;

        private void ConvertFromString(string packetString)
        {
            string[] bitarr = packetString.Split(' ');


            id = Convert.ToInt32(bitarr[2] + bitarr[1] + bitarr[0], 16);

            type = Convert.ToInt32(bitarr[4], 16);
            subtype = Convert.ToInt32(bitarr[5], 16);
            length = Convert.ToInt32(bitarr[9] + bitarr[8] + bitarr[7] + bitarr[6], 16);
            sateliteGroup = groups[Convert.ToInt32(bitarr[3])];

            typeDex = jsonObject.typenum.FindIndex(item => item.id == type);
            subtypeDex = jsonObject.typenum[typeDex].subTypes.FindIndex(item => item.id == subtype);
            int j = 10;
            foreach (Params par in jsonObject.typenum[typeDex].subTypes[subtypeDex].parmas)
            {
                switch (par.type)
                {
                    case "int":
                        data.Add(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16).ToString());
                        j += 4;
                        break;

                    case "char":
                        if (par.values == null)
                        {
                            data.Add(Convert.ToInt32(bitarr[j], 16).ToString());
                        }
                        else
                        {
                            data.Add(par.values[par.values.FindIndex(item => item.id == Convert.ToInt32(bitarr[j], 16))].name);
                        }
                        j++;
                        break;

                    case "short":
                        data.Add(Convert.ToInt32(bitarr[j + 1] + bitarr[j], 16).ToString());
                        j += 2;
                        break;

                    case "datetime":
                        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dtDateTime.AddSeconds(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16)).ToLocalTime();
                        data.Add(dtDateTime.ToString());
                        j += 4;
                        break;

                    case "date":
                        System.DateTime dDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                        dtDateTime = dDateTime.AddSeconds(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16)).ToLocalTime();
                        data.Add(dtDateTime.ToString().Substring(0,10));
                        j += 4;
                        break;
                    case "bytes":
                        string allBit = "";
                        for(int i = 10; i < bitarr.Length; i++)
                        {
                            allBit += bitarr[i] + " ";
                        }
                        data.Add(allBit);
                        return;
                }
            }
        }

        static public packetObject create(TypeList json, string packetString)
        {
            packetObject createdPacket = new packetObject();
            createdPacket.jsonObject = json;
            createdPacket.ConvertFromString(packetString);
            return createdPacket;
        }

        public string castToString()
        {
            string HexType = type.ToString("X2");
            string HexSubType = subtype.ToString("X2");
            string HexId =String.Join(" ",Regex.Replace(id.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexLen = String.Join(" ", Regex.Replace(length.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexGru = Array.FindIndex(groups,a => a==sateliteGroup).ToString("X2");
            if (data.Count != 0)
            {
                
            }
            return HexId + " "+HexGru+" "+HexType+" "+HexSubType+" "+HexLen;
        }

        public string getTypeName()
        {
            return jsonObject.typenum[typeDex].name;
        }
        public string getSubTypeName()
        {
            return jsonObject.typenum[typeDex].subTypes[subtypeDex].name;
        }
        public int getTypeDex()
        {
            return typeDex;
        }
        public int getSubTypeDex()
        {
            return subtypeDex;
        }
    }
}
