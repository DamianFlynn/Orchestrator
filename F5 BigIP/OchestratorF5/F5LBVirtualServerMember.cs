using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;
using iControl;

namespace OchestratorF5
{
    [OpalisData("F5 Local LB Virtual Server Member")]
    class F5LBVirtualServerMember
    {
        private string virtualServer = string.Empty;
        private CommonIPPortDefinition virtualServerMember = new CommonIPPortDefinition();
        private string virtualServerDefaultPool = string.Empty;
        private string virtualServerAvailability = string.Empty;
        private string virtualServerStatus = string.Empty;
        private string virtualServerDescription = string.Empty;
            
        internal F5LBVirtualServerMember()
        {
            this.virtualServer = string.Empty;
            this.virtualServerDefaultPool = string.Empty;
            this.virtualServerMember.address = string.Empty;
            this.virtualServerMember.port = 0;
            this.virtualServerAvailability = string.Empty;
            this.virtualServerStatus = string.Empty;
            this.virtualServerDescription = string.Empty;
        }


        internal F5LBVirtualServerMember(string VirtualServerName, string DefaultPool, CommonIPPortDefinition VirtualServerMember, LocalLBObjectStatus VirtualServerInfo)
        {
            this.virtualServer = VirtualServerName;
            this.virtualServerDefaultPool = DefaultPool;
            this.virtualServerMember = VirtualServerMember;
            this.objectStatus = VirtualServerInfo;
        }


        [OpalisOutput("F5 Virtual Server Name",Description = "Virtual Server Name"), OpalisFilter]
        public string name
        {
            get { return virtualServer; }
            set { virtualServer = value; }
        }

        
        [OpalisOutput("F5 Virtual Server Default Pool",Description = "Virtual Server Default Pool Name"), OpalisFilter]
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

        [OpalisOutput("F5 Virtual Server Member Address",Description = "Virtual Server Member Name"), OpalisFilter]
        public string address
        {
            get { return virtualServerMember.address; }
            set { virtualServerMember.address = value; }
        }

        [OpalisOutput("F5 Virtual Server Member Port",Description = "Virtual Server Member Port"), OpalisFilter]
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

        [OpalisOutput("F5 Virtual Server State",Description = "Virtual Server State"), OpalisFilter]
        public string state
        {
            get { return virtualServerStatus; }
            set { virtualServerStatus = value; }
        }

        [OpalisOutput("F5 Virtual Server Availability",Description = "Virtual Server Availability"), OpalisFilter]
        public string availability
        {
            get { return virtualServerAvailability; }
            set { virtualServerAvailability = value; }
        }

        [OpalisOutput("F5 Virtual Server Status",Description = "Virtual Server Status"), OpalisFilter]
        public string status
        {
            get { return virtualServerDescription; }
            set { virtualServerDescription = value; }
        }

    }
}
