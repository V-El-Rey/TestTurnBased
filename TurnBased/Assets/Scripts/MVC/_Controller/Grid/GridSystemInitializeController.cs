using System.Diagnostics;

public class GridSystemInitializeController : IBaseController, IEnterController, IUpdateController
{
    private IGridModel m_gridModel;

    public GridSystemInitializeController(IGridModel gridModel)
    {
        m_gridModel = gridModel;
    }
    public void OnEnterExecute()
    {
        m_gridModel.grid = new GridNode[m_gridModel.width, m_gridModel.height];
        for (int x = 0; x < m_gridModel.width; x++)
        {
            for (int y = 0; y < m_gridModel.height; y++)
            {
                m_gridModel.grid[x, y] = new GridNode(x, y);
            }
        }
        m_gridModel.gridView = new CellView[m_gridModel.width, m_gridModel.height];
    }

    public void OnUpdateExecute()
    {
        for (int x = 0; x < m_gridModel.width; x++)
        {
            for (int y = 0; y < m_gridModel.height; y++)
            {
                // m_gridModel.gridView[x,y].debug.text = $"Occupied: {m_gridModel.grid[x,y].isOccupied} \n" + 
                //     $"Selected: {m_gridModel.grid[x,y].isSelected} \n";
            }
        }
    }
}
