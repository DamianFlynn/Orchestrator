using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;


namespace OrchestratorDFS
{
    [OpalisData("DFS Root")]
    class DFSRoot
    {
        private string dfsLinkPath = string.Empty;
        private string dfsDescription = string.Empty;
        private uint dfsTimeout = 0;

        internal DFSRoot()
        {
            this.dfsLinkPath = string.Empty;
            this.dfsDescription = string.Empty;
            this.dfsTimeout = 0;
        }

        internal DFSRoot(string DFSLink, string DFSDescription, uint DFSTimeout)
        {
            this.dfsLinkPath = DFSLink;
            this.dfsDescription = DFSDescription;
            this.dfsTimeout = DFSTimeout;
        }

        [OpalisOutput("DFS Link Path", Description = "Path name for DFS Share"), OpalisFilter]
        public string DFSLink
        {
            get { return dfsLinkPath; }
            set { dfsLinkPath = value; }
        }

        
        [OpalisOutput("DFS Description", Description = "Description of a DFS Share"), OpalisFilter]
        public string DFSDescription
        {
            get { return dfsDescription; }
            set { dfsDescription = value; }
        }


        [OpalisOutput("DFS Timeout", Description = "Timeout duration for DFS Share"), OpalisFilter]
        public uint DFSTimeout
        {
            get { return dfsTimeout; }
            set { dfsTimeout = value; }
        }

    }
}
