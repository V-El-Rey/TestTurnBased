public interface IGridModel : IStateModel
{
    int width {get; set;}
    int height {get;set;}
    GridNode[,] grid {get; set;}
    CellView[,] gridView { get; set; }
}
