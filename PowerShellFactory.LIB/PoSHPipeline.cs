using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Globalization;

namespace PowerShellFactory
{
    public class PowershellRunspace
    {
        private Runspace runspaceInstance;
        private PSSnapInException warning = new PSSnapInException();
        private int defaultWinRMPort;

        public int DefaultWinRMPort
        {
            get { return defaultWinRMPort; }
            set { defaultWinRMPort = value; }
        }


        public PowershellRunspace()
        {

            WSManConnectionInfo connectionInfo = new WSManConnectionInfo();
            connectionInfo.OperationTimeout = 4 * 60 * 1000; // 4 minutes.
            connectionInfo.OpenTimeout = 1 * 60 * 1000; // 1 minute.
            connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;

            runspaceInstance = RunspaceFactory.CreateRunspace(connectionInfo);
        }


        public PowershellRunspace(string hostname)
        {

            WSManConnectionInfo connectionInfo = new WSManConnectionInfo(new Uri(string.Format(CultureInfo.InvariantCulture, "http://{0}/wsman", hostname)));
            connectionInfo.OperationTimeout = 4 * 60 * 1000; // 4 minutes.
            connectionInfo.OpenTimeout = 1 * 60 * 1000; // 1 minute.
            connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;

            runspaceInstance = RunspaceFactory.CreateRunspace(connectionInfo);
        }


        public PowershellRunspace(string username, string password, string domain, string hostname)
        {
            var securePassword = new System.Security.SecureString();

            foreach (char c in password) securePassword.AppendChar(c);

            if (!string.IsNullOrEmpty(domain))
            {
                username = domain + "\\" + username;
            }

            defaultWinRMPort = 80;

            WSManConnectionInfo connectionInfo = new WSManConnectionInfo(new Uri(string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}/wsman", hostname, DefaultWinRMPort)), "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", new PSCredential(username, securePassword));
            connectionInfo.OperationTimeout = 4 * 60 * 1000; // 4 minutes.
            connectionInfo.OpenTimeout = 1 * 60 * 1000; // 1 minute.
            connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;

            runspaceInstance = RunspaceFactory.CreateRunspace(connectionInfo);
        }


        public PowershellRunspace(string username, string password, string domain, string hostname, bool useSSL, int portNumber)
        {
            var securePassword = new System.Security.SecureString();

            foreach (char c in password) securePassword.AppendChar(c);

            if (!string.IsNullOrEmpty(domain))
            {
                username = domain + "\\" + username;
            }

            if (string.IsNullOrEmpty(hostname))
            {
                hostname = "localhost";
            }

            WSManConnectionInfo connectionInfo = new WSManConnectionInfo(useSSL, hostname, portNumber, "wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", new PSCredential(username, securePassword));


            connectionInfo.OperationTimeout = 4 * 60 * 1000; // 4 minutes.
            connectionInfo.OpenTimeout = 1 * 60 * 1000; // 1 minute.
            connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Kerberos;
            
            /*
            if (string.IsNullOrEmpty(authentication))
            {
                authentication = "Default";
            }

            connectionInfo.AuthenticationMechanism = (AuthenticationMechanism)Enum.Parse(typeof(AuthenticationMechanism), authentication);
            */
            runspaceInstance = RunspaceFactory.CreateRunspace(connectionInfo);
        }


        public void openPipeline()
        {
            runspaceInstance.Open();
        }


        public void openPipeline(string psSnapin)
        {
            runspaceInstance.RunspaceConfiguration.AddPSSnapIn(psSnapin, out warning);
            runspaceInstance.Open();
        }


        public void closePipeline()
        {
            runspaceInstance.Close();
        }




        public void executePipeline(String Script)
        {
            Pipeline pipeline = runspaceInstance.CreatePipeline();

            pipeline.Commands.AddScript(Script);
            Collection<PSObject> results = new Collection<PSObject>();

            try
            {
                results = pipeline.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            foreach (PSObject obj in results)
            {
                if (obj.BaseObject.GetType().ToString().Contains("VMware.VimAutomation.ViCore.Impl.V1.Inventory.VirtualMachineImpl"))
                {
                    String PowerState = obj.Members["PowerState"].Value.ToString();
                    String VMVersion = obj.Members["Version"].Value.ToString();
                    String Number_Cpu = obj.Members["NumCpu"].Value.ToString();
                    String Memory_MB = obj.Members["MemoryMB"].Value.ToString();
                    String HostId = obj.Members["HostId"].Value.ToString();
                    String FolderId = obj.Members["FolderId"].Value.ToString();
                    String ResourcePoolId = obj.Members["ResourcePoolId"].Value.ToString();
                    String UsedSpaceGB = obj.Members["UsedSpaceGB"].Value.ToString();
                    String ProvisionedSpaceGB = obj.Members["ProvisionedSpaceGB"].Value.ToString();
                    String id = obj.Members["Id"].Value.ToString();
                    String name = obj.Members["Name"].Value.ToString();

                    String VM_Description = String.Empty;
                    String Notes = String.Empty;
                    try { VM_Description = obj.Members["Description"].Value.ToString(); }
                    catch { }
                    try { Notes = obj.Members["Notes"].Value.ToString(); }
                    catch { }

                    //yield return new vm(PowerState, VMVersion, VM_Description, Notes, Number_Cpu, Memory_MB, HostId, FolderId, ResourcePoolId, UsedSpaceGB, ProvisionedSpaceGB, id, name);
                }
            }
        }


        


        /*
        public Collection<PSObject> executePipeline( String Script)
        {
            Pipeline pipeline = runspaceInstance.CreatePipeline();

            pipeline.Commands.AddScript(Script);
            Collection<PSObject> results = new Collection<PSObject>();

            try
            {
                results = pipeline.Invoke();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return results;
        }
        */


    }


}

