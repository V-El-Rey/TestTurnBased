public class GridModel : IGridModel
{
    public GridModel(GridConfiguration gridConfiguration)
    {
        width = gridConfiguration.width;
        height = gridConfiguration.height;
    }
    public int width { get; set; }
    public int height { get; set; }
    public CellView[,] gridView { get; set; }
    public GridNode[,] grid { get; set; }

    public void ClearModel()
    {
        width = 0;
        height = 0;
        grid = null;
        gridView = null;
    }
}
