using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Nest;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Collections.ObjectModel;
using System.Web.Helpers;

namespace packet_maker
{
    public class FilePacket
    {
        public string PacketPrefix { get; set; }

        public List<PacketWithTime> Packets { get; set; }

        public List<string> FirstColumn { get; set; }
    }

    public class PacketWithTime : PacketObject
    {
        public string Time { get; set; }

        public PacketWithTime(PacketObject packet, string timeString) : base(packet)
        {
            Time = timeString;
        }
    }



    public class PacketObject
    {
        private static readonly List<Group> Groups = Program.groups;

        public int Id { get; set; }

        public string SateliteGroup { get; set; }

        public int Type { get; set; }

        public int Subtype { get; set; }

        public int Length { get; set; }

        public Dictionary<string, object> DataCatalog { get; set; }

        public TypeList JsonObject { get; set; }

        public string RawPacket { get; set; }


        public PacketObject(TypeList json, string packetString, int gpDex = -1)
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
        public PacketObject()
        {
            DataCatalog = new Dictionary<string, object>();
            RawPacket = "";
            Type = -1;
        }
        public PacketObject(PacketObject packet)
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
            if (Type == 1) return "Errored Packet";
            return $"ID: {Id}, Type: {GetTypeName()}, SubType: {GetSubTypeName()}";
        }

        public async Task Upload(string COLLECTION_NAME)
        {
            if (GetSatDex() == 9) return;
            Dictionary<string, object> data = this.CastToDict();

            var dbCollection = Program.mongoDB.GetCollection<BsonDocument>(COLLECTION_NAME);
            BsonDocument bson = data.ToBsonDocument();
            dbCollection.InsertOne(bson);

        }

        public string GetDescriptionString()
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



