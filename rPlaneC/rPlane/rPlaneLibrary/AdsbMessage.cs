using System;
using System.Collections.Generic;

namespace rPlaneLibrary
{
    public class AdsbMessage : MessageBitRepresentation
    {
        public int DownlinkFormat { get; set; }
        public int MessageSubtype { get; set; }
        public string ICAO { get; set; }
        public string AircraftId { get; set; }
        public int Data { get; set; }
        public int ParityCheck { get; set; }
        public int TypeCode { get; set; }

        public AdsbMessage(string message) : base(message)
        {
            DownlinkFormat = DecodeMessageToInt(0, 4);
            MessageSubtype = DecodeMessageToInt(5, 7);
            TypeCode = DecodeMessageToInt(32, 37);
            AircraftId = GetAircraftId();
            //ICAO = GetIcaoId();
            //Data = DecodeMessage(32, 87);
            //ParityCheck = DecodeMessage(88, 111);
        }

        public string GetAircraftId()
        {
            var icaoList = new List<bool>(DecodeMessageToList(32, 87));

            string result = string.Empty;
            var value = 0;
            int pow = 5;
            for (var a = 8; a < icaoList.Count; a++)
            {
                if (icaoList[a])
                    value += Convert.ToInt16(Math.Pow(2, pow));
                pow--;

                if (((a - 1)%6) != 0) continue;
                pow = 5;
                result += this.Index[value].ToString();
                value = 0;
            }
            return result;
        }
    }
}