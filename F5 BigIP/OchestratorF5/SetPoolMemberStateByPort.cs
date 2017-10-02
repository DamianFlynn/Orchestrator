using System;
using System.Collections.Generic;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;
using System.Linq;

namespace OchestratorF5
{

    [OpalisObject("Set Pool Member State By Port", ShowFilters = false)]
    public class SetPoolMemberStateByPort : IOpalisObject
    {
        private ConnectionSettings connection;
        Interfaces F5Connection = new Interfaces();

        private string memberPort = string.Empty;
        private string memberAddress = string.Empty;
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
            CommonIPPortDefinition[][] bigIPNodeNames = F5Connection.LocalLBPool.get_member(bigIPPoolNameData);
            
            List<string> poolMemberNames = new List<string>();
            foreach (CommonIPPortDefinition[] bigIPNode in bigIPNodeNames)
            {
                poolMemberNames.Add(bigIPNode[0].address.Trim());
            }
            
            // Using Linq, remove Duplicates (Sexy!)
            poolMemberNames.Distinct().ToList();
            poolMemberNames.Sort();

            designer.AddInput("Member Name").WithListBrowser(poolMemberNames);
            designer.AddInput("Member Port");
            designer.AddInput("Member State").WithListBrowser(new string[] { "Enabled", "Disabled", "Forced Offline" });
            designer.AddCorellatedData(typeof(F5PoolMemberState));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Open a connection to the F5
            bool bInitialized = F5Connection.initialize(connection.Host, connection.UserName, connection.Password);

            // Parse the Inputs for this Method
            memberAddress = request.Inputs["Member Name"].AsString();
            memberPort = request.Inputs["Member Port"].AsString();
            memberState = request.Inputs["Member State"].AsString();

            //Define and Set the Correct State flags to represent the requested Member State setting
            CommonEnabledState monitorState;
            CommonEnabledState sessionState;

            switch (memberState)
            {
                case "Enabled":
                    monitorState = CommonEnabledState.STATE_ENABLED;
                    sessionState = CommonEnabledState.STATE_ENABLED;
                    break;
                case "Disabled":
                    monitorState = CommonEnabledState.STATE_DISABLED;
                    sessionState = CommonEnabledState.STATE_ENABLED;
                    break;
                case "Forced Offline":
                    monitorState = CommonEnabledState.STATE_DISABLED;
                    sessionState = CommonEnabledState.STATE_DISABLED;
                    break;
                default:
                    // We should never get here, but if we do we will let the node go active
                    monitorState = CommonEnabledState.STATE_ENABLED;
                    sessionState = CommonEnabledState.STATE_ENABLED;
                    break;
            }


            // We do not know the name of the pool we are going to modify, so we will instead need to gather a list of all known pools
            // from this list, we will then request a list of all the know Node/Member names which are asscoiated with the pools
            String[] bigIPPoolNames = F5Connection.LocalLBPool.get_list();
            CommonIPPortDefinition[][] bigIPNodeNames = F5Connection.LocalLBPool.get_member(bigIPPoolNames);


            // Now, we need to create to instances; one for both the monitor state and the session state. 
            // Each of these will be single member 2 dimensional array.
            LocalLBPoolMemberMemberMonitorState[][] monitor_states = new LocalLBPoolMemberMemberMonitorState[1][];
            monitor_states[0] = new LocalLBPoolMemberMemberMonitorState[1];
            monitor_states[0][0] = new LocalLBPoolMemberMemberMonitorState();
            monitor_states[0][0].member = new CommonIPPortDefinition();

            LocalLBPoolMemberMemberSessionState[][] session_states = new LocalLBPoolMemberMemberSessionState[1][];
            session_states[0] = new LocalLBPoolMemberMemberSessionState[1];
            session_states[0][0] = new LocalLBPoolMemberMemberSessionState();
            session_states[0][0].member = new CommonIPPortDefinition();
             
            // Now, lets create some storage variables to contain the name of the Pool we determine this node is related to
            // and also the node itself, we will then loop trough all the pools, and its members looking for a match to the node/member
            // which the user has presented to us to update with the new state setting.

            string poolName = string.Empty;
            CommonIPPortDefinition nodeName = new CommonIPPortDefinition();
            List<string> poolMemberNames = new List<string>();

            for (int i = 0; i < bigIPPoolNames.Length; i++)
            {
                for (int j = 0; j < bigIPNodeNames[i].Length; i++)
                {
                    nodeName = bigIPNodeNames[i][j];

                    if (nodeName.address.Equals(memberAddress) && nodeName.port == long.Parse(memberPort))
                    {
                        // Cool - We just located the member/node which were were asked to update
                        monitor_states[0][0].member = nodeName;
                        monitor_states[0][0].monitor_state = monitorState;
                        
                        session_states[0][0].member = nodeName;
                        session_states[0][0].session_state = sessionState;

                        poolName = bigIPPoolNames[i];

                        F5Connection.LocalLBPoolMember.set_monitor_state(new string[] { poolName }, monitor_states);
                        F5Connection.LocalLBPoolMember.set_session_enabled_state(new string[] { poolName }, session_states);

                        // Now that we have a match, lets take the information we have, and ask the F5 to report the new status 
                        // of this member back to us, so that we can update the pipeline, with currentl information
                        poolMemberNames.Add( GetF5PoolMemberState(poolName, nodeName) );
                    }
                }
            }


            // Now - Sanity Check time; lets go back and ask the F5 what is the current status of this member and its pool
            // So we can report back to real staus to the pipeline

            //List<string> poolMemberNames = new List<string>();

            //LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
            //LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = F5Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

            //for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
            //{
            //    if (bigIPPoolMemberState[0][i].member.address.Equals(nodeName.address) && bigIPPoolMemberState[0][i].member.port == nodeName.port)
            //    {
            //        string state = string.Empty;
            //        if (bigIPPoolMemberMonitor[0][i].monitor_status == LocalLBMonitorStatus.MONITOR_STATUS_FORCED_DOWN)
            //            // Monitor State = Disabled
            //            state = "Offline Forced";
            //        else
            //            if (bigIPPoolMemberState[0][i].session_state == CommonEnabledState.STATE_ENABLED)
            //                // Session State = Enabled
            //                // Monitor State = Enabled
            //                state = "Enabled";
            //            else
            //                // Session State = Disabled
            //                // Monitor State = Enabled
            //                state = "Disabled";

            //        poolMemberNames.Add(poolName + ";" + bigIPPoolMemberState[0][i].member.address + ";" + bigIPPoolMemberState[0][i].member.port.ToString() + ";" + state);
            //    }
            //}


            IEnumerable bigIPPoolData = GetF5PoolMembers(poolMemberNames);
            int numPools = response.WithFiltering().PublishRange(bigIPPoolData);
        }

        private string GetF5PoolMemberState(string poolName, CommonIPPortDefinition nodeName)
        {
            // Now - Sanity Check time; lets go back and ask the F5 what is the current status of this member and its pool
            // So we can report back to real staus to the pipeline

            //List<string> poolMemberNames = new List<string>();
            string memberState = string.Empty;

            LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
            LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = F5Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

            for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
            {
                if (bigIPPoolMemberState[0][i].member.address.Equals(nodeName.address) && bigIPPoolMemberState[0][i].member.port == nodeName.port)
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

                    memberState = poolName + ";" + bigIPPoolMemberState[0][i].member.address + ";" + bigIPPoolMemberState[0][i].member.port.ToString() + ";" + state;
                }
            }

            return memberState;
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
