using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DNS_LIB
{
    public class ConnectionCredentials
    {
        private String userName = String.Empty;
        private String password = String.Empty;
        private String domain = String.Empty;
        private String dnsServer = String.Empty;

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
            get { return dnsServer; }
            set { dnsServer = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

    }
}
