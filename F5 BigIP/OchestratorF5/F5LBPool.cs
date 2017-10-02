using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;

namespace OchestratorF5
{
    [OpalisData("F5 Local LB Pool")]
    public class F5LBPool
    {
        private string name = string.Empty;

        internal F5LBPool(string poolName)
        {
            this.name = poolName;
        }

        [OpalisOutput("F5 Pool Names",Description = "Pool Names"), OpalisFilter]
        public string PoolName
        {
            get { return name; }
        }
        
    }
}
