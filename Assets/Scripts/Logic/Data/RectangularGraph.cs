using System.Collections.Generic;
using UnityEngine;

public class RectangularGraph : Graph
{
	public Vector2Int Size { get; private set; }

	public RectangularGraph(List<GraphNode> nodes, Vector2Int size) : base(nodes)
	{
		Size = size;
	}
}
