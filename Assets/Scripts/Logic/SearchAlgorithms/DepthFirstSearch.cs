using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Search algorithms/Depth-first search")]
public class DepthFirstSearch : BaseSearchAlgorithm
{
	public override GraphNode GetPathToNode(DesiredPathData desiredPathData, Graph graph)
	{
		Debug.Log("depth");

		return null;
	}
}
