using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RemoteDesktop.LIB.ROOT.CIMV2.TERMINALSERVICES;
using System.Management;

namespace RemoteDesktop.LIB
{
    public class RemoteDesktopService
    {

        public static string SetAllowTSConnection(string Username, string Password, string Domain, string Server, bool enabled)
        {
            string TSStatus;
            TSStatus = "Undefined";


            string providerPath = @"root\CIMv2\TerminalServices";
            ConnectionOptions wmiConnOptions = new ConnectionOptions();
            ManagementScope scope = new ManagementScope(providerPath);

            if (Server != null)
            {
                // If we are connecting to a remote host, we need to specify the hostname, and user details
                providerPath = @"\\" + Server + "\\root\\CIMv2\\TerminalServices";

                wmiConnOptions.Username = Username;
                wmiConnOptions.Password = Password;
                wmiConnOptions.EnablePrivileges = true;
                wmiConnOptions.Authority = "ntlmdomain:" + Domain;
                wmiConnOptions.Authentication = AuthenticationLevel.PacketPrivacy;

                scope = new ManagementScope(providerPath, wmiConnOptions);
            }
            scope.Connect();

            // Define any Query Condition we may require
            string condition = ""; // Sample "Name LIKE 'SQL%'";


            foreach (TerminalServiceSetting TSSetting in TerminalServiceSetting.GetInstances(scope, condition))
            {

                // Check the initial setting of the TS Access to this machine
                if (TSSetting.AllowTSConnections == TerminalServiceSetting.AllowTSConnectionsValues.FALSE)
                {
                    // TS Access is Disabled
                    TSStatus = "Disabled";
                    if (enabled)
                    {
                        //Method is requesting we Enable TS Connections
                        TSStatus = TSSetting.SetAllowTSConnections(1, 1).ToString();
                    }
                    else TSStatus = "Disabled - Unchanged";
                }
                else
                {
                    // TS Access is Enabled
                    TSStatus = "Enabled";
                    if (!enabled)
                    {
                        //Method is requesting we Disable TS Connections
                        TSStatus = TSSetting.SetAllowTSConnections(0, 1).ToString();
                    }
                    else TSStatus = "Enabled - Unchanged";
                }
            }
            return TSStatus;
        }

    }
}
