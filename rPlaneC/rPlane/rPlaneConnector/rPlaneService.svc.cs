using rPlaneLibrary.Cassandra;
using rPlaneLibrary.Decoder;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace rPlaneConnector
{
    public class AdsbHandlerService : IAdsbHandlerService
    {
        private static CPlane _plane;

        public string SendMessage(string package)
        {
            package = Regex.Replace(package, @"\t|\n|\r", "");
            var message = new AdsbMessage(package);
            var dw = message.GetDownlinkFormat();
            var tc = message.GetTypeCode();
            _plane = new CPlane();
            //if (!dw.Equals(17) || (tc <= 0 || tc >= 15)) return "Not supported message received";
            if (dw.Equals(17) && (tc == 11))
            {
                lock (_plane)
                {
                    return Lat_Lon_Calculation(message, package);
                }
            }
            if (dw.Equals(17))
            {
                lock (_plane)
                {
                    return AltitudeCalculation(message, package);
                }
            }

            return "Not supported message received";
        }

        private string AltitudeCalculation(AdsbMessage message, string package)
        {
            var plane = message.GetIcaoId();
            var result = _plane.CheckRowExist(plane).Any();
            if (!result)
            {
                var positionPlane = new AdsbMessage(package);
                var altitude = positionPlane.GetAltitude();
                _plane.InsertNewPlane(message.GetIcaoId(), plane, string.Empty, false, string.Empty, false, altitude, 0, 0);
            }
            else
            {
                var positionPlane = new AdsbMessage(package);
                var altitude = positionPlane.GetAltitude();
                _plane.UpdateRow(CPlane.PlaneColumn.Altitude, altitude, plane);
            }
            return "Aircraft identification message received properly";
        }

        private string Lat_Lon_Calculation(AdsbMessage message, string package)
        {
            var plane = message.GetIcaoId();
            var result = _plane.CheckRowExist(plane).Any();
            if (!result)
            {
                var odd = string.Empty;
                var even = string.Empty;
                var oddEven = message.GetOddEven();
                if (oddEven) odd = package;
                else even = package;
                var positionPlane = new AdsbMessage(package);
                var altitude = positionPlane.GetAltitude();
                _plane.InsertNewPlane(message.GetIcaoId(), plane, odd, oddEven, even, !oddEven, altitude, 0, 0);
            }
            else
            {
                var positionPlane = new AdsbMessage(package);
                var altitude = positionPlane.GetAltitude();
                _plane.UpdateRow(CPlane.PlaneColumn.Altitude, altitude, plane);

                var oddEven = message.GetOddEven();
                if (oddEven)
                {
                    _plane.UpdateRow(CPlane.PlaneColumn.OddStatus, true, plane);
                    _plane.UpdateRow(CPlane.PlaneColumn.OddMessage, package, plane);
                }
                else
                {
                    _plane.UpdateRow(CPlane.PlaneColumn.EvenStatus, true, plane);
                    _plane.UpdateRow(CPlane.PlaneColumn.EvenMessage, package, plane);
                }

                var test2 = _plane.CassandraDb.Session.Execute($"SELECT * FROM plane where icao='{plane}';");
                var te = test2.GetRows().ElementAt(0);
                if (Convert.ToBoolean(te[4]) && Convert.ToBoolean(te[8]))
                {
                    var planeLocationFirst = new AircraftPosition(te[3].ToString(), te[7].ToString());
                    var latitude = planeLocationFirst.GetLatitude();
                    var longitude = planeLocationFirst.GetLongitude();

                    _plane.UpdateRow(CPlane.PlaneColumn.Latitude, latitude, plane);
                    _plane.UpdateRow(CPlane.PlaneColumn.Longitude, longitude, plane);
                    _plane.UpdateRow(CPlane.PlaneColumn.EvenStatus, false, plane);
                    _plane.UpdateRow(CPlane.PlaneColumn.OddStatus, false, plane);
                }

                _plane.CassandraDb.CasandraCluster.Shutdown();
            }

            return "Aircraft identification message received properly";
        }
    }
}