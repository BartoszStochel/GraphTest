using UnityEngine;

public abstract class BaseSearchAlgorithm : ScriptableObject
{
	public abstract GraphNode GetPathToNode(GraphNode startingNode, GraphNode endingNode, Graph graph);
}
