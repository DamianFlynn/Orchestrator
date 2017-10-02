using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using FIM_Sync_LIB;


namespace FIM_Sync_OIP
{
    [Activity("Clear Password History", ShowFilters = false)]
    public class ClearPasswordHistory : IActivity
    {
        private ConnectionCredentials connectionSettings;
        
        [ActivityConfigurationAttribute]
        public ConnectionCredentials Connection
        {
            get { return connectionSettings; }
            set { connectionSettings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddInput("Clear History Before Date").WithDateTimeBrowser().ToString();
            designer.AddOutput("Action Result").AsNumber().WithDescription("Result of Clearing Request");
            designer.AddCorellatedData(typeof(FIMSyncManagementAgents));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string EndingBefore = request.Inputs["Clear History Before Date"].AsString();
            string actionResult = FimSyncWmiServices.ClearPasswordHistory(connectionSettings, EndingBefore);

            response.Publish("Action Result", actionResult);            
        }


    }


    
}

