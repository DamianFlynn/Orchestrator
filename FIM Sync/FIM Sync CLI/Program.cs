using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FIM_Sync_LIB;


namespace FIM_Sync_CLI
{

    class Program
    {

        
        
        static void Main(string[] args)
        {

            ConnectionCredentials connection = new ConnectionCredentials();

            ETWLog log = new ETWLog();
            
            Console.WriteLine("FIM Sync Utility...");
            Console.WriteLine("");
            Console.Write("FIM Sync Server to connect to (FQDN) : ");
            connection.FIMServer = Console.ReadLine();
            
            Console.WriteLine("");
            Console.WriteLine("Authentication Details...");
            Console.WriteLine("");
            Console.Write("   Username : ");
            connection.UserName = Console.ReadLine();
            Console.Write("     Domain : ");
            connection.Domain = Console.ReadLine();
            Console.Write("   Password : ");
            connection.Password = Console.ReadLine();

            log.DebugFormat(string.Format("DNS initialized, Domain User '{0}', user '{1}'", connection.Domain, connection.UserName));

            // Test to get a list of Management Agents on the FIM server
            Console.WriteLine("");
            Console.WriteLine("Executing...");
            Console.WriteLine("");

            List<FIMSyncManagementAgents> agentInfo = FimSyncWmiServices.GetManagementAgents(connection);

            foreach (FIMSyncManagementAgents agent in agentInfo)
            {
                Console.WriteLine("Agent Details");
                Console.WriteLine("Name: {0}", agent.Name);
                Console.WriteLine("Type: {0}", agent.Type);
                Console.WriteLine("Guid: {0}", agent.GUID);
            }


            // Select the Agent which you wish to Invoke
            string fimMAName = "My Agent";
            string fimProfileName = "Profile To Run";

            List<FIMSyncManagementAgentRunProfiles> agentRunInfo = FimSyncWmiServices.StartMARunProfile(connection, fimMAName, fimProfileName);

            foreach (FIMSyncManagementAgentRunProfiles agentRun in agentRunInfo)
            {
                Console.WriteLine("Agent Run Feedback");
                Console.WriteLine("Num Connectors: {0}", agentRun.NumConnectors);
                Console.WriteLine("CS Objects    : {0}", agentRun.NumCSObjects);
                Console.WriteLine("Disconnects   : {0}", agentRun.NumDisconnectors);
            }
        }

    }
}