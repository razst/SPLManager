using SPL_Manager.Library.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPL_Manager.Library.PacketModel.Converters
{
    static class StringToPacket
    {
        private static int j;
        private static List<string> bitarr;
        private static PacketProtocol json;
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


        public static void FromString(this PacketObject po, int groupDex = -1)
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
            var t = string.Join("", bits);
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
            temp = string.Join(string.Empty, temp.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            for (int i = 0; i < temp.Length; i++)
            {
                pacObj.DataCatalog.Add(currentParams.SubParams[i], temp[i].ToString());
            }
            j += numOfBytes;
        }


        #endregion
    }
}