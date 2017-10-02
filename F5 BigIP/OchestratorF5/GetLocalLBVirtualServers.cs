using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;


namespace OchestratorF5
{

    [OpalisObject("Get Local LB Virtual Servers", ShowFilters = false)]
    public class GetLocalLBVirtualServers : IOpalisObject
    {
        private ConnectionSettings connection;
        private F5Interface F5BigIP = new F5Interface();
            
        [OpalisConfiguration]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public void Design(IOpalisDesigner designer)
        {
            designer.AddOutput("F5 Virtual Servers Count").AsNumber().WithDescription("Number of Virtual Servers Defined on the F5");
            designer.AddCorellatedData(typeof(F5LBVirtualServer));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<string> virtualServerNames = F5BigIP.GetLBVirtualServers();
                
                // Now Convert the List of PoolNames in F5LBPool Objects
                IEnumerable virtualServerObjects = GetF5LBVirtualServerObjects(virtualServerNames);

                int numVirtualServers = response.WithFiltering().PublishRange(virtualServerObjects);
                response.Publish("F5 Virtual Servers Count", numVirtualServers);
            }
        }

        private IEnumerable<F5LBVirtualServer> GetF5LBVirtualServerObjects(List<string> virtualServerData)
        {
            foreach (string dataItem in virtualServerData)
            {
                yield return new F5LBVirtualServer(dataItem);
            }

        }
    }
    
}
