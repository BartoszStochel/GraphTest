using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Custom events/Custom graph event")]
public class CustomGraphEvent : ScriptableObject
{
	public event Action<Graph> Event;

	public void InvokeEvent(Graph graph)
	{
		Event?.Invoke(graph);
	}
}
