using System;
using System.Collections.Generic;

public interface IPlayerArmyModel : IStateModel
{
    Action<PlayerUnitModel, int, bool> onTakeDamage { get; set; }
    List<PlayerUnitModel> units { get; set; }
    List<UnitView> unitViews { get; set; }
}
