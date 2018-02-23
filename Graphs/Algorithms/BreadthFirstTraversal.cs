using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public class BreadthFirstTraversal : BaseTraversal
    {
        public BreadthFirstTraversal(IGraph graph, INode startNode)
            :base(graph, startNode)
        {
        }

        protected override void Traverse()
        {
            Queue<INode> nodeQueue = new Queue<INode>();
            nodeQueue.Enqueue(StartNode);

            while(nodeQueue.Count > 0)
            {
                INode currentNode = nodeQueue.Dequeue();

                if(!currentNode.IsVisited)
                {
                    currentNode.IsVisited = true;
                    Result.Nodes.Add(currentNode);

                    foreach(IEdge edge in Graph.GetEdgesForNode(currentNode))
                    {
                        if(edge.CanGetOppositeNode(currentNode))
                        {
                            nodeQueue.Enqueue(edge.GetOppositeNode(currentNode));
                        }
                    }
                }
            }
        }
    }
}