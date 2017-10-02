using System;
using System.Collections.Generic;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;


namespace OchestratorF5
{

    [OpalisObject("Set Pool Member State", ShowFilters = false)]
    public class SetPoolMemberState : IOpalisObject
    {
        private ConnectionSettings connection;
        Interfaces F5Connection = new Interfaces();

        private string poolName = string.Empty;
        private string memberName = string.Empty;
        private string memberState = string.Empty;

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
            designer.AddInput("Member Name");
            designer.AddInput("Member State").WithListBrowser(new string[] { "Enabled", "Disabled", "Forced Offline" });
            designer.AddCorellatedData(typeof(F5PoolMemberState));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Open a connection to the F5
            bool bInitialized = F5Connection.initialize(connection.Host, connection.UserName, connection.Password);

            poolName = request.Inputs["Pool Name"].AsString();
            memberName = request.Inputs["Member Name"].AsString();
            memberState = request.Inputs["Member State"].AsString();
            CommonIPPortDefinition Node = parseMember(memberName);

            LocalLBPoolMemberMemberMonitorState[][] monitor_states = new LocalLBPoolMemberMemberMonitorState[1][];
            monitor_states[0] = new LocalLBPoolMemberMemberMonitorState[1];
            monitor_states[0][0] = new LocalLBPoolMemberMemberMonitorState();
            monitor_states[0][0].member = new CommonIPPortDefinition();
            monitor_states[0][0].member = Node;
                    
            LocalLBPoolMemberMemberSessionState[][] session_states = new LocalLBPoolMemberMemberSessionState[1][];
            session_states[0] = new LocalLBPoolMemberMemberSessionState[1];
            session_states[0][0] = new LocalLBPoolMemberMemberSessionState();
            session_states[0][0].member = new CommonIPPortDefinition();
            session_states[0][0].member = Node;
                    

            switch (memberState)
            {
                case "Enabled":
                    monitor_states[0][0].monitor_state = CommonEnabledState.STATE_ENABLED;
                    session_states[0][0].session_state = CommonEnabledState.STATE_ENABLED;
                    break;

                case "Disabled":
                    monitor_states[0][0].monitor_state = CommonEnabledState.STATE_ENABLED;
                    session_states[0][0].session_state = CommonEnabledState.STATE_DISABLED;
                    break;

                case "Forced Offline":
                    monitor_states[0][0].monitor_state = CommonEnabledState.STATE_DISABLED;
                    session_states[0][0].session_state = CommonEnabledState.STATE_DISABLED;
                    break;

                default:
                    break;  
            }

            F5Connection.LocalLBPoolMember.set_monitor_state(new string[] { poolName }, monitor_states);
            F5Connection.LocalLBPoolMember.set_session_enabled_state(new string[] { poolName }, session_states);

            //
            List<string> poolMemberNames = new List<string>();

            LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberData = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });

            for (int i = 0; i < bigIPPoolMemberData[0].Length; i++)
            {
                if (bigIPPoolMemberData[0][i].member.address.Equals(Node.address) && bigIPPoolMemberData[0][i].member.port == Node.port )
                {
                    poolMemberNames.Add(poolName + ";" + bigIPPoolMemberData[0][i].member.address + ";" + bigIPPoolMemberData[0][i].member.port.ToString() + ";" + bigIPPoolMemberData[0][i].session_state.ToString());    
                }
            }


            IEnumerable bigIPPoolData = GetF5PoolMembers(poolMemberNames);
            int numPools = response.WithFiltering().PublishRange(bigIPPoolData);
            //response.Publish("Pool Members Count", numPools);
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
