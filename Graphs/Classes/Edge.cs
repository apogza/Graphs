using System;
using Graphs.Interfaces;

namespace Graphs.Classes
{
    public class Edge : BaseGraphElement, IEdge
    {
        
        public Edge(INode firstNode, INode secondNode, double weight)
        {
            FirstNode = firstNode;
            SecondNode = secondNode;
            Weight = weight;

            IsDirected = false;
        }

        public Edge(INode firstNode, INode secondNode, bool isDirected, double weight)
            :this(firstNode, secondNode, weight)
        {
            IsDirected = isDirected;
        }

        public bool IsDirected { get; protected set; }
        public INode FirstNode { get; protected set; }
        public INode SecondNode { get; protected set; }
        public double Weight { get; protected set; }

        public bool CanGetOppositeNode(INode node)
        {
            return (IsDirected && node == FirstNode) || !IsDirected;
        }

        public INode GetOppositeNode(INode node)
        {
            if(FirstNode == node)
            {
                return SecondNode;
            }
            else if(SecondNode == node)
            {
                 if(IsDirected)
                 {
                     throw new InvalidOperationException("Cannot go against the direction of the edge");
                 }

                 return FirstNode;
            }
            else
            {
                throw new ArgumentException("The node is not part of the edge");
            }
        }
    }
}