using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using System.IO;
using AD_Utilities_LIB;
using PS_Factory;


namespace AD_Utilities_OIP
{

    [Activity("Get BitLocker Recovery Keys", ShowFilters = false)]
    public class GetBitLockerRecoveryKeys : IActivity
    {
        private ConnectionSettings connection;

        [ActivityConfiguration]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        
        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Computer Name");
            designer.AddOutput("Number of Keys").AsNumber().WithDescription("Number of stored Bitlocker keys for this computer.");
            designer.AddCorellatedData(typeof(BitLocker_KeySet));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            //response.WithFiltering().PublishRange(runCommand(request.Inputs["Computer Name"].AsString()));

            string strComputer = request.Inputs["Computer Name"].AsString();

            PSFactory myPowershell = new PSFactory();
            if (myPowershell.openRunspace(connection.UserName, connection.Password, connection.Domain, connection.Host))
            {

                IEnumerable<BitLocker_KeySet> computerKeys = Bitlocker.Get_ADBitLockerRecoveryKeys(myPowershell, "BAL-DF-E6500");
                int numKeys = response.WithFiltering().PublishRange(computerKeys);
                response.Publish("Number of Keys", numKeys);

                myPowershell.closeRunspace();
            }
        }


        
    }        
}
