using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using System.Collections;

namespace OrchestratorDFS
{

    [OpalisObject("Get DFS Namespaces", ShowFilters = false)]
    public class GetDFSNamespaces : IOpalisObject
    {
        private ConnectionSettings connection;
        private DFSUtilities DFS = new DFSUtilities();
            
        [OpalisConfiguration]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public void Design(IOpalisDesigner designer)
        {
            // Set Up the Variables
            List<string> rootNames = new List<string>();

            // Get a list of Pools from the Device
            List<DFSRoot> dfsRoots = new List<DFSRoot>();
            dfsRoots = DFS.GetDFSRoots();
            

            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Root Name").WithListBrowser(rootNames);

            designer.AddOutput("F5 Pools Count").AsNumber().WithDescription("Number of Pools Defined on the F5");
            designer.AddCorellatedData(typeof(F5LBPool));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
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

        private IEnumerable<F5LBPool> GetF5LBPoolObjects(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new F5LBPool(dataItem);
            }

        }
    }
    
}
