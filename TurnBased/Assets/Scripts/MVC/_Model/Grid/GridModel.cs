public class GridModel : IGridModel
{
    public GridModel(GridConfiguration gridConfiguration)
    {
        width = gridConfiguration.width;
        height = gridConfiguration.height;
    }
    public int width { get; set; }
    public int height { get; set; }
    public int[,] gridCells { get; set; }
    public CellLineView[,] gridView { get; set; }

    public void ClearModel()
    {
        width = 0;
        height = 0;
        gridCells = null;
        gridView = null;
    }
}
