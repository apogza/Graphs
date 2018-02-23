using System;
using System.Linq;
using Graphs.Algorithms;
using Graphs.Classes;
using Graphs.Interfaces;
using Xunit;

namespace GraphsTests
{
    public class DepthFirstTraversalTests
    {
        [Fact]
        public void TestDFS()
        {   
            INode node1 = new Node("1");
            INode node2 = new Node("2");
            INode node3 = new Node("3");
            INode node4 = new Node("4");
            INode node5 = new Node("5");

            IGraph graph = new Graph(true);
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);

            graph.BuildEdge(node1, node2, 0);
            graph.BuildEdge(node1, node3, 0);
            graph.BuildEdge(node2, node4, 0);
            graph.BuildEdge(node2, node5, 0);

            DepthFirstTraversal dfs = new DepthFirstTraversal(graph, node1);
            TraversalResult traversalResult = dfs.Run();

            string result = string.Join(",", traversalResult.Nodes.Select(node => node.ID));

            Assert.True(result == "1,2,4,5,3");
        }
    }
}