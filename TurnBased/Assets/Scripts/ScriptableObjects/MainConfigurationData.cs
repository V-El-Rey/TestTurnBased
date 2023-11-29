using UnityEngine;

[CreateAssetMenu(fileName = "MainConfigurationData", menuName = "Configs/Main", order = 0)]
public class MainConfigurationData : ScriptableObject
{
    public GridConfiguration GridConfiguration;
    public ArmyConfiguration ArmyConfiguration;
}