        private int GetTypeDex() => JsonObject.typenum.FindIndex(item => item.id == Type);
        private int GetSubtypeDex() => JsonObject.typenum[GetTypeDex()].subTypes.FindIndex(item => item.id == Subtype);
        public string GetTypeName() => JsonObject.typenum[GetTypeDex()].name;
        public string GetSubTypeName() => JsonObject.typenum[GetTypeDex()].subTypes[GetSubtypeDex()].name;
        public int GetSatDex() => Groups.Find(x => x.Str == SateliteGroup).Id;
    }


    static class HexStringToPacket
    {
        private static int j;
        private static List<string> bitarr;
        private static TypeList json;
        private static Params currentParams;
        private static PacketObject pacObj;


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


        public static void ConvertFromString(this PacketObject po, int groupDex = -1)
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
                po.SateliteGroup = Program.groups.Find(x => x.Id % 10 == Convert.ToInt32(bitarr[3], 16) % 10).Str;
            }
            else
            {
                po.SateliteGroup = Program.groups[groupDex].Str;
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
            return ConvertBytesToSplInt(r);
        }
        private static int ConvertBytesToSplInt(int NumOfBytes)
        {
            List<string> r = bitarr.GetRange(j,NumOfBytes);
            return ConvertBytesToSplInt(r);
        }
        private static int ConvertBytesToSplInt(List<string> bits)
        {
            if (json.ReverseBytes)
                bits.Reverse();
            var t = String.Join("", bits);
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
            pacObj.DataCatalog.Add(currentParams.name, (int)Calibrate(ConvertBytesToSplInt(4)));
            j += 4;
        }
        private static void HandeleShortParam()
        {
            pacObj.DataCatalog.Add(currentParams.name, (int)Calibrate(ConvertBytesToSplInt(2)));
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



    static class PacketToHexString
    {
        private static PacketObject pacObj;
        private static TypeList json;
        private static List<string> HexBytes;
        private static int Length;
        private static int PacketMode;

        private static Params currentParams;
        private static string currentField;

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

        public static string CastToString(this PacketObject po, int packetMode)
        {
            HexBytes = new List<string>();
            pacObj = po;
            json = pacObj.JsonObject;
            PacketMode = packetMode;
            Length = 0;

            po.Subtype = json.typenum[po.Type].subTypes[po.Subtype].id;
            po.Type = json.typenum[po.Type].id;

            var typeDex = json.typenum.FindIndex(item => item.id == po.Type);
            var subtypeDex = json.typenum[typeDex].subTypes.FindIndex(item => item.id == po.Subtype);

            HexBytes.Add(ConvertToHexBytes(pacObj.Id, 3));
            HexBytes.Add(ConvertToHexBytes(pacObj.GetSatDex(), 1));
            HexBytes.Add(ConvertToHexBytes(json.typenum[typeDex].id, 1));
            HexBytes.Add(ConvertToHexBytes(json.typenum[typeDex].subTypes[subtypeDex].id, 1));
            HexBytes.Add("");//length, will be calced in code. Hexbytes[4]

            foreach (Params par in json.typenum[typeDex].subTypes[subtypeDex].parmas)
            {
                currentParams = par;
                currentField = pacObj.DataCatalog[par.name].ToString();
                DataTypesActions[par.type]();
            }
            HexBytes[4] = ConvertToHexBytes(Length, 2);

            return String.Join(" ", HexBytes);
        }


        private static string ConvertToHexBytes(int value, int numberOfBytes)
        {
            string format = "X" + (numberOfBytes * 2);
            string Tid = value.ToString(format);
            string[] TidArr = Regex.Replace(Tid, ".{2}", "$0 ").Trim().Split(' ');
            Array.Reverse(TidArr);
            return String.Join(" ", TidArr).Trim();
        }


        #region Data types handelers
        private static void HandeleIntParam()
        {
            HexBytes.Add(ConvertToHexBytes(Convert.ToInt32(currentField), 4));
            Length += 4;
        }
        private static void HandeleShortParam()
        {
            HexBytes.Add(ConvertToHexBytes(Convert.ToInt32(currentField), 2));
            Length += 2;
        }
        private static void HandeleCharParam()
        {
            if(currentParams.values == null)
            {
                HexBytes.Add(ConvertToHexBytes(Convert.ToInt32(currentField), 1));
            }
            else
            {
                HexBytes.Add(ConvertToHexBytes(currentParams.values.Find(val => val.name == currentField).id, 1));
            }
            Length++;
        }
        private static void HandeleDateParam()
        {
            DateTime dt;
            int unix;

            if (currentField.Substring(0, 3) != "now")
            {
                dt = DateTime.Parse(currentField);
                unix = (int)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                HexBytes.Add(ConvertToHexBytes(unix, 4));
            }
            else if (currentField == "now")
            {
                if (PacketMode == 2)
                {
                    HexBytes.Add("00 00 00 NN");
                    return;
                }
                dt = DateTime.Now;
                unix = (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                HexBytes.Add(ConvertToHexBytes(unix, 4));
            }
            else if (PacketMode == 2)
            {
                string p = ConvertToHexBytes(int.Parse(currentField.Substring(4)), 3).Replace(" ", String.Empty) + "N" + currentField[3];
                HexBytes.Add(p);
            }
            else
            {
                unix = currentField[3].ToString().Operator((int)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds, int.Parse(currentField.Substring(4)));
                HexBytes.Add(ConvertToHexBytes(unix, 4));
            }
            Length += 4;

        }
        private static void HandeleBytesParam() 
        {
            HexBytes.Add(currentField.ToUpper());
            Length += (currentField.Trim().Length + 1) / 3;
        }
        private static void HandeleAsciiParam()
        {

        }
        private static void HandeleBitwiseParam()
        {

        }


        #endregion
    }



    static class PacketToDatabase
    {
        public static Dictionary<string, object> CastToDict(this PacketObject PacObj)
        {
            Dictionary<string, object> DBPacket = new Dictionary<string, object>
            {
                {"packetString",PacObj.RawPacket },
                {"time",DateTime.UtcNow }
            };
            if (PacObj.Type == -1)
            {
                DBPacket.Add("type", "Error");
                return DBPacket;
            }

            DBPacket.Add("splID", PacObj.Id);
            //DBPacket.Add("_id", PacObj.Id); we wnat mongo to choose the ID
            DBPacket.Add("type", PacObj.GetTypeName());
            DBPacket.Add("subtype", PacObj.GetSubTypeName());
            DBPacket.Add("lenght", PacObj.Length);
            DBPacket.Add("satId", PacObj.GetSatDex());
            DBPacket.Add("satName", PacObj.SateliteGroup);

            foreach (var item in PacObj.DataCatalog) DBPacket.Add(item.Key, item.Value);

            return DBPacket;
        }
    }
}
