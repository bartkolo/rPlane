using rPlaneLibrary.Database;
using rPlaneLibrary.Decoder;

namespace rPlaneConnector
{
    public class AdsbHandlerService : IAdsbHandlerService
    {
        public string SendMessage(string package)
        {
            var message = new AdsbMessage(package);
            var dw = message.GetDownlinkFormat();
            var tc = message.GetTypeCode();

            if (dw.Equals(17) && (tc > 0 && tc < 5))
            {
                var ttt = new RPlaneDbHendler();
                ttt.AddAdsbPackage(package);
                return "Aircraft identification message received properly";
            }

            return "Not supported message received";
        }

        private void AddMessageToDb(string message)
        {
            
        }
    }
}