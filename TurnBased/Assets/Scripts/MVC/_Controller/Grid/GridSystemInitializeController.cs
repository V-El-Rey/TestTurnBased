public class GridSystemInitializeController : IBaseController, IEnterController
{
    private IGridModel m_gridModel;

    public GridSystemInitializeController(IGridModel gridModel)
    {
        m_gridModel = gridModel;
    }
    public void OnEnterExecute()
    {
        m_gridModel.gridCells = new int[m_gridModel.width, m_gridModel.height];
    }
}
