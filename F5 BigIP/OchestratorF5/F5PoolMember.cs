using System;
using Opalis.QuickIntegrationKit;
using iControl;

namespace OchestratorF5
{
    [OpalisData("F5 Pool Member State")]
    class F5PoolMemberState
    {
        private string pool = string.Empty;
        private string address = string.Empty;
        private long   port;
        private string state;

        internal F5PoolMemberState()
        {
            this.pool = string.Empty;
            this.address = string.Empty;
            this.port = 0;
            this.state = string.Empty;
        }

        internal F5PoolMemberState(string resultData)
        {
            string[] poolData;
    
            poolData = resultData.Split(';');

            pool = poolData[0];
            address = poolData[1];
            port = long.Parse(poolData[2]);
            state = poolData[3];
        }

        internal F5PoolMemberState(string PoolName, string MemberName, long MemberPort, string MemberState)
        {
            this.pool = PoolName;
            this.address = MemberName;
            this.port = MemberPort;
            this.state = MemberState;
        }

        internal F5PoolMemberState(string PoolName, CommonIPPortDefinition Member, string MemberState)
        {
            this.pool = PoolName;
            this.address = Member.address;
            this.port = Member.port;
            this.state = MemberState;
        }

        internal F5PoolMemberState(string PoolName, CommonIPPortDefinition Member)
        {
            this.pool = PoolName;
            this.address = Member.address;
            this.port = Member.port;
            this.state = string.Empty;
        }



        [OpalisOutput(Description = "Pool Name"), OpalisFilter]
        public string Pool
        {
            get { return pool; }
            set { pool = value; }
        }

        [OpalisOutput(Description = "Pool Member Name"), OpalisFilter]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        [OpalisOutput(Description = "Pool Member Port"), OpalisFilter]
        public long Port
        {
            get { return port; }
            set { port = value; }
        }

        [OpalisOutput(Description = "Pool Member State"), OpalisFilter]
        public string State
        {
            get { return state; }
            set { state = value; }
        }
    }
}
