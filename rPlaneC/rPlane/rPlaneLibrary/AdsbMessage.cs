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
        public bool Odd { get; set; }

        public AdsbMessage(string message) : base(message)
        {
            DownlinkFormat = DecodeMessageToInt(0, 4);
            MessageSubtype = DecodeMessageToInt(5, 7);
            TypeCode = DecodeMessageToInt(32, 36);
            Odd = DecodeMessageToList(53, 53)[0];
            AircraftId = GetAircraftId();
            ICAO = GetIcaoId();
        }

        private string GetIcaoId()
        {
            return ReceivedMessage.Substring(3, 6);
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
                result += this.Index[value].ToString();
                value = 0;
            }
            return result;
        }

        #region Lat-Long

        public float GetLatitudeLongitude(string even, string odd)
        {
            var ev = new MessageBitRepresentation(even);
            var od = new MessageBitRepresentation(odd);

            var latCprEven = (float)ev.DecodeMessageToInt(54, 70) / 131072;
            var lonCprEven = (float)ev.DecodeMessageToInt(71, 87) / 131072;
            var latCprOdd = (float)od.DecodeMessageToInt(54, 70) / 131072;
            var lonCprOdd = (float)od.DecodeMessageToInt(71, 87) / 131072;

            var j = GetLatitudeIndex(latCprEven, latCprOdd);

            var LateE = (360 / 60) * ((j % 60 + latCprEven));
            var LatO = (360 / 59) * ((j % 59) + latCprOdd);

            return 0;
        }

        public int GetLatitudeIndex(float latCprE, float latCprO)
        {
            return (int)Math.Floor((59 * latCprE - 60 * latCprO + 0.5));
        }

        public int GetNl(int lat)
        {
            return (int)Math.Floor((2 * Math.PI) / Math.Acos(1 - ((1 - Math.Cos(Math.PI / 30)) / Math.Pow(Math.Cos(Math.PI * lat / 180), 2))));
        }

        #endregion Lat-Long
    }
}