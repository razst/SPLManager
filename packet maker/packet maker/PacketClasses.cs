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

        public PacketWithTime(packetObject packet, string timeString) : base(packet)
        {
            time = timeString;
        }
    }



    public class packetObject
    {
        private static readonly List<string> Groups = Program.groups;

        public int Id { get; set; }

        public string SateliteGroup { get; set; }

        public int Type { get; set; }

        public int Subtype { get; set; }

        public int Length { get; set; }

        public Dictionary<string, object> DataCatalog { get; set; }

        public TypeList JsonObject { get; set; }

        public string RawPacket { get; set; }


        public packetObject(TypeList json, string packetString, int gpDex = -1)
        {
            DataCatalog = new Dictionary<string, object>();
            RawPacket = packetString;
            JsonObject = json;
            try
            {
                this.ConvertFromString(gpDex);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Type = -1;
            }
        }
        public packetObject()
        {
            DataCatalog = new Dictionary<string, object>();
            RawPacket = "";
            Type = -1;
        }
        public packetObject(packetObject packet)
        {
            Id = packet.Id;
            Type = packet.Type;
            Subtype = packet.Subtype;
            Length = packet.Length;
            DataCatalog = packet.DataCatalog;
            JsonObject = packet.JsonObject;
            RawPacket = packet.RawPacket;
        }




        public override string ToString()
        {
            string output = "";
            if (Type == -1)
            {
                output += "manager was not able to translate this packet:" + Environment.NewLine;
                output += RawPacket + Environment.NewLine;
                return output;
            }

            output += "Satlite: " + SateliteGroup + Environment.NewLine;
            output += "ID: " + Id + Environment.NewLine;
            output += "type: " + GetTypeName() + Environment.NewLine;
            output += "subtype: " + GetSubTypeName() + Environment.NewLine;
            output += "length: " + Length + Environment.NewLine;
            if (DataCatalog.Count != 0)
            {
                output += "*******************************" + Environment.NewLine;
                foreach (var field in DataCatalog)
                {
                    output += field.Key + ": " + field.Value + Environment.NewLine;
                }
            }
            return output;

        }

        public string ToHeaderString(DateTime time)
        {
            if (Type == -1)
                return $"[{time}]   ERROR";
            return $"[{time}]   {GetTypeName()} - {GetSubTypeName()}  ||| ID:{Id}";
        }






        public string castToString()
        {
            string HexType = Type.ToString("X2");
            string HexSubType = Subtype.ToString("X2");
            string HexId = String.Join(" ", Regex.Replace(Id.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexLen = String.Join(" ", Regex.Replace(Length.ToString("X6"), ".{2}", "$0 ").Split(' ').Reverse());
            string HexGru = Groups.FindIndex(a => a == SateliteGroup).ToString("X2");
            if (DataCatalog.Count != 0)
            {

            }
            return HexId + " " + HexGru + " " + HexType + " " + HexSubType + " " + HexLen;
        }











        private int GetTypeDex() => JsonObject.typenum.FindIndex(item => item.id == Type);
        private int GetSubtypeDex() => JsonObject.typenum[GetTypeDex()].subTypes.FindIndex(item => item.id == Subtype);
        public string GetTypeName() => JsonObject.typenum[GetTypeDex()].name;
        public string GetSubTypeName() => JsonObject.typenum[GetTypeDex()].subTypes[GetSubtypeDex()].name;
        public int GetSatDex() => Groups.FindIndex(x => x == SateliteGroup);
    }


    static class PacketConvertion
    {
        private static int j;
        private static List<string> bitarr;
        private static TypeList json;
        private static Params currentParams;
        private static packetObject pacObj;


        //add new data types here (after you wrote an apropriate function):
        private static readonly Dictionary<string, Action> DataTypesActions = new Dictionary<string, Action>()
        {
            {"int", HandeleIntParam},
            {"short", HandeleShortParam},
            {"char", HandeleCharParam},
            {"date", HandeleDateParam},
            {"datetime", HandeleDateParam},
            {"ascii", HandeleAsciiParam},
            {"bytes", HandeleBytesParam},
            {"bitwise", HandeleBitwiseParam}
        };


        public static void ConvertFromString(this packetObject po, int groupDex = -1)
        {
            pacObj = po;
            json = po.JsonObject;
            bitarr = po.RawPacket.Split(' ').ToList();

            //header args (id, type, ect..)
            po.Id = ConvertBytesToSplInt(json.header.ID);
            po.Type = ConvertBytesToSplInt(json.header.Type);
            po.Subtype = ConvertBytesToSplInt(json.header.Subtype);
            po.Length = ConvertBytesToSplInt(json.header.Length);
            if (groupDex == -1)
            {
                po.SateliteGroup = Program.groups[Convert.ToInt32(bitarr[3])];
            }
            else
            {
                po.SateliteGroup = Program.groups[groupDex];
            }

            var typeDex = json.typenum.FindIndex(item => item.id == po.Type);
            var subtypeDex = json.typenum[typeDex].subTypes.FindIndex(item => item.id == po.Subtype);

            j = (int)json.header.DataStartingByte;
            foreach (Params par in json.typenum[typeDex].subTypes[subtypeDex].parmas)//runs on all params
            {
                currentParams = par;
                if (j < bitarr.Count)
                    DataTypesActions[currentParams.type]();//activates the right func from the dataTypesActions dictionary
            }
        }


        #region helper functions for the handelers
        private static int ConvertBytesToSplInt(dynamic setting)
        {
            List<string> r = bitarr.GetRange((int)setting.startByte, (int)setting.length);
            r.Reverse();
            var t = String.Join("", r);
            return Convert.ToInt32(t, 16);
        }
        private static int ConvertBytesToSplInt(int NumOfBytes)
        {
            List<string> r = bitarr.GetRange(j,NumOfBytes);
            r.Reverse();
            var t = String.Join("", r);
            return Convert.ToInt32(t, 16);
        }
        static float Calibrate(int input)
        {
            if (currentParams.calibration != null)
            {
                Calibration calibration = json.calibrations.Find(x => x.ID == currentParams.calibration);
                return calibration.muliplayer * input + calibration.constent;
            }
            return input;
        }


        #endregion


        #region Data types handelers
        private static void HandeleIntParam()
        {
            pacObj.DataCatalog.Add(currentParams.name, Calibrate(ConvertBytesToSplInt(4)));
            j += 4;
        }
        private static void HandeleShortParam()
        {
            pacObj.DataCatalog.Add(currentParams.name, Calibrate(ConvertBytesToSplInt(2)));
            j += 2;
        }
        private static void HandeleCharParam()
        {
            if (currentParams.values == null)
            {
                pacObj.DataCatalog.Add(currentParams.name, Calibrate(ConvertBytesToSplInt(1)));
            }
            else
            {
                pacObj.DataCatalog.Add(currentParams.name, currentParams.values.Find(item => item.id == ConvertBytesToSplInt(1)).name);
            }
            j++;
        }
        private static void HandeleDateParam()
        {
            string temp;
            if (bitarr[j][0] == 'N') 
            { 
                temp = "now"; 
                if(bitarr[j][1] != 'N')
                {
                    temp += $" {bitarr[j][1]} {Convert.ToInt32(bitarr[j + 1] + bitarr[j + 2] + bitarr[j + 3], 16)}";
                }
            }
            else
            {
                DateTimeOffset dtDateTime = DateTimeOffset.FromUnixTimeSeconds(ConvertBytesToSplInt(4)).ToLocalTime();
                temp = dtDateTime.ToUniversalTime().ToString();
            }

            pacObj.DataCatalog.Add(currentParams.name, temp);
            j += 4;
        }
        private static void HandeleBytesParam()
        {
            string temp = "";
            for (int i = j; i < bitarr.Count; i++)
            {
                temp += bitarr[i] + " ";
            }
            pacObj.DataCatalog.Add(currentParams.name, temp);
            j = bitarr.Count;
        }
        private static void HandeleAsciiParam()
        {
            List<char> letters = new List<char>();
            for (int i = j; i < bitarr.Count - 1; i++)
            {
                var intedChar = Convert.ToInt32(bitarr[i], 16);
                if (intedChar > 0 && intedChar < 128)
                {
                    letters.Add(Convert.ToChar(intedChar));
                }
            }
            string temp = string.Join("", letters);
            pacObj.DataCatalog.Add(currentParams.name, temp);
            j = bitarr.Count;
        }
        private static void HandeleBitwiseParam()
        {
            string temp = "";
            int numOfBytes = currentParams.subParams.Count / 8;
            for (int i = j; i < j + numOfBytes; i++)
            {
                temp += bitarr[i];
            }
            temp = String.Join(String.Empty, temp.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            for (int i = 0; i < temp.Length; i++)
            {
                pacObj.DataCatalog.Add(currentParams.subParams[i], temp[i].ToString());
            }
            j += numOfBytes;
        }


        #endregion
    }
}
