using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using F5_Big_IP_LIB;
using System.Collections;


namespace F5_Big_IP_OIP
{

    [ActivityAttribute("Get Local LB Virtual Servers", ShowFilters = false)]
    public class GetLocalLBVirtualServers : IActivity
    {
        private ConnectionSettings connection;
        private F5Interface F5BigIP = new F5Interface();
            
        [ActivityConfigurationAttribute]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddOutput("F5 Virtual Servers Count").AsNumber().WithDescription("Number of Virtual Servers Defined on the F5");
            designer.AddCorellatedData(typeof(F5LTMVirtualServers));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
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

        private IEnumerable<F5LTMVirtualServers> GetF5LBVirtualServerObjects(List<string> virtualServerData)
        {
            foreach (string dataItem in virtualServerData)
            {
                yield return new F5LTMVirtualServers(dataItem);
            }

        }
    }
    
}
