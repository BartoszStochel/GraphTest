using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Scriptable objects/Graph creators/Rectangular board creator")]
public class RectangularBoardCreator : BaseGraphCreator
{
#pragma warning disable 0649
	[SerializeField] private Vector2Int size;
#pragma warning restore 0649

	public override Graph GetNewGraph()
	{
		GraphNode[,] board = new GraphNode[size.x, size.y];
		List<GraphNode> graphNodes = new List<GraphNode>();

		for (int x = 0; x < size.x; x++)
		{
			for (int y = 0; y < size.y; y++)
			{
				board[x, y] = new GraphNode();
				graphNodes.Add(board[x, y]);
			}
		}

		for (int x = 0; x < size.x; x++)
		{
			for (int y = 0; y < size.y; y++)
			{
				board[x, y].SetAdjacentNodes(GetAdjacentNodesOfNode(x, y, board));
			}
		}

		return new RectangularGraph(graphNodes, board);
	}

	private List<GraphNode> GetAdjacentNodesOfNode(int x, int y, GraphNode[,] board)
	{
		List<GraphNode> adjacentNodes = new List<GraphNode>();

		// Upper tile.
		if (y < size.y - 1)
		{
			adjacentNodes.Add(board[x, y + 1]);
		}

		// Right tile.
		if (x < size.x - 1)
		{
			adjacentNodes.Add(board[x + 1, y]);
		}

		// Lower tile.
		if (y > 0)
		{
			adjacentNodes.Add(board[x, y - 1]);
		}

		// Left tile.
		if (x > 0)
		{
			adjacentNodes.Add(board[x - 1, y]);
		}

		return adjacentNodes;
	}
}
