public class DesiredPathData
{
    public GraphNode StartNode { get; private set; }
    public GraphNode FinishNode { get; private set; }

	public DesiredPathData(GraphNode startNode, GraphNode finishNode)
	{
		StartNode = startNode;
		FinishNode = finishNode;
	}
}
