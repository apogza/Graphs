using Graphs.Interfaces;
using System;

namespace Graphs.Classes
{
    public class Node : BaseGraphElement, INode
    {
        public Node(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("The id of a node cannot be null or empty");
            }

            ID = id;
            IsVisited = false;
        }

        public string ID { get; private set; }

    }
}