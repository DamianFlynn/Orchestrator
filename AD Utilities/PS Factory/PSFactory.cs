using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Globalization;
using System.Collections.ObjectModel;

namespace PS_Factory
{
    public class PSFactory
    {
        private const int DefaultWinRMPort = 5985;
        private Runspace space;

        public bool openRunspace(string userName, string password, string domain, string hostName)
        {
            return openRunspace(userName, password, domain, hostName, DefaultWinRMPort, false, null, null, null);
        }

        /// <summary>
        ///     Opens a local or remote runspace
        /// </summary>
        /// <param name="runspaceName">Name of the runspace, should not be null</param>
        /// <param name="userName">User name, null if openning local runspace with the current credential</param>
        /// <param name="password">Password for the given user, can be null</param>
        /// <param name="domain">Domain of the user, can be null</param>
        /// <param name="hostName">Host name if openning a remote runspace, otherwise null</param>
        /// <param name="portNumber">Port number greater than 0, or use the default if less than zero</param>
        /// <param name="useSsl">Whether to use SSL transport</param>
        /// <param name="authentication">AuthenticationMechanism, null for "Default"</param>
        /// <param name="outFilename">File name of standard output log, null if the logging is not needed</param>
        /// <param name="errFilename">File name of standard error log, null if the logging is not needed</param>
        /// <returns>If the runspace has been openned successfully</returns>
        
        public bool openRunspace(string userName, string password, string domain, string hostName, int portNumber, bool useSsl, string authentication, string outFileName, string errorFileName)
        {
            

            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(hostName))
            {
                // Local Server - Default Credentials (current user)
                InitialSessionState session = InitialSessionState.CreateDefault();
                space = RunspaceFactory.CreateRunspace( session );
            }
            else
            {
                WSManConnectionInfo conn;

                if (string.IsNullOrEmpty(hostName))
                {
                    hostName = "localhost";
                }

                if (string.IsNullOrEmpty(userName))
                {
                    // Current user, WinRM Connection
                    conn = new WSManConnectionInfo(new Uri(string.Format(CultureInfo.InvariantCulture, "http://{0}/wsman", hostName)));
                }
                else
                {
                    // Another user, remote runspace
    
                    // Change the password to secure string
                    var securePassword = new System.Security.SecureString();
                    foreach (char c in password) { securePassword.AppendChar(c); }

                    if (!string.IsNullOrEmpty(domain))
                    {
                        userName = domain + "\\" + userName;
                    }

                    if (portNumber > 0)
                    {
                        conn = new WSManConnectionInfo( useSsl, hostName, portNumber, "wsman", "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", new PSCredential(userName, securePassword));
                    }
                    else
                    {
                        // Remote Server - Default WinRM Port
                        conn = new WSManConnectionInfo(new Uri(string.Format(CultureInfo.InvariantCulture,"http://{0}:{1}/wsman",hostName, DefaultWinRMPort)), "http://schemas.microsoft.com/powershell/Microsoft.PowerShell", new PSCredential( userName, securePassword));
                    }
                }

                if (string.IsNullOrEmpty(authentication))
                {
                    authentication = "Default";
                }

                conn.AuthenticationMechanism = (AuthenticationMechanism)Enum.Parse(typeof(AuthenticationMechanism), authentication);
                space = RunspaceFactory.CreateRunspace(conn);
            }
                
                    
            try
            {
                space.Open();
            }
            catch (Exception e)
            {
                closeRunspace();
                return false;
            }

            return true;
        }

        public bool addSnapIn(string PSSnapIn)
        {
            PSSnapInException warning = new PSSnapInException();
            //Runspace runspace = RunspaceFactory.CreateRunspace();
            
            try
            {
                space.RunspaceConfiguration.AddPSSnapIn(PSSnapIn, out warning);
            }
            catch (Exception e)
            {
                closeRunspace();
                return false;
            }

            return true;
        }
          
        public bool closeRunspace()
        {
             space.Close();
             return true;
        }



        public List<Dictionary<string, string>> RunScript(List<string> scripts)
        {
            
            if (scripts == null || scripts.Count == 0)
            {
                // ERROR: No script to run"
                return null;
            }

            // Push scripts into the pipeline in order
            var pipeline = space.CreatePipeline();
            scripts.ForEach(line => pipeline.Commands.AddScript(line));

            var results = new List<Dictionary<string, string>>();

            Collection<PSObject> psobjs;

            // Run the scripts
            try
            {
                psobjs = pipeline.Invoke();
            }
            catch (RuntimeException e)
            {
                // On exception, return the detail of the exception
                var result = new Dictionary<string, string>();
                result["Exception.Message"] = e.Message;
                result["Exception.StackTrace"] = e.StackTrace;
                result["Exception.Source"] = e.Source;

                results.Add(result);

                return results;
            }
            catch (InvalidRunspaceStateException e)
            {
                // On exception, return the detail of the exception
                var result = new Dictionary<string, string>();
                result["Exception.Message"] = e.Message;
                result["Exception.StackTrace"] = e.StackTrace;
                result["Exception.Source"] = e.Source;

                results.Add(result);

                
                return results;
            }

            // Convert the list of PSObject to the list of Dictionary
            foreach (PSObject psobj in psobjs)
            {
                var result = new Dictionary<string, string>();

                result[".ToString"] = psobj.ToString();

                foreach (PSPropertyInfo prop in psobj.Properties)
                {
                    try
                    {
                        result[prop.Name] = prop.Value == null ? "" : prop.Value.ToString();

                
                    }
                    catch (GetValueInvocationException)
                    {
                        // If any property cannot be retrieved, just ignore it.
                    }
                }

                results.Add(result);
            }

            return results;
        }
    }
}
