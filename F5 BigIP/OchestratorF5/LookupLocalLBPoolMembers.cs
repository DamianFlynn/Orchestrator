using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;


namespace OchestratorF5
{

    [OpalisObject("Lookup Local LB Pool Members", ShowFilters = false)]
    public class LookupLocalLBPoolMembers : IOpalisObject
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
            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Lookup Query");
            designer.AddOutput("F5 Pool Members Count").AsNumber().WithDescription("Number of Members in the Pools");
            designer.AddCorellatedData(typeof(F5LBPoolMember));
        }


        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string lookupQuery = request.Inputs["Lookup Query"].AsString();

            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<F5LBPoolMember> memberInfo = F5BigIP.LookupLBPoolMembers(lookupQuery);

                int numPools = response.WithFiltering().PublishRange(memberInfo);
                response.Publish("F5 Pool Members Count", numPools);
            }
        }

    }

}
