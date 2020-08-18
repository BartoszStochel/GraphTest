using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Search algorithms/Breadth-first search")]
public class BreadthFirstSearch : BaseSearchAlgorithm
{
	public override GraphNode GetPathToNode(DesiredPathData desiredPathData, Graph graph)
	{
		Debug.Log("breadth");

		return null;
	}
}
