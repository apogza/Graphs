using System;
using Graphs.Classes;
using Graphs.Interfaces;

namespace Graphs.Algorithms
{
    public class MinimumDistance : BaseTraversal<WeightedTraversalResult>
    {
        public MinimumDistance(IGraph graph)
            : base(graph)
        {
        }
        protected override void Traverse()
        {
            throw new NotImplementedException();
        }


    }
}