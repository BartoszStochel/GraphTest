using System.Collections.Generic;

public class RectangularGraph : Graph
{
	public GraphNode[,] GraphAsBoard { get; private set; }

	public RectangularGraph(List<GraphNode> nodes, GraphNode[,] graphAsBoard) : base(nodes)
	{
		GraphAsBoard = graphAsBoard;
	}
}
