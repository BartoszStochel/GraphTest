using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Presentation/Tile colors preset")]
public class TileColorsPreset : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private Color defaultTile;
	[SerializeField] private Gradient visitedTile;
	[SerializeField] private Color lastVisitedTile;
	[SerializeField] private Color startTile;
	[SerializeField] private Color finishTile;
	[SerializeField] private Gradient finalPathTile;
#pragma warning restore 0649

	public Color DefaultTile => defaultTile;
	public Gradient VisitedTile => visitedTile;
	public Color LastVisitedTile => lastVisitedTile;
	public Color StartTile => startTile;
	public Color FinishTile => finishTile;
	public Gradient FinalPathTile => finalPathTile;
}
