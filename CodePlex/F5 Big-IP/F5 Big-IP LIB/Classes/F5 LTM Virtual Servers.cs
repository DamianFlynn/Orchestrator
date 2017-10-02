using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace F5_Big_IP_LIB
{
    [ActivityData("F5 LTM Virtual Server")]
    public class F5LTMVirtualServers
    {
        private string name = string.Empty;

        public F5LTMVirtualServers(string virtualServerName)
        {
            this.name = virtualServerName;
        }

        [ActivityOutput("F5 LTM Virtual Server Names",Description = "Virtual Server Names"), ActivityFilter]
        public string VirtualServerName
        {
            get { return name; }
        }
        
    }
}
