using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using RemoteDesktop.LIB;

namespace RemoteDesktop.OIP
{
    [Activity("Set Remote Desktop Connection", ShowFilters = false)]
    public class SetRemoteDesktopConnection : IActivity
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
            designer.AddInput("Server Name").WithBooleanBrowser();
            designer.AddInput("Allow Remote Desktop Connection").WithBooleanBrowser();
            designer.AddOutput("Action Result").AsNumber().WithDescription("Result of Request");
            //designer.AddCorellatedData(typeof(FIMSyncManagementAgents));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            string targetServer = request.Inputs["Server Name"].AsString();
            bool targetSetting = request.Inputs["Allow Remote Desktop Connection"].AsBoolean();

            string actionResult = RemoteDesktopService.SetAllowTSConnection(connectionSettings.UserName, connectionSettings.Password, connectionSettings.Domain, targetServer, targetSetting);
            response.Publish("Action Result", actionResult);            
        }

    }
    
}

