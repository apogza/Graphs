using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public class DepthFirstTraversal : BaseTraversal<TraversalResult>
    {
        public DepthFirstTraversal(IGraph graph, INode startNode)
            : base(graph, startNode)
        {
        }

        protected override void Traverse()
        {
            DFS(StartNode);
        }

        private void DFS(INode node)
        {
            if(node != null && !node.IsVisited)
            {
                node.IsVisited = true;
                Result.Nodes.Add(node);

                IEnumerable<IEdge> edges = Graph.GetEdgesForNode(node);
                if(edges != null)
                {
                    foreach(IEdge edge in edges)
                    {
                        if(edge.CanGetOppositeNode(node))
                        {
                            DFS(edge.GetOppositeNode(node));
                        }
                    }
                }
            }
        }
    }
}