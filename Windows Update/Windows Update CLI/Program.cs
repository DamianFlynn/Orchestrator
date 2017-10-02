using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows_Update_LIB;
using PS_Factory;

namespace Windows_Update_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            PSFactory myPowershell = new PSFactory();
            if (myPowershell.openRunspace("dpfadmin", "Express10n!", "corpnet", "bil-corp12.corpnet.liox.org"))
            {
                // Start Basic and run a simple command in the runspace
                WriteResult(myPowershell.RunScript(new List<string> { "Get-Service", "Get-Host" }));

                // Now, lets try our AD Utilities Library
                // Return the BitLocker Recover Keys for a System

                IEnumerable<WU_RebootStatus> rebootState = WU_Activities.Get_RebootStatus(myPowershell, "BAL-DF-E6500");
                foreach (WU_RebootStatus computerRebootState in rebootState)
                {
                    Console.WriteLine("Pending Reboot : {0}", computerRebootState.PendingReboot);
                }

                // Clean up Afterwards
                myPowershell.closeRunspace();
            }

        }


        static void WriteResult(List<Dictionary<string, string>> results)
        {
            Console.WriteLine("RESULTS ({0})", results.Count);

            foreach (var dict in results)
            {
                Console.WriteLine("==== {0}", dict.Count);
                foreach (var kvp in dict.OrderBy(e => e.Key))
                {
                    Console.WriteLine("    {0,20} = {1}", kvp.Key, kvp.Value);
                }
            }
        }

    }
}
