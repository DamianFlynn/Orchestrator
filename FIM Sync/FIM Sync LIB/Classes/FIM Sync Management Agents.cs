using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;


namespace FIM_Sync_LIB
{
    [ActivityData("FIM Sync Management Agents")]
    public class FIMSyncManagementAgents
    {
        private string name = string.Empty;
        private string type = string.Empty;
        private string guid = string.Empty;

        internal FIMSyncManagementAgents()
        {
            // Dummy Constructor 
        }

        internal FIMSyncManagementAgents(string managementAgentName, string managementAgentType, string managementAgentGUID)
        {
            this.name = managementAgentName;
            this.type = managementAgentType;
            this.guid = managementAgentGUID;
        }

        [ActivityOutput("Management Agent Name", Description = "FIM Sync Management Agent Name"), ActivityFilter]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [ActivityOutput("Management Agent Type", Description = "FIM Sync Management Agent Type"), ActivityFilter]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        [ActivityOutput("Management Agent GUID", Description = "FIM Sync Management Agent GUID"), ActivityFilter]
        public string GUID
        {
            get { return guid; }
            set { guid = value; }
        }        
    }
}
