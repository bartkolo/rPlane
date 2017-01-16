using System;
using System.Collections.Generic;

namespace rPlaneLibrary.Decoder
{
    public class AdsbMessage : MessageBitRepresentation
    {
        public AdsbMessage(string message) : base(message)
        {
        }
        
        public string GetIcaoId()
        {
            return FirsReceivedMessage.Substring(2, 6);
        }

        public int GetDownlinkFormat()
        {
            return DecodeMessageToInt(0, 4);
        }

        public int GetTypeCode()
        {
            return DecodeMessageToInt(32, 36);
        }

        public bool GetOddEven()
        {
            return DecodeMessageToList(53, 53)[0];
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

                if (((a - 1) % 6) != 0) continue;
                pow = 5;
                result += Index[value].ToString();
                value = 0;
            }
            return result;
        }

        public int GetAltitude()
        {
            var altirudeArray = DecodeMessageToList(40, 46);
            altirudeArray.AddRange(DecodeMessageToList(48, 51));
            var crudeAltitude = GetIntFromBitArray(altirudeArray);

            if (DecodeMessageToInt(47, 47) == 1)
                return crudeAltitude * 25 - 1000;
            return crudeAltitude * 100 - 1000; //TODO check that calculation is proper for Q-bit equals 0
        }

    }
}