using System;

namespace Graphs.Interfaces
{
    public interface IEdge : IGraphElement, IComparable<IEdge>
    {
        INode FirstNode { get; }
        INode SecondNode { get; }

        double Weight { get; }

        bool IsDirected { get; }

        bool CanGetOppositeNode(INode node);

        INode GetOppositeNode(INode node);
    }
}