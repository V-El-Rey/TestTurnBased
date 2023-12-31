using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GridPathfindingSystemController : IBaseController, IEnterController, IExitController
{
    private const int MOVE_COST = 10;
    private const int DIAGONAL_COST = 14;
    private IGridModel m_gridModel;
    private IInputModel m_inputModel;
    private IPathfindingModel m_pathfindingModel;
    private List<GridNode> m_openGridNodes;
    private List<GridNode> m_closedGridNodes;
    public GridPathfindingSystemController(IGridModel gridModel, IInputModel inputModel, IPathfindingModel pathfindingModel)
    {
        m_gridModel = gridModel;
        m_inputModel = inputModel;
        m_pathfindingModel = pathfindingModel;
    }

    public void OnEnterExecute()
    {
        m_pathfindingModel.onPathRequested += SetPathInModel;
    }

    public void OnExitExecute()
    {
        m_pathfindingModel.onPathRequested -= SetPathInModel;
    }

    // УДОЛИТЬ
    // private void DebugDrawPath()
    // {
    //     var gridCellPosX = (int)m_inputModel.gridCellHitCoordinates.x;
    //     var gridCellPosY = (int)m_inputModel.gridCellHitCoordinates.y;
    //     var path = FindPath(0, 0, gridCellPosX, gridCellPosY);
    //     if (path != null)
    //     {
    //         for (int i = 0; i < path.Count - 1; i++)
    //         {
    //             Debug.DrawLine(m_gridModel.gridView[path[i].x, path[i].y].unitSpawnPoint.transform.position, m_gridModel.gridView[path[i + 1].x, path[i + 1].y].unitSpawnPoint.transform.position, Color.green, 2f);
    //         }
    //     }
    // }


    private List<GridNode> FindPath(int startX, int startY, int endX, int endY)
    {
        if (endX == -1 || endY == -1)
        {
            return null;
        }
        var startNode = m_gridModel.grid[startX, startY];
        var endNode = m_gridModel.grid[endX, endY];
        var startNodeNeighbors = Helpers.GetNeighborNodes(m_gridModel, startNode);
        if (startNodeNeighbors.Contains(endNode))
        {
            return null;
        }
        if (endNode.isOccupied)
        {
            var endNodeNeighbors = Helpers.GetNeighborNodes(m_gridModel, endNode).Where(e => !e.isOccupied).ToList();
            int r = Random.Range(0, endNodeNeighbors.Count);
            endNode = endNodeNeighbors[r];
        }

        m_openGridNodes = new List<GridNode> { startNode };
        m_closedGridNodes = new List<GridNode>();

        for (int x = 0; x < m_gridModel.width; x++)
        {
            for (int y = 0; y < m_gridModel.height; y++)
            {
                m_gridModel.grid[x, y].gCost = int.MaxValue;
                m_gridModel.grid[x, y].fCost = CalculateFCostForNode(m_gridModel.grid[x, y]);
                m_gridModel.grid[x, y].previousPathNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateHCost(startNode, endNode);
        startNode.fCost = CalculateFCostForNode(startNode);


        while (m_openGridNodes.Count > 0)
        {
            var currentNode = GetLowestFCost(m_openGridNodes);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            m_openGridNodes.Remove(currentNode);
            m_closedGridNodes.Add(currentNode);

            foreach (var neighborNode in Helpers.GetNeighborNodes(m_gridModel, currentNode))
            {
                if (m_closedGridNodes.Contains(neighborNode))
                {
                    continue;
                }
                if (neighborNode.isOccupied)
                {
                    m_closedGridNodes.Add(neighborNode);
                    continue;
                }

                var testGCost = currentNode.gCost + CalculateHCost(currentNode, neighborNode);
                if (testGCost < neighborNode.gCost)
                {
                    neighborNode.previousPathNode = currentNode;
                    neighborNode.gCost = testGCost;
                    neighborNode.hCost = CalculateHCost(neighborNode, endNode);
                    neighborNode.fCost = CalculateFCostForNode(neighborNode);

                    if (!m_openGridNodes.Contains(neighborNode))
                    {
                        m_openGridNodes.Add(neighborNode);
                    }
                }
            }

        }
        return null;
    }

    private List<GridNode> CalculatePath(GridNode endNode)
    {
        List<GridNode> result = new List<GridNode>
        {
            endNode
        };
        var currentNode = endNode;
        while (currentNode.previousPathNode != null)
        {
            result.Add(currentNode.previousPathNode);
            currentNode = currentNode.previousPathNode;
        }
        result.Reverse();
        return result;
    }

    private int CalculateFCostForNode(GridNode node)
    {
        return node.gCost + node.hCost;
    }

    private int CalculateHCost(GridNode nodeA, GridNode nodeB)
    {
        int xDistance = Mathf.Abs(nodeA.x - nodeB.x);
        int yDistance = Mathf.Abs(nodeA.y - nodeB.y);
        int remain = System.Math.Abs(xDistance - yDistance);
        return DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_COST * remain;
    }

    private GridNode GetLowestFCost(List<GridNode> nodeList)
    {
        var lowestFCostNode = nodeList[0];
        foreach (var node in nodeList)
        {
            if (node.fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = node;
            }
        }
        return lowestFCostNode;
    }

    private void SetPathInModel(int startX, int startY, int endX, int endY)
    {
        if(startX == endX && startY == endY)
        {
            m_pathfindingModel.Path.Clear();
            return;
        }
        var path = FindPath(startX, startY, endX, endY);
        if (path != null)
        {
            m_pathfindingModel.Path.Clear();
            //рисует линию для дебуга
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(m_gridModel.gridView[path[i].x, path[i].y].unitSpawnPoint.transform.position, m_gridModel.gridView[path[i + 1].x, path[i + 1].y].unitSpawnPoint.transform.position, Color.green, 2f);
            }
            m_pathfindingModel.Path = path;
        }
    }
}
