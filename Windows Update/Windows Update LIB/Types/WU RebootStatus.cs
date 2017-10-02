using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;

namespace Windows_Update_LIB
{
    [ActivityDataAttribute("WU Pending Reboot")]
    public class WU_RebootStatus
    {
        private string pendingReboot = string.Empty;

        internal WU_RebootStatus(string PendingReboot)
        {
            this.pendingReboot = PendingReboot;
        }

        [ActivityOutputAttribute("Pending Reboot", Description = "Indicate if the machine is pending a reboot"), ActivityFilterAttribute]
        public string PendingReboot
        {
            get { return pendingReboot; }
        }
    }
}
