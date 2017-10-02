using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iControl;

namespace OchestratorF5
{
    // We will create a support class, to hold the detail of a pool member
    public class F5PoolMember
    {
        public CommonIPPortDefinition member { get; set; }
        public string pool { get; set; }
    }


    // Our Main Worker Class to help make using the iControl Interface from F5 just a little easier, 
    // and help me cut down on all the typing i need to do! :)

    public class  iControlHelper
    {
        // Before we do a thing, we need to create an Interface to the F5 iControl Library
        private Interfaces F5Connection = new Interfaces();
        
        
        // Now, We will wrap the Connection process up
        public bool Connect(string Host, string UserName, string Password)
        {
            // Invoke the Initialization and connection to rhe F5 Environment
            return (F5Connection.initialize(Host, UserName, Password));
        }

        // After we have a connection, we will need to pass references back to the worker classes.
        public Interfaces Connection
        {
            get 
            { 
                return F5Connection; 
            }
        }

        // Function to query the the F5 for the list of Pools which are currently Defined on the device
        public List<string> GetPools()
        {
            string[] poolList;
            poolList = Connection.LocalLBPool.get_list();

            List<string> poolNames = new List<string>();
            poolNames = poolList.ToList();
            poolNames.Distinct().ToList();
            poolNames.Sort();

            return poolNames;
        }

        // Function to return a List of all the Members in a singe pool
        public List<string> GetPoolMembers(string poolName)
        {
            List<string> F5MemberNames = new List<string>();

            foreach(CommonIPPortDefinition[] F5Member in Connection.LocalLBPool.get_member(new string[] { poolName }))
            {
                F5MemberNames.Add(F5Member[0].address.Trim() + ":" + F5Member[0].port.ToString());
            }

            return F5MemberNames;
        }

        
        // Function to return a list of Members per Pool from a list of pools
        public List<F5PoolMemberState> GetPoolMembers(List<string> poolNames)
        {
            List<F5PoolMember> F5MemberNames = new List<F5PoolMemberState>();
            foreach (string F5Pool in poolNames)
            {
                foreach (CommonIPPortDefinition[] F5Member in Connection.LocalLBPool.get_member(new string[] { F5Pool }))
                {
                    F5PoolMember memberInfo = new F5PoolMember();

                    memberInfo.member = F5Member[0];
                    memberInfo.pool = F5Pool;
                }
            }
            
            return F5MemberNames;
        }


        // Function to query the Pool and its member of the status of the object and return this back to the user.
        public LocalLBObjectStatus GetPoolMemberStatus(string poolName, CommonIPPortDefinition memberName)
        {
            
            foreach (LocalLBPoolMemberMemberObjectStatus[] F5Member in Connection.LocalLBPoolMember.get_object_status(new string[] { poolName }))
            {
                if(F5Member[0].member.address.Equals(memberName.address) && F5Member[0].member.port == memberName.port)
                {
                    return F5Member[0].object_status; 
                }   
            }

            return new LocalLBObjectStatus();
        }


        public void EnableMember(string poolName, string memberName)
        {
        }


        public long GetCurrentMemberConnections(string poolName, string memberName)
        {
            CommonIPPortDefinition[][] activeMember = new CommonIPPortDefinition[1][];
            activeMember[0][0] = parseMember(memberName);

            long activeConnections = 1;
            while (activeConnections > 0)
            {
                LocalLBPoolMemberMemberStatistics[] memberStatistics = Connection.LocalLBPoolMember.get_statistics(new string[] { poolName }, activeMember);
                foreach (CommonStatistic memberStat in memberStatistics[0].statistics[0].statistics)
                {
                    if (memberStat.type == CommonStatisticType.STATISTIC_SERVER_SIDE_CURRENT_CONNECTIONS)
                    {
                        activeConnections = memberStat.value.low;
                    }
                }
            }
            return activeConnections;
        }

