using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PS_Factory;
using System.Collections;
using AD_Utilities_LIB.Properties;

namespace AD_Utilities_LIB
{
    public static class Bitlocker
    {
        public static IEnumerable<BitLocker_KeySet> Get_ADBitLockerRecoveryKeys(PSFactory psRunspace, string strComputer)
        {
            // First  Load up our Powershell Resources (Modules and Functions)
            string fileContent = Resources.Get_ADBitLockerRecoveryKeys;
            
            // Create a Dictionary to store the results
            List<Dictionary<string, string>> results;

            // Execute our Query
            results = psRunspace.RunScript(new List<string> { fileContent, "Get-BitLockerRecoveryKey -Computer " + strComputer });
            foreach (var entry in results)
            {
                if (entry.ContainsKey("RecoveryPassword"))
                {
                    string canonicalName = entry["CanonicalName"];
                    string recoveryPassword = entry["RecoveryPassword"];
                    yield return new BitLocker_KeySet(canonicalName, recoveryPassword);
                }
            }
        }


    }
}

