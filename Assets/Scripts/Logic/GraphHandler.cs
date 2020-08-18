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

	private Graph graph;

	private void OnEnable()
	{
		generateNewGraphEvent.Event += CreateNewGraph;
	}

	public void FindPath(DesiredPathData desiredPathData)
	{
		searchAlgorithm.GetPathToNode(desiredPathData, graph);
		// invoke event and pass search process data AND final path
	}

	private void CreateNewGraph()
	{
		graph = graphCreator.GetNewGraph();
		newGraphCreatedEvent.InvokeEvent(graph);
	}
}
