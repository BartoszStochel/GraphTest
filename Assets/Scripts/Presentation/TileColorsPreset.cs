using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Presentation/Tile colors preset")]
public class TileColorsPreset : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private Color defaultTile;
	[SerializeField] private Color visitedTile;
	[SerializeField] private Color lastVisitedTile;
	[SerializeField] private Color startTile;
	[SerializeField] private Color finishTile;
	[SerializeField] private Color finalPathTile;
#pragma warning restore 0649

	public Color DefaultTile => defaultTile;
	public Color VisitedTile => visitedTile;
	public Color LastVisitedTile => lastVisitedTile;
	public Color StartTile => startTile;
	public Color FinishTile => finishTile;
	public Color FinalPathTile => finalPathTile;
}
