using System.Collections.Generic;

public class PlayerArmyModel : IStateModel
{
    public PlayerArmyModel(List<PlayerUnitModel> units)
    {
        m_units = units;
    }
    public List<PlayerUnitModel> m_units;
    public void ClearModel()
    {
        m_units.Clear();
    }
}
