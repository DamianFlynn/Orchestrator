using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;


namespace OchestratorF5
{

    [OpalisObject("Get Local LB Virtual Server Pool Status", ShowFilters = false)]
    public class GetLocalLBVirtualServerPoolStatus : IOpalisObject
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
            // Set Up the Variables
            List<string> virtualServerNames = new List<string>();

            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                // Get a list of Pools from the Device
                virtualServerNames = F5BigIP.GetLBVirtualServers();
            }

            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Virtual Server Name").WithListBrowser(virtualServerNames);
            designer.AddCorellatedData(typeof(F5LBVirtualServerMember));
        }


        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string virtualServerName = request.Inputs["Virtual Server Name"].AsString();

            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<F5LBVirtualServerMember> memberInfo = F5BigIP.GetLBVirtualServerMembers(virtualServerName);

                int numPools = response.WithFiltering().PublishRange(memberInfo);
            }
        }


    }

}
