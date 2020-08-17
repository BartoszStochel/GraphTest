using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private VisualizationPreset visualizationPreset;
	[SerializeField] private CustomGraphEvent newGraphCreatedEvent;
#pragma warning restore 0649

	private void Awake()
	{
		newGraphCreatedEvent.Event += VisualizeGraph;
	}

	private void VisualizeGraph(Graph graph)
	{

	}
}
