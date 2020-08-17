using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Custom events/Custom event")]
public class CustomEvent : ScriptableObject
{
	public event Action Event;

	public void InvokeEvent()
	{
		Event?.Invoke();
	}
}
