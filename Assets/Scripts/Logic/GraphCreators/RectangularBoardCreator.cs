using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objects/Graph creators/Rectangular board creator")]
public class RectangularBoardCreator : BaseGraphCreator
{
#pragma warning disable 0649
	[SerializeField] private Vector2 size;
#pragma warning restore 0649

	public override Graph GetNewGraph()
	{
		// Use size to create a new graph.
		throw new System.NotImplementedException();
	}
}
