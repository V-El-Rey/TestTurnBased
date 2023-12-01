
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectUnitController : IBaseController, IEnterController, IExitController
{
    private IInputModel m_inputModel;
    private IGridModel m_gridModel;
    private IPlayerArmyModel m_playerArmyModel;
    private IUnitSelectionModel m_unitSelectionModel;

    public SelectUnitController(IInputModel inputModel, IPlayerArmyModel playerArmyModel, IGridModel gridModel, IUnitSelectionModel unitSelectionModel)
    {
        m_inputModel = inputModel;
        m_playerArmyModel = playerArmyModel;
        m_gridModel = gridModel;
        m_unitSelectionModel = unitSelectionModel;
    }
    public void OnEnterExecute()
    {
        m_inputModel.onMouseClicked += SelectUnitOnCell;
        m_unitSelectionModel.onCellSelectionChangeRequested += CelectCell;
    }

    public void OnExitExecute()
    {
        DeselectPreviousSelectedCell();
        m_unitSelectionModel.onCellSelectionChangeRequested -= CelectCell;
        m_inputModel.onMouseClicked -= SelectUnitOnCell;
    }

    private void SelectUnitOnCell()
    {
        var x = (int)m_inputModel.gridCellHitCoordinates.x;
        var y = (int)m_inputModel.gridCellHitCoordinates.y;
        if (x != -1 && y != -1)
        {
            if (m_playerArmyModel.unitViews.Any(e => e == m_gridModel.grid[x, y].unit))
            {
                m_unitSelectionModel.onCellSelectionHighlightChangeRequested?.Invoke(m_gridModel.gridView[x, y], CellSelectionState.Selected);
                if (m_unitSelectionModel.selectedCell != null)
                {
                    DeselectPreviousSelectedCell();
                }
                m_unitSelectionModel.selectedCell = m_gridModel.gridView[x, y];
                m_unitSelectionModel.selectedUnit = m_gridModel.grid[x, y].unit;
                m_unitSelectionModel.selectedUnitModel = m_gridModel.grid[x, y].playerUnitModel;

                m_gridModel.grid[x, y].isSelected = true;
                ShowPossibleCells(x, y, m_gridModel.grid[x, y].playerUnitModel.actionPoints);
            }
        }
    }

    private void ShowPossibleCells(int x, int y, int actionPoints)
    {
        for (int e = 0; e < m_gridModel.width; e++)
        {
            for (int r = 0; r < m_gridModel.height; r++)
            {
                 m_gridModel.gridView[e,r].debug.text = "";
            }
        }

        var startNode = m_gridModel.grid[x, y];
        var neighbors = Helpers.GetNeighborNodes(m_gridModel, startNode);
        var checkedList = new List<GridNode>();
        var uncheckedList = new List<GridNode>();
        uncheckedList.AddRange(neighbors);

        while (actionPoints - 1 > 0)
        {
            var newNeighbors = new List<GridNode>();
            foreach (var node in uncheckedList)
            {
                checkedList.Add(node);
                foreach(var newN in Helpers.GetNeighborNodes(m_gridModel, node))
                {
                    if(!checkedList.Contains(newN))
                    {
                        newNeighbors.Add(newN);
                    }
                }
            }
            uncheckedList.Clear();
            uncheckedList.AddRange(newNeighbors);

            actionPoints--;
        }
        foreach(var n in checkedList)
        {
            m_gridModel.gridView[n.x, n.y].debug.text = "+";
        }
    }

    private void DeselectPreviousSelectedCell()
    {
        if (m_unitSelectionModel.selectedCell != null)
        {
            m_unitSelectionModel.onCellSelectionHighlightChangeRequested?.Invoke(m_unitSelectionModel.selectedCell, CellSelectionState.None);
            m_gridModel.grid[(int)m_unitSelectionModel.selectedCell.transform.position.x,
                (int)m_unitSelectionModel.selectedCell.transform.position.z].isSelected = false;
            m_unitSelectionModel.selectedCell = null;
            m_unitSelectionModel.selectedUnitModel = null;
        }
    }

    private void CelectCell(CellView view)
    {
        if (m_unitSelectionModel.selectedCell != null)
        {
            m_unitSelectionModel.onCellSelectionHighlightChangeRequested?.Invoke(m_unitSelectionModel.selectedCell, CellSelectionState.None);
            m_gridModel.grid[(int)m_unitSelectionModel.selectedCell.transform.position.x,
                (int)m_unitSelectionModel.selectedCell.transform.position.z].isSelected = false;
            m_unitSelectionModel.selectedCell = view;
            m_unitSelectionModel.onCellSelectionHighlightChangeRequested?.Invoke(view, CellSelectionState.Selected);
            m_gridModel.grid[(int)m_unitSelectionModel.selectedCell.transform.position.x,
                (int)m_unitSelectionModel.selectedCell.transform.position.z].isSelected = true;
        }
    }
}
