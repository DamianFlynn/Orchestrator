using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Collections;
using FIM_Sync_LIB;


namespace FIM_Sync_OIP
{
    [Activity("Get Management Agents", ShowFilters = false)]
    public class GetManagementAgents : IActivity
    {
        private FIMConnectionCredentials connectionSettings;
        
        [ActivityConfigurationAttribute]
        public FIMConnectionCredentials Connection
        {
            get { return connectionSettings; }
            set { connectionSettings = value; }
        }

        public void Design(IActivityDesigner designer)
        {
            designer.AddOutput("Management Agents Count").AsNumber().WithDescription("Number of Management Agents Defined");
            designer.AddCorellatedData(typeof(FIMSyncManagementAgents));
        }

        public void Execute(IActivityRequest request, IActivityResponse response)
        {
            List<FIMSyncManagementAgents> agentInfo = FimSyncWmiServices.GetManagementAgents(connectionSettings);

            int numAgents = response.WithFiltering().PublishRange(agentInfo);
            response.Publish("Management Agent Count", numAgents);
        }

    }

}




        

 
 


//function Get-FIMAgents {
//   begin {    
//      # Prepair an Array for the report
//      $obj = New-Object PSObject
//      $obj | Add-Member NoteProperty Agent ("")
//      $obj | Add-Member NoteProperty GUID ("")
//      $obj | Add-Member NoteProperty Type ("")

//   }

//   process {
//      #Quer WMI for Agents Known

//      $foundMA = @(get-wmiobject -class "MIIS_ManagementAgent"`
//                          -namespace "root\MicrosoftIdentityIntegrationServer"`
//                          -computername "." ) 

//      if($foundMA.count -eq 0) {
//         # No Agent Located, report and give up
//         write-Host ("Agent Not Found")
//      }
//      else {
//         foreach($curAgent in $FoundMA) {
//            # Agent Located, Execute and store its result
//            $obj.Agent = $curAgent.Name
//            $obj.GUID = $curAgent.GUID
//            $obj.Type = $curAgent.Type
//            Write-Output $obj
//         }
//      }
//   }   
//}

