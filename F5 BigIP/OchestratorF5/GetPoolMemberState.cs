using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;

namespace OchestratorF5
{

    [OpalisObject("Get Pool Member State", ShowFilters = false)]
    public class GetPoolMemberState : IOpalisObject
    {
        private ConnectionSettings connection;
        private iControlHelper F5BigIP;

        private string poolName = string.Empty;
        private string memberName = string.Empty;

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
                poolNames = F5BigIP.GetPools();
            }
            
            // Define the Input and Output Paramaters for the Interface
            designer.AddInput("Pool Name").WithListBrowser(poolNames);
            designer.AddInput("Member Name");
            designer.AddCorellatedData(typeof(F5PoolMemberState));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Prepaire the Paramaters provided for use
            poolName = request.Inputs["Pool Name"].AsString();
            memberName = request.Inputs["Member Name"].AsString();
            CommonIPPortDefinition nodeName = parseMember(memberName);

            
            // Open a connection to the F5
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                List<F5PoolMemberState> memberState = F5BigIP.GetPoolMemberState(poolName, nodeName);

                int count = response.WithFiltering().PublishRange(memberState);

                //List<string> poolMemberNames = new List<string>();
                //IEnumerable bigIPPoolData = GetF5PoolMembers(poolMemberNames);

                //int numPools = response.WithFiltering().PublishRange(bigIPPoolData);
                //response.Publish("Pool Members Count", numPools);
            }
        }

        private IEnumerable<F5PoolMemberState> GetF5PoolMembers(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new F5PoolMemberState(dataItem);
            }

        }

        private CommonIPPortDefinition parseMember(String member)
        {
            CommonIPPortDefinition ipPort = null;
            String[] sSplit = member.Split(new char[] { ':' });
            if (2 == sSplit.Length)
            {
                ipPort = new CommonIPPortDefinition();
                ipPort.address = sSplit[0];
                ipPort.port = Convert.ToInt32(sSplit[1]);
            }
            return ipPort;
        }
    }

}
