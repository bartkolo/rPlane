using rPlaneLibrary.Cassandra;
using rPlaneLibrary.Decoder;
using System;
using System.Linq;

namespace rPlaneConnector
{
    public class AdsbHandlerService : IAdsbHandlerService
    {
        private const int PortNo = 5555;
        private const string ServerIp = "127.0.0.1";
        private CPlane _plane = new CPlane();

        public string SendMessage(string package)
        {
            var message = new AdsbMessage(package);
            var dw = message.GetDownlinkFormat();
            var tc = message.GetTypeCode();

            if (dw.Equals(17) && (tc > 0 && tc < 15)) //todo chceck tc proper
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

                    _plane.InsertNewPlane(message.GetIcaoId(), plane, odd, oddEven, even, !oddEven, 0, 0, 0);
                }
                else
                {
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
                }

                return "Aircraft identification message received properly";
            }

            return "Not supported message received";
        }

        private void AddMessageToDb(string message)
        {
        }
    }
}