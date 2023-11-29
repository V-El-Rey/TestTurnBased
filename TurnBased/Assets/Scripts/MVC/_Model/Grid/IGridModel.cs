public interface IGridModel : IStateModel
{
    int width {get; set;}
    int height {get;set;}
    int[,] gridCells {get; set;}
    CellLineView[,] gridView { get; set; }
}
