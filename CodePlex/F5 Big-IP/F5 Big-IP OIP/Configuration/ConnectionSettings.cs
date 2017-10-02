using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;


namespace F5_Big_IP_OIP
{

    [ActivityDataAttribute("F5 Big-IP Connection Settings")]
    public class ConnectionSettings
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String host = String.Empty;

        [ActivityInputAttribute]
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [ActivityInputAttribute(PasswordProtected = true)]
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        [ActivityInputAttribute]
        public String Host
        {
            get { return host; }
            set { host = value; }
        }
    }
}
