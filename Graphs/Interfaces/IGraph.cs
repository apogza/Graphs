using System.Collections.Generic;

namespace Graphs.Interfaces
{
    public interface IGraph
    {
        bool IsDirected { get; }
        IEnumerable<INode> GetNodes();       
        IEnumerable<IEdge> GetSortedEdges();
        IEnumerable<IEdge> GetEdgesForNode(INode node);

        void AddNode(INode node);

        void BuildEdge(INode firstNode, INode secondNode, double weight);

        bool HasEdge(INode firstNode, INode secondNode);

        bool HasNode(INode node);

        void ResetGraphElements();
    }
}