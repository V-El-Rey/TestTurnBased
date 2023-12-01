using System.Collections.Generic;

public class ResetActionPointsController : IBaseController, IEnterController
{
    private IPlayerArmyModel m_playerArmyModel;
    private List<PlayerUnitModel> m_armyConfiguration;

    public ResetActionPointsController(IPlayerArmyModel playerArmyModel, List<PlayerUnitModel> armyConfig)
    {
        m_playerArmyModel = playerArmyModel;
        m_armyConfiguration = armyConfig;
    }
    public void OnEnterExecute()
    {
        for(int i = 0; i < m_playerArmyModel.units.Count; i++)
        {
            if(m_playerArmyModel.units[i].Health > 0)
            {
                m_playerArmyModel.units[i].actionPoints = m_armyConfiguration[i].actionPoints;
            }
        }
    }  
}
