using System;
using Microsoft.SystemCenter.Orchestrator.Integration;
using iControl;

namespace F5_Big_IP_LIB
{
    [ActivityData("F5 LTM Pool Members")]
    public class F5LTMPoolMembers
    {
        private string nodePool = string.Empty;
        private CommonIPPortDefinition nodeMember = new CommonIPPortDefinition();
        private long   nodeActiveConnections;
        private string nodeState;

        internal F5LTMPoolMembers()
        {
            this.nodePool = string.Empty;
            this.nodeMember.address = string.Empty;
            this.nodeMember.port = 0;
            this.nodeState = string.Empty;
            this.nodeActiveConnections = 0;
        }

        internal F5LTMPoolMembers(string resultData)
        {
            string[] poolData;
    
            poolData = resultData.Split(';');

            nodePool = poolData[0];
            nodeMember.address = poolData[1];
            nodeMember.port = long.Parse(poolData[2]);
            nodeState = poolData[3];
            nodeActiveConnections = 0;
        }

        internal F5LTMPoolMembers(string PoolName, string MemberName, long MemberPort, string MemberState)
        {
            
            this.nodePool = PoolName;
            this.nodeMember.address = MemberName;
            this.nodeMember.port = MemberPort;
            this.nodeState = MemberState;
            this.nodeActiveConnections = 0;
        }

        internal F5LTMPoolMembers(string PoolName, CommonIPPortDefinition Member, string MemberState)
        {
            this.nodePool = PoolName;
            this.nodeMember.address = Member.address;
            this.nodeMember.port = Member.port;
            this.nodeState = MemberState;
            this.nodeActiveConnections = 0;
        }

        internal F5LTMPoolMembers(string PoolName, CommonIPPortDefinition Member)
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

        [ActivityOutput("F5 LTM Pool Name",Description = "Pool Name"), ActivityFilter]
        public string pool
        {
            get { return nodePool; }
            set { nodePool = value; }
        }

        [ActivityOutput("F5 LTM Pool Member Address",Description = "Pool Member Name"), ActivityFilter]
        public string address
        {
            get { return nodeMember.address; }
            set { nodeMember.address = value; }
        }

        [ActivityOutput("F5 LTM Pool Member Port",Description = "Pool Member Port"), ActivityFilter]
        public long port
        {
            get { return nodeMember.port; }
            set { nodeMember.port = value; }
        }

        [ActivityOutput("F5 LTM Pool Member Active Connections", Description = "Number of Active Connections on this Pool member"), ActivityFilter]
        public long activeConnections
        {
            get { return nodeActiveConnections; }
            set { nodeActiveConnections = value; }
        }

        [ActivityOutput("F5 LTM Pool Member State",Description = "Pool Member State"), ActivityFilter]
        public string state
        {
            get { return nodeState; }
            set { nodeState = value; }
        }
    }
}
