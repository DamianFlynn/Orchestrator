﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iControl;
using System.Text.RegularExpressions;

namespace F5_Big_IP_LIB
{
    public class F5Interface
    {
        /*
         * Overview
         * 
         * The Purpose of this library is to simplify the integration with the iControl library with Orchestrator
         * 
         */

        // Before we do a thing, we need to create an Interface to the F5 iControl Library, and expose it
        private Interfaces F5Connection = new Interfaces();
        public Interfaces Connection { get { return F5Connection; } }
        private bool isF5Connected;
        private bool isAdminUser;



        /// <summary>
        /// This Method communicates with the F5, opens up a connection session for us
        /// </summary>
        /// <param name="Host">The Hostname of the F5</param>
        /// <param name="UserName">User Name of the connecting user</param>
        /// <param name="Password">Password of the connecting user</param>
        /// <returns>Bool, Was the connction established</returns>
        public bool Connect(string Host, string UserName, string Password)
        {
            // Invoke the Initialization and connection to rhe F5 Environment
            isF5Connected = F5Connection.initialize(Host, UserName, Password);
            
            // Next, we need to check if we are connected to the active node?
            if (F5Connection.SystemFailover.get_failover_state() == SystemFailoverFailoverState.FAILOVER_STATE_ACTIVE)
            {
                // After we connect, we should check what access level we have, as some functions like adding nodes
                // and members require that we have Admin type privilages.

                ManagementUserManagementUserRole[] myRoleIs = F5Connection.ManagementUserManagement.get_role(new string[] { UserName });
                switch (myRoleIs[0])
                {
                    case ManagementUserManagementUserRole.USER_ROLE_ADMINISTRATOR:
                    case ManagementUserManagementUserRole.USER_ROLE_APPLICATION_EDITOR:
                    case ManagementUserManagementUserRole.USER_ROLE_RESOURCE_ADMINISTRATOR:
                    case ManagementUserManagementUserRole.USER_ROLE_MANAGER:
                    case ManagementUserManagementUserRole.USER_ROLE_EDITOR:
                    case ManagementUserManagementUserRole.USER_ROLE_ASM_POLICY_EDITOR:
                        isAdminUser = true;
                        break;

                    case ManagementUserManagementUserRole.USER_ROLE_ASM_EDITOR:
                    case ManagementUserManagementUserRole.USER_ROLE_CERTIFICATE_MANAGER:
                    case ManagementUserManagementUserRole.USER_ROLE_GUEST:
                    case ManagementUserManagementUserRole.USER_ROLE_INVALID:
                    case ManagementUserManagementUserRole.USER_ROLE_TRAFFIC_MANAGER:
                    case ManagementUserManagementUserRole.USER_ROLE_USER_MANAGER:
                        isAdminUser = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Apparently we are not connected to the active node; so been a good citizen
                // I am going to drop off now, and let the world know we are disconnecting.

                isF5Connected = false;
                throw new Exception("We are not connected to the active F5 Appliance! Quitting.");
            }

            return isF5Connected;
        }


        /// <summary>
        /// This Method communicates with the F5, and Saves any current changes on the configuration
        /// And now also will Sync any devices in the F5 Cluster.
        /// </summary>
        public void SaveLBConfiguration()
        {

            if (isF5Connected)
            {
                Connection.SystemConfigSync.save_configuration("/config/bigip.conf", SystemConfigSyncSaveMode.SAVE_HIGH_LEVEL_CONFIG);
                Connection.SystemConfigSync.synchronize_configuration(SystemConfigSyncSyncMode.CONFIGSYNC_ALL);
            }
            else
                throw new Exception("We are not connected to the F5!");
        }



        /* ------------------------------------------------------------------------------------------------------------
         * F5 Virtual Servers Management Methods
         * 
         * Create a new LB Pool
         * Remove a LB Pool
         * 
         */


        /// <summary>
        /// This Method communicates with the F5, and checks  the list of Pools currently created on the device for a specified virtual name
        /// </summary>
        /// <param name="poolName">The name of the Virtual Server Name we are verifying to exist</param>
        /// <param name="memberList">The list of members in the pool, this can be ";" list</param>
        /// <param name="memberPort">The TCP Port that will be used for the Pool Listener</param>
        /// <param name="monitorType">The Type of Monitor to use for the Pool</param>
        public void CreateLBPool(string poolName, string memberList, long memberPort, string monitorType)
        {
            if (isF5Connected && isAdminUser)
            {
                string version = "1";
                AddLBMonitorTemplate(poolName + "_" + version + "_" + monitorType, "GET /CardMgmt/" + version + "/CMS.asmx", "FindCardRecord", monitorType);
                AddLBPool(poolName + "_" + version + "_" + monitorType, memberList, memberPort);
                AssignLBMonitorTemplate(poolName + "_" + version + "_" + monitorType, poolName);
            }
            else
                throw new Exception("We do not have privilages to remove a member to a pool");
        }



        /// <summary>
        /// This Method Removes a Pool from the F5 Ballancer using the name and montior
        /// </summary>
        /// <param name="poolName">The name of the Pool we will add</param>
        /// <param name="monitorType">The Type of Monitor we will use for the Pool</param>
        public void RemoveLBPool(string poolName, string monitorType)
        {
            if (isF5Connected && isAdminUser)
            {
                string version = "1";
                DeleteLBPool(poolName + "_" + version + "_" + monitorType);
                DeleteLBMonitorTemplate(poolName + "_" + version + "_" + monitorType);
            }
            else
                throw new Exception("We do not have privilages to remove a pool");
        }



        /* ------------------------------------------------------------------------------------------------------------
         * F5 Virtual Servers Management Methods
         * 
         * Get Load Balancer Virtual Servers List
         * Check a Virtual Server Exists
         * Lookup the Load Balancer to Match the Virtual Servers against our Query
         * Get the Pool List for a specified Virtual Server
         */


        /// <summary>
        /// This Method communicates with the F5, and returns the list of Virtual Servers currently created on the device
        /// </summary>
        /// <returns>String List, containing all the Virtual Server Names</returns>
        public List<string> GetLBVirtualServers()
        {

            List<string> virtualServerNames = new List<string>();

            if (isF5Connected)
            {
                string[] virtualServerList;
                virtualServerList = Connection.LocalLBVirtualServer.get_list();

                virtualServerNames = virtualServerList.ToList();
                virtualServerNames.Distinct().ToList();
                virtualServerNames.Sort();
            }
            else
                throw new Exception("We are not connected to the F5!");

            return virtualServerNames;
        }



        /// <summary>
        /// This Method communicates with the F5, and checks  the list of Pools currently created on the device for a specified virtual name
        /// </summary>
        /// <param name="poolName">The name of the Virtual Server Name we are verifying to exist</param>
        /// <returns>Bool, Identifying if the Virtual Server Name was located</returns>
        public bool CheckLBVirtualServerExists(string virtualServerName)
        {
            List<string> poolList;

            if (isF5Connected)
            {
                poolList = GetLBVirtualServers();
            }
            else
                throw new Exception("We are not connected to the F5!");

            return poolList.Contains(virtualServerName);
        }



        /// <summary>
        /// This Method communicates with the F5, and let us search for virtual servers
        /// </summary>
        /// <param name="query">The patern we are expecting the query string, is the same format which we would expect if they 
        /// user was to provide the name of a member, ie. "address:port", however in order to make this smart, we will accept "*:*"
        /// or a diritive of the pattern</param>
        /// <returns>Pool Member List, Identifying the matched Virtual Servers</returns>
        public List<F5LTMPoolMembers> LookupLBVirtualServer(string query)
        {
            // The patern we are expecting the query string, is the same format which we would expect if they 
            // user was to provide the name of a member, ie. address:port 
            // however, in order to make this smart, we will accept *:* or a diritive of the patter

            // Lets begin assuming the query will be for ALL Members and Port
            string queryAddress = "*";
            string queryPort = "*";

            // Now, lets see if the user actaully asked for a port
            if (query != null)
            {
                queryAddress = query;
                String[] sSplit = query.Split(new char[] { ':' });
                if (2 == sSplit.Length)
                {
                    queryAddress = sSplit[0];
                    queryPort = sSplit[1];
                }

                // Now after applying the split, we need to just check that we have not
                // ended up with assigning a null value; eg ":*" or ":" as passed in.
                if (queryAddress == null) { queryAddress = "*"; };
                if (queryPort == null) { queryPort = "*"; };

            }


            List<F5LTMPoolMembers> virtualMembers = new List<F5LTMPoolMembers>();

            if (isF5Connected)
            {
                string[] virtualServers = Connection.LocalLBVirtualServer.get_list();
                CommonIPPortDefinition[] members = Connection.LocalLBVirtualServer.get_destination(virtualServers);
                for (int i = 0; i < members.Length; i++)
                {

                    if (Regex.IsMatch(members[i].address + ":" + members[i].port, query))
                    {
                        F5LTMPoolMembers matchedMember = new F5LTMPoolMembers(virtualServers[i], members[i]);
                        virtualMembers.Add(matchedMember);
                    }
                }
            }
            else
                throw new Exception("We are not connected to the F5!");

            return virtualMembers;
        }


        
        /// <summary>
        /// This Method communicates with the F5, and returns the list of Pools currently created on the device
        /// </summary>
        /// <param name="virtualServerName">The name of the Virtual Server we are looking to query for Pools</param>
        /// <returns>Virtual Server List, of all the pools in a Virtual Server</returns>
        public List<F5LTMVirtualServerMembers> GetLBVirtualServerMembers(string virtualServerName)
        {
            // Create a list of F5 Pools Member Objects
            List<F5LTMVirtualServerMembers> F5VirtualServerMembers = new List<F5LTMVirtualServerMembers>();

            // Query the F5 for the Status, Members and Pool for the vi
            if (isF5Connected)
            {
                string[] bigIPVirtualMemberDefaultPool = Connection.LocalLBVirtualServer.get_default_pool_name(new string[] { virtualServerName });
                CommonIPPortDefinition[] bigIPVirtualMember = Connection.LocalLBVirtualServer.get_destination(new string[] { virtualServerName });
                LocalLBObjectStatus[] bigIPVirtualMemberStatus = Connection.LocalLBVirtualServer.get_object_status(new string[] { virtualServerName });

                for (int i = 0; i < bigIPVirtualMember.Length; i++)
                {
                    F5LTMVirtualServerMembers virtualServer = new F5LTMVirtualServerMembers(virtualServerName, bigIPVirtualMemberDefaultPool[i], bigIPVirtualMember[i], bigIPVirtualMemberStatus[i]);

                    // add the member and its details into the object list.
                    F5VirtualServerMembers.Add(virtualServer);
                }
            }
            else
                throw new Exception("We are not connected to the F5!");

            return F5VirtualServerMembers;
        }




        /* ------------------------------------------------------------------------------------------------------------
         * F5 Pool Management Methods
         * 
         * Public - Get Load Balancer Pool List
         * Public - Check a specified Load Balancer Pool exists
         * Public - Lookup the Load Balancer to Match the Pools against our Query
         * Public - Get the Members List for a specified Pool
         * Private - Add a new Load Balancer Pool
         * Private - Delete a specified Load Balancer Pool
         * 
         */


        /// <summary>
        /// This Method communicates with the F5, and returns the list of Pools currently created on the device
        /// </summary>
        /// <returns>String List, containing all the Pool Names</returns>
        public List<string> GetLBPools()
        {

            List<string> poolNames = new List<string>();

            if (isF5Connected)
            {
                string[] poolList;
                poolList = Connection.LocalLBPool.get_list();

                poolNames = poolList.ToList();
                poolNames.Distinct().ToList();
                poolNames.Sort();
            }
            else
                throw new Exception("We are not connected to the F5!");

            return poolNames;
        }



        /// <summary>
        /// This Method communicates with the F5, and returns the list of Pools currently created on the device
        /// </summary>
        /// <param name="poolName">The name of the Pool we are verifying to exist</param>
        /// <returns>Bool, Identifying if the Pool was located</returns>
        public bool CheckLBPoolExists(string poolName)
        {
            List<string> poolList;
            
            if (isF5Connected)
                poolList = GetLBPools();
            else
                throw new Exception("We are not connected to the F5!");

            return poolList.Contains(poolName);
        }



        /// <summary>
        /// This Method communicates with the F5, and adds a new Pool, along with its member list and TCP Port address
        /// </summary>
        /// <param name="query">The patern we are expecting the query string, is the same format which we would expect if they 
        /// user was to provide the name of a member, ie. "address:port", however in order to make this smart, we will accept "*:*"
        /// or a diritive of the pattern</param>
        /// <returns>Pool Member List, Identifying the matched Pool Members</returns>
        public List<F5LTMPoolMembers> LookupLBPoolMembers(string query)
        {
            // Lets begin assuming the query will be for ALL Members and Port
            string queryAddress = "*";
            string queryPort = "*";

            // Now, lets see if the user actaully asked for a port
            if (query != null)
            {
                queryAddress = query;
                String[] sSplit = query.Split(new char[] { ':' });
                if (2 == sSplit.Length)
                {
                    queryAddress = sSplit[0];
                    queryPort = sSplit[1];
                }

                // Now after applying the split, we need to just check that we have not
                // ended up with assigning a null value; eg ":*" or ":" as passed in.
                if (queryAddress == null) { queryAddress = "*"; };
                if (queryPort == null) { queryPort = "*"; };

                query = queryAddress + ":" + queryPort;
            }


            List<F5LTMPoolMembers> virtualMembers = new List<F5LTMPoolMembers>();
            if (isF5Connected)
            {
                string[] poolList = Connection.LocalLBPool.get_list();
                CommonIPPortDefinition[][] poolMembers = Connection.LocalLBPool.get_member(poolList);

                for (int poolLoop = 0; poolLoop < poolList.Length; poolLoop++)
                {
                    CommonIPPortDefinition[] currentPoolMember = poolMembers[poolLoop];

                    for (int memberLoop = 0; memberLoop < poolMembers.Length; memberLoop++)
                    {
                        if (Regex.IsMatch(currentPoolMember[memberLoop].address + ":" + currentPoolMember[memberLoop].port, query, RegexOptions.IgnoreCase))
                        {
                            F5LTMPoolMembers matchedMember = new F5LTMPoolMembers(poolList[poolLoop], currentPoolMember[memberLoop]);
                            virtualMembers.Add(matchedMember);
                        }
                    }
                }
            }
            else
                throw new Exception("We are not connected to the F5!");

            return virtualMembers;
        }


        /// <summary>
        /// This Method communicates with the F5 and returns the member list for a specified Pool, and adds a new Pool
        /// </summary>
        /// <param name="poolName">The name of the Pool we are querying the member list of</param>
        /// <returns>A F5 Pool Member List, containing all the members of a specified Pool</returns>
        public List<F5LTMPoolMembers> GetLBPoolMembers(string poolName)
        {
            // Create a list of F5 Pools Member Objects
            List<F5LTMPoolMembers> F5PoolMembers = new List<F5LTMPoolMembers>();

            if (isF5Connected)
            {
                // Query the F5 for the Session State and Monitor State of the Members in this pool
                LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
                LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

                // Now, loop trough the Elements in the Array, 1 per Pool Member, and update the Pool Member Object each time with the information we have.
                for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
                {
                    F5LTMPoolMembers memberState = new F5LTMPoolMembers(poolName, bigIPPoolMemberState[0][i].member, "");

                    memberState.state = CalculateNodeState(bigIPPoolMemberMonitor[0][i].monitor_status, bigIPPoolMemberState[0][i].session_state);
                    memberState.activeConnections = this.GetLBPoolMemberActiveConnections(poolName, bigIPPoolMemberState[0][i].member.address + ":" + bigIPPoolMemberState[0][i].member.port);

                    // Add the member and its details into the object list.
                    F5PoolMembers.Add(memberState);
                }
            }
            else
                throw new Exception("We are not connected to the F5!");
        
            return F5PoolMembers;
        }



        /// <summary>
        /// This Method communicates with the F5, and adds a new Pool, along with its member list and TCP Port address
        /// </summary>
        /// <param name="poolName">The name of the Pool we are verifying to exist</param>
        /// <param name="memberList">The computer name which we would like to add to the pool, this can be a ";" delimited list of members</param>
        /// <param name="memberPort">The TCP port which this pool will be working on.</param>
        private void AddLBPool(string poolName, string memberList, long memberPort)
        {
            if (isF5Connected && isAdminUser)
            {
                if (!CheckLBPoolExists(poolName))
                {
                    String[] memberArray = memberList.Split(new char[] { ';' });
                    CommonIPPortDefinition[][] poolMembers = new CommonIPPortDefinition[memberArray.Length][];

                    for (int i = 0; i < memberArray.Length; i++)
                    {
                        // Create an object for each member and port
                        CommonIPPortDefinition newMember = new CommonIPPortDefinition();
                        newMember.address = memberArray[i];
                        newMember.port = memberPort;

                        // Now update the array of poolMembers with the new member information for pool creation
                        poolMembers[i] = new CommonIPPortDefinition[1];
                        poolMembers[i][0] = new CommonIPPortDefinition();
                        poolMembers[i][0] = newMember;
                    }

                    Connection.LocalLBPool.create(new string[] { poolName }, new LocalLBLBMethod[] { LocalLBLBMethod.LB_METHOD_ROUND_ROBIN }, poolMembers);
                }
            }
            else
                throw new Exception("We do not have privilages to add a pool");
        }


        /// <summary>
        /// This Method communicates with the F5, and returns the list of Pools currently created on the device
        /// </summary>
        /// <param name="poolName">The name of the Pool we are going to delete</param>
        private void DeleteLBPool(string poolName)
        {
            if (isF5Connected)
            {
                if (!CheckLBPoolExists(poolName))
                    Connection.LocalLBPool.delete_pool(new string[] { poolName });
            }
            else
                throw new Exception("");
        }



        /* ------------------------------------------------------------------------------------------------------------
         * F5 Pool Member Management Methods
         *
         * Public - Get the details for a member of a specified Pool
         * Private - Get the current active connections for a pool member
         * Public - Set a Member of a Load Balancer Pool
         * Private - Add a new Member to a Load Balancer Pool
         * Private - Delete a specified member for a Load Balancer Pool
         * 
         */



        /// <summary>
        /// Get the details for a member on a specified pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to Query for details</param>
        /// <returns>Pool Member List, deails on the member of a pool</returns>
        public List<F5LTMPoolMembers> GetLBPoolMember(string poolName, string poolMember)
        {
            // Convert the Pool Member into a format we can work with
            CommonIPPortDefinition member = parseMember(poolMember);

            // Create a list of F5 Pools Member Objects
            List<F5LTMPoolMembers> F5PoolMembers = new List<F5LTMPoolMembers>();

            if (isF5Connected)
            {
                // Query the F5 for the Session State and Monitor State of the Members in this pool
                LocalLBPoolMemberMemberSessionState[][] bigIPPoolMemberState = Connection.LocalLBPoolMember.get_session_enabled_state(new string[] { poolName });
                LocalLBPoolMemberMemberMonitorStatus[][] bigIPPoolMemberMonitor = Connection.LocalLBPoolMember.get_monitor_status(new string[] { poolName });

                // Now, loop trough the Elements in the Array, 1 per Pool Member, and update the Pool Member Object each time with the information we have.
                for (int i = 0; i < bigIPPoolMemberState[0].Length; i++)
                {
                    if (bigIPPoolMemberState[0][i].member.address.Equals(member.address) && bigIPPoolMemberState[0][i].member.port == member.port)
                    {

                        F5LTMPoolMembers memberState = new F5LTMPoolMembers(poolName, bigIPPoolMemberState[0][i].member, "");

                        memberState.state = CalculateNodeState(bigIPPoolMemberMonitor[0][i].monitor_status, bigIPPoolMemberState[0][i].session_state);
                        memberState.activeConnections = this.GetLBPoolMemberActiveConnections(poolName, bigIPPoolMemberState[0][i].member.address + ":" + bigIPPoolMemberState[0][i].member.port);

                        // Add the member and its details into the object list.
                        F5PoolMembers.Add(memberState);
                    }
                }
            }
            else
                throw new Exception("We are not connected to the F5!");

            return F5PoolMembers;
        }



        /// <summary>
        /// The number of current connections on a pool member
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to Query for current connections</param>
        /// <returns>Long, the current number of connections on a member</returns>
        private long GetLBPoolMemberActiveConnections(string poolName, string member)
        {
            long activeConnections = 0;

            CommonIPPortDefinition[][] searchMember = new CommonIPPortDefinition[1][];
            searchMember[0] = new CommonIPPortDefinition[1];
            searchMember[0][0] = new CommonIPPortDefinition();
            searchMember[0][0] = parseMember(member);

            if (isF5Connected)
            {
                LocalLBPoolMemberMemberStatistics[] memberStatistics = Connection.LocalLBPoolMember.get_statistics(new string[] { poolName }, searchMember);

                foreach (CommonStatistic memberStat in memberStatistics[0].statistics[0].statistics)
                {
                    if (memberStat.type == CommonStatisticType.STATISTIC_SERVER_SIDE_CURRENT_CONNECTIONS)
                    {
                        activeConnections = memberStat.value.low;
                    }
                }
            }
            else
                throw new Exception("We are not connected to the F5!");

            return activeConnections;
        }



        /// <summary>
        /// Set a member of a pool to specified state
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="poolMember">The name of the member we are looking to change state of for a pool</param>
        /// <param name="poolMemberState">The state we would like to set the member to be Enabled|Disabled|Forced Offline</param>
        public void SetLBPoolMember(string poolName, string poolMember, string poolMemberState)
        {
            // Convert the Pool Member into a format we can work with
            CommonIPPortDefinition memberNode = parseMember(poolMember);

            if (isF5Connected)
            {
                LocalLBPoolMemberMemberMonitorState[][] monitor_states = new LocalLBPoolMemberMemberMonitorState[1][];
                monitor_states[0] = new LocalLBPoolMemberMemberMonitorState[1];
                monitor_states[0][0] = new LocalLBPoolMemberMemberMonitorState();
                monitor_states[0][0].member = new CommonIPPortDefinition();
                monitor_states[0][0].member = memberNode;

                LocalLBPoolMemberMemberSessionState[][] session_states = new LocalLBPoolMemberMemberSessionState[1][];
                session_states[0] = new LocalLBPoolMemberMemberSessionState[1];
                session_states[0][0] = new LocalLBPoolMemberMemberSessionState();
                session_states[0][0].member = new CommonIPPortDefinition();
                session_states[0][0].member = memberNode;

                switch (poolMemberState)
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

                Connection.LocalLBPoolMember.set_monitor_state(new string[] { poolName }, monitor_states);
                Connection.LocalLBPoolMember.set_session_enabled_state(new string[] { poolName }, session_states);
            }
            else
                throw new Exception("We are not connected to the F5!");
        }


        /// <summary>
        /// Add a member to a Load Balancer Pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to add to the pool</param>
        public void AddLBPoolMember(string poolName, string poolMember)
        {
            // Before we add the poolMember to the pool, we need to check first if there is a node already inplace for the this member

            // Now we can proceed and add the new poolMember
            CommonIPPortDefinition[][] newMember = new CommonIPPortDefinition[1][];
            newMember[0] = new CommonIPPortDefinition[1];
            newMember[0][0] = new CommonIPPortDefinition();
            newMember[0][0] = parseMember(poolMember);

            if (isF5Connected && isAdminUser)
                Connection.LocalLBPool.add_member(new string[] { poolName }, newMember);
            else
                throw new Exception("We do not have privilages to add a members to a pool");
        }




        /// <summary>
        /// Remove a member for a Load Balancer Pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to remove from the pool</param>
        public void RemoveLBPoolMember(string poolName, string poolMember)
        {
            CommonIPPortDefinition[][] newMember = new CommonIPPortDefinition[1][];
            newMember[0] = new CommonIPPortDefinition[1];
            newMember[0][0] = new CommonIPPortDefinition();
            newMember[0][0] = parseMember(poolMember);


            if (isF5Connected && isAdminUser)
                Connection.LocalLBPool.remove_member(new string[] { poolName }, newMember);
            else
                throw new Exception("We do not have privilages to remove a member to a pool");
        }



        /* ------------------------------------------------------------------------------------------------------------
         * F5 Pool Member Management Methods
         *
         * Private - Get list of the defined Montior Templates
         * Private - Checks to see if a specified Montior Template Exists
         * Private - Set a Monitor template to a specified pool
         * Private - Add a new Monitor Template
         * Private - Delete a specified Monitor Template
         * 
         */


        private List<string> GetLBMonitorTemplates()
        {

            List<string> monitorTemplateNames = new List<string>();

            if (isF5Connected)
            {
                LocalLBMonitorMonitorTemplate[] monitorTemplateList;
                monitorTemplateList = Connection.LocalLBMonitor.get_template_list();

                foreach (LocalLBMonitorMonitorTemplate monitorTemplate in monitorTemplateList)
                {
                    monitorTemplateNames.Add(monitorTemplate.template_name);
                }

                monitorTemplateNames.Distinct().ToList();
                monitorTemplateNames.Sort();
            }
            else
                throw new Exception("We are not connected to the F5!");

            return monitorTemplateNames;
        }



        private bool CheckLBMonitorTemplateExists(string templateName)
        {
            List<string> monitorTemplateList;

            if(isF5Connected)
            {
                monitorTemplateList = GetLBMonitorTemplates();
            }
            else
                throw new Exception("We are not connected to the F5!");

            return monitorTemplateList.Contains(templateName);
        }


        /// <summary>
        /// Get the details for a member on a specified pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to Query for details</param>
        /// <returns>Pool Member List, deails on the member of a pool</returns>
        private void AssignLBMonitorTemplate(string templateName, string poolName)
        {
            LocalLBPoolMonitorAssociation monitor = new LocalLBPoolMonitorAssociation();
            monitor.pool_name = poolName;
            monitor.monitor_rule = new LocalLBMonitorRule();
            monitor.monitor_rule.type = LocalLBMonitorRuleType.MONITOR_RULE_TYPE_SINGLE;
            monitor.monitor_rule.quorum = 0;
            monitor.monitor_rule.monitor_templates = new string[] { templateName };

            if (isF5Connected && isAdminUser)
                Connection.LocalLBPool.set_monitor_association(new LocalLBPoolMonitorAssociation[] { monitor });
            else
                throw new Exception("We do not have privilages to assign a monitor");
        }




        /// <summary>
        /// Get the details for a member on a specified pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to Query for details</param>
        /// <returns>Pool Member List, deails on the member of a pool</returns>
        private void AddLBMonitorTemplate(string name, string send, string receive, string type)
        {
            if (isF5Connected && isAdminUser)
            {

                // Convert the Type to the correct Template Type Format
                LocalLBMonitorTemplateType templateType = new LocalLBMonitorTemplateType();
                switch (type)
                {
                    case "TCP":
                        templateType = LocalLBMonitorTemplateType.TTYPE_TCP;
                        break;
                    case "HTTP":
                        templateType = LocalLBMonitorTemplateType.TTYPE_HTTP;
                        break;
                    case "HTTPS":
                        templateType = LocalLBMonitorTemplateType.TTYPE_HTTPS;
                        break;
                    case "FTP":
                        templateType = LocalLBMonitorTemplateType.TTYPE_FTP;
                        break;
                    default:
                        break;
                }

                if (!CheckLBMonitorTemplateExists(name))
                {

                    LocalLBMonitorMonitorTemplate template = new LocalLBMonitorMonitorTemplate();
                    template.template_name = name;
                    template.template_type = templateType;

                    LocalLBMonitorCommonAttributes templateAttributes = new LocalLBMonitorCommonAttributes();
                    templateAttributes.parent_template = "";
                    templateAttributes.interval = 5;
                    templateAttributes.timeout = 16;
                    templateAttributes.dest_ipport = new LocalLBMonitorIPPort();

                    templateAttributes.dest_ipport.address_type = LocalLBAddressType.ATYPE_STAR_ADDRESS_STAR_PORT;
                    templateAttributes.dest_ipport.ipport = new CommonIPPortDefinition();
                    templateAttributes.dest_ipport.ipport.address = "0.0.0.0";
                    templateAttributes.dest_ipport.ipport.port = 0;
                    templateAttributes.is_read_only = false;
                    templateAttributes.is_directly_usable = true;

                    Connection.LocalLBMonitor.create_template(new LocalLBMonitorMonitorTemplate[] { template }, new LocalLBMonitorCommonAttributes[] { templateAttributes });

                    LocalLBMonitorStringValue[] stringValues = new LocalLBMonitorStringValue[2];
                    stringValues[0] = new LocalLBMonitorStringValue();
                    stringValues[0].type = LocalLBMonitorStrPropertyType.STYPE_SEND;
                    stringValues[0].value = send;
                    stringValues[1] = new LocalLBMonitorStringValue();
                    stringValues[1].type = LocalLBMonitorStrPropertyType.STYPE_RECEIVE;
                    stringValues[1].value = receive;

                    Connection.LocalLBMonitor.set_template_string_property(new string[] { name }, stringValues);
                }
            }
            else
                throw new Exception("We do not have privilages to add a new monitor");

        }



        /// <summary>
        /// Get the details for a member on a specified pool
        /// </summary>
        /// <param name="poolName">The name of the pool</param>
        /// <param name="member">The name of the member we are going to Query for details</param>
        /// <returns>Pool Member List, deails on the member of a pool</returns>
        private void DeleteLBMonitorTemplate(string name)
        {
            if (isF5Connected && isAdminUser)
            {
                if (CheckLBMonitorTemplateExists(name))
                    Connection.LocalLBMonitor.delete_template(new string[] { name });
            }
            else
                throw new Exception("We do not have privilages to delete a monitor");

        }





        /* ------------------------------------------------------------------------------------------------------------
         * Support Functions
         * 
         * Private - Calculate the Node State
         * Private - Delete a specified Load Balancer Pool
         * 
         */


        /// <summary>
        /// The State of a node is calculated by comparing the monitor and session information, to return a state result
        /// </summary>
        /// <param name="monitorState">Montior State information for the member we are checking</param>
        /// <param name="sessionState">Session State information for the member we are checking</param>
        /// <returns>String, containing the state of the Node</returns>
        private string CalculateNodeState(LocalLBMonitorStatus monitorState, CommonEnabledState sessionState)
        {

            // We need to determine the State of the member, this is a 2 step process, combining the Monitor and Session State Information
            //   Enabled        = Monitor Enabled  && Session Enabled
            //   Disabled       = Monitor Enabled  && Session Disabled
            //   Offline Forced = Monitor Disabled && Session Disabled

            string state = "Unknown";

            if (monitorState == LocalLBMonitorStatus.MONITOR_STATUS_FORCED_DOWN)
                state = "Offline Forced";
            else
                if (sessionState == CommonEnabledState.STATE_ENABLED)
                    state = "Enabled";
                else
                    state = "Disabled";

            return state;
        }



        /// <summary>
        /// The State of a node is calculated by comparing the monitor and session information, to return a state result
        /// </summary>
        /// <param name="member">The Member name provided in the format of NAME:PORT</param>
        /// <returns>IP Port Definition, the Member details in the correct structure</returns>
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



