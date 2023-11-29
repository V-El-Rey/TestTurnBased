using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArmyConfigurator", menuName = "Configs/Army", order = 2)]
public class ArmyConfiguration : ScriptableObject
{
    public List<PlayerUnitModel> playerOneArmy;
    public List<PlayerUnitModel> playerTwoArmy;
}
