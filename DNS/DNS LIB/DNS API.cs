using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Management;
using System.Net;

namespace DNS_LIB
{
    class DNS_API
    {

        public enum ZoneType
        {
            //DSIntegrated,
            Primary,
            Secondary,
            Stub,
            Forward
        }


        public enum ZoneTransferType
        {
            None,
            AllowAny,
            OnlyNameServers,
            Specific
        }


        public enum ResourceRecordType
        {
            A,
            AAAA,
            CNAME,
            NS,
            SOA,
            RP,
            MB,
            MX,
            TXT
        }


        private ManagementClass GetClass(String className)
        {
            ConnectionOptions conOptions = this.GetConnection(this.serverName);

            String path = this.GetDNSPath(this.serverName) + ":" + className;
            ManagementScope scope = new ManagementScope(path, conOptions);

            // Explict connection to WMI namespace
            scope.Connect();

            //this.Log.DebugFormat("Getting class '{0}'", path);
            ManagementPath manPath = new ManagementPath(path);

            return new ManagementClass(scope, manPath, null);
        }


        /// <summary>
        /// Create a new zone on the DNS server.
        /// </summary>
        /// <param name="zoneName">The name of the zone.</param>
        /// <param name="type">The type of zone that needs to be created.</param>
        /// <param name="primaries">A list of primary servers, or null if creating a Primary zone.</param>
        private void CreateZone(String zoneName, ZoneType type, List<IPAddress> primaries)
        {
            ManagementClass domain = this.GetClass("MicrosoftDNS_Zone");

            ManagementBaseObject inputParams = domain.GetMethodParameters("CreateZone");
            inputParams["ZoneName"] = zoneName;
            inputParams["ZoneType"] = (UInt32)type;
            inputParams["DsIntegrated"] = false;
            if (type == ZoneType.Secondary && primaries != null && primaries.Count > 0)
            {
                IEnumerable<String> ipStrings = primaries.Select(p => p.ToString());

                inputParams["IpAddr"] = ipStrings.ToArray();
            }

            //this.Log.DebugFormat("Creating zone '{0}' on server '{1}'", zoneName, this.GetLocalServerName());
            ManagementBaseObject outParams = domain.InvokeMethod("CreateZone", inputParams, null);
            //this.Log.DebugFormat("Zone '{0}' created.", zoneName);
        }


        #region Zone related public methods
        public override void CreatePrimaryZone(String zoneName, ZoneTransferType transfer, List<IPAddress> allowTransferTo, List<String> ns, Int32 ttl, Int32 expiryLimit, Int32 retryDelay, Int32 refreshInterval, Int32 minimumTTL, String soaStart, String responsiblePerson)
        {
            this.CreateZone(zoneName, ZoneType.Primary, null);

            String query = String.Format("SELECT * FROM MicrosoftDNS_NSType WHERE DomainName = \"{0}\"", zoneName);
            // Get the default created NS record(s)
            ManagementObjectCollection NSList = this.GetRecordsFromDns(query);
            foreach (ManagementObject oManObject in NSList)
            {
                // Remove the default created NS record(s)
                oManObject.Delete();
            }

            query = String.Format("SELECT * FROM MicrosoftDNS_SOAType WHERE DomainName = \"{0}\"", zoneName);
            // Get the SOA record, we need to update it!
            ManagementObjectCollection SOAList = this.GetRecordsFromDns(query);

            foreach (ManagementObject oManObject in SOAList)
            {
                Boolean propertyChange = false;
                String sProNames = "";
                foreach (PropertyData oPropData in oManObject.Properties)
                {
                    sProNames += " " + oPropData.Name;
                    if (oPropData.Name == "PrimaryServer" && (string)oPropData.Value != soaStart)
                    {
                        oPropData.Value = soaStart;
                        propertyChange = true;
                    }
                    if (oPropData.Name == "ResponsibleParty" && (string)oPropData.Value != responsiblePerson)
                    {
                        oPropData.Value = responsiblePerson;
                        propertyChange = true;
                    }
                    if (oPropData.Name == "ExpireLimit" && (UInt32)oPropData.Value != expiryLimit)
                    {
                        oPropData.Value = expiryLimit;
                        propertyChange = true;
                    }
                    else if (oPropData.Name == "RetryDelay" && (UInt32)oPropData.Value != retryDelay)
                    {
                        oPropData.Value = retryDelay;
                        propertyChange = true;
                    }
                    else if (oPropData.Name == "RefreshInterval" && (UInt32)oPropData.Value != refreshInterval)
                    {
                        oPropData.Value = refreshInterval;
                        propertyChange = true;
                    }
                    else if (oPropData.Name == "MinimumTTL" && (UInt32)oPropData.Value != minimumTTL)
                    {
                        oPropData.Value = minimumTTL;
                        propertyChange = true;
                    }
                    else if (oPropData.Name == "TTL" && (UInt32)oPropData.Value != ttl)
                    {
                        oPropData.Value = ttl;
                        propertyChange = true;
                    }
                }

                // Did we need to change any of the properties?
                if (propertyChange)
                {
                    InvokeMethodOptions options = new InvokeMethodOptions();
                    // Yes, let's save the changes!
                    ManagementBaseObject outParams = oManObject.InvokeMethod("Modify", oManObject, options);
                }
            }

            foreach (String nameServer in ns)
            {
                this.CreateNSRecord(zoneName, zoneName, nameServer);
            }

            // TODO:
            //this.UpdateZoneTransfer(zone, transfer, allowTransferTo);

        }
        #endregion

    }
}
