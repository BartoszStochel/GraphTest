using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Presentation/Visualization preset")]
public class VisualizationPreset : ScriptableObject
{
#pragma warning disable 0649
	[SerializeField] private float tilesPerSecond;
	[SerializeField] private TileColorsPreset tileColorsPreset;
#pragma warning restore 0649

	public float TilesPerSecond => tilesPerSecond;
	public TileColorsPreset TileColorsPreset => tileColorsPreset;
}
