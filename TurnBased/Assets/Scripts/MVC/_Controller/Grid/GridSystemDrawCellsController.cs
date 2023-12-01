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
        var gridLineElement = Resources.Load<GameObject>(LocalConstants.GRID_CELL_ELEMENT);
        for (int i = 0; i < m_gridModel.width; i++)
        {
            for (int j = 0; j < m_gridModel.height; j++)
            {
                var debugGO = GameObject.Instantiate(gridLineElement);
                debugGO.transform.SetParent(m_environmentRoot);
                debugGO.transform.localPosition = new Vector3(i, 0, j);
                m_gridModel.gridView[i, j] = debugGO.GetComponent<CellView>();
                //m_gridModel.gridView[i, j].debug.gameObject.SetActive(false);
            }
        }

        // for (int x = 0; x < m_gridModel.width; x++)
        // {
        //     for (int y = 0; y < m_gridModel.height; y++)
        //     {
        //         m_gridModel.gridView[x, y].debug.text = m_gridModel.grid[x, y].ToString();
        //     }
        // }
    }
}
