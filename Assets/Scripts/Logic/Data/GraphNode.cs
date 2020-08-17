using System.Collections.Generic;

public class GraphNode
{
	public List<GraphNode> AdjacentNodes { get; private set; }

	public void SetAdjacentNodes(List<GraphNode> adjacentNodes)
	{
		AdjacentNodes = adjacentNodes;
	}
}
