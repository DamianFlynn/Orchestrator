using System.Net;
using Opalis.QuickIntegrationKit;

namespace OchestratorF5
{
    [OpalisObject("Host To IP")]
    public class HostName2IP : IOpalisObject
    {
        public void Design(IOpalisDesigner designer)
        {
            designer.AddInput("Host Name");
            designer.AddOutput("IP Address");
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            IPHostEntry host = Dns.GetHostEntry(request.Inputs["Host Name"].AsString());
            response.PublishRange("IP Address", host.AddressList);
        }
    
    }
}