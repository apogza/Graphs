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

            IGraph directedGraph = new Graph(true);
            Assert.True(false);
        }
    }
}