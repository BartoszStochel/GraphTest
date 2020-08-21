using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable objects/Search algorithms/Breadth-first search")]
public class BreadthFirstSearch : BaseSearchAlgorithm
{
	public override PathfindingResult GetPathToNode(DesiredPathData desiredPathData)
	{
		InitializePathfinding(desiredPathData);

		// Consider using different data structure, LinkedList<> maybe?
		List<GraphNode> pathToFinish = new List<GraphNode>();
		LinkedGraphNode linkedFinishNode = GetFinishNode(desiredPathData.StartNode);

		while (linkedFinishNode != null && linkedFinishNode.PreviousNode != null)
		{
			pathToFinish.Insert(0, linkedFinishNode.Node);
			linkedFinishNode = linkedFinishNode.PreviousNode;
		}

		return new PathfindingResult(visitedNodes, pathToFinish);
	}

	private LinkedGraphNode GetFinishNode(GraphNode startNode)
	{
		List<LinkedGraphNode> nodesToCheck = new List<LinkedGraphNode> { new LinkedGraphNode(startNode, null) };
		visitedNodes.Add(startNode);

		for (int i = 0; i < nodesToCheck.Count; i++)
		{
			List<LinkedGraphNode> adjacentLinkedNodes = GetAdjacentLinkedNodes(nodesToCheck[i]);

			for (int j = 0; j < adjacentLinkedNodes.Count; j++)
			{
				if (adjacentLinkedNodes[j].Node == finishNode)
				{
					return adjacentLinkedNodes[j];
				}

				if (!visitedNodes.Contains(adjacentLinkedNodes[j].Node))
				{
					visitedNodes.Add(adjacentLinkedNodes[j].Node);
					nodesToCheck.Add(adjacentLinkedNodes[j]);
				}
			}
		}

		return null;
	}

	private List<LinkedGraphNode> GetAdjacentLinkedNodes(LinkedGraphNode linkedNode)
	{
		List<LinkedGraphNode> adjacentLinkedNodes = new List<LinkedGraphNode>();

		foreach (GraphNode adjacentNode in linkedNode.Node.AdjacentNodes)
		{
			adjacentLinkedNodes.Add(new LinkedGraphNode(adjacentNode, linkedNode));
		}

		return adjacentLinkedNodes;
	}

	private class LinkedGraphNode
	{
		public GraphNode Node { get; private set; }
		public LinkedGraphNode PreviousNode { get; private set; }

		public LinkedGraphNode(GraphNode node, LinkedGraphNode previousNode)
		{
			Node = node;
			PreviousNode = previousNode;
		}
	}
}
