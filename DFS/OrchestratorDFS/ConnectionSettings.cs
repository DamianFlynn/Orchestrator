using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;


namespace OrchestratorDFS
{

    [OpalisData("DFS Connection Settings")]
    public class ConnectionSettings
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String host = String.Empty;

        [OpalisInput]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [OpalisInput(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [OpalisInput]
        public String Host
        {
            get { return host; }
            set { host = value; }
        }
    }
}
