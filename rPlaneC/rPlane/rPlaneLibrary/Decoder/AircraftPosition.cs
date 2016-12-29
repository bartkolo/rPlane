using System;

namespace rPlaneLibrary.Decoder
{
    public class AircraftPosition : MessageBitRepresentation
    {
        public double LatCprEven { get; set; }
        public double LonCprEven { get; set; }
        public double LatCprOdd { get; set; }
        public double LonCprOdd { get; set; }
        public int TimeE { get; set; }
        public int TimeO { get; set; }
        public int ZoneE { get; set; }
        public int ZoneO { get; set; }
        public double Latitude { get; set; }

        public AircraftPosition(string evenMessage, string oddMessage) : base(evenMessage, oddMessage)
        {
            LatCprEven = (double)DecodeMessageToInt(54, 70, BitesOfEvenMessage) / 131072;
            LonCprEven = (double)DecodeMessageToInt(71, 87, BitesOfEvenMessage) / 131072;
            LatCprOdd = (double)DecodeMessageToInt(54, 70, BitesOfOddMessage) / 131072;
            LonCprOdd = (double)DecodeMessageToInt(71, 87, BitesOfOddMessage) / 131072;
            TimeE = DecodeMessageToInt(52, 52, BitesOfEvenMessage);
            TimeO = DecodeMessageToInt(52, 52, BitesOfOddMessage);
        }

        #region Lat-Long

        public double GetLatitude()
        {
            var j = GetLatitudeIndex(LatCprEven, LatCprOdd);

            var latE = 6 * (((j - 60 * Math.Floor((double)j / 60)) + LatCprEven));

            const double airD = 360.0 / 59.0;
            var latO = airD * ((j - 59 * Math.Floor((double)j / 59)) + LatCprOdd);

            if (latE > 270)
                latE -= 360;
            if (latO > 270)
                latO -= 360;

            ZoneO = GetNl(latO);
            ZoneE = GetNl(latE);
            Latitude = TimeE >= TimeO ? latE : latO;
            return Latitude;
        }

        public double GetLongitude()
        {
            double longitude;
            if (ZoneO != ZoneE)
                return -1;
            if (TimeE >= TimeO)
            {
                var ni = Math.Max(ZoneE, 1);
                var dLon = 360.0 / ni;
                var m = Math.Floor(LonCprEven * (ZoneE - 1) - LonCprOdd * ZoneE + 0.5);
                longitude = dLon * ((m - ni * Math.Floor(m / ni)) + LonCprEven);
                if (longitude > 180) longitude = longitude - 360;
                return longitude;
            }
            else
            {
                var ni = Math.Max(ZoneO - 1, 1);
                double dLon = 360.0 / ni;
                var m = Math.Floor(LonCprEven * (ZoneO - 1) - LonCprOdd * ZoneO + 0.5);
                longitude = dLon * ((m - ni * Math.Floor(m / ni)) + LonCprOdd);
                if (longitude > 180) longitude = longitude - 360;
                return longitude;
            }
        }

        public int GetLatitudeIndex(double latCprE, double latCprO)
        {
            return (int)Math.Floor((59 * latCprE - 60 * latCprO + 0.5));
        }

        public int GetNl(double lat)
        {
            return (int)Math.Floor((2 * Math.PI) / Math.Acos(1 - ((1 - Math.Cos(Math.PI / 30)) / Math.Pow(Math.Cos(Math.PI * lat / 180), 2))));
        }

        #endregion Lat-Long
    }
}