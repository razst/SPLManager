using Nest;
using SPL_Manager.Library.Models.General;
using SPL_Manager.Library.Models.SatPacketModels.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SPL_Manager.Library.Models.SatPacketModels
{
    public class PacketObject
    {
        private static readonly List<string> Groups = ProgramProps.groups;

        public int Id { get; set; }

        public string SateliteGroup { get; set; } = Groups[0];

        public int Type { get; set; }

        public int Subtype { get; set; }

        public int Length { get; set; }

        public Dictionary<string, object> DataCatalog { get; set; }

        public PacketTypeList JsonObject { get; set; }

        public string RawPacket { get; set; }


        public PacketObject(PacketTypeList json, string packetString, int gpDex = -1)
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
                DataCatalog.Add("reason", e.Message);
                Type = -1;
            }
        }
        public PacketObject(string packetString)
        {
            PacketObject packet = new PacketObject();
            foreach (var RxOption in ProgramProps.PacketJsonFiles)
            {
                int currentGroupDex = -1;
                if (RxOption.Key.EndsWith("Tau")) currentGroupDex = 9;
                packet = new PacketObject(RxOption.Value, packetString, currentGroupDex);
                if (packet.Type != -1) break;
            }


            SateliteGroup = packet.SateliteGroup ?? (packet.Type == -1 ? Groups[0] : Groups[9]);
            Id = packet.Id;
            Type = packet.Type;
            Subtype = packet.Subtype;
            Length = packet.Length;
            DataCatalog = packet.DataCatalog;
            JsonObject = packet.JsonObject;
            RawPacket = packetString;
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
            if (Type == -1) return "Errored Packet";
            return $"ID: {Id}, Type: {GetTypeName()}, SubType: {GetSubTypeName()}";
        }



        public string GetDescriptionString()
        {
            var newline = Environment.NewLine;
            string output = "";
            if (Type == -1)
            {
                output += $"manager was not able to translate this packet:{newline}";
                output += $"{RawPacket}{newline}";
                return output;
            }

            output += $"Satlite: {SateliteGroup}{newline}";
            output += $"ID: {Id}{newline}";
            output += $"type: {GetTypeName()}{newline}";
            output += $"subtype: {GetSubTypeName()}{newline}";
            output += $"length: {Length}{newline}";

            if (DataCatalog.Count == 0) return output;

            output += $"*******************************{newline}";
            foreach (var field in DataCatalog)
            {
                output += $"{field.Key}: {field.Value}{newline}";
            }
            return output;
        }
        public string GetDescriptionStringInline()
        {
            if (Type == -1) return "manager was not able to translate this packet";

            string dataStr = "";
            foreach (var line in DataCatalog)
            {
                dataStr += $"{line.Key}: {line.Value}. ";
            }

            return dataStr;
        }

        public string ToHeaderString(DateTime time)
        {
            if (Type == -1)
                return $"[{time}]   ERROR";
            return $"[{time.ToString("dd/MM/yyyy HH:mm:ss")}]   {GetTypeName()} - {GetSubTypeName()}  ||| ID:{Id}";
        }



        public int GetTypeIndex() => JsonObject?.Types?.FindIndex(item => item.Id == Type) ?? -1;
        public int GetSubtypeIndex() => JsonObject?.Types?[GetTypeIndex()].SubTypes?.FindIndex(item => item.Id == Subtype) ?? -1;
        public string GetTypeName() => Type == -1 ? "Error" : JsonObject?.Types?[GetTypeIndex()]?.Name ?? "Error";
        public string GetSubTypeName() => Type == -1 ? "logs" : JsonObject?.Types?[GetTypeIndex()]?.SubTypes[GetSubtypeIndex()]?.Name ?? "nill";
        public int GetSatDex() => Type == -1 ? 0 : Groups.FindIndex(x => x == SateliteGroup);
    }


    static class HexStringToPacket
    {
        private static int j;
        private static List<string> bitarr;
        private static PacketTypeList json;
        private static Params currentParams;
        private static PacketObject pacObj;


        //add new data types here (after you wrote an apropriate function):
        private static readonly Dictionary<string, Action> DataTypesActions = new Dictionary<string, Action>()
        {
            {"int", HandeleIntParam},
            {"short", HandeleShortParam},
            {"char", HandeleCharParam},
            {"float", HandeleFloatParam },
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
                po.SateliteGroup = ProgramProps.groups[Convert.ToInt32(bitarr[3])];
            }
            else
            {
                po.SateliteGroup = ProgramProps.groups[groupDex];
            }

            var typeDex = json.Types.FindIndex(item => item.Id == po.Type);
            var subtypeDex = json.Types[typeDex].SubTypes.FindIndex(item => item.Id == po.Subtype);

            j = (int)json.header.DataStartingByte;
            foreach (Params par in json.Types[typeDex].SubTypes[subtypeDex].Parmas)//runs on all params
            {
                currentParams = par;
                if (j < bitarr.Count)
                    DataTypesActions[currentParams.ParamType]();//activates the right func from the dataTypesActions dictionary
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
            List<string> r = bitarr.GetRange(j, NumOfBytes);
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
            if (currentParams.Calibration == "") return input;

            Calibration calibration = json.Calibrations.Find(x => x.ID == currentParams.Calibration);
            return calibration.Apply(input);
        }


        #endregion


        #region Data types handelers
        private static void HandeleIntParam()
        {
            pacObj.DataCatalog.Add(currentParams.Name, Calibrate(ConvertBytesToSplInt(4)));
            j += 4;
        }
        private static void HandeleShortParam()
        {
            pacObj.DataCatalog.Add(currentParams.Name, Calibrate(ConvertBytesToSplInt(2)));
            j += 2;
        }
        private static void HandeleCharParam()
        {
            if (currentParams.Values == null)
            {
                pacObj.DataCatalog.Add(currentParams.Name, Calibrate(ConvertBytesToSplInt(1)));
            }
            else
            {
                pacObj.DataCatalog.Add(currentParams.Name, currentParams.Values.Find(item => item.VarId == ConvertBytesToSplInt(1)).VarName);
            }
            j++;
        }

        private static void HandeleFloatParam()
        {
            try
            {
                HandeleIntParam(); //TODO: special method for float

            }
            catch
            {
                throw new Exception("Int Caster did not work for float value");
            }
        }

        private static void HandeleDateParam()
        {
            dynamic temp;
            if (bitarr[j + 3][0] == 'N')
            {
                temp = "now";
                if (bitarr[j + 3][1] != 'N')
                {
                    temp += $"{bitarr[j + 3][1]}{ConvertBytesToSplInt(3)}";
                }
            }
            else
            {
                string format = currentParams.ParamType == "date" ? "d" : "G";
                temp = DateTimeOffset.FromUnixTimeSeconds(ConvertBytesToSplInt(4)).ToLocalTime();
            }

            pacObj.DataCatalog.Add(currentParams.Name, temp);
            j += 4;
        }
        private static void HandeleBytesParam()
        {
            string temp = "";
            for (int i = j; i < bitarr.Count; i++)
            {
                temp += bitarr[i] + " ";
            }
            pacObj.DataCatalog.Add(currentParams.Name, temp);
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
            pacObj.DataCatalog.Add(currentParams.Name, temp);
            j = bitarr.Count;
        }
        private static void HandeleBitwiseParam()
        {
            string temp = "";
            int numOfBytes = currentParams.SubParams.Count / 8;
            for (int i = j; i < j + numOfBytes; i++)
            {
                temp += bitarr[i];
            }
            temp = String.Join(String.Empty, temp.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            for (int i = 0; i < temp.Length; i++)
            {
                pacObj.DataCatalog.Add(currentParams.SubParams[i], temp[i].ToString());
            }
            j += numOfBytes;
        }


        #endregion
    }

    static class PacketToHexString
    {
        private static PacketObject pacObj;
        private static PacketTypeList json;
        private static List<string> HexBytes;
        private static int Length;
        private static string PacketMode;

        private static Params currentParams;
        private static string currentField;

        //add new data types here (after you wrote an apropriate function):
        private static readonly Dictionary<string, Action> DataTypesActions = new Dictionary<string, Action>()
        {
            {"int", HandeleIntParam},
            {"short", HandeleShortParam},
            {"char", HandeleCharParam},
            {"float", HandeleFloatParam },
            {"date", HandeleDateParam},
            {"datetime", HandeleDateParam},
            {"ascii", HandeleAsciiParam},
            {"bytes", HandeleBytesParam},
            {"bitwise", HandeleBitwiseParam}
        };



        public static string CastToString(this PacketObject po, string packetMode)
        {
            HexBytes = new List<string>();
            pacObj = po;
            json = pacObj.JsonObject;
            PacketMode = packetMode;
            Length = 0;

            po.Subtype = json.Types[po.Type].SubTypes[po.Subtype].Id;
            po.Type = json.Types[po.Type].Id;

            var typeDex = json.Types.FindIndex(item => item.Id == po.Type);
            var subtypeDex = json.Types[typeDex].SubTypes.FindIndex(item => item.Id == po.Subtype);

            HexBytes.Add(ConvertToHexBytes(pacObj.Id, 3));
            HexBytes.Add(ConvertToHexBytes(pacObj.GetSatDex(), 1));
            HexBytes.Add(ConvertToHexBytes(json.Types[typeDex].Id, 1));
            HexBytes.Add(ConvertToHexBytes(json.Types[typeDex].SubTypes[subtypeDex].Id, 1));
            HexBytes.Add("");//length, will be calced in code. Hexbytes[4]

            foreach (Params par in json.Types[typeDex].SubTypes[subtypeDex].Parmas)
            {
                currentParams = par;
                currentField = pacObj.DataCatalog[par.Name].ToString();
                DataTypesActions[par.ParamType]();
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
            if (currentParams.Values == null)
            {
                HexBytes.Add(ConvertToHexBytes(Convert.ToInt32(currentField), 1));
            }
            else
            {
                HexBytes.Add(ConvertToHexBytes(currentParams.Values.Find(val => val.VarName == currentField).VarId, 1));
            }
            Length++;
        }
        private static void HandeleFloatParam()
        {
            try
            {
                HandeleIntParam(); //TODO: special method for float

            }
            catch
            {
                throw new Exception("Int Caster did not work for float value");
            }
        }
        private static void HandeleDateParam()
        {
            DateTime dt;
            int unix;

            if (currentField.Substring(0, 3) != "now")
            {
                dt = DateTime.Parse(currentField);
                unix = (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                HexBytes.Add(ConvertToHexBytes(unix, 4));
            }
            else if (currentField == "now")
            {
                if (PacketMode == "database")
                {
                    HexBytes.Add("00 00 00 NN");
                    return;
                }
                dt = DateTime.Now;
                unix = (int)dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                HexBytes.Add(ConvertToHexBytes(unix, 4));
            }
            else if (PacketMode == "database")
            {
                string p = ConvertToHexBytes(int.Parse(currentField[4..]), 3) + $" N{currentField[3]}";
                HexBytes.Add(p);
            }
            else
            {
                unix = currentField[3].ToString()
                    .Operator(
                    (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                    int.Parse(currentField[4..])
                    );
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
}