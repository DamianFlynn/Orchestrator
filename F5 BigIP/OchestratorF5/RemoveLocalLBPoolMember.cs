using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;


namespace OchestratorF5
{

    [OpalisObject("Remove Local LB Pool Member", ShowFilters = false)]
    public class RemoveLocalLBPoolMember : IOpalisObject
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
            List<string> poolNames = new List<string>();

            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                // Get a list of Pools from the Device
                poolNames = F5BigIP.GetLBPools();
            }

            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Pool Name").WithListBrowser(poolNames);
            designer.AddInput("Pool Member");
            designer.AddCorellatedData(typeof(F5LBPoolMember));
        }


        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string poolName = request.Inputs["Pool Name"].AsString();
            string poolMember = request.Inputs["Pool Member"].AsString();

            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                F5BigIP.RemoveLBPoolMember(poolName, poolMember);
                List<F5LBPoolMember> memberInfo = F5BigIP.GetLBPoolMember(poolName, poolMember);

                int numPools = response.WithFiltering().PublishRange(memberInfo);
            }
        }


    }

}
