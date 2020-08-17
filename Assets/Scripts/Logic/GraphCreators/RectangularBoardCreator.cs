using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Graph creators/Rectangular board creator")]
public class RectangularBoardCreator : BaseGraphCreator
{
#pragma warning disable 0649
	[SerializeField] private int xSize;
	[SerializeField] private int ySize;
#pragma warning restore 0649

	public override Graph GetNewGraph()
	{
		// Use xSize and ySize to create a new graph.
		throw new System.NotImplementedException();
	}
}
