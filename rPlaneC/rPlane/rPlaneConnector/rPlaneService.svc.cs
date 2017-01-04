using rPlaneLibrary.Decoder;

namespace rPlaneConnector
{
    public class AdsbHandlerService : IAdsbHandlerService
    {
        private const int PortNo = 5555;
        private const string ServerIp = "127.0.0.1";

        public string SendMessage(string package)
        {
            var message = new AdsbMessage(package);
            var dw = message.GetDownlinkFormat();
            var tc = message.GetTypeCode();

            if (dw.Equals(17) && (tc > 0 && tc < 12))
            {
                //var ttt = new RPlaneDbHendler();
                //ttt.AddAdsbPackage(package);
                //var client = new TcpClient(ServerIp, PortNo);
                //var nwStream = client.GetStream();
               // var adsbMessage = System.Text.Encoding.ASCII.GetBytes(package);
               // nwStream.Write(adsbMessage, 0, package.Length);
                return "Aircraft identification message received properly";
            }

            return "Not supported message received";
        }

        private void AddMessageToDb(string message)
        {
        }
    }
}