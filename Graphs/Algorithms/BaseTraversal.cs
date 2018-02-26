using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public abstract class BaseTraversal<R> where R : TraversalResult, new()
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

        protected R Result { get; set;}

        public R Run()
        {
            Result = new R();

            Graph.ResetGraphElements();
            Traverse();
            Graph.ResetGraphElements();

            return Result;
        }

        protected abstract void Traverse();
    }
}