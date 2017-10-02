using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using FIM_Sync_LIB;


namespace FIM_Sync_OIP
{
    [Activity("Start Management Agent Run Profile", ShowFilters = false)]
    public class StartManagementAgentRunProfile : IActivity
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
            designer.AddInput("Management Agent Name");
            designer.AddInput("Run Profile Name");
            designer.AddOutput("Status").AsNumber().WithDescription("Status");
            designer.AddCorellatedData(typeof(FIMSyncManagementAgentRunProfiles));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            // Enumberate the Paramaters Passed to this Method
            string fimMAName = request.Inputs["Management Agent Name"].AsString();
            string fimProfileName = request.Inputs["Run Profile Name"].AsString();

            List<FIMSyncManagementAgentRunProfiles> agentRunInfo = FimSyncWmiServices.StartMARunProfile(connectionSettings, fimMAName, fimProfileName);

            int numAgents = response.WithFiltering().PublishRange(agentRunInfo);
            response.Publish("Management Agent Count", numAgents);
        }
    }    
}



//function Start-FIMRunAgent {
//   [cmdletbinding()]
//   param(
//      [Parameter(Mandatory = $true)] 
//      [string]$MAName,
      
//      [Parameter(Mandatory = $true)] 
//      [string]$ProfileName
//   )  

//   begin {    
//      # Prepair an Array for the report
//      $obj = New-Object PSObject
//      $obj | Add-Member NoteProperty Agent ("")
//      $obj | Add-Member NoteProperty Profile ("")
//      $obj | Add-Member NoteProperty Status ("")
 
//      $obj | Add-Member NoteProperty Started ("")
//      $obj | Add-Member NoteProperty Ended ("")
//      $obj | Add-Member NoteProperty TotalConnectorSpaceObjects ("")
//      $obj | Add-Member NoteProperty TotalConnectors ("")
//      $obj | Add-Member NoteProperty Connectors ("")
//      $obj | Add-Member NoteProperty ExplicitConnectors ("")
//      $obj | Add-Member NoteProperty TotalDisconnectors ("")
//      $obj | Add-Member NoteProperty Disconnectors ("")
//      $obj | Add-Member NoteProperty ExplicitDisconnectors ("")
//      $obj | Add-Member NoteProperty FilteredDisconnectors ("")
//      $obj | Add-Member NoteProperty TotalPlaceholders ("")
//      $obj | Add-Member NoteProperty ImportAdd ("")
//      $obj | Add-Member NoteProperty ImportUpdates ("")
//      $obj | Add-Member NoteProperty ImportDeletes ("")
//      $obj | Add-Member NoteProperty ImportNoChange ("")
//      $obj | Add-Member NoteProperty ExportAdd ("")
//      $obj | Add-Member NoteProperty ExportUpdates ("")
//      $obj | Add-Member NoteProperty ExportDeletes ("")
//   }

//   process {
//    # Update the Object with the Names of the Agent and Profile
//    $obj.Agent  = $MAName
//    $obj.Profile = $ProfileName
    
//    #Check for the Agent
    
//    $curMA = @(get-wmiobject -class "MIIS_ManagementAgent"`
//                          -namespace "root\MicrosoftIdentityIntegrationServer"`
//                          -computername "."`
//                          -filter "Name='$MAName'") 
    
//    if($curMA.count -eq 0) {
//       # No Agent Located, report and give up
//       $obj.Status = "Agent Not Found"
//    }
//    else {
//       # Agent Located, Execute and store its result
//       $obj.Status = $($curMA[0].Execute($ProfileName).ReturnValue)
//       $obj.Started = $($curMA[0].RunStartTime().ReturnValue)
//       $obj.Ended = $($curMA[0].RunEndTime().ReturnValue)
//       $obj.TotalConnectorSpaceObjects = $($curMA[0].NumCSObjects().ReturnValue)
//       $obj.TotalConnectors = $($curMA[0].NumTotalConnectors().ReturnValue)
//       $obj.Connectors = $($curMA[0].NumConnectors().ReturnValue)
//       $obj.ExplicitDisconnectors = $($curMA[0].NumExplicitConnectors().ReturnValue)
//       $obj.TotalDisconnectors = $($curMA[0].NumTotalDisconnectors().ReturnValue)
//       $obj.Disconnectors = $($curMA[0].NumDisconnectors().ReturnValue)
//       $obj.ExplicitDisconnectors = $($curMA[0].NumExplicitDisconnectors().ReturnValue)
//       $obj.FilteredDisconnectors = $($curMA[0].NumFilteredDisconnectors().ReturnValue)
//       $obj.TotalPlaceholders = $($curMA[0].NumPlaceholders().ReturnValue)
//       $obj.ImportAdd = $($curMA[0].NumImportAdd().ReturnValue)
//       $obj.ImportUpdates = $($curMA[0].NumImportUpdate().ReturnValue)
//       $obj.ImportDelete = $($curMA[0].NumImportDelete().ReturnValue)
//       $obj.ImportNoChange = $($curMA[0].NumImportNoChange().ReturnValue)
//       $obj.ExportAdd = $($curMA[0].NumExportAdd().ReturnValue)
//       $obj.ExportUpdates = $($curMA[0].NumExportUpdate().ReturnValue)
//       $obj.ExportDeletes = $($curMA[0].NumExportDelete().ReturnValue) 
//    }
        
//    Write-Output $obj
//   }   
//}

