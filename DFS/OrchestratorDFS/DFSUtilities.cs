using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchestratorDFS.ROOT.CIMV2;

namespace OrchestratorDFS
{
    public class DFSUtilities
    {

        public List<DFSRoot> GetDFSRoots()
        {
            List<DFSRoot> dfsRoots = new List<DFSRoot>();

            foreach (DfsNode dfsNodeInformation in DfsNode.GetInstances())
            {
                if (dfsNodeInformation.Root)
                {
                    DFSRoot dfsRootData = new DFSRoot(dfsNodeInformation.Name, dfsNodeInformation.Description, dfsNodeInformation.Timeout);
                    dfsRoots.Add(dfsRootData);    
                }
            }

            return dfsRoots;
        }


        public List<DFSRoot> GetDFSNamespaces(string DFSRoot)
        {
            List<DFSRoot> dfsNamespace = new List<DFSRoot>();

            foreach (DfsNode dfsNodeInformation in DfsNode.GetInstances())
            {
                if (!dfsNodeInformation.Root)
                    if (dfsNodeInformation.Name.Contains(DFSRoot))
                    {
                        DFSRoot dfsRootData = new DFSRoot(dfsNodeInformation.Name, dfsNodeInformation.Description, dfsNodeInformation.Timeout);
                        dfsNamespace.Add(dfsRootData);    
                    }
            }

            return dfsNamespace;
        }


        public void GetDFSTargets(string DFSNamespace)
        {
            List<DFSRoot> dfsTarget = new List<DFSRoot>();

            foreach (DfsTarget dfsTargetInformation in DfsTarget.GetInstances())
            {
                
                if (dfsTargetInformation.Name.Contains(DFSNamespace))
                {
                    DFSRoot dfsRootData = new DFSRoot(dfsNodeInformation.Name, dfsNodeInformation.Description, dfsNodeInformation.Timeout);
                    dfsTarget.Add(dfsRootData);
                }
            }

            return dfsTarget;
        
        }


        public List<DFSNode> GetDFSNodes()
        {
            List<DFSNode> dfsNodes = new List<DFSNode>();

            foreach (DfsNode dfsNodeInformation in DfsNode.GetInstances())
            {
                DFSNode dfsNodeData = new DFSNode(dfsNodeInformation.Name,dfsNodeInformation.Root,dfsNodeInformation.Description,dfsNodeInformation.Timeout);

                dfsNodes.Add(dfsNodeData);
            }

            return dfsNodes;
        }


        public List<DFSTarget> GetDFSTargets()
        {
            List<DFSTarget> dfsTargets = new List<DFSTarget>();

            foreach (DfsTarget dfsTargetInformation in DfsTarget.GetInstances())
            {
                DFSTarget dfsTargetData = new DFSTarget(dfsTargetInformation.Name,dfsTargetInformation.ServerName,dfsTargetInformation.ShareName,dfsTargetInformation.Description,dfsTargetInformation.State.ToString());
                dfsTargets.Add(dfsTargetData);
            }

            return dfsTargets;
        }


        public uint DFSCreateNode(string dfsDescription, string dfsPath, string dfsServerName, string dfsServerPath)
        { 
            return (DfsNode.Create(dfsDescription, dfsPath, dfsServerName, dfsServerPath));
        }

        public void DFSSetNodeState(string state, string dfsPath, string serverPath)
        {
            
        }
    }
}
