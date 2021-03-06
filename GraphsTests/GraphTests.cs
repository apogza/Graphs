using System;
using System.Collections.Generic;
using System.Linq;
using Graphs.Classes;
using Graphs.Interfaces;
using Xunit;

namespace GraphsTests
{
    public class GraphTests
    {
        [Fact]
        public void TestInitGraph()   
        {
            IGraph undirectedGraph = new Graph();
            IGraph directedGraph = new Graph(true);

            Assert.False(undirectedGraph.IsDirected);
            Assert.True(directedGraph.IsDirected);
        }

        [Fact]
        public void TestAddNode()
        {
            INode node = new Node("1");

            IGraph directedGraph = new Graph();
            directedGraph.AddNode(node);
            INode nodeFromGraph = directedGraph.GetNodes().FirstOrDefault(n => n.ID == node.ID);

            Assert.Equal(node, nodeFromGraph);
        }

        [Fact]
        public void TestAddNodeWithExistingID()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("1");

            IGraph directedGraph = new Graph();
            directedGraph.AddNode(firstNode);
            Assert.Throws<ArgumentException>(() => directedGraph.AddNode(secondNode));
        }

        [Fact]
        public void TestBuildEdgeUndirectedGraph()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");

            IGraph directedGraph = new Graph();
            directedGraph.AddNode(firstNode);
            directedGraph.AddNode(secondNode);
            directedGraph.BuildEdge(firstNode, secondNode, 1);

            IEnumerable<IEdge> edgesFirstNode = directedGraph.GetEdgesForNode(firstNode);
            IEnumerable<IEdge> edgesSecondNode = directedGraph.GetEdgesForNode(secondNode);

            Assert.Single(edgesFirstNode);
            Assert.Single(edgesSecondNode);

            Assert.Equal(edgesFirstNode.FirstOrDefault().FirstNode, firstNode);
            Assert.Equal(edgesFirstNode.FirstOrDefault().SecondNode, secondNode);

            Assert.Equal(edgesSecondNode.FirstOrDefault().FirstNode, firstNode);
            Assert.Equal(edgesSecondNode.FirstOrDefault().SecondNode, secondNode);
        }

        [Fact]
        public void TestBuildEdgeDirectedGraph()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");

            IGraph directedGraph = new Graph(true);
            directedGraph.AddNode(firstNode);
            directedGraph.AddNode(secondNode);
            directedGraph.BuildEdge(firstNode, secondNode, 1);

            IEnumerable<IEdge> edgesFirstNode = directedGraph.GetEdgesForNode(firstNode);
            IEnumerable<IEdge> edgesSecondNode = directedGraph.GetEdgesForNode(secondNode);

            Assert.Single(edgesFirstNode);
            Assert.Null(edgesSecondNode);

            Assert.Equal(edgesFirstNode.FirstOrDefault().FirstNode, firstNode);
            Assert.Equal(edgesFirstNode.FirstOrDefault().SecondNode, secondNode);
        }

        [Fact]
        public void TestBuildEdgeById()
        {
            IGraph graph = new Graph();
            graph.AddNode(new Node("1"));
            graph.AddNode(new Node("2"));

            graph.BuildEdge("1", "2", 1);

            Assert.Single(graph.GetEdgesForNode("1"));
            Assert.Single(graph.GetEdgesForNode("2"));
            Assert.Empty(graph.GetEdgesForNode("3"));
            
            Assert.Throws<ArgumentException>(() => graph.BuildEdge("1", "3", 1));
        }


        [Fact]
        public void TestBuildInvalidEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");

            IGraph directedGraph = new Graph(true);
            Assert.Throws<ArgumentException>(() => directedGraph.BuildEdge(firstNode, secondNode, 1));

            directedGraph.AddNode(firstNode);
            Assert.Throws<ArgumentException>(() => directedGraph.BuildEdge(firstNode, secondNode, 1));
        }

        // TODO - Finish the test below
        [Fact]
        public void TestGetOrderedEdges()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IGraph graph = new Graph();
            graph.AddNode(firstNode);
            graph.AddNode(secondNode);
            graph.AddNode(thirdNode);

            graph.BuildEdge(secondNode, thirdNode, 5);
            graph.BuildEdge(firstNode, secondNode, 2);
            graph.BuildEdge(firstNode, thirdNode, 1);

            IEdge[] sortedEdges = graph.GetEdges().ToArray();

            Assert.Equal(5, sortedEdges[0].Weight);
            Assert.Equal(2, sortedEdges[1].Weight);
            Assert.Equal(1, sortedEdges[2].Weight);
        }

        [Fact]
        public void TestHasNode()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IGraph graph = new Graph();
            graph.AddNode(firstNode);
            graph.AddNode(secondNode);

            Assert.True(graph.HasNode(firstNode));
            Assert.True(graph.HasNode(secondNode));
            Assert.False(graph.HasNode(thirdNode));
        }

        [Fact]
        public void TestResetGraphElements()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IGraph graph = new Graph();
            graph.AddNode(firstNode);
            graph.AddNode(secondNode);
            graph.AddNode(thirdNode);

            graph.BuildEdge(secondNode, thirdNode, 5);
            graph.BuildEdge(firstNode, secondNode, 2);
            graph.BuildEdge(firstNode, thirdNode, 1);

            foreach(INode node in graph.GetNodes())
            {
                node.IsVisited = true;
            }

            foreach(IEdge edge in graph.GetEdges())
            {
                edge.IsVisited = true;
            }

            graph.ResetGraphElements();

            foreach(INode node in graph.GetNodes())
            {
                Assert.False(node.IsVisited);
            }

            foreach(IEdge edge in graph.GetEdges())
            {
                Assert.False(edge.IsVisited);
            }
            
            
        }
    }
}