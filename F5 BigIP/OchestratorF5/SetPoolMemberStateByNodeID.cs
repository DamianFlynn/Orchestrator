﻿using System;
using System.Collections.Generic;
using Opalis.QuickIntegrationKit;
using iControl;
using System.Collections;
using System.Linq;

namespace OchestratorF5
{

    [OpalisObject("Set Pool Member State By Node ID", ShowFilters = false)]
    public class SetPoolMemberStateByNodeID : IOpalisObject
    {
        private ConnectionSettings connection;
        private iControlHelper F5BigIP;

        //Interfaces F5Connection = new Interfaces();

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
            // Set Up the Variables
            List<string> F5MemberName = new List<string>();

            // Try to Connect with the F5 Device
            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                // Get a list of Pools from the Device
                List<string> F5Pools = F5BigIP.GetPools();
                
                // Now Get a list of Members per Pool, and format this list for presentation in the UX
                foreach(F5PoolMemberState F5Member in F5BigIP.GetPoolMembers(F5Pools))
                {
                    F5MemberName.Add(F5Member.Address.Trim() + ":" + F5Member.Port.ToString());
                }

                // Great, next using Linq remove Duplicates from the list and sort it (Sexy!)
                F5MemberName.Distinct();
                F5MemberName.Sort();    
            }

            // We will now persent to the user the options we need them to provide to us

            designer.AddInput("Member Name").WithListBrowser(F5MemberName);
            designer.AddInput("Member State").WithListBrowser(new string[] { "Enabled", "Disabled", "Forced Offline" });
            designer.AddCorellatedData(typeof(F5PoolMemberState));
        }

        public void Execute(IOpalisRequest request, IOpalisResponse response)
        {
            // Parse the Inputs for this Method
            memberName = request.Inputs["Member Name"].AsString();
            memberState = request.Inputs["Member State"].AsString();

            if (F5BigIP.Connect(connection.Host, connection.UserName, connection.Password))
            {
                // We have been Povided with the State Setting requested, now lets set this up correctly
                // using the correct iControl flag representation.
                
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
                List<string> poolList = F5BigIP.GetPools();
                List<F5PoolMember> poolMembers = F5BigIP.GetPoolMembers(poolList);


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

                foreach (F5PoolMember poolMember in poolMembers)
                {

                }

                
                for (int i = 0; i < poolList.Count; i++)
                {
                    for (int j = 0; j < poolMembers.Count; i++)
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
                            poolMemberNames.Add(F5BigIP.GetPoolMemberState(poolName, nodeName));
                        }
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

 


        private IEnumerable<BigIPPoolMemberState> GetF5PoolMembers(List<string> poolData)
        {
            foreach (string dataItem in poolData)
            {
                yield return new BigIPPoolMemberState(dataItem);
            }

        }

    }

}
