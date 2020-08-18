using UnityEngine;
using UnityEngine.Tilemaps;

public class GraphVisualizer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private VisualizationPreset visualizationPreset;
	[SerializeField] private CustomGraphEvent newGraphCreatedEvent;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private Tile tile;
#pragma warning restore 0649

	private Transform tilemapTransform;
	private Camera mainCamera;

	private void Awake()
	{
		newGraphCreatedEvent.Event += VisualizeGraph;
		tilemapTransform = tilemap.transform;
		mainCamera = Camera.main;
	}

	private void Update()
	{
		HandleMouseClicking();
	}

	private void HandleMouseClicking()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int mouseTilemapPosition = tilemap.WorldToCell(mouseWorldPosition);
		}
	}

	private void VisualizeGraph(Graph graph)
	{
		if (graph is RectangularGraph rectangularGraph)
		{
			VisualizeRectangularGraph(rectangularGraph.Size);
		}
		else
		{
			Debug.Log("Only rectangular graphs are supported now.");
		}
	}

	private void VisualizeRectangularGraph(Vector2Int graphSize)
	{
		tilemap.ClearAllTiles();
		tilemap.size = new Vector3Int(graphSize.x, graphSize.y, 0);
		tilemap.FloodFill(Vector3Int.zero, tile);

		for (int x = 0; x < graphSize.x; x++)
		{
			for (int y = 0; y < graphSize.y; y++)
			{
				tilemap.SetColor(new Vector3Int(x, y, 0), visualizationPreset.TileColorsPreset.DefaultTile);
			}
		}

		tilemapTransform.position = -tilemap.localBounds.center;
	}
}
