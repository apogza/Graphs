using System;
using System.Linq;
using Graphs.Algorithms;
using Graphs.Classes;
using Graphs.Interfaces;
using Xunit;

namespace GraphsTests
{
    public class MinimumSpanningTreeTests
    {
        [Fact]
        public void TestMinimumSpanningTreeUndirectedGraph()
        {
            IGraph graph = new Graph();

            graph.AddNode(new Node("1"));
            graph.AddNode(new Node("2"));
            graph.AddNode(new Node("3"));
            graph.AddNode(new Node("4"));
            graph.AddNode(new Node("5"));

            graph.BuildEdge("1", "2", 8);
            graph.BuildEdge("1", "3", 4);
            graph.BuildEdge("1", "4", 2);
            graph.BuildEdge("1", "5", 3);

            graph.BuildEdge("2", "4", 15);
            graph.BuildEdge("2", "3", 9);

            graph.BuildEdge("3", "4", 10);
            graph.BuildEdge("3", "5", 7);

            graph.BuildEdge("4", "5", 9);

            MinimumSpanningTree mst = new MinimumSpanningTree(graph);
            WeightedTraversalResult result = mst.Run();

            Assert.Equal("1,4,5,3,2", string.Join(",", result.Nodes.Select(node => node.ID)));
            Assert.Equal(21, result.TotalWeight);
        }
    }
}
