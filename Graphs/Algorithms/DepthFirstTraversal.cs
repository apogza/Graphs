using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;
using System.Linq;

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
            Stack<INode> nodeStack = new Stack<INode>();
            nodeStack.Push(StartNode);

            while(nodeStack.Count > 0)
            {
                INode currentNode = nodeStack.Pop();

                if(!currentNode.IsVisited)
                {
                    currentNode.IsVisited = true;
                    Result.Nodes.Add(currentNode);

                    if(Graph.HasEdgesForNode(currentNode))
                    {
                        // do a reverse on the collection of edges to preserve the natural order on the stack
                        foreach(IEdge edge in Graph.GetEdgesForNode(currentNode).Reverse())
                        {
                            if(!edge.IsVisited && edge.CanGetOppositeNode(currentNode))
                            {   
                                edge.IsVisited = true;
                                nodeStack.Push(edge.GetOppositeNode(currentNode));
                            }
                        }
                    }
                }
            }
        }
    }
}