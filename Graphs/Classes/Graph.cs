using System.Collections.Generic;
using Graphs.Interfaces;
using System.Linq;
using System;

namespace Graphs.Classes
{
    public class Graph : IGraph
    {
        protected IDictionary<string, INode> Nodes { get; set; }
        protected IDictionary<INode, IList<IEdge>> Edges { get; set; }

        public bool IsDirected { get; private set; }

        public Graph(bool isDirected = false)
        {
            IsDirected = isDirected;
            Nodes = new Dictionary<string, INode>();
            Edges = new Dictionary<INode, IList<IEdge>>();
        }

        public void BuildEdge(INode firstNode, INode secondNode, double weight)
        {
            CheckEdgeNodes(firstNode, secondNode);

            if(!HasEdge(firstNode, secondNode))
            {
                IEdge edge = new Edge(firstNode, secondNode, IsDirected, weight);
                AddEdgeForNode(firstNode, edge);

                if(!IsDirected)
                {
                    AddEdgeForNode(secondNode, edge);
                }
            }           
        }

        public void BuildEdge(string firstNodeId, string secondNodeId, double weight)
        {
            CheckEdgeNodes(firstNodeId, secondNodeId);
            
            INode firstNode = Nodes[firstNodeId];
            INode secondNode = Nodes[secondNodeId];

            BuildEdge(firstNode, secondNode, weight);
        }

        public IEnumerable<IEdge> GetEdgesForNode(INode node)
        {
            if(HasEdgesForNode(node))
            {
                return Edges[node];    
            }

            return null;
        }

        public IEnumerable<IEdge> GetEdgesForNode(string nodeID)
        {
            if(HasEdgesForNode(nodeID))
            {
                return GetEdgesForNode(Nodes[nodeID]);
            }

            return new List<IEdge>();
        }

        public bool HasEdgesForNode(INode node)
        {
            return Edges.ContainsKey(node);
        }

        public bool HasEdgesForNode(string nodeID)
        {
            return Nodes.ContainsKey(nodeID) && HasEdgesForNode(Nodes[nodeID]);
        }

        public bool HasEdge(INode firstNode, INode secondNode)
        {
            if(IsDirected)
            {
                return Edges.ContainsKey(firstNode) 
                        && Edges[firstNode].Any(edge => edge.SecondNode == secondNode);
            }

            return Edges.ContainsKey(secondNode)
                    && Edges[secondNode].Any(edge => edge.SecondNode == firstNode);
        }

        public bool HasNode(INode node)
        {
            return HasNode(node.ID);
        }

        public bool HasNode(string nodeID)
        {
            return Nodes.ContainsKey(nodeID);
        }

        public void ResetGraphElements()
        {
            foreach(IGraphElement graphElement in Nodes.Values)
            {
                graphElement.ResetGraphElement();
            }

            foreach(ICollection<IEdge> edgeCollection in Edges.Values)
            {
                foreach(IGraphElement graphElement in edgeCollection)
                {
                    graphElement.ResetGraphElement();
                }
            }
        }
        public INode GetNodeByID(string id)
        {
            return Nodes.ContainsKey(id) ? Nodes[id]: null;
        }

        public void AddNode(INode node)
        {
            if(Nodes.ContainsKey(node.ID))
            {
                throw new ArgumentException(string.Format("There is already a node with ID {0}", node.ID));
            }

            Nodes.Add(node.ID, node);
        }

        public IEnumerable<INode> GetNodes()
        {
            return Nodes.Values;
        }

        public IEnumerable<IEdge> GetEdges()
        {
            List<IEdge> edges = new List<IEdge>();
            foreach(ICollection<IEdge> edgeCollection in Edges.Values)
            {
                foreach(IEdge edge in edgeCollection)
                {
                    if(!edges.Contains(edge))
                    {
                        edges.Add(edge);
                    }
                }
            }

            return edges;
        }

        protected void AddEdgeForNode(INode node, IEdge edge)
        {
            if(Edges.ContainsKey(node))
            {
                if(Edges[node].Contains(edge))
                {
                    throw new ArgumentException(
                        string.Format("The edge {0}-{1} already exists for node {2}",  
                            edge.FirstNode.ID, edge.SecondNode.ID, node.ID));
                }

                Edges[node].Add(edge);
            }
            else
            {
                IList<IEdge> edges = new List<IEdge>();
                edges.Add(edge);

                Edges.Add(node, edges);
            }
        }
        private void CheckEdgeNodes(INode firstNode, INode secondNode)
        {
            if(!HasNode(firstNode))
            {
                throw new ArgumentException(string.Format("The node {0} is not part of the graph", firstNode.ID));
            }

            if(!HasNode(secondNode))
            {
                throw new ArgumentException(string.Format("The node {0} is not part of the graph", secondNode.ID));
            }
        }    

        private void CheckEdgeNodes(string firstNodeId, string secondNodeId)
        {
            if(!HasNode(firstNodeId))
            {
                throw new ArgumentException(string.Format("The node {0} is not part of the graph", firstNodeId));
            }

            if(!HasNode(secondNodeId))
            {
                throw new ArgumentException(string.Format("The node {0} is not part of the graph", secondNodeId));
            }
        }
    }
}