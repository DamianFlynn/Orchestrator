using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Opalis.QuickIntegrationKit;

namespace DFSService
{
    [OpalisData("DFS Target")]
    class DFSTarget
    {
        private string dfsLinkPath = string.Empty;
        private string dfsTargetServer = string.Empty;
        private string dfsTargetShare = string.Empty;
        private string dfsDescription = string.Empty;
        private string dfsState = string.Empty;

        internal DFSTarget()
        {
            this.dfsLinkPath = string.Empty;
            this.dfsTargetServer = string.Empty;
            this.dfsTargetShare = string.Empty;
            this.dfsDescription = string.Empty;
            this.dfsState = string.Empty;
        }

        internal DFSTarget(string DFSLink, string DFSServer, string DFSShare, string DFSDescription, string DFSState)
        {
            this.dfsLinkPath = DFSLink;
            this.dfsTargetServer = DFSServer;
            this.dfsTargetShare = DFSShare;
            this.dfsDescription = DFSDescription;
            this.dfsState = DFSState;
        }

        [OpalisOutput("DFS Link Path", Description = "Path name for DFS Share"), OpalisFilter]
        public string DFSLink
        {
            get { return dfsLinkPath; }
            set { dfsLinkPath = value; }
        }

        [OpalisOutput("DFS Server", Description = "Path name for DFS Share"), OpalisFilter]
        public string DFSServer
        {
            get { return dfsTargetServer; }
            set { dfsTargetServer = value; }
        }

        [OpalisOutput("DFS Share", Description = "Path name for DFS Share"), OpalisFilter]
        public string DFSShare
        {
            get { return dfsTargetShare; }
            set { dfsTargetShare = value; }
        }

        [OpalisOutput("DFS Description", Description = "Description of a DFS Share"), OpalisFilter]
        public string DFSDescription
        {
            get { return dfsDescription; }
            set { dfsDescription = value; }
        }


        [OpalisOutput("DFS State", Description = "State for DFS Share"), OpalisFilter]
        public string DFSState
        {
            get { return dfsState; }
            set { dfsState = value; }
        }

    }
}
