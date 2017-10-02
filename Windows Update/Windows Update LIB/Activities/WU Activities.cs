using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PS_Factory;

namespace Windows_Update_LIB
{
    public static class WU_Activities
    {
        public static IEnumerable<WU_RebootStatus> Get_RebootStatus(PSFactory psRunspace, string strComputer)
        {
            // Create a Dictionary to store the results
            List<Dictionary<string, string>> results;

            // Execute our Query
            results = psRunspace.RunScript(new List<string> { "New-Object -ComObject 'Microsoft.Update.SystemInfo'" });
            foreach (var entry in results)
            {
                if (entry.ContainsKey("RebootRequired"))
                {
                    string rebootRequired = entry["RebootRequired"];
                    yield return new WU_RebootStatus(rebootRequired);
                }
            }
        }
    }
}
