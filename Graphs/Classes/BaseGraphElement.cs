using Graphs.Interfaces;

namespace Graphs.Classes
{
    public abstract class BaseGraphElement : IGraphElement
    {
        public bool IsVisited { get; set; }

        public void ResetGraphElement()
        {
            IsVisited = false;
        }
    }
}