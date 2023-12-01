using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class PlayerUnitModel : ICloneable
{
    public string UnitName;
    public int Health;
    public int Attack;
    public int actionPoints;
    public GameObject prefab;

    public object Clone()
    {
        var res = new PlayerUnitModel()
        {
            UnitName = UnitName,
            Health = Health,
            Attack = Attack,
            actionPoints = actionPoints,
            prefab = prefab
        };
        return res;
    }
}
