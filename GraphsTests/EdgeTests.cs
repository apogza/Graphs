using System;
using Graphs.Classes;
using Graphs.Interfaces;
using Xunit;

namespace GraphsTests
{
    public class EdgeTests
    {
        [Fact]
        public void TestInitUndirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            IEdge edge = new Edge(firstNode, secondNode, 1);

            Assert.True(firstNode == edge.FirstNode);
            Assert.True(secondNode == edge.SecondNode);
            Assert.Equal(1, edge.Weight);
        }

        [Fact]
        public void TestInitDirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            IEdge edge = new Edge(firstNode, secondNode, true, 2);

            Assert.True(firstNode == edge.FirstNode);
            Assert.True(secondNode == edge.SecondNode);
            Assert.Equal(2, edge.Weight);
        }

        [Fact]
        public void TestCanGetOppositeNodeUndirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IEdge edge = new Edge(firstNode, secondNode, 2);

            Assert.True(edge.CanGetOppositeNode(firstNode));
            Assert.True(edge.CanGetOppositeNode(secondNode));
            Assert.False(edge.CanGetOppositeNode(thirdNode));
        }

        [Fact]
        public void TestCanGetOppositeNodeDirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IEdge edge = new Edge(firstNode, secondNode, true, 2);

            Assert.True(edge.CanGetOppositeNode(firstNode));
            Assert.False(edge.CanGetOppositeNode(secondNode));
            Assert.False(edge.CanGetOppositeNode(thirdNode));
        }

        [Fact]
        public void TestGetOppositeNodeUndirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IEdge edge = new Edge(firstNode, secondNode, 2);

            Assert.Equal(edge.FirstNode, edge.GetOppositeNode(secondNode));
            Assert.Equal(edge.SecondNode, edge.GetOppositeNode(firstNode));

            Assert.Throws<ArgumentException>(() => edge.GetOppositeNode(thirdNode));
        }

        [Fact]
        public void TestGetOppositeNodeDirectedEdge()
        {
            INode firstNode = new Node("1");
            INode secondNode = new Node("2");
            INode thirdNode = new Node("3");

            IEdge edge = new Edge(firstNode, secondNode, true, 2);

            Assert.Equal(edge.SecondNode, edge.GetOppositeNode(firstNode));
            Assert.Throws<InvalidOperationException>(() => edge.GetOppositeNode(secondNode));
            Assert.Throws<ArgumentException>(() => edge.GetOppositeNode(thirdNode));
        }
    }
}