using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;


namespace FIM_Sync_LIB
{
    [ActivityData("FIM Sync Management Agent Run Profiles")]
    public class FIMSyncManagementAgentRunProfiles
    {
        private string status = string.Empty;
        private string started = string.Empty;
        private string ended = string.Empty;
        private string totalConnectorSpaceObjects = string.Empty;
        private string totalConnectors = string.Empty;
        private string connectors = string.Empty;
        private string explicitConnectors = string.Empty;
        private string totalDisconnectors = string.Empty;
        private string disconnectors = string.Empty;
        private string explicitDisconnectors = string.Empty;
        private string filteredDisconnectors = string.Empty;
        private string totalPlaceholders = string.Empty;
        private string importAdd = string.Empty;
        private string importUpdates = string.Empty;
        private string importDeletes = string.Empty;
        private string importNoChange = string.Empty;
        private string exportAdd = string.Empty;
        private string exportUpdates = string.Empty;
        private string exportDeletes = string.Empty;


        internal FIMSyncManagementAgentRunProfiles()
        {
            // Dummy Constructor 
        }

        internal FIMSyncManagementAgentRunProfiles(string runStatus, string runStart, string runEnd)
        {
            this.status = runStatus;
            this.started = runStart;
            this.ended = runEnd;
        }


        [ActivityOutput("Status", Description = "FIM Sync Management Agent Run Status"), ActivityFilter]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }


        //       $obj.Started = $($curMA[0].RunStartTime().ReturnValue)
        [ActivityOutput("Run Start Time", Description = "FIM Sync Management Agent Run Start Time"), ActivityFilter]
        public string RunStartTime
        {
            get { return started; }
            set { ended = value; }
        }


        //       $obj.Ended = $($curMA[0].RunEndTime().ReturnValue)
        [ActivityOutput("Run End Time", Description = "FIM Sync Management Agent Run End Time"), ActivityFilter]
        public string RunEndTime
        {
            get { return ended; }
            set { ended = value; }
        }


        //       $obj.TotalConnectorSpaceObjects = $($curMA[0].NumCSObjects().ReturnValue)
        [ActivityOutput("Total Connector Space Objects", Description = "FIM Sync Management Agent Run Total Connector Space Objects"), ActivityFilter]
        public string NumCSObjects
        {
            get { return totalConnectorSpaceObjects; }
            set { totalConnectorSpaceObjects = value; }
        }


        //       $obj.TotalConnectors = $($curMA[0].NumTotalConnectors().ReturnValue)
        [ActivityOutput("Total Connectors", Description = "FIM Sync Management Agent Run Total Connectors"), ActivityFilter]
        public string NumTotalConnectors
        {
            get { return totalConnectors; }
            set { totalConnectors = value; }
        }


        //       $obj.Connectors = $($curMA[0].NumConnectors().ReturnValue)
        [ActivityOutput("Connectors", Description = "FIM Sync Management Agent Run Connectors"), ActivityFilter]
        public string NumConnectors
        {
            get { return connectors; }
            set { connectors = value; }
        }


        //       $obj.ExplicitDisconnectors = $($curMA[0].NumExplicitConnectors().ReturnValue)
        [ActivityOutput("Explicit Connectors", Description = "FIM Sync Management Agent Run Explicit Connectors"), ActivityFilter]
        public string NumExplicitConnectors
        {
            get { return explicitConnectors; }
            set { explicitConnectors = value; }
        }  


        //       $obj.TotalDisconnectors = $($curMA[0].NumTotalDisconnectors().ReturnValue)
        [ActivityOutput("Total Disconnectors", Description = "FIM Sync Management Agent Run Total Disconnectors"), ActivityFilter]
        public string NumTotalDisconnectors
        {
            get { return totalDisconnectors; }
            set { totalDisconnectors = value; }
        }     

        //       $obj.Disconnectors = $($curMA[0].NumDisconnectors().ReturnValue)
        [ActivityOutput("Disconnectors", Description = "FIM Sync Management Agent Run Disconnectors"), ActivityFilter]
        public string NumDisconnectors
        {
            get { return disconnectors; }
            set { disconnectors = value; }
        }     


        //       $obj.ExplicitDisconnectors = $($curMA[0].NumExplicitDisconnectors().ReturnValue)
        [ActivityOutput("Explicit Disconnectors", Description = "FIM Sync Management Agent Run Explicit Disconnectors"), ActivityFilter]
        public string NumExplicitDisconnectors
        {
            get { return explicitDisconnectors; }
            set { explicitDisconnectors = value; }
        }
        

        //       $obj.FilteredDisconnectors = $($curMA[0].NumFilteredDisconnectors().ReturnValue)
        [ActivityOutput("Filtered Disconnectors", Description = "FIM Sync Management Agent Run Filtered Disconnectors"), ActivityFilter]
        public string NumFilteredDisconnectors
        {
            get { return filteredDisconnectors; }
            set { filteredDisconnectors = value; }
        }     


        //       $obj.TotalPlaceholders = $($curMA[0].NumPlaceholders().ReturnValue)
        [ActivityOutput("Total Placeholders", Description = "FIM Sync Management Agent Run Total Placeholders"), ActivityFilter]
        public string NumPlaceholders
        {
            get { return totalPlaceholders; }
            set { totalPlaceholders = value; }
        }     



        //       $obj.ImportAdd = $($curMA[0].NumImportAdd().ReturnValue)
        [ActivityOutput("Import Additions", Description = "FIM Sync Management Agent Run Import Add"), ActivityFilter]
        public string NumImportAdd
        {
            get { return importAdd; }
            set { importAdd = value; }
        }


        //       $obj.ImportUpdates = $($curMA[0].NumImportUpdate().ReturnValue)
        [ActivityOutput("Import Updates", Description = "FIM Sync Management Agent Run Import Updates"), ActivityFilter]
        public string NumImportUpdate
        {
            get { return importUpdates; }
            set { importUpdates = value; }
        }


        //       $obj.ImportDelete = $($curMA[0].().ReturnValue)
        [ActivityOutput("Import Deletes", Description = "FIM Sync Management Agent Run Import Deletes"), ActivityFilter]
        public string NumImportDelete
        {
            get { return importDeletes; }
            set { importDeletes = value; }
        }


        //       $obj.ImportNoChange = $($curMA[0].NumImportNoChange().ReturnValue)
        [ActivityOutput("Import No Change", Description = "FIM Sync Management Agent Run Import No Change"), ActivityFilter]
        public string NumImportNoChange
        {
            get { return importNoChange; }
            set { importNoChange = value; }
        }


        //       $obj.ExportAdd = $($curMA[0].NumExportAdd().ReturnValue)
        [ActivityOutput("Export Additions", Description = "FIM Sync Management Agent Run Exports Adds"), ActivityFilter]
        public string NumExportAdd
        {
            get { return exportAdd; }
            set { exportAdd = value; }
        }


        //       $obj.ExportUpdates = $($curMA[0].NumExportUpdate().ReturnValue)
        [ActivityOutput("Export Updates", Description = "FIM Sync Management Agent Run Exports Updates"), ActivityFilter]
        public string NumExportUpdate
        {
            get { return exportUpdates; }
            set { exportUpdates = value; }
        }
 
        //       $obj.ExportDeletes = $($curMA[0].NumExportDelete().ReturnValue) 
        [ActivityOutput("Export Deletes", Description = "FIM Sync Management Agent Run Exports Deletes"), ActivityFilter]
        public string NumExportDelete
        {
            get { return exportDeletes; }
            set { exportDeletes = value; }
        }        

    }
}
