using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;

namespace FIM_Sync_LIB
{
    public class FimSyncWmiServices
    {

        public enum ReturnValue
        {
            Success = 0,
            NotSupported = 1,
            AccessDenied = 2,
            DependentServicesRunning = 3,
            InvalidServiceControl = 4,
            ServiceCannotAcceptControl = 5,
            ServiceNotActive = 6,
            ServiceRequestTimeout = 7,
            UnknownFailure = 8,
            PathNotFound = 9,
            ServiceAlreadyRunning = 10,
            ServiceDatabaseLocked = 11,
            ServiceDependencyDeleted = 12,
            ServiceDependencyFailure = 13,
            ServiceDisabled = 14,
            ServiceLogonFailure = 15,
            ServiceMarkedForDeletion = 16,
            ServiceNoThread = 17,
            StatusCircularDependency = 18,
            StatusDuplicateName = 19,
            StatusInvalidName = 20,
            StatusInvalidParameter = 21,
            StatusInvalidServiceAccount = 22,
            StatusServiceExists = 23,
            ServiceAlreadyPaused = 24,
            ServiceNotFound = 25
        }


        public static ManagementClass createFIMWMIConnection(string Username, string Password, string Domain, string FIMServer)
        {

            // Connect to WMI on the Remote Server
            ConnectionOptions wmiConnOptions = new ConnectionOptions();

            wmiConnOptions.Username = Username;
            wmiConnOptions.Password = Password;
            wmiConnOptions.EnablePrivileges = true;
            wmiConnOptions.Authority = "ntlmdomain:" + Domain;

            //Create the scope that will connect to the default root for WMI
            ManagementScope scope = new ManagementScope(@"\\" + FIMServer + "\\root\\MicrosoftIdentityIntegrationServer", wmiConnOptions);
            scope.Connect();

            ObjectGetOptions wmiOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath wmiClassPath = new ManagementPath(string.Format("MIIS_ManagementAgent"));
            ManagementClass managementClass = new ManagementClass(scope, wmiClassPath, wmiOptions);

            return managementClass;
        }

        
        public static ManagementClass createFIMWMIConnection(string Username, string Password, string Domain, string FIMServer, string WMIScope, string WMIClass)
        {

            // Connect to WMI on the Remote Server
            ConnectionOptions wmiConnOptions = new ConnectionOptions();

            wmiConnOptions.Username = Username;
            wmiConnOptions.Password = Password;
            wmiConnOptions.EnablePrivileges = true;
            wmiConnOptions.Authority = "ntlmdomain:" + Domain;

            //Create the scope that will connect to the default root for WMI
            ManagementScope scope = new ManagementScope(@"\\" + FIMServer + WMIScope, wmiConnOptions);
            scope.Connect();

            ObjectGetOptions wmiOptions = new ObjectGetOptions(null, TimeSpan.MaxValue, true);
            ManagementPath wmiClassPath = new ManagementPath(string.Format(WMIClass));
            ManagementClass managementClass = new ManagementClass(scope, wmiClassPath, wmiOptions);

            return managementClass;
        }

        public static List<FIMSyncManagementAgents> GetManagementAgents(ConnectionCredentials connCreds)
        {
            ManagementClass managementClass = createFIMWMIConnection(connCreds.UserName, connCreds.Password, connCreds.Domain, connCreds.FIMServer);

            List<FIMSyncManagementAgents> managementAgents = new List<FIMSyncManagementAgents>();

            using (ManagementObjectCollection services = managementClass.GetInstances())
            {
                foreach (ManagementObject service in services)
                {
                    FIMSyncManagementAgents newAgent = new FIMSyncManagementAgents(service["Name"].ToString(), service["Type"].ToString(), service["GUID"].ToString());
                    managementAgents.Add(newAgent);
                }
            }

            return managementAgents;
        }


