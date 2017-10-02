using System;
using Opalis.QuickIntegrationKit;
using iControl;

namespace OchestratorF5
{
    [OpalisData("F5 Local LB Pool Member")]
    class F5LBPoolMember
    {
        private string nodePool = string.Empty;
        private CommonIPPortDefinition nodeMember = new CommonIPPortDefinition();
        private long   nodeActiveConnections;
        private string nodeState;

        internal F5LBPoolMember()
        {
            this.nodePool = string.Empty;
            this.nodeMember.address = string.Empty;
            this.nodeMember.port = 0;
            this.nodeState = string.Empty;
            this.nodeActiveConnections = 0;
        }

        internal F5LBPoolMember(string resultData)
        {
            string[] poolData;
    
            poolData = resultData.Split(';');

            nodePool = poolData[0];
            nodeMember.address = poolData[1];
            nodeMember.port = long.Parse(poolData[2]);
            nodeState = poolData[3];
            nodeActiveConnections = 0;
        }

        internal F5LBPoolMember(string PoolName, string MemberName, long MemberPort, string MemberState)
        {
            
            this.nodePool = PoolName;
            this.nodeMember.address = MemberName;
            this.nodeMember.port = MemberPort;
            this.nodeState = MemberState;
            this.nodeActiveConnections = 0;
        }

        internal F5LBPoolMember(string PoolName, CommonIPPortDefinition Member, string MemberState)
        {
            this.nodePool = PoolName;
            this.nodeMember.address = Member.address;
            this.nodeMember.port = Member.port;
            this.nodeState = MemberState;
            this.nodeActiveConnections = 0;
        }

        internal F5LBPoolMember(string PoolName, CommonIPPortDefinition Member)
        {
            this.nodePool = PoolName;
            this.nodeMember.address = Member.address;
            this.nodeMember.port = Member.port;
            this.nodeState = string.Empty;
            this.nodeActiveConnections = 0;
        }


        public CommonIPPortDefinition member
        {
               get { return nodeMember; }
            set { nodeMember = value; }
        }

        [OpalisOutput("F5 Pool Name",Description = "Pool Name"), OpalisFilter]
        public string pool
        {
            get { return nodePool; }
            set { nodePool = value; }
        }

        [OpalisOutput("F5 Pool Member Address",Description = "Pool Member Name"), OpalisFilter]
        public string address
        {
            get { return nodeMember.address; }
            set { nodeMember.address = value; }
        }

        [OpalisOutput("F5 Pool Member Port",Description = "Pool Member Port"), OpalisFilter]
        public long port
        {
            get { return nodeMember.port; }
            set { nodeMember.port = value; }
        }

        [OpalisOutput("F5 Pool Member Active Connections", Description = "Number of Active Connections on this Pool member"), OpalisFilter]
        public long activeConnections
        {
            get { return nodeActiveConnections; }
            set { nodeActiveConnections = value; }
        }

        [OpalisOutput("F5 Pool Member State",Description = "Pool Member State"), OpalisFilter]
        public string state
        {
            get { return nodeState; }
            set { nodeState = value; }
        }
    }
}
