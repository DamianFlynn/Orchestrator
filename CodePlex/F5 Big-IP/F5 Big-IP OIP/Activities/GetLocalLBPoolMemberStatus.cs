using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using F5_Big_IP_LIB;
using System.Collections;


namespace F5_Big_IP_OIP
{

    [ActivityAttribute("Get Local LB Pool Member Status", ShowFilters = false)]
    public class GetLocalLBPoolMemberStatus : IActivity
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
            // Set Up the Variables
            List<string> poolNames = new List<string>();

            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                // Get a list of Pools from the Device
                poolNames = F5BigIP.GetLBPools();
            }

            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Pool Name").WithListBrowser(poolNames);
            designer.AddInput("Pool Member");
            designer.AddCorellatedData(typeof(F5LTMPoolMembers));
        }


        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string poolName = request.Inputs["Pool Name"].AsString();
            string poolMember = request.Inputs["Pool Member"].AsString();

            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<F5LTMPoolMembers> memberInfo = F5BigIP.GetLBPoolMember(poolName, poolMember);

                int numPools = response.WithFiltering().PublishRange(memberInfo);
            }
        }


    }

}
