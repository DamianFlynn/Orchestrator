using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace F5_Big_IP_LIB
{
    [ActivityData("F5 LTM Pool")]
    public class F5LTMPools
    {
        private string name = string.Empty;

        public F5LTMPools(string poolName)
        {
            this.name = poolName;
        }

        [ActivityOutput("F5 LTM Pool Names",Description = "Pool Names"), ActivityFilter]
        public string PoolName
        {
            get { return name; }
        }
        
    }
}
