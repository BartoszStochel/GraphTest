using System;
using UnityEngine;

public class FindPathCommand : ICommand
{
	private Func<DesiredPathData> getDesiredPathData;
	private Action<PathfindingResult> finalAction;
	private GraphHandler graphHandler;

	public FindPathCommand(Func<DesiredPathData> desiredPathDataFunc, Action<PathfindingResult> finalAction, GraphHandler graphHandler)
	{
		getDesiredPathData = desiredPathDataFunc;
		this.finalAction = finalAction;
		this.graphHandler = graphHandler;
	}

    public void Execute()
	{
		if (getDesiredPathData == null ||
			finalAction == null ||
			graphHandler == null)
		{
			Debug.LogError("FindPathCommand was not initialized properly!");
			return;
		}

		DesiredPathData desiredPathData = getDesiredPathData();
		PathfindingResult result = graphHandler.FindPath(desiredPathData);
		finalAction(result);
	}
}
