using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public sealed class MinimumSpanningTree : BaseTraversal<WeightedTraversalResult>
    {
        private Queue<IEdge> _edgesQueue;
        private Dictionary<string, List<INode>> _clouds;

        private HashSet<INode> _nodes;
        
        public MinimumSpanningTree(IGraph graph)
            : base(graph)
        {
        }

        private void InitMST()
        {
            _edgesQueue = new Queue<IEdge>(Graph.GetSortedEdges());

            _nodes = new HashSet<INode>();

            InitNodeClouds();
        }

        private void InitNodeClouds()
        {
            _clouds = new Dictionary<string, List<INode>>();

            foreach(INode node in Graph.GetNodes())
            {
                List<INode> nodeList = new List<INode>();
                nodeList.Add(node);

                _clouds.Add(node.ID, nodeList);
            }
        }

        private void AddNewNodes(IEdge edge)
        {
            AddNewNode(edge.FirstNode);
            AddNewNode(edge.SecondNode);
        }

        private void AddNewNode(INode node)
        {
            if(!_nodes.Contains(node))
            {
                _nodes.Add(node);
            }
        }

        private void AddNodesToResult()
        {
            foreach(INode node in _nodes)
            {
                Result.Nodes.Add(node);
            }
        }

        private bool CheckEdgeConnectsTwoClouds(IEdge edge)
        {
            List<INode> firstCloud = _clouds[edge.FirstNode.ID];
            List<INode> secondCloud = _clouds[edge.SecondNode.ID];
            
            return firstCloud != secondCloud;
        }

        private void MergeClouds(IEdge edge)
        {
            List<INode> firstCloud = _clouds[edge.FirstNode.ID];
            List<INode> secondCloud = _clouds[edge.SecondNode.ID];

            firstCloud.AddRange(secondCloud);

            _clouds[edge.FirstNode.ID] = firstCloud;
            _clouds[edge.SecondNode.ID] = firstCloud;
        }

        protected override void Traverse()
        {
            InitMST();
            
            while(_edgesQueue.Count > 0)
            {
                IEdge edge = _edgesQueue.Dequeue();
                if(CheckEdgeConnectsTwoClouds(edge))
                {
                    Result.Edges.Add(edge);
                    Result.TotalWeight += edge.Weight;
                    
                    MergeClouds(edge);
                    AddNewNodes(edge);
                }
            }

            AddNodesToResult();
        }
    }
}