using System;

public class FindPathCommand : ICommand
{
	private Func<DesiredPathData> getDesiredPathData;
	private GraphHandler graphHandler;

	public FindPathCommand(Func<DesiredPathData> desiredPathDataFunc, GraphHandler graphHandler)
	{
		getDesiredPathData = desiredPathDataFunc;
		this.graphHandler = graphHandler;
	}

    public void Execute()
	{
		DesiredPathData desiredPathData = getDesiredPathData();
		graphHandler.FindPath(desiredPathData);
	}
}
