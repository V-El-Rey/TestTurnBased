using UnityEngine;

public class GridCellHighlightController : IBaseController, IUpdateController, IEnterController, IExitController
{
    private IInputModel m_inputModel;
    private IGridModel m_gridModel;
    private IUnitSelectionModel m_unitSelectionModel;

    private CellView m_prevCell;
    private CellView m_currCell;

    private GridConfiguration m_gridConfiguration;

    public GridCellHighlightController(IInputModel inputModel, IGridModel gridModel, IUnitSelectionModel unitSelectionModel, GridConfiguration gridConfiguration)
    {
        m_inputModel = inputModel;
        m_gridModel = gridModel;
        m_unitSelectionModel = unitSelectionModel;
        m_gridConfiguration = gridConfiguration;
    }

    public void OnEnterExecute()
    {
        m_unitSelectionModel.onCellSelectionHighlightChangeRequested += SetCellMaterial;
        m_prevCell = m_gridModel.gridView[(int)m_inputModel.gridCellHitCoordinates.x, (int)m_inputModel.gridCellHitCoordinates.y];
        m_currCell = m_gridModel.gridView[(int)m_inputModel.gridCellHitCoordinates.x, (int)m_inputModel.gridCellHitCoordinates.y];
    }

    public void OnExitExecute()
    {
        m_unitSelectionModel.onCellSelectionHighlightChangeRequested -= SetCellMaterial;
    }

    public void OnUpdateExecute()
    {
        HighlightCell();
    }

    private void HighlightCell()
    {
        var x = (int)m_inputModel.gridCellHitCoordinates.x;
        var y = (int)m_inputModel.gridCellHitCoordinates.y;
        if (x != -1 && y != -1)
        {
            var currGridNode = m_gridModel.grid[(int)m_currCell.transform.position.x, (int)m_currCell.transform.position.z];
            var prevGridNode = m_gridModel.grid[(int)m_prevCell.transform.position.x, (int)m_prevCell.transform.position.z];

            m_currCell = m_gridModel.gridView[x, y];
            if (!currGridNode.isSelected)
            {
                SetCellMaterial(m_currCell, CellSelectionState.Highlighted);
            }
            if (m_currCell.transform.position != m_prevCell.transform.position)
            {
                if (x < m_gridModel.width && y < m_gridModel.height &&
                    x >= 0 && y >= 0)
                {
                    if (!prevGridNode.isSelected)
                    {
                        SetCellMaterial(m_prevCell, CellSelectionState.None);
                    }
                    else
                    {
                        SetCellMaterial(m_prevCell, CellSelectionState.Selected);
                    }
                }
                m_prevCell = m_currCell;
            }
        }
    }

    private void SetCellMaterial(CellView cell, CellSelectionState state)
    {
        Material mat = m_gridConfiguration.regularMaterial;
        switch (state)
        {
            case CellSelectionState.None:
                mat = m_gridConfiguration.regularMaterial;
                break;
            case CellSelectionState.Highlighted:
                mat = m_gridConfiguration.highLightedMaterial;
                break;
            case CellSelectionState.Selected:
                mat = m_gridConfiguration.selectedCellMaterial;
                break;
            case CellSelectionState.Possible:
                mat = m_gridConfiguration.possibleCellMaterial;
                break;
        }
        foreach (var meshR in cell.meshRenderers)
        {
            meshR.material = mat;
        }
    }
}
