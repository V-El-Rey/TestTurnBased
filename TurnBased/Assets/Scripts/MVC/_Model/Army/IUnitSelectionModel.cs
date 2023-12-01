using System;

public interface IUnitSelectionModel : IStateModel
{
    public Action<CellView> onCellSelectionChangeRequested { get; set; }
    Action<CellView, CellSelectionState> onCellSelectionHighlightChangeRequested { get; set; }
    public PlayerUnitModel selectedUnitModel { get; set; }
    public PlayerUnitModel unitToAttackModel { get; set; }
    public CellView selectedCell { get; set; }
    public UnitView selectedUnit { get; set; }
    public UnitView unitToAttack { get; set; }
}
