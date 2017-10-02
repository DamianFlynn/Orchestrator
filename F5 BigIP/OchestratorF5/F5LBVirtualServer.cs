using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;

namespace OchestratorF5
{
    [OpalisData("F5 Local LB Virtual Server")]
    public class F5LBVirtualServer
    {
        private string name = string.Empty;

        internal F5LBVirtualServer(string virtualServerName)
        {
            this.name = virtualServerName;
        }

        [OpalisOutput("F5 Virtual Server Names",Description = "Virtual Server Names"), OpalisFilter]
        public string VirtualServerName
        {
            get { return name; }
        }
        
    }
}