        public void DisableMember(string poolName, string memberName)
        {

          //param($pool_name, $member);
          //$vals = $member.Split( (, ':'));
          //$member_addr = $vals[0];
          //$member_port = $vals[1];
 
          //Write-Host "Disabling Session Enabled State...";  $MemberSessionState = New-Object -TypeName iControl.LocalLBPoolMemberMemberSessionState;
          //$MemberSessionState.member = New-Object -TypeName iControl.CommonIPPortDefinition;
          //$MemberSessionState.member.address = $member_addr;
          //$MemberSessionState.member.port = $member_port;
          //$MemberSessionState.session_state = "STATE_DISABLED";
          //$MemberSessionStateAofA = New-Object -TypeName "iControl.LocalLBPoolMemberMemberSessionState[][]" 1,1
          //$MemberSessionStateAofA[0][0] = $MemberSessionState;
          //(Get-F5.iControl).LocalLBPoolMember.set_session_enabled_state( (, $pool_name), $MemberSessionStateAofA);
          //Write-Host "Waiting for current connections to drop to zero..."
          //$MemberDef = New-Object -TypeName iControl.CommonIPPortDefinition;
          //$MemberDef.address = $member_addr;
          //$MemberDef.port = $member_port;
 
          //$MemberDefAofA = New-Object -TypeName "iControl.CommonIPPortDefinition[][]" 1,1
          //$MemberDefAofA[0][0] = $MemberDef;
 
          //$cur_connections = 1;
            
            
            while (GetCurrentMemberConnections(poolName , memberName) > 0)
            {
                // Wait Loop
            }

          //while ( $cur_connections -gt 0 )
          //{
          //  $MemberStatisticsA = (Get-F5.iControl).LocalLBPoolMember.get_statistics( (, $pool_name), $MemberDefAofA);
          //  $MemberStatisticEntry = $MemberStatisticsA[0].statistics[0];
          //  $Statistics = $MemberStatisticEntry.statistics;
          //  foreach ($Statistic in $Statistics)
          //  {
          //    $type = $Statistic.type;
          //    $value = $Statistic.value;
     
          //    if ( $type -eq "STATISTIC_SERVER_SIDE_CURRENT_CONNECTIONS" )
          //    {
          //      # just use the low value.  Odds are there aren't over 2^32 current connections.
          //      # If your site is this big, you'll have to convert this to a 64 bit number.
          //      $cur_connections = $value.low;
          //      Write-Host "Current Connections: $cur_connections"
          //    }
          //  }
          //  Start-Sleep -s 1
          //}
          //Write-Host "Disabling Monitor State...";

          //$MemberMonitorState = New-Object -TypeName iControl.LocalLBPoolMemberMemberMonitorState;
          //$MemberMonitorState.member = New-Object -TypeName iControl.CommonIPPortDefinition;
          //$MemberMonitorState.member.address = $member_addr;
          //$MemberMonitorState.member.port = $member_port;
          //$MemberMonitorState.monitor_state = "STATE_DISABLED";
          //$MemberMonitorStateAofA = New-Object -TypeName "iControl.LocalLBPoolMemberMemberMonitorState[][]" 1,1
          //$MemberMonitorStateAofA[0][0] = $MemberMonitorState;
 
          //(Get-F5.iControl).LocalLBPoolMember.set_monitor_state( (, $pool_name), $MemberMonitorStateAofA);
 
          //Get-PoolMemberStatus $pool_name $member


        }

        
        public List<F5PoolMemberState> GetPoolMemberState(string poolName, CommonIPPortDefinition nodeName)
        {
            // Now - Sanity Check time; lets go back and ask the F5 what is the current status of this member and its pool
            // So we can report back to real staus to the pipeline

            List<F5PoolMemberState> poolMemberState = new List<F5PoolMemberState>();
            //string memberState = string.Empty;

            LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
            LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = F5Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

            for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
            {
                if (bigIPPoolMemberState[0][i].member.address.Equals(nodeName.address) && bigIPPoolMemberState[0][i].member.port == nodeName.port)
                {
                    F5PoolMemberState memberState = new F5PoolMemberState(poolName, bigIPPoolMemberState[0][i].member, "");

                    string state = string.Empty;
                    if (bigIPPoolMemberMonitor[0][i].monitor_status == LocalLBMonitorStatus.MONITOR_STATUS_FORCED_DOWN)
                        // Monitor State = Disabled
                        memberState.State = "Offline Forced";
                    else
                        if (bigIPPoolMemberState[0][i].session_state == CommonEnabledState.STATE_ENABLED)
                            // Session State = Enabled
                            // Monitor State = Enabled
                            memberState.State = "Enabled";
                        else
                            // Session State = Disabled
                            // Monitor State = Enabled
                            memberState.State = "Disabled";

                    poolMemberState.Add(memberState);
                }
            }

            return poolMemberState;
        }


        public List<F5PoolMemberState> GetPoolMembersState(string poolName)
        {
            // Now - Sanity Check time; lets go back and ask the F5 what is the current status of this member and its pool
            // So we can report back to real staus to the pipeline

            List<F5PoolMemberState> poolMemberState = new List<F5PoolMemberState>();
            
            LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = F5Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
            LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = F5Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

            for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
            {
                F5PoolMemberState memberState = new F5PoolMemberState(poolName, bigIPPoolMemberState[0][i].member, "");

                string state = string.Empty;
                if (bigIPPoolMemberMonitor[0][i].monitor_status == LocalLBMonitorStatus.MONITOR_STATUS_FORCED_DOWN)
                    // Monitor State = Disabled
                    memberState.State = "Offline Forced";
                else
                    if (bigIPPoolMemberState[0][i].session_state == CommonEnabledState.STATE_ENABLED)
                        // Session State = Enabled
                        // Monitor State = Enabled
                        memberState.State = "Enabled";
                    else
                        // Session State = Disabled
                        // Monitor State = Enabled
                        memberState.State = "Disabled";
                
                poolMemberState.Add(memberState);
            }
            
            return poolMemberState;
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
