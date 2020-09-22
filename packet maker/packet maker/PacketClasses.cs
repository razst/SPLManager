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



    public class FilePacket
    {
        public string packetPrefix { get; set; }

        public List<PacketWithTime> packets { get; set; }

        public List<string> firstColumn { get; set; }
    }

    public class PacketWithTime : packetObject
    {
        public string time { get; set; }

        public PacketWithTime(packetObject packet,string timeString)
        {
            time = timeString;
            try
            {
                jsonObject = packet.jsonObject;
                ConvertFromString(packet.rawPacket);
            }
            catch
            {
                jsonObject = packet.jsonObject;
                rawPacket = packet.rawPacket;
                type = -1;
            }
        }
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

        public Dictionary<string, object> dataCatalog = new Dictionary<string, object>();


        public TypeList jsonObject { get; set; }

        public string rawPacket { get; set; }

        private int typeDex;
        private int subtypeDex;

        public void ConvertFromString(string packetString)
        {
            rawPacket = packetString;
            string[] bitarr = packetString.Split(' ');


            id = Convert.ToInt32(bitarr[3] + bitarr[2] + bitarr[1] + bitarr[0], 16);

            type = Convert.ToInt32(bitarr[4], 16);
            subtype = Convert.ToInt32(bitarr[5], 16);
            length = Convert.ToInt32(bitarr[7] + bitarr[6], 16);
            sateliteGroup = groups[Convert.ToInt32(bitarr[3])];

            typeDex = jsonObject.typenum.FindIndex(item => item.id == type);
            subtypeDex = jsonObject.typenum[typeDex].subTypes.FindIndex(item => item.id == subtype);
            int j = 8;
            foreach (Params par in jsonObject.typenum[typeDex].subTypes[subtypeDex].parmas)
            {
                string temp = null;
                switch (par.type)
                {
                    case "int":

                        dataCatalog.Add(par.name, Calibrate(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16), par.calibration));
                        j += 4;
                        break;

                    case "char":
                        if (par.values == null)
                        {
                            dataCatalog.Add(par.name, Calibrate(Convert.ToInt32(bitarr[j], 16), par.calibration));
                        }
                        else
                        {
                            dataCatalog.Add(par.name, par.values[par.values.FindIndex(item => item.id == Convert.ToInt32(bitarr[j], 16))].name);
                        }
                        j++;
                        break;

                    case "short":
                        dataCatalog.Add(par.name, Calibrate(Convert.ToInt32(bitarr[j + 1] + bitarr[j], 16), par.calibration));
                        j += 2;
                        break;

                    case "datetime":
                    case "date":
                        if (bitarr[j] != "NN" && bitarr[j] != "N+" && bitarr[j] != "N-")
                        {
                            DateTimeOffset dtDateTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt32(bitarr[j + 3] + bitarr[j + 2] + bitarr[j + 1] + bitarr[j], 16)).ToLocalTime();
                            if (par.type == "datetime")
                            {
                                dataCatalog.Add(par.name, dtDateTime.ToUniversalTime());
                            }
                            else
                            {
                                dataCatalog.Add(par.name, dtDateTime.ToUniversalTime());
                            }
                        }
                        else
                        {
                            if (bitarr[j][1] != 'N')
                            {
                                dataCatalog.Add(par.name, $"now{bitarr[j][1]}{Convert.ToInt32(bitarr[j + 1] + bitarr[j + 2] + bitarr[j + 3], 16)}");
                            }
                            else
                            {
                                dataCatalog.Add(par.name, "now");
                            }
                        }

                        j += 4;
                        break;

                    case "bytes":
                        temp = "";
                        for (int i = j; i < bitarr.Length; i++)
                        {
                            temp += bitarr[i] + " ";
                        }
                        dataCatalog.Add(par.name, temp);
                        return;

                    case "ascii":
                        temp = "";
                        List<char> letters = new List<char>();
                        for (int i = j; i < bitarr.Length - 1; i++)
                        {
                            if (Convert.ToInt32(bitarr[i], 16) > 0 && Convert.ToInt32(bitarr[i], 16) < 128)
                            {
                                letters.Add(Convert.ToChar(Convert.ToInt32(bitarr[i], 16)));
                            }
                        }
                        temp = string.Join("", letters).ToString();
                        dataCatalog.Add(par.name, temp);
                        return;

                    case "bitwise":
                        temp = "";
                        int numOfBytes = par.subParams.Count / 8;
                        for (int i = j; i < j + numOfBytes; i++)
                        {
                            temp += bitarr[i];
                        }
                        temp = String.Join(String.Empty, temp.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                        for (int i = 0; i < temp.Length; i++)
                        {
                            dataCatalog.Add(par.subParams[i], temp[i].ToString());
                        }
                        j += numOfBytes;
                        break;
                }
            }
        }

        private float Calibrate(int input, int paramId)
        {
            if (paramId != 0)
            {
                Calibration calibration = jsonObject.calibrations.Find(x => x.ID == paramId);
                return calibration.muliplayer * input + calibration.constent;
            }
            return input;
        }

        public packetObject(TypeList json, string packetString)
        {
            try
            {
                jsonObject = json;
                ConvertFromString(packetString);
            }
            catch
            {
                jsonObject = json;
                rawPacket = packetString;
                type = -1;
            }
        }
        public packetObject(Dictionary<string, object> DBpacket)
        {
            try
            {
                id = int.Parse(DBpacket["splID"].ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public packetObject()
        {

        }

        public string castToString()
        {
            string HexType = type.ToString("X2");
            string HexSubType = subtype.ToString("X2");
            string HexId = String.Join(" ", Regex.Replace(id.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexLen = String.Join(" ", Regex.Replace(length.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexGru = Array.FindIndex(groups, a => a == sateliteGroup).ToString("X2");
            if (dataCatalog.Count != 0)
            {

            }
            return HexId + " " + HexGru + " " + HexType + " " + HexSubType + " " + HexLen;
        }

        public string getTypeName() => jsonObject.typenum[typeDex].name;
        public string getSubTypeName() => jsonObject.typenum[typeDex].subTypes[subtypeDex].name;
        public int getTypeDex() => typeDex;
        public int getSubTypeDex() => subtypeDex;
        public int getSatDex() => Array.FindIndex(groups, a => a == sateliteGroup);
    }
}
