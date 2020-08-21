using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Graph handler", order = 0)]
public class GraphHandler : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private BaseGraphCreator graphCreator;
	[SerializeField] private BaseSearchAlgorithm searchAlgorithm;
	[SerializeField] private CustomEvent generateNewGraphEvent;
	[SerializeField] private CustomGraphEvent newGraphCreatedEvent;
#pragma warning restore 0649

	private void OnEnable()
	{
		generateNewGraphEvent.Event += CreateNewGraph;
	}

	public PathfindingResult FindPath(DesiredPathData desiredPathData)
	{
		return searchAlgorithm.GetPathToNode(desiredPathData);
	}

	private void CreateNewGraph()
	{
		Graph newgraph = graphCreator.GetNewGraph();
		newGraphCreatedEvent.InvokeEvent(newgraph);
	}
}
