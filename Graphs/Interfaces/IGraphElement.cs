namespace Graphs.Interfaces
{
    public interface IGraphElement 
    {
        bool IsVisited { get; set; }

        void ResetGraphElement();
    }
}