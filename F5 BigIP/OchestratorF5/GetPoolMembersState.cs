using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;

namespace OchestratorF5
{

    [OpalisObject("Get Pool Members State", ShowFilters = false)]
    public class GetPoolMembersState : IOpalisObject
    {
        private ConnectionSettings connection;
        Interfaces F5Connection = new Interfaces();

        private string poolName = string.Empty;

        [OpalisConfiguration]
        public ConnectionSettings Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public void Design(IOpalisDesigner designer)
        {
            bool bInitialized = F5Connection.initialize(connection.Host, connection.UserName, connection.Password);
            String[] bigIPPoolNameData = F5Connection.LocalLBPool.get_list();

            designer.AddInput("Pool Name").WithListBrowser(bigIPPoolNameData);
            designer.AddOutput("Pool Members Count").AsNumber().WithDescription("Number of Members in the Pools");
            designer.AddCorellatedData(typeof(F5PoolMemberState));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Open a connection to the F5
            bool bInitialized = F5Connection.initialize(connection.Host, connection.UserName, connection.Password);


            poolName = request.Inputs["Pool Name"].AsString();

            List<string> poolMemberNames = new List<string>();

            LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
            LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = F5Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

            for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
            {
                string state = string.Empty;
                if (bigIPPoolMemberMonitor[0][i].monitor_status == LocalLBMonitorStatus.MONITOR_STATUS_FORCED_DOWN)
                    // Monitor State = Disabled
                    state = "Offline Forced";
                else
                    if (bigIPPoolMemberState[0][i].session_state == CommonEnabledState.STATE_ENABLED)
                        // Session State = Enabled
                        // Monitor State = Enabled
                        state = "Enabled";
                    else
                        // Session State = Disabled
                        // Monitor State = Enabled
                        state = "Disabled";
                
                //poolMemberNames.Add(poolName + ";" + bigIPPoolMemberState[0][i].member.address + ";" + bigIPPoolMemberState[0][i].member.port.ToString() + ";" + bigIPPoolMemberState[0][i].session_state.ToString());
                poolMemberNames.Add(poolName + ";" + bigIPPoolMemberState[0][i].member.address + ";" + bigIPPoolMemberState[0][i].member.port.ToString() + ";" + state);
            }


            IEnumerable bigIPPoolData = GetF5PoolMembers(poolMemberNames);
            int numPools = response.WithFiltering().PublishRange(bigIPPoolData);
            response.Publish("Pool Members Count", numPools);
        }

        private IEnumerable<F5PoolMemberState> GetF5PoolMembers(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new F5PoolMemberState(dataItem);
            }

        }
    }

}
