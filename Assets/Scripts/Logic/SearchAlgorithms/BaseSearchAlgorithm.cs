using UnityEngine;
using System.Collections.Generic;

public abstract class BaseSearchAlgorithm : ScriptableObject
{
	protected GraphNode finishNode;
	protected List<GraphNode> visitedNodes;

	public abstract PathfindingResult GetPathToNode(DesiredPathData desiredPathData);

	protected void InitializePathfinding(DesiredPathData desiredPathData)
	{
		finishNode = desiredPathData.FinishNode;
		visitedNodes = new List<GraphNode>();
	}
}
