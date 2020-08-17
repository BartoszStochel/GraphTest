using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Graph handler", order = 0)]
public class GraphHandler : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private BaseSearchAlgorithm searchAlgorithm;
	[SerializeField] private BaseGraphCreator graphCreator;
#pragma warning restore 0649
}
