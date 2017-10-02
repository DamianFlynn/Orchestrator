using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using F5_Big_IP_LIB;

namespace F5_Big_IP_OIP
{

    [ActivityAttribute("Get Local LB Pools", ShowFilters = false)]
    public class GetLocalLBPools : IActivity
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
            designer.AddOutput("F5 Pools Count").AsNumber().WithDescription("Number of Pools Defined on the F5");
            designer.AddCorellatedData(typeof(F5LTMPools));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<string> poolNames = F5BigIP.GetLBPools();

                
                // Now Convert the List of PoolNames in F5LBPool Objects
                IEnumerable poolObjects = GetF5LBPoolObjects(poolNames);

                int numPools = response.WithFiltering().PublishRange(poolObjects);
                response.Publish("F5 Pool Count", numPools);
            }
        }

        private IEnumerable<F5LTMPools> GetF5LBPoolObjects(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new F5LTMPools(dataItem);
            }

        }
    }
    
}
