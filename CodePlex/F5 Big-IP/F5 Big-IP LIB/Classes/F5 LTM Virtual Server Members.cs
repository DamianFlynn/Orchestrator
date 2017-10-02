using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SystemCenter.Orchestrator.Integration;
using iControl;

namespace F5_Big_IP_LIB
{
    [ActivityData("F5 LTM Virtual Server Member")]
    public class F5LTMVirtualServerMembers
    {
        private string virtualServer = string.Empty;
        private CommonIPPortDefinition virtualServerMember = new CommonIPPortDefinition();
        private string virtualServerDefaultPool = string.Empty;
        private string virtualServerAvailability = string.Empty;
        private string virtualServerStatus = string.Empty;
        private string virtualServerDescription = string.Empty;
            
        internal F5LTMVirtualServerMembers()
        {
            this.virtualServer = string.Empty;
            this.virtualServerDefaultPool = string.Empty;
            this.virtualServerMember.address = string.Empty;
            this.virtualServerMember.port = 0;
            this.virtualServerAvailability = string.Empty;
            this.virtualServerStatus = string.Empty;
            this.virtualServerDescription = string.Empty;
        }


        internal F5LTMVirtualServerMembers(string VirtualServerName, string DefaultPool, CommonIPPortDefinition VirtualServerMember, LocalLBObjectStatus VirtualServerInfo)
        {
            this.virtualServer = VirtualServerName;
            this.virtualServerDefaultPool = DefaultPool;
            this.virtualServerMember = VirtualServerMember;
            this.objectStatus = VirtualServerInfo;
        }


        [ActivityOutput("F5 LTM Virtual Server Name",Description = "Virtual Server Name"), ActivityFilter]
        public string name
        {
            get { return virtualServer; }
            set { virtualServer = value; }
        }

        
        [ActivityOutput("F5 LTM Virtual Server Default Pool",Description = "Virtual Server Default Pool Name"), ActivityFilter]
        public string defaultPool
        {
            get { return virtualServerDefaultPool; }
            set { virtualServerDefaultPool = value; }
        }

        public CommonIPPortDefinition member
        {
            get { return virtualServerMember; }
            set { virtualServerMember = value; }
        }

        [ActivityOutput("F5 LTM Virtual Server Member Address",Description = "Virtual Server Member Name"), ActivityFilter]
        public string address
        {
            get { return virtualServerMember.address; }
            set { virtualServerMember.address = value; }
        }

        [ActivityOutput("F5 LTM Virtual Server Member Port",Description = "Virtual Server Member Port"), ActivityFilter]
        public long port
        {
            get { return virtualServerMember.port; }
            set { virtualServerMember.port = value; }
        }

        public LocalLBObjectStatus objectStatus 
        {
            set 
            {
                virtualServerDescription = value.status_description;

                switch (value.availability_status)
                {
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_BLUE:
                        virtualServerAvailability = "Blue";
                        break;
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_GRAY:
                        virtualServerAvailability = "Gray";
                        break;
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_GREEN:
                        virtualServerAvailability = "Green";
                        break;
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_NONE:
                        virtualServerAvailability = "None";
                        break;
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_RED:
                        virtualServerAvailability = "Red";
                        break;
                    case LocalLBAvailabilityStatus.AVAILABILITY_STATUS_YELLOW:
                        virtualServerAvailability = "Yellow";
                        break;
                    default:
                        break;
                }

                switch (value.enabled_status)
                {
                    case LocalLBEnabledStatus.ENABLED_STATUS_DISABLED:
                        virtualServerStatus = "Disabled";
                        break;
                    case LocalLBEnabledStatus.ENABLED_STATUS_DISABLED_BY_PARENT:
                        virtualServerStatus = "Disabled By Parent";
                        break;
                    case LocalLBEnabledStatus.ENABLED_STATUS_ENABLED:
                        virtualServerStatus = "Enabled";
                        break;
                    case LocalLBEnabledStatus.ENABLED_STATUS_NONE:
                        virtualServerStatus = "None";
                        break;
                    default:
                        break;
                }
                
            }
        }

        [ActivityOutput("F5 LTM Virtual Server State",Description = "Virtual Server State"), ActivityFilter]
        public string state
        {
            get { return virtualServerStatus; }
            set { virtualServerStatus = value; }
        }

        [ActivityOutput("F5 LTM Virtual Server Availability",Description = "Virtual Server Availability"), ActivityFilter]
        public string availability
        {
            get { return virtualServerAvailability; }
            set { virtualServerAvailability = value; }
        }

        [ActivityOutput("F5 LTM Virtual Server Status",Description = "Virtual Server Status"), ActivityFilter]
        public string status
        {
            get { return virtualServerDescription; }
            set { virtualServerDescription = value; }
        }

    }
}
