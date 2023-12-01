using System;

public class UnitSelectionModel : IUnitSelectionModel
{
    public Action<CellView> onCellSelectionChangeRequested { get; set; }
    public UnitView selectedUnit { get; set; }
    public Action<CellView, CellSelectionState> onCellSelectionHighlightChangeRequested { get; set; }
    public CellView selectedCell { get; set; }
    public PlayerUnitModel selectedUnitModel { get; set; }
    public PlayerUnitModel unitToAttackModel { get; set; }
    public UnitView unitToAttack { get; set; }

    public void ClearModel()
    {
        selectedCell = null;
        selectedUnit = null;
        onCellSelectionChangeRequested = null;
        onCellSelectionHighlightChangeRequested = null;
    }
}
