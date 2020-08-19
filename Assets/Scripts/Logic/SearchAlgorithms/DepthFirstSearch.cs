using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable objects/Search algorithms/Depth-first search")]
public class DepthFirstSearch : BaseSearchAlgorithm
{
	public override PathfindingResult GetPathToNode(DesiredPathData desiredPathData)
	{
		InitializePathfinding(desiredPathData);
		return new PathfindingResult(visitedNodes, CheckNode(desiredPathData.StartNode));
	}

	private List<GraphNode> CheckNode(GraphNode nodeToInscpect)
	{
		visitedNodes.Add(nodeToInscpect);

		foreach (GraphNode node in nodeToInscpect.AdjacentNodes)
		{
			if (node == finishNode)
			{
				return new List<GraphNode> { node };
			}
			else if (!visitedNodes.Contains(node))
			{
				List<GraphNode> pathToFinish = CheckNode(node);

				if (pathToFinish.Count > 0)
				{
					pathToFinish.Insert(0, node);
					return pathToFinish;
				}
			}
		}

		return new List<GraphNode>();
	}
}
