using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using System.Management;
using System.Management.Instrumentation;


namespace FIM_Sync_LIB
{
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String fimServer = String.Empty;

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public String Domain
        {
            get { return domain; }
            set { domain = value; }
        }

        public String FIMServer
        {
            get { return fimServer; }
            set { fimServer = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }   

    }
}