        public static List<FIMSyncManagementAgentRunProfiles> StartMARunProfile(ConnectionCredentials connCreds, string fimMAName, String fimProfileName)
        {
            ManagementClass managementClass = createFIMWMIConnection(connCreds.UserName, connCreds.Password, connCreds.Domain, connCreds.FIMServer);

            List<FIMSyncManagementAgentRunProfiles> managementAgents = new List<FIMSyncManagementAgentRunProfiles>();

            using (ManagementObjectCollection services = managementClass.GetInstances())
            {
                foreach (ManagementObject service in services)
                {
                    if (service["Name"].ToString().ToLower().CompareTo(fimMAName.ToLower()) == 0)
                    {
                        // We just matched the name of the Agent, so its now in context for us to start processing
                        FIMSyncManagementAgentRunProfiles newAgent = new FIMSyncManagementAgentRunProfiles();
                        newAgent.Status = "Located the Management Agent";

                        ManagementBaseObject inParams = service.GetMethodParameters("Execute");
                        inParams["RunProfileName"] = fimProfileName;
                        try
                        {
                            ManagementBaseObject outParams = service.InvokeMethod("Execute", inParams, null);
                            newAgent.Status = outParams["ReturnValue"].ToString();


                            if (newAgent.Status.Contains("success"))
                            {
                                outParams = service.InvokeMethod("RunStartTime", null, null);
                                newAgent.RunStartTime = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("RunEndTime", null, null);
                                newAgent.RunEndTime = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumCSObjects", null, null);
                                newAgent.NumCSObjects = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumTotalConnectors", null, null);
                                newAgent.NumTotalConnectors = outParams["ReturnValue"].ToString();
                                
                                outParams = service.InvokeMethod("NumConnectors", null, null);
                                newAgent.NumConnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumExplicitConnectors", null, null);
                                newAgent.NumExplicitConnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumTotalDisconnectors", null, null);
                                newAgent.NumTotalDisconnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumDisconnectors", null, null);
                                newAgent.NumDisconnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumExplicitDisconnectors", null, null);
                                newAgent.NumExplicitDisconnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumFilteredDisconnectors", null, null);
                                newAgent.NumFilteredDisconnectors = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumPlaceholders", null, null);
                                newAgent.NumPlaceholders = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumImportAdd", null, null);
                                newAgent.NumImportAdd = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumImportUpdates", null, null);
                                newAgent.NumImportUpdate = outParams["ReturnValue"].ToString();

                                //outParams = service.InvokeMethod("NumImportDelete", null, null);
                                //newAgent.NumImportDelete = outParams["ReturnValue"].ToString();
                                
                                outParams = service.InvokeMethod("NumImportNoChange", null, null);
                                newAgent.NumImportNoChange = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumExportAdd", null, null);
                                newAgent.NumExportAdd = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumExportUpdate", null, null);
                                newAgent.NumExportUpdate = outParams["ReturnValue"].ToString();

                                outParams = service.InvokeMethod("NumExportDelete", null, null);
                                newAgent.NumExportDelete = outParams["ReturnValue"].ToString();
                            }

                            managementAgents.Add(newAgent);
                        }

                        catch (Exception ex)
                        {
                            //throw ex;
                            newAgent.Status = ex.ToString();
                        }
                    }
                }
            }

            return managementAgents;
        }

        //Removes run histories from the server that have a run end date that is earlier than, or equal to, the specified date.
        public static string ClearRunHistory(ConnectionCredentials connCreds, string EndingBefore)
        {
            ManagementClass managementClass = createFIMWMIConnection(connCreds.UserName, connCreds.Password, connCreds.Domain, connCreds.FIMServer, "\\root\\MicrosoftIdentityIntegrationServer", "MIIS_Server");

            string clearResult = "WMI Failure";

            using (ManagementObjectCollection services = managementClass.GetInstances())
            {
                foreach (ManagementObject service in services)
                {
                    ManagementBaseObject inParams = service.GetMethodParameters("ClearRuns");
                    inParams["EndingBefore"] = EndingBefore;
                    
                    try
                    {
                        ManagementBaseObject outParams = service.InvokeMethod("ClearRuns", inParams, null);
                        clearResult = outParams["ReturnValue"].ToString();
                    }

                    catch (Exception ex)
                    {
                        //throw ex;
                        clearResult = ex.ToString();
                    }
                }
            }

            return clearResult;
        }


        //Removes password histories that have a date and time that are earlier than the specified date and time.
        public static string ClearPasswordHistory(ConnectionCredentials connCreds, string EndingBefore)
        {
            ManagementClass managementClass = createFIMWMIConnection(connCreds.UserName, connCreds.Password, connCreds.Domain, connCreds.FIMServer, "\\root\\MicrosoftIdentityIntegrationServer", "MIIS_Server");

            string clearResult = "WMI Failure";

            using (ManagementObjectCollection services = managementClass.GetInstances())
            {
                foreach (ManagementObject service in services)
                {
                    ManagementBaseObject inParams = service.GetMethodParameters("ClearPasswordHistory");
                    inParams["EndingBefore"] = EndingBefore;

                    try
                    {
                        ManagementBaseObject outParams = service.InvokeMethod("ClearPasswordHistory", inParams, null);
                        clearResult = outParams["ReturnValue"].ToString();
                    }

                    catch (Exception ex)
                    {
                        //throw ex;
                        clearResult = ex.ToString();
                    }
                }
            }

            return clearResult;
        }





    }
}
