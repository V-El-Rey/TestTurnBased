using System;
using System.Collections.Generic;

public interface IPathfindingModel : IStateModel
{
    Action onPathEnded { get; set; }
    Action<int, int, int, int> onPathRequested { get; set; }
    List<GridNode> Path { get; set; }
}
