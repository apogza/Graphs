using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public abstract class BaseTraversal
    {
        protected IGraph Graph { get; set; }

        protected INode StartNode { get; set; }

        public BaseTraversal(IGraph graph)
        {
            Graph = graph;
        }
        
        public BaseTraversal(IGraph graph, INode startNode)
            : this(graph)
        {
            StartNode = startNode;
        }

        protected TraversalResult Result { get; set;}

        public TraversalResult Run()
        {
            Result = new TraversalResult();

            Graph.ResetGraphElements();
            Traverse();
            Graph.ResetGraphElements();

            return Result;
        }

        protected abstract void Traverse();
    }
}