using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AD_Utilities_LIB;
using PS_Factory;

namespace AD_Utilities_CLI
{
    class Program
    {
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

        static void Main(string[] args)
        {
            
            PSFactory myPowershell = new PSFactory();
            if (myPowershell.openRunspace("dpfadmin", "Express10n!", "corpnet", "bil-corp12.corpnet.liox.org"))
            {
                // Start Basic and run a simple command in the runspace
                WriteResult(myPowershell.RunScript(new List<string> { "Get-Service", "Get-Host" }));

                // Now, lets try our AD Utilities Library
                // Return the BitLocker Recover Keys for a System

                IEnumerable<BitLocker_KeySet> computerKeys =  Bitlocker.Get_ADBitLockerRecoveryKeys(myPowershell, "BAL-DF-E6500");
                foreach (BitLocker_KeySet keySet in computerKeys)
                {
                    Console.WriteLine("Date Created : {0}", keySet.KeyDate);
                    Console.WriteLine("Machine ID   : {0}", keySet.MachineID);
                    Console.WriteLine("Recovery Key : {0}", keySet.RecoveryKey);
                }

                // Clean up Afterwards
                myPowershell.closeRunspace();
            }
            ////;

            ////WriteResult(myPowershell.RunScript(new List<string> { "hostname | write-host" }));
            ////WriteResult(myPowershell.RunScript(new List<string> { "a=1+2" }));


            //myPowershell.closeRunspace();

        }
    }
}
