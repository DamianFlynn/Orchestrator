using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;

namespace OchestratorF5
{

    [OpalisObject("Get Pools", ShowFilters = false)]
    public class GetPools : IOpalisObject
    {
        private ConnectionSettings connection;
        Interfaces F5Connection = new Interfaces();
            
        [OpalisConfiguration]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public void Design(IOpalisDesigner designer)
        {
            designer.AddOutput("Pools Count").AsNumber().WithDescription("Number of Pools Defined on the F5");
            designer.AddCorellatedData(typeof(F5Pool));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Open a connection to the F5
            bool bInitialized = F5Connection.initialize(connection.Host, connection.UserName, connection.Password);
            
            List<string> poolNames = new List<string>();

            String[] bigIPPoolNameData = F5Connection.LocalLBPool.get_list();

            foreach (string bigIPPoolName in bigIPPoolNameData)
            {
                poolNames.Add(bigIPPoolName);
            }

            
            IEnumerable bigIPPoolData = GetF5Pools(poolNames);
            int numPools = response.WithFiltering().PublishRange(bigIPPoolData);
            response.Publish("Pools Count", numPools);
        }

        private IEnumerable<F5Pool> GetF5Pools(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new F5Pool(dataItem);
            }

        }
    }
    
}
