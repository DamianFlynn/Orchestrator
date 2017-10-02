using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using F5_Big_IP_LIB;
using System.Collections;


namespace F5_Big_IP_OIP
{

    [ActivityAttribute("Add Local LB Pool", ShowFilters = false)]
    public class AddLocalLBPool : IActivity
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
            designer.AddInput("Pool Name");
            designer.AddInput("Pool Members List");
            designer.AddInput("Pool Members Port");
            designer.AddInput("Pool Monitor Type").WithListBrowser("HTTP", "HTTPS");
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string poolName = request.Inputs["Pool Name"].AsString();
            string poolMembersList = request.Inputs["Pool Members List"].AsString();
            string poolMembersPort = request.Inputs["Pool Members Port"].AsString();
            string poolMonitorType = request.Inputs["Pool Monitor Type"].AsString();

            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                F5BigIP.CreateLBPool(poolName, poolMembersList, long.Parse(poolMembersPort), poolMonitorType);
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
