using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SPL_Manager.Library.PacketModel.Converters
{
    static class PacketToString
    {
        private static PacketObject pacObj;
        private static PacketProtocol json;
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



        public static string ToRawString(this PacketObject po, string packetMode)
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

            return string.Join(" ", HexBytes);
        }


        private static string ConvertToHexBytes(long value, int numberOfBytes)
        {
            string format = "X" + numberOfBytes * 2;
            string Tid = value.ToString(format);
            string[] TidArr = Regex.Replace(Tid, ".{2}", "$0 ").Trim().Split(' ');
            Array.Reverse(TidArr);
            return string.Join(" ", TidArr).Trim();
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
                throw new Exception("Int data type Handler did not work for float value");
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