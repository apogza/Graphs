using System.Collections.Generic;
using Graphs.Interfaces;
using System.Linq;
using System;

namespace Graphs.Classes
{
    public class Graph : IGraph
    {
        protected IList<INode> Nodes { get; set; }
        protected IDictionary<INode, IList<IEdge>> Edges { get; set; }

        public bool IsDirected { get; private set; }

        public Graph(bool isDirected = false)
        {
            IsDirected = isDirected;
            Nodes = new List<INode>();
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

        public IEnumerable<IEdge> GetEdgesForNode(INode node)
        {
            if(!Edges.ContainsKey(node))
            {
                return null;
            }

            return Edges[node];
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
            return Nodes.Contains(node);
        }

        public void ResetGraphElements()
        {
            foreach(IGraphElement graphElement in Nodes)
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
            return Nodes.FirstOrDefault(node => node.ID == id);
        }

        public void AddNode(INode node)
        {
            if(Nodes.Any(n => n.ID == node.ID))
            {
                throw new ArgumentException(string.Format("There is already a node with ID {0}", node.ID));
            }

            Nodes.Add(node);
        }

        public IEnumerable<INode> GetNodes()
        {
            return Nodes;
        }

        public IEnumerable<IEdge> GetSortedEdges()
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

            edges.Sort();

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
    }
}