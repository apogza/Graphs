using System.Collections.Generic;
using Graphs.Interfaces;

namespace Graphs.Classes
{
    public class TraversalResult
    {
        public IList<INode> Nodes { get; protected set;}

        public IList<IEdge> Edges { get; protected set;}

        public TraversalResult()
        {
            Nodes = new List<INode>();
            Edges = new List<IEdge>();
        }
    }
}