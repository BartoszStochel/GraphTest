using UnityEngine;

public abstract class BaseSearchAlgorithm : ScriptableObject
{
	public abstract PathfindingResult GetPathToNode(DesiredPathData desiredPathData);
}
