using UnityEngine;
using UnityEngine.Tilemaps;

public class GraphVisualizer : MonoBehaviour
{
#pragma warning disable 0649
	[SerializeField] private VisualizationPreset visualizationPreset;
	[SerializeField] private CustomGraphEvent newGraphCreatedEvent;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private Tile tile;

	[SerializeField] private GraphHandler graphHandler;
#pragma warning restore 0649

	public GraphNode FirstNode { get; private set; }
	public GraphNode SecondNode { get; private set; }

	private Transform tilemapTransform;
	private Camera mainCamera;

	private GraphNode[,] graphAsBoard;
	private Vector2Int graphSize;
	private ICommand findPathCommand;

	private void Awake()
	{
		newGraphCreatedEvent.Event += VisualizeGraph;
		tilemapTransform = tilemap.transform;
		mainCamera = Camera.main;

		SetFindPathCommand(new FindPathCommand(GetDesiredPathData, graphHandler));
	}

	private void Update()
	{
		HandleMouseClicking();
	}

	public void SetFindPathCommand(ICommand findPathCommand)
	{
		this.findPathCommand = findPathCommand;
	}

	public DesiredPathData GetDesiredPathData()
	{
		return new DesiredPathData(FirstNode, SecondNode);
	}

	private void HandleMouseClicking()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int mouseCellPosition = tilemap.WorldToCell(mouseWorldPosition);

			if (graphAsBoard == null || !IsCellPositionWithinGraph(mouseCellPosition))
			{
				return;
			}

			if (FirstNode == null)
			{
				FirstNode = graphAsBoard[mouseCellPosition.x, mouseCellPosition.y];
				tilemap.SetColor(mouseCellPosition, visualizationPreset.TileColorsPreset.StartTile);
			}
			else
			{
				SecondNode = graphAsBoard[mouseCellPosition.x, mouseCellPosition.y];

				if (FirstNode == SecondNode)
				{
					tilemap.SetColor(mouseCellPosition, visualizationPreset.TileColorsPreset.DefaultTile);
				}
				else
				{
					tilemap.SetColor(mouseCellPosition, visualizationPreset.TileColorsPreset.FinishTile);
					findPathCommand.Execute();
				}

				FirstNode = null;
				SecondNode = null;
			}
		}
	}
	
	private bool IsCellPositionWithinGraph(Vector3Int cellPosition)
	{
		return
			cellPosition.x >= 0 &&
			cellPosition.y >= 0 &&
			cellPosition.x < graphSize.x &&
			cellPosition.y < graphSize.y;
	}

	private void VisualizeGraph(Graph graph)
	{
		if (graph is RectangularGraph rectangularGraph)
		{
			FirstNode = null;
			SecondNode = null;
			graphAsBoard = rectangularGraph.GraphAsBoard;
			graphSize = new Vector2Int(graphAsBoard.GetLength(0), graphAsBoard.GetLength(1));

			VisualizeRectangularGraph(graphSize);
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
