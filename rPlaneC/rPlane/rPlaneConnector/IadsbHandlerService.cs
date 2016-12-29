using System.ServiceModel;

namespace rPlaneConnector
{
    [ServiceContract]
    public interface IAdsbHandlerService
    {
        [OperationContract]
        string SendMessage(string package);
    }
}