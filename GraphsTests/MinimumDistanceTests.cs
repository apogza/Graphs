using System.Linq;
using Graphs.Algorithms;
using Graphs.Classes;
using Graphs.Interfaces;
using Xunit;

namespace GraphsTests
{
    public class MinimumDistanceTests
    {
        [Fact]
        public void TestMinimumDistanceUndirectedGraph()
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

            graph.BuildEdge("2", "3", 9);
            graph.BuildEdge("2", "4", 15);           

            graph.BuildEdge("3", "4", 10);
            graph.BuildEdge("3", "5", 7);

            graph.BuildEdge("4", "5", 9);

            MinimumDistance minDistance = new MinimumDistance(graph, "2", "5");
            WeightedTraversalResult result = minDistance.Run();
            
            Assert.Equal(11, result.TotalWeight);
            Assert.Equal("5,1,2", string.Join(",", result.Nodes.Select(node => node.ID)));
        }

        [Fact]
        public void TestMinimumDistanceDirectedGraph()
        {
            IGraph graph = new Graph(true);

            graph.AddNode(new Node("1"));
            graph.AddNode(new Node("2"));
            graph.AddNode(new Node("3"));
            graph.AddNode(new Node("4"));
            graph.AddNode(new Node("5"));
            graph.AddNode(new Node("6"));
            graph.AddNode(new Node("7"));
            graph.AddNode(new Node("8"));

            graph.BuildEdge("1", "2", 18);
            graph.BuildEdge("1", "7", 10);
            graph.BuildEdge("2", "8", 7);
            graph.BuildEdge("2", "6", 11);
            graph.BuildEdge("3", "1", 2);
            graph.BuildEdge("3", "4", 6);
            graph.BuildEdge("3", "6", 21);
            graph.BuildEdge("5", "1", 5);
            graph.BuildEdge("5", "2", 4);
            graph.BuildEdge("6", "1", 19);
            graph.BuildEdge("7", "3", 17);
            graph.BuildEdge("7", "4", 3);
            graph.BuildEdge("7", "5", 3);
            graph.BuildEdge("8", "6", 9);

            MinimumDistance minDistance = new MinimumDistance(graph, "3", "8");
            WeightedTraversalResult result = minDistance.Run();
            
            Assert.Equal(26, result.TotalWeight);
            Assert.Equal("8,2,5,7,1,3", string.Join(",", result.Nodes.Select(node => node.ID)));          
        }
    }
}