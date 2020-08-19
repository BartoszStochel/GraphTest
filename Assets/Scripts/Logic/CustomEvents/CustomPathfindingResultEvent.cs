using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Custom events/Custom pathfinding result event")]
public class CustomPathfindingResultEvent : ScriptableObject
{
	public event Action<PathfindingResult> Event;

	public void InvokeEvent(PathfindingResult pathfindingResult)
	{
		Event?.Invoke(pathfindingResult);
	}
}
