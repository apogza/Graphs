using System;
using System.Collections.Generic;
using Graphs.Interfaces;
using Graphs.Classes;
using Xunit;

namespace GraphsTests
{
    public class NodeTests
    {
        [Fact]
        public void TestNodeErrorInstantiation()
        {
            INode node = null;
            Assert.Throws<ArgumentException>(() =>  node = new Node(string.Empty));
        }

        [Fact]
        public void TestNodeInstantiation()
        {
            string nodeId = "1";

            INode node = new Node(nodeId);
            Assert.True(node.ID == nodeId);
        }
    }
}