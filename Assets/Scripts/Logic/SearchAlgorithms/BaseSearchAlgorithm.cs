using UnityEngine;

public abstract class BaseSearchAlgorithm : ScriptableObject
{
	public abstract GraphNode GetPathToNode(DesiredPathData desiredPathData, Graph graph);
}
