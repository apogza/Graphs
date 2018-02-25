using System;
using Xunit;
using Graphs.Classes;
using Graphs.Algorithms;
using Graphs.Interfaces;
using System.Linq;

namespace GraphsTests
{
    public class BreadthFirstTraversalTests
    {
        [Fact]
        public void TestBFS()
        {   
            INode node1 = new Node("1");
            INode node2 = new Node("2");
            INode node3 = new Node("3");
            INode node4 = new Node("4");
            INode node5 = new Node("5");

            IGraph graph = new Graph(false);
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);

            graph.BuildEdge(node1, node2, 0);
            graph.BuildEdge(node1, node3, 0);
            graph.BuildEdge(node2, node4, 0);
            graph.BuildEdge(node2, node5, 0);

            BreadthFirstTraversal bfs = new BreadthFirstTraversal(graph, node1);
            TraversalResult traversalResult = bfs.Run();

            string result = string.Join(",", traversalResult.Nodes.Select(node => node.ID));

            Assert.Equal("1,2,3,4,5",result);
        }
    }
}