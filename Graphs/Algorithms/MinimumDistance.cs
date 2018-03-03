using System;
using System.Collections.Generic;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public class MinimumDistance : BaseTraversal<WeightedTraversalResult>
    {
        private SortedList<double, List<IEdge>> _cloudEdges;

        private Dictionary<INode, NodeEdgePair> _nodePath;

        private INode EndNode { get; set; }        

        public MinimumDistance(IGraph graph, INode startNode, INode endNode)
            : base(graph, startNode)
        {
            EndNode = endNode;
        }

        public MinimumDistance(IGraph graph, string startNodeID, string endNodeID)
            :base(graph)
        {
            if(!graph.HasNode(startNodeID) || !graph.HasNode(endNodeID))
            {
                throw new ArgumentException("Invalid start or end node");
            }

            StartNode = graph.GetNodeByID(startNodeID);
            EndNode = graph.GetNodeByID(endNodeID);
        }

        private void InitMinimumDistance()
        {
            _cloudEdges = new SortedList<double, List<IEdge>>();
            _nodePath = new Dictionary<INode, NodeEdgePair>();
        }

        protected override void Traverse()
        {
            InitMinimumDistance();

            INode currentNode = StartNode;

            while(currentNode != EndNode)
            {
                currentNode.IsVisited = true;
                DiscoverCloudEdges(currentNode);

                NodeEdgePair nextNode = GetNextNode();
                CheckNextNode(nextNode);
                
                _nodePath.Add(nextNode.Node, 
                    new NodeEdgePair{ Node = nextNode.Edge.GetOppositeNode(nextNode.Node), Edge = nextNode.Edge});
                
                RemoveCloudEdge(nextNode.Edge);
                currentNode = nextNode.Node;
            }

            TraceBackMinDistancePath();
        }

        private void DiscoverCloudEdges(INode node)
        {
            IEnumerable<IEdge> edges = Graph.GetEdgesForNode(node);

            INode nextNode = null;

            foreach(IEdge edge in edges)
            {
                if(edge.CanGetOppositeNode(node))
                {
                    nextNode = edge.GetOppositeNode(node);
                    if(!nextNode.IsVisited)
                    {
                        AddCloudEdge(edge);
                    }
                }
            }
        }

        private void AddCloudEdge(IEdge edge)
        {
            if(_cloudEdges.ContainsKey(edge.Weight))
            {
                _cloudEdges[edge.Weight].Add(edge);
            }
            else
            {
                List<IEdge> edgeList = new List<IEdge>();
                edgeList.Add(edge);

                _cloudEdges.Add(edge.Weight, edgeList);
            }
        }

        private void CheckNextNode(NodeEdgePair nextNode)
        {
            if(nextNode == null)
            {
                throw new Exception(
                    string.Format("No path found from node {0} to node {1}", StartNode.ID, EndNode.ID));
            }
        }

        private void RemoveCloudEdge(IEdge edge)
        {
            if(_cloudEdges.ContainsKey(edge.Weight))
            {
                List<IEdge> edges = _cloudEdges[edge.Weight];

                if(edges.Count <= 1)
                {
                    _cloudEdges.Remove(edge.Weight);
                }
                else
                {
                    edges.Remove(edge);
                }
            }
        }



        private void TraceBackMinDistancePath()
        {
            INode currentNode = EndNode;

            while(currentNode != StartNode)
            {
                NodeEdgePair previousNode = _nodePath[currentNode];
                Result.Nodes.Add(currentNode);
                Result.Edges.Add(previousNode.Edge);
                Result.TotalWeight += previousNode.Edge.Weight;

                currentNode = previousNode.Node;
            }

            Result.Nodes.Add(StartNode);
        }

        private NodeEdgePair GetNextNode()
        {   
            INode nextNode = null;
            IEdge nextEdge = null;

            foreach(List<IEdge> edgeList in _cloudEdges.Values)
            {
                foreach(IEdge edge in edgeList)
                {
                    if(!edge.IsVisited)
                    {
                        nextEdge = edge;
                        nextNode = !edge.FirstNode.IsVisited ? edge.FirstNode : edge.SecondNode;
                        return new NodeEdgePair { Node = nextNode, Edge = nextEdge};
                    }
                }
            }

            return null;
        }
    }
}