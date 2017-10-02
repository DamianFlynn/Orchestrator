using Microsoft.SystemCenter.Orchestrator.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AD_Utilities_LIB
{
    [ActivityDataAttribute("AD BitLocker Keys")]
    public class BitLocker_KeySet
    {
        private string recoveryKey = string.Empty;
        private string canonicalName = string.Empty;
        private string machineId = string.Empty;
        private string keyDate = string.Empty;

        internal BitLocker_KeySet(string CanonicalName, string RecoveryKey)
        {
            this.recoveryKey = RecoveryKey;

            // Cannoical Name Format Sample
            // "corpnet.liox.org/!Offices/BAL/Computers/Mobile/BAL-DF-E6500/2011-01-31T17:55:51-00:00{83F48728-D5C5-437B-AF74-C0E961F1FE8A}"
            this.canonicalName = CanonicalName.Substring(0, CanonicalName.Length - 64);
            this.machineId = CanonicalName.Substring(CanonicalName.Length - 38);
            this.keyDate = CanonicalName.Substring(CanonicalName.Length - 63,25);
        }

        [ActivityOutputAttribute("AD BitLocker Recovery Key",Description = "Recovery Key"), ActivityFilterAttribute]
        public string RecoveryKey
        {
            get { return recoveryKey; }
        }

        [ActivityOutputAttribute("AD BitLocker CanonicalName", Description = "Canonical Name for Key"), ActivityFilterAttribute]
        public string CanonicalName
        {
            get { return canonicalName; }
        }

        [ActivityOutputAttribute("AD BitLocker Key Date", Description = "Creation Date of the Key"), ActivityFilterAttribute]
        public string KeyDate
        {
            get { return keyDate; }
        }

        [ActivityOutputAttribute("AD BitLocker Machine ID", Description = "Machine ID associated with the Key"), ActivityFilterAttribute]
        public string MachineID
        {
            get { return machineId; }
        }
    }
}
