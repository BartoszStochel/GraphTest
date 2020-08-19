using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Search algorithms/Breadth-first search")]
public class BreadthFirstSearch : BaseSearchAlgorithm
{
	public override PathfindingResult GetPathToNode(DesiredPathData desiredPathData)
	{
		Debug.Log("breadth");

		return null;
	}
}
