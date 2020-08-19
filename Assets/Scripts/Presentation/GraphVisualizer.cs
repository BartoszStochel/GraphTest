using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class GraphVisualizer : MonoBehaviour
{
	private const float MINIMAL_VISUALIZATION_SPEED = 0.1f;

#pragma warning disable 0649
	[SerializeField] private VisualizationPreset visualizationPreset;
	[SerializeField] private CustomEvent finalizeVisualizationEvent;
	[SerializeField] private CustomEvent stopAndClearVisualizationEvent;
	[SerializeField] private CustomGraphEvent newGraphCreatedEvent;
	[SerializeField] private CustomPathfindingResultEvent pathfindingFinishedEvent;
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
	private bool isCurrentlyVisualizingPathfinding;
	private bool visualizeImmediately;

	private void Awake()
	{
		finalizeVisualizationEvent.Event += FinalizeVisualization;
		stopAndClearVisualizationEvent.Event += StopAndClearVisualization;
		newGraphCreatedEvent.Event += VisualizeGraph;
		pathfindingFinishedEvent.Event += VisualizePathfinding;

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

	private void FinalizeVisualization()
	{
		visualizeImmediately = true;
	}

	private void StopAndClearVisualization()
	{
		StopAllCoroutines();
		isCurrentlyVisualizingPathfinding = false;
		ResetTileColorsToDefault();
		ResetSelectedNodes();
	}

	private void HandleMouseClicking()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector3Int mouseCellPosition = tilemap.WorldToCell(mouseWorldPosition);

			if (graphAsBoard == null || !IsCellPositionWithinGraph(mouseCellPosition) || isCurrentlyVisualizingPathfinding)
			{
				return;
			}

			if (FirstNode == null)
			{
				SelectFirstNode(mouseCellPosition);
			}
			else
			{
				SelectSecondNode(mouseCellPosition);
			}
		}
	}

	private void SelectFirstNode(Vector3Int mouseCellPosition)
	{
		FirstNode = graphAsBoard[mouseCellPosition.x, mouseCellPosition.y];
		ResetTileColorsToDefault();
		tilemap.SetColor(mouseCellPosition, visualizationPreset.TileColorsPreset.StartTile);
	}

	private void SelectSecondNode(Vector3Int mouseCellPosition)
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

		ResetSelectedNodes();
	}
	
	private bool IsCellPositionWithinGraph(Vector3Int cellPosition)
	{
		return
			cellPosition.x >= 0 &&
			cellPosition.y >= 0 &&
			cellPosition.x < graphSize.x &&
			cellPosition.y < graphSize.y;
	}

	private void ResetSelectedNodes()
	{
		FirstNode = null;
		SecondNode = null;
	}

	private void VisualizeGraph(Graph graph)
	{
		if (isCurrentlyVisualizingPathfinding)
		{
			return;
		}

		ResetSelectedNodes();

		if (graph is RectangularGraph rectangularGraph)
		{
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

		ResetTileColorsToDefault();

		tilemapTransform.position = -tilemap.localBounds.center;
	}

	private void ResetTileColorsToDefault()
	{
		for (int x = 0; x < graphSize.x; x++)
		{
			for (int y = 0; y < graphSize.y; y++)
			{
				tilemap.SetColor(new Vector3Int(x, y, 0), visualizationPreset.TileColorsPreset.DefaultTile);
			}
		}
	}

	private void VisualizePathfinding(PathfindingResult result)
	{
		StopAllCoroutines();

		visualizeImmediately = false;
		StartCoroutine(VisualizePathfindingOverTime(result));
	}

	private IEnumerator VisualizePathfindingOverTime(PathfindingResult result)
	{
		isCurrentlyVisualizingPathfinding = true;

		yield return StartCoroutine(VisualizeSearching(result.VisitedNodes));
		VisualizePath(result.Path);

		isCurrentlyVisualizingPathfinding = false;
	}

	private IEnumerator VisualizeSearching(List<GraphNode> visitedNodes)
	{
		float gradientEvaluationValue = 0f;

		for (int i = 1; i < visitedNodes.Count; i++)
		{
			if (!visualizeImmediately)
			{
				float visualizationSpeed = Mathf.Max(visualizationPreset.TilesPerSecond, MINIMAL_VISUALIZATION_SPEED);
				yield return new WaitForSeconds(1f / visualizationSpeed);
			}

			if (i > 1)
			{
				tilemap.SetColor(
					GetCellPosition(visitedNodes[i - 1]),
					visualizationPreset.TileColorsPreset.VisitedTile.Evaluate(gradientEvaluationValue));

				gradientEvaluationValue += 1f / (visitedNodes.Count - 3f);
			}

			tilemap.SetColor(GetCellPosition(visitedNodes[i]), visualizationPreset.TileColorsPreset.LastVisitedTile);
		}
	}

	private void VisualizePath(List<GraphNode> path)
	{
		float gradientEvaluationValue = 0f;

		for (int i = 0; i < path.Count - 1; i++)
		{
			tilemap.SetColor(
				GetCellPosition(path[i]),
				visualizationPreset.TileColorsPreset.FinalPathTile.Evaluate(gradientEvaluationValue));

			gradientEvaluationValue += 1f / (path.Count - 2f);
		}
	}

	private Vector3Int GetCellPosition(GraphNode graphNode)
	{
		for (int x = 0; x < graphSize.x; x++)
		{
			for (int y = 0; y < graphSize.y; y++)
			{
				if (graphAsBoard[x, y] == graphNode)
				{
					return new Vector3Int(x, y, 0);
				}
			}
		}

		return new Vector3Int(-1, -1, -1);
	}
}
