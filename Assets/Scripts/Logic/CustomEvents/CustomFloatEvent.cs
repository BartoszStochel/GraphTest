using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Custom events/Custom float event")]
public class CustomFloatEvent : ScriptableObject
{
	public event Action<float> Event;

	public void InvokeEvent(float value)
	{
		Event?.Invoke(value);
	}
}
