using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;


namespace DFSService
{
    [OpalisData("DFS Node")]
    class DFSNode
    {
        private string dfsLinkPath = string.Empty;
        private bool isRoot = false;
        private string dfsDescription = string.Empty;
        private uint dfsTimeout = 0;

        internal DFSNode()
        {
            this.dfsLinkPath = string.Empty;
            this.isRoot = false;
            this.dfsDescription = string.Empty;
            this.dfsTimeout = 0;
        }

        internal DFSNode(string DFSLink, bool IsRoot, string DFSDescription, uint DFSTimeout)
        {
            this.dfsLinkPath = DFSLink;
            this.isRoot = IsRoot;
            this.dfsDescription = DFSDescription;
            this.dfsTimeout = DFSTimeout;
        }

        [OpalisOutput("DFS Link Path", Description = "Path name for DFS Share"), OpalisFilter]
        public string DFSLink
        {
            get { return dfsLinkPath; }
            set { dfsLinkPath = value; }
        }

        [OpalisOutput("DFS Is Root", Description = "Indicates if the path is a Root DFS Share"), OpalisFilter]
        public bool IsRoot
        {
            get { return isRoot; }
            set { isRoot = value; }
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
