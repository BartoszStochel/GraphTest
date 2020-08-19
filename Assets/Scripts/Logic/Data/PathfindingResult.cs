using System.Collections.Generic;

public class PathfindingResult
{
    public List<GraphNode> VisitedNodes { get; private set; }
    public List<GraphNode> Path { get; private set; }

	public PathfindingResult(List<GraphNode> visitedNodes, List<GraphNode> path)
	{
		VisitedNodes = visitedNodes;
		Path = path;
	}
}
