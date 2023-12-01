using System;
using System.Collections.Generic;

public class PlayerArmyModel : IPlayerArmyModel
{
    public List<PlayerUnitModel> units { get; set; }
    public List<UnitView> unitViews { get; set; }
    public Action<PlayerUnitModel, int, bool> onTakeDamage { get; set; }

    public PlayerArmyModel(List<PlayerUnitModel> units)
    {
        unitViews = new List<UnitView>();
        this.units = units;
    }

    public void ClearModel()
    {
        onTakeDamage = null;
        units.Clear();
        unitViews.Clear();
    }
}
