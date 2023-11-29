using UnityEngine;

public class GridSystemDrawCellsController : IBaseController, IEnterController
{
    private IGridModel m_gridModel;
    private Transform m_environmentRoot;

    public GridSystemDrawCellsController(IGridModel gridModel, Transform environmentRoot)
    {
        m_gridModel = gridModel;
        m_environmentRoot = environmentRoot;
    }
    public void OnEnterExecute()
    {
        var gridLineElement = Resources.Load<GameObject>(LocalConstants.GRID_LINERENDERER_CELL_ELEMENT);
        for(int i = 0; i < m_gridModel.width + 1; i++)
        {
            for(int j = 0; j < m_gridModel.height + 1; j++)
            {
                var debugGO = GameObject.Instantiate(gridLineElement);
                debugGO.transform.SetParent(m_environmentRoot);
                debugGO.transform.localPosition = new Vector3(i, 0, j);
                m_gridModel.gridView[i,j] = debugGO.GetComponent<CellView>();
            }
        }

        for(int i = 0; i < m_gridModel.width; i++)
        {
            m_gridModel.gridView[i, m_gridModel.height].Up.SetActive(false);
            m_gridModel.gridView[i, m_gridModel.height].Collider.enabled = false;
        }
        for(int i = 0; i < m_gridModel.height; i++)
        {
            m_gridModel.gridView[m_gridModel.width, i].Right.SetActive(false);
            m_gridModel.gridView[m_gridModel.width, i].Collider.enabled = false;
        }
        m_gridModel.gridView[m_gridModel.width, m_gridModel.height].Up.SetActive(false);
        m_gridModel.gridView[m_gridModel.width, m_gridModel.height].Right.SetActive(false);
        m_gridModel.gridView[m_gridModel.width, m_gridModel.height].Collider.enabled = false;
    }
}
