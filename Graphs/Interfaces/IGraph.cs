using System.Collections.Generic;

namespace Graphs.Interfaces
{
    public interface IGraph
    {
        bool IsDirected { get; }
        IEnumerable<INode> GetNodes();       
        IEnumerable<IEdge> GetEdges();
        IEnumerable<IEdge> GetEdgesForNode(INode node);
        IEnumerable<IEdge> GetEdgesForNode(string nodeID);

        void AddNode(INode node);

        void BuildEdge(INode firstNode, INode secondNode, double weight);

        void BuildEdge(string firstNodeId, string secondNodeId, double weight);

        bool HasEdge(INode firstNode, INode secondNode);

        bool HasNode(INode node);

        bool HasNode(string nodeID);

        INode GetNodeByID(string nodeID);

        void ResetGraphElements();
    }
}