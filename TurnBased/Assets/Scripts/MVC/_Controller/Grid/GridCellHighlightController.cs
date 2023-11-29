using UnityEngine;

public class GridCellHighlightController : IBaseController, IUpdateController, IEnterController
{
    private IInputModel m_inputModel;
    private IGridModel m_gridModel;

    private CellView m_prevCell;
    private CellView m_currCell;

    private GridConfiguration m_gridConfiguration;

    public GridCellHighlightController(IInputModel inputModel, IGridModel gridModel, GridConfiguration gridConfiguration)
    {
        m_inputModel = inputModel;
        m_gridModel = gridModel;
        m_gridConfiguration = gridConfiguration;
    }

    public void OnEnterExecute()
    {
        m_prevCell = m_gridModel.gridView[(int)m_inputModel.gridCellHitCoordinates.x, (int)m_inputModel.gridCellHitCoordinates.y];
        m_currCell = m_gridModel.gridView[(int)m_inputModel.gridCellHitCoordinates.x, (int)m_inputModel.gridCellHitCoordinates.y];
    }

    public void OnUpdateExecute()
    {
        if (m_inputModel.gridCellHitCoordinates.x != -1 && m_inputModel.gridCellHitCoordinates.y != -1)
        {
            m_currCell = m_gridModel.gridView[(int)m_inputModel.gridCellHitCoordinates.x, (int)m_inputModel.gridCellHitCoordinates.y];
            SetCellMaterial(m_currCell, m_gridConfiguration.highLightedMaterial);
            if (m_currCell.transform.position != m_prevCell.transform.position)
            {
                if (m_inputModel.gridCellHitCoordinates.x < m_gridModel.width && m_inputModel.gridCellHitCoordinates.y < m_gridModel.height &&
                    m_inputModel.gridCellHitCoordinates.x >= 0 && m_inputModel.gridCellHitCoordinates.y >= 0)
                {
                    SetCellMaterial(m_prevCell, m_gridConfiguration.regularMaterial);
                    var commonEdge = Helpers.DetectCommonEdge(m_currCell.transform.position, m_prevCell.transform.position);

                    switch (commonEdge)
                    {
                        case CommonCellEdges.None:
                            break;
                        case CommonCellEdges.Right:
                            m_prevCell.UpMeshRenderer.material = m_gridConfiguration.highLightedMaterial;
                            break;
                        case CommonCellEdges.Left:
                            m_currCell.UpMeshRenderer.material = m_gridConfiguration.highLightedMaterial;
                            break;
                        case CommonCellEdges.Up:
                            m_prevCell.RightMeshRenderer.material = m_gridConfiguration.highLightedMaterial;
                            break;
                        case CommonCellEdges.Bottom:
                            m_currCell.RightMeshRenderer.material = m_gridConfiguration.highLightedMaterial;
                            break;
                    }
                }
                m_prevCell = m_currCell;
            }
        }
        else
        {
            if (m_prevCell.RightMeshRenderer.material != m_gridConfiguration.regularMaterial)
            {
                SetCellMaterial(m_prevCell, m_gridConfiguration.regularMaterial);
            }
        }
    }

    private void SetCellMaterial(CellView cell, Material material)
    {
        cell.RightMeshRenderer.material = material;
        cell.UpMeshRenderer.material = material;
        m_gridModel.gridView[(int)cell.transform.position.x, (int)cell.transform.position.z + 1].RightMeshRenderer.material
            = material;
        m_gridModel.gridView[(int)cell.transform.position.x + 1, (int)cell.transform.position.z].UpMeshRenderer.material
            = material;
    }
}
