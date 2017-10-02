using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;


namespace RemoteDesktop.OIP
{
    [ActivityData("Remote Desktop Connection Settings")]
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String fimServer = String.Empty;

        [ActivityInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInput]
        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        [ActivityInput("Remote Desktop Host")]
        public String FIMServer
        {
            get { return fimServer; }
            set { fimServer = value; }
        }

        [ActivityInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }   
    }
}

