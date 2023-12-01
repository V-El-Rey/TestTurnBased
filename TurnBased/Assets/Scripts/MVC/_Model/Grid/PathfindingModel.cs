using System;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingModel : IPathfindingModel
{
    public Action<int, int, int, int> onPathRequested { get; set; }
    public List<GridNode> Path { get; set; }
    public Action onPathEnded { get; set; }

    public PathfindingModel()
    {
        Path = new List<GridNode>();
    }

    public void ClearModel()
    {
        Path.Clear();
        onPathRequested = null;
        onPathEnded = null;
    }
}
