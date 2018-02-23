using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public sealed class MinimumSpanningTree : BaseTraversal
    {
        private Queue<IEdge> _edgesQueue;
        private Dictionary<string, List<INode>> _clouds;
        public MinimumSpanningTree(IGraph graph)
            : base(graph)
        {
            _edgesQueue = new Queue<IEdge>(Graph.GetSortedEdges());

            InitNodeClouds();
        }

        protected override void Traverse()
        {
            while(_edgesQueue.Count > 0)
            {
                IEdge edge = _edgesQueue.Dequeue();
                if(CheckEdgeConnectsTwoClouds(edge))
                {
                    Result.Edges.Add(edge);
                    MergeClouds(edge);
                }
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
    }
}